using System.ComponentModel.DataAnnotations;

namespace zilver.domain.Entities
{
    public class FileType 
    {
        #region Ctor
        public FileType()
        {

        }
        #endregion

        #region Properties
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن پسوند، اجباری است")]
        [Display(Name = " پسوند( مثلِ jpg.)")]
        [StringLength(10)] 
        public string FileTypeName { get; set; }
        public  ICollection<Attachment> attachments { get; set; }
        #endregion
    }
}
