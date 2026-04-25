using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class UserSkill
    {
        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Skill")]
        public int SkillID { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
