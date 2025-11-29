using System;
using System.Collections.Generic;
using System.Text;
using zilver.auth.Models;

namespace zilver.auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        Task<AuthResult> RegisterAsync(RegisterDto model);
        Task LogoutAsync();
    }

}
