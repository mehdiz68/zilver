using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zilver.domain.Entities; 

namespace zilver.data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // محصولات (قبلاً بود)
        public DbSet<Product> Products { get; set; } = null!;

        // Refresh tokens
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        // اختیاری: تنظیمات بیشتر مدل‌ها
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // مثال: ایندکس یا محدودیت
            builder.Entity<RefreshToken>()
                   .HasIndex(r => r.Token)
                   .IsUnique(false);

            builder.Entity<ApplicationUser>()
                   .HasMany(u => u.RefreshTokens)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
