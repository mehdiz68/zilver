using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using zilver.domain.Entities;

namespace zilver.data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;

        // Refresh tokens
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // مثال: ایندکس یا محدودیت
            builder.Entity<RefreshToken>()
                   .HasIndex(r => r.Token)
                   .IsUnique(false);

            #region User
            builder.Entity<ApplicationUser>()
                   .HasMany(u => u.RefreshTokens)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<City>().HasMany(x=>x.Users).WithOne(x=>x.CityEntity).HasForeignKey(x=>x.CityId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.Avatars).WithOne(x => x.Avatarattachment).HasForeignKey(x => x.Avatar).OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Attachement
            builder.Entity<Folder>().HasMany(x=>x.attachments).WithOne(x=>x.Folder).HasForeignKey(x=>x.FolderId).OnDelete(DeleteBehavior.NoAction); ;
            builder.Entity<FileType>().HasMany(x => x.attachments).WithOne(x => x.FileType).HasForeignKey(x => x.FileTypeId).OnDelete(DeleteBehavior.NoAction); ;
            builder.Entity<ApplicationUser>().HasMany(x => x.Attachements).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<FileType>().HasIndex(e => e.FileTypeName).IsUnique().IsClustered();
            #endregion

            #region Setting
            builder.Entity<Setting>().HasIndex(e => e.LanguageId).IsUnique().IsClustered();
            builder.Entity<Attachment>().HasMany(x => x.Settings).WithOne(x => x.attachment).HasForeignKey(x => x.Logo).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.SettingMags).WithOne(x => x.attachmentLogoMag).HasForeignKey(x => x.LogoMag).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.WaterMarkSettings).WithOne(x => x.Waterattachment).HasForeignKey(x => x.WaterMark).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.FaviconMagSettings).WithOne(x => x.FaviconattachmentMag).HasForeignKey(x => x.FaviconMag).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.FaviconMagSettings).WithOne(x => x.FaviconattachmentMag).HasForeignKey(x => x.FaviconMag).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.FaviconSettings).WithOne(x => x.Faviconattachment).HasForeignKey(x => x.Favicon).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Attachment>().HasMany(x => x.FactorSettings).WithOne(x => x.FactorAttachment).HasForeignKey(x => x.FactorLogo).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Province>().HasMany(x => x.Settings).WithOne(x => x.Province).HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<City>().HasMany(x => x.Settings).WithOne(x => x.City).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
