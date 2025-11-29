using global::zilver.auth.Interfaces;
using global::zilver.auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using zilver.auth.Models;
using zilver.data;
using zilver.domain.Entities;

namespace zilver.auth.Services
{
    

    namespace zilver.auth.Services
    {
        public class AuthService : IAuthService
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly ApplicationDbContext _db;
            private readonly IConfiguration _config;

            public AuthService(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ApplicationDbContext db,
                IConfiguration config)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _db = db;
                _config = config;
            }

            // =======================
            // LOGIN — COOKIE + TOKEN
            // =======================
            public async Task<AuthResult> LoginAsync(string email, string password)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return Fail("User not found");

                var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
                if (!result.Succeeded)
                    return Fail("Invalid credentials");

                return await GenerateTokenResponse(user);
            }

            // =======================
            // REGISTER
            // =======================
            public async Task<AuthResult> RegisterAsync(RegisterDto model)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return Fail(string.Join(" | ", result.Errors.Select(x => x.Description)));

                return await GenerateTokenResponse(user);
            }

            // =======================
            // REFRESH TOKEN
            // =======================
            public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
            {
                var token = _db.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);

                if (token == null || token.IsRevoked || token.Expires < DateTime.UtcNow)
                    return Fail("Invalid refresh token");

                var user = await _userManager.FindByIdAsync(token.UserId);
                if (user == null)
                    return Fail("User not found");

                // Revoke old
                token.IsRevoked = true;
                _db.RefreshTokens.Update(token);

                return await GenerateTokenResponse(user);
            }

            // =======================
            // LOGOUT
            // =======================
            public async Task LogoutAsync()
            {
                await _signInManager.SignOutAsync();
            }

            // =======================
            // PRIVATE HELPERS
            // =======================

            private async Task<AuthResult> GenerateTokenResponse(ApplicationUser user)
            {
                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                var rt = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    Expires = DateTime.UtcNow.AddDays(15),
                    IsRevoked = false
                };

                _db.RefreshTokens.Add(rt);
                await _db.SaveChangesAsync();

                return new AuthResult
                {
                    Success = true,
                    UserId = user.Id,
                    AccessTokenExpires = DateTime.UtcNow.AddDays(15),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }

            private string GenerateJwtToken(ApplicationUser user)
            {
                var jwt = _config.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));

                var claims = new[]
                {
                new Claim("uid", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt["Issuer"],
                    jwt["Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            private string GenerateRefreshToken()
            {
                return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            }

            private AuthResult Fail(string message)
            {
                return new AuthResult { Success = false, Message = message };
            }
        }
    }

}
