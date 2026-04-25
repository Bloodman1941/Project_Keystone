using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class Priority
    {
        [Key]
        public int PriorityID { get; set; }

        [Required, MaxLength(50)]
        public string PriorityName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string PriorityDesc { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string PriorityColor { get; set; } = string.Empty;

        public int PriorityLevel { get; set; }

        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
