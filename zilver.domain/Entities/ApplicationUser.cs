using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace zilver.domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // هر فیلد اضافی که لازم باشه اینجا اضافه کن
        public string? FullName { get; set; }

        // ارتباط با RefreshToken
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
