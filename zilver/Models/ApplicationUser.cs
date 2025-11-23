using Microsoft.AspNetCore.Identity;
namespace zilver.Models
{

    public class ApplicationUser : IdentityUser
    {
        // فیلدهای اضافه (مثلاً نام واقعی)
        public string FullName { get; set; }
    }

}
