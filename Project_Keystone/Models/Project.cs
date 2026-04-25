using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required, MaxLength(50)]
        public string ProjectName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string ProjectDesc { get; set; } = string.Empty;

        [ForeignKey("ProjectOwnerUserID")]
        public int ProjectOwnerUserID { get; set; }

        public DateTime? ProjectTimestamp { get; set; }

        public virtual User OwnerUser { get; set; } = null!;
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
