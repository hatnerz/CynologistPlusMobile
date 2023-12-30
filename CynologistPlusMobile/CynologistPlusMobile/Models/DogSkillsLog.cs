using System;
using System.Collections.Generic;

namespace CynologistPlusMobile.Model
{
    public class DogSkillsLog
    {
        public int Id { get; set; }

        public int? DogId { get; set; }

        public int? SkillId { get; set; }

        public double? CurrentValue { get; set; }

        public DateTime? ChangeDate { get; set; }

        public virtual Dog Dog { get; set; }

        public virtual Skill Skill { get; set; }
    }

}

