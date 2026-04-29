using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class Text
    {
        [Key]
        public int TextID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required, MaxLength(500)]
        public string TextBody { get; set; } = string.Empty;

        [Required]
        public DateTime DateSent { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }
}
