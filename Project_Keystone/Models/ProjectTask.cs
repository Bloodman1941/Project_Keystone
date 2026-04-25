using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class ProjectTask
    {
        [Key]
        public int TaskID { get; set; }

        [Required, MaxLength(50)]
        public string TaskName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string TaskDesc { get; set; } = string.Empty;

        [ForeignKey("TaskPriority")]
        public int TaskPriority { get; set; }

        [ForeignKey("TaskStatus")]
        public int TaskStatus { get; set; }

        public DateTime? TaskTimeStamp { get; set; }

        [Required]
        public DateTime TaskDueDate { get; set; }

        [ForeignKey("TaskProjectID")]
        public int TaskProjectID { get; set; }

        public virtual Priority Priority { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
