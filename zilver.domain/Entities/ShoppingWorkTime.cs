using System.ComponentModel.DataAnnotations;

namespace zilver.domain.Entities
{
    public class ShoppingWorkTime
    {
       

        #region Configuration
        public class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ShoppingWorkTime>
        {
            public Configuration()
            {
                HasRequired(Current => Current.Setting).WithMany(Current => Current.ShoppingWorkTimes).HasForeignKey(Current => Current.SettingId).WillCascadeOnDelete(false);

            }
        }

        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "تنظیمات سایت")]
        public int SettingId { get; set; }
        public  Setting Setting { get; set; }

        /*
         0- شنبه
         ...
         6- جمعه
             */

        [Required]
        [Display(Name = "روز هفته")]
        public short WeekDay { get; set; }


        [Required]
        [Display(Name = "ساعت شروع")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "ساعت پایان")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        #endregion
    }
}
