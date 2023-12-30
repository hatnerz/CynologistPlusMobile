using System;
using System.Collections.Generic;
using System.Text;

namespace CynologistPlusMobile.Model
{
    public partial class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public double MaxValue { get; set; }

        public string MeasureUnit { get; set; }

        public virtual ICollection<DogSkill> DogSkills { get; set; } = new List<DogSkill>();

        public virtual ICollection<DogSkillsLog> DogSkillsLogs { get; set; } = new List<DogSkillsLog>();
    }

}
