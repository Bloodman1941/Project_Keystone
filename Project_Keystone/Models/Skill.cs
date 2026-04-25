using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        [Required, MaxLength(50)]
        public string SkillName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string SkillDesc { get; set; } = string.Empty;

        public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
