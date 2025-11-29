using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using zilver.auth.Interfaces;
using zilver.auth.Services.zilver.auth.Services;
using zilver.data;
using zilver.data.Repositories;
using zilver.domain.Entities;
using zilver.domain.Interfaces;
using zilver.services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity (cookie-based)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT config
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

builder.Services.AddAuthentication()
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSection["Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true
    };
});

// MVC + API controllers
builder.Services.AddControllersWithViews();

builder.Services.AddOpenApi();



// CORS برای اجازه به کلاینت‌ها
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
    var uow = scope.ServiceProvider.GetService<IUnitOfWork>();

    Console.WriteLine(db != null ? "DbContext OK" : "DbContext NULL");
    Console.WriteLine(uow != null ? "UnitOfWork OK" : "UnitOfWork NULL");
}

if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// Routeهای MVC و API
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//    // ایجاد Role
//    if (!await roleManager.RoleExistsAsync("Admin"))
//        await roleManager.CreateAsync(new IdentityRole("Admin"));

//    // گرفتن کاربر
//    var user = await userManager.FindByEmailAsync("admin@lelimond.com");

//    // اضافه کردن به Role
//    if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
//        await userManager.AddToRoleAsync(user, "Admin");
//}

app.Run();
