using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace zilver.domain.Entities
{

    public class Setting
    {
      

        #region Configuration
        public class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Setting>
        {
            public Configuration()
            {
                Property(current => current.SettingName).IsUnicode(true).HasMaxLength(50).IsVariableLength().IsRequired();
                Property(current => current.WebSiteName).IsUnicode(true).HasMaxLength(50).IsVariableLength().IsRequired();
                Property(current => current.WebSiteTitle).IsUnicode(true).HasMaxLength(64).IsVariableLength().IsRequired();
                Property(current => current.WebSiteMetaDescription).IsUnicode(true).HasMaxLength(155).IsVariableLength().IsRequired();
                Property(current => current.WebSiteMetakeyword).IsUnicode(true).HasMaxLength(48).IsVariableLength().IsRequired();
                Property(current => current.Logo).IsOptional();
                HasOptional(Current => Current.attachment).WithMany(Current => Current.Settings).HasForeignKey(Current => Current.Logo).WillCascadeOnDelete(false);
                HasOptional(Current => Current.attachmentLogoMag).WithMany(Current => Current.SettingMags).HasForeignKey(Current => Current.LogoMag).WillCascadeOnDelete(false);
                HasOptional(Current => Current.Waterattachment).WithMany(Current => Current.WaterMarkSettings).HasForeignKey(Current => Current.WaterMark).WillCascadeOnDelete(false);
                HasOptional(Current => Current.FaviconattachmentMag).WithMany(Current => Current.FaviconMagSettings).HasForeignKey(Current => Current.FaviconMag).WillCascadeOnDelete(false);
                HasOptional(Current => Current.Faviconattachment).WithMany(Current => Current.FaviconSettings).HasForeignKey(Current => Current.Favicon).WillCascadeOnDelete(false);
                HasOptional(Current => Current.FactorAttachment).WithMany(Current => Current.FactorSettings).HasForeignKey(Current => Current.FactorLogo).WillCascadeOnDelete(false);
                HasOptional(Current => Current.Province).WithMany(Current => Current.Settings).HasForeignKey(Current => Current.ProvinceId).WillCascadeOnDelete(false);
                HasOptional(Current => Current.City).WithMany(Current => Current.Settings).HasForeignKey(Current => Current.CityId).WillCascadeOnDelete(false);
                Property(current => current.LargeSizeWidth).IsRequired();
                Property(current => current.LargeSizeHeight).IsRequired();
                Property(current => current.MediumSizeWidth).IsRequired();
                Property(current => current.MediumSizeHeight).IsRequired();
                Property(current => current.SmallSizeWidth).IsRequired();
                Property(current => current.SmallSizeHeight).IsRequired();
                Property(current => current.XsmallSizeWidth).IsRequired();
                Property(current => current.XsmallSizeHeight).IsRequired();
                Property(current => current.LanguageId).IsOptional();

            }
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن  نام تنظیم ، اجباری است")]
        [Display(Name = "نام تنظیم")]
        [MaxLength(50, ErrorMessage = "حداکثر طول کارکتر ، 50")]
        public string SettingName { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن  نام وب سایت ، اجباری است")]
        [Display(Name = "نام وب سایت")]
        [MaxLength(50, ErrorMessage = "حداکثر طول کارکتر ، 50")]
        public string WebSiteName { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن  عنوان وب سایت، اجباری است")]
        [Display(Name = "عنوان اصلی وب سایت")]
        [MaxLength(64, ErrorMessage = "حداکثر طول کارکتر ، 64")]
        public string WebSiteTitle { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن متای اصلی توضیحات، اجباری است")]
        [Display(Name = "متای اصلی توضیحات وب سایت")]
        [MaxLength(155, ErrorMessage = "حداکثر طول کارکتر ، 155")]
        public string WebSiteMetaDescription { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن  نام وب سایت مگ ، اجباری است")]
        [Display(Name = "نام وب سایت مگ")]
        [MaxLength(50, ErrorMessage = "حداکثر طول کارکتر ، 50")]
        public string WebSiteNameMag { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن  عنوان وب سایت مگ، اجباری است")]
        [Display(Name = "عنوان اصلی وب سایت مگ")]
        [MaxLength(64, ErrorMessage = "حداکثر طول کارکتر ، 64")]
        public string WebSiteTitleMag { get; set; } = null!;

        [Required(ErrorMessage = "وارد کردن متای اصلی توضیحات مگ، اجباری است")]
        [Display(Name = "متای اصلی توضیحات وب سایت مگ")]
        [MaxLength(155, ErrorMessage = "حداکثر طول کارکتر ، 155")]
        public string WebSiteMetaDescriptionMag { get; set; } = null!;


        [Required(ErrorMessage = "وارد کردن متای اصلی کلمات کلیدی، اجباری است")]
        [Display(Name = "متای اصلی کلمات کلیدی وب سایت")]
        [MaxLength(48, ErrorMessage = "حداکثر طول کارکتر ، 48")]
        public string WebSiteMetakeyword { get; set; } = null!;



        [Display(Name = "فعال سازی کمپین ریتارگتینگ یکتانت")]
        public bool yektanet { get; set; }

       [Display(Name = "دامنه ی بدون کوکی")]
        public string StaticContentDomain { get; set; } = null!;

        [Display(Name = "لوگوی وب سایت")]
        public Guid? Logo { get; set; }
        public Attachment attachment { get; set; } = null!;

        [Display(Name = "لوگوی وب سایت مگ")]
        public Guid? LogoMag { get; set; }
        public Attachment attachmentLogoMag { get; set; } = null!;

        [Display(Name = "واترمارک وب سایت")]
        public Guid? WaterMark { get; set; }
        public Attachment Waterattachment { get; set; } = null!;


        [Display(Name = "لوگو برای فاکتورها")]
        public Guid? FactorLogo { get; set; }
        public Attachment FactorAttachment { get; set; } = null!;

        /*0-پایین چپ
         * 1- پایین مرکز
         * ...
         */
        [Display(Name = "مکان پیش فرض واترمارک")]
        public int WaterMarkPosition { get; set; }

        [Display(Name = "اعمال واترمارک فقط برای عکس اصلی")]
        public bool LargeImageWaremark { get; set; }


        [Display(Name = "آیکن هدر سایت")]
        public Guid? Favicon { get; set; }
        public Attachment Faviconattachment { get; set; } = null!;


        [Display(Name = "آیکن هدر سایت مگ")]
        public Guid? FaviconMag { get; set; }
        public Attachment FaviconattachmentMag { get; set; } = null!;



        [Display(Name = "کدِ ثبت وب سایت در وب سایت های آماری ( اسکریپت )")]
        public string AnalyticsVerification { get; set; } = null!;

        [Display(Name = "نرخ مالیات بر ارزش افزوده")]
        [Required(ErrorMessage = "اجباری")]
        public double TaxRate { get; set; }


        [Display(Name = "ارزش عددی بن تخفیف")]
        [Required(ErrorMessage = "اجباری")]
        public int BonPrice { get; set; }

        [Display(Name = "تعداد روز برای انقضای بن تخفیف")]
        [Required(ErrorMessage = "اجباری")]
        public int BonExpireDay { get; set; }


        [Display(Name = "فعال سازی پنجره پاپ آپ صفحه اصلی")]
        public bool PopUpActive { get; set; }

        /*
         1- content
         2- only image
             */
        [Display(Name = "نوع پاپ آپ")]
        [Required(ErrorMessage = "اجباری")]
        public bool PopUpType { get; set; }

        [Display(Name = "محتوای پنجره پاپ آپ صفحه اصلی")]
        //[Required(ErrorMessage = "اجباری")]
        public string PopUpMessage { get; set; } = null!;

        [Required(ErrorMessage = "اجباری")]
        public int PopUpEditVersion { get; set; }

      
        [Display(Name = "زمان اعلام قیمت سفارش استعلامی(دقیقه)")]
        public int ShoppingEstelamMinutes { get; set; }

        [Display(Name = "مهلت پرداخت سفارش استعلامی(دقیقه)")]
        public int ShoppingPayEstelamMinutes { get; set; }

        [Required]
        [Display(Name = "زبان ( وب سایت)")]
        public Int16? LanguageId { get; set; }


        [Display(Name = "استان کنونی فروشگاه")]
        public int? ProvinceId { get; set; }
        public Province Province { get; set; }


        [Display(Name = "شهر کنونی فروشگاه")]
        public int? CityId { get; set; }
        public City City { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; } = null!;

        [Display(Name = "تلفن")]
        public string Tele { get; set; } = null!;

        [Display(Name = "موبایل")]
        public string Mobile { get; set; } = null!;

        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "شماره اقتصادی")]
        public string TaxNumber { get; set; } = null!;

        [Display(Name = "عرض جغرافیایی")]
        public string FooterGoogleMapLongitude { get; set; } = null!;

        [Display(Name = "طول جغرافیایی")]
        public string FooterGoogleMapLatitude { get; set; } = null!;

        [Display(Name = "زوم نقشه")]
        public string FooterGoogleMapZoom { get; set; } = null!;


        [Display(Name = "بستن قیمت‌ها")]
        public bool siteclosed { get; set; }

        [Display(Name = "پیامِ بستن قیمت‌ها")]
        public string siteclosedmsg { get; set; } = null!;
        public IList<EmailSender> EmailSenders { get; set; }

        public IList<SmsSender> SmsSenders { get; set; }
        public ICollection<ShoppingWorkTime> ShoppingWorkTimes { get; set; }
        public ICollection<SettingState> settingStates { get; set; }


        #endregion

    }

   
}
