using System;
using System.Collections.Generic;

namespace CynologistPlusMobile.Model
{
    public class Dog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public int ClientId { get; set; }

        //public virtual ICollection<DogSkill> DogSkills { get; set; } = new List<DogSkill>();

        //public virtual ICollection<DogSkillsLog> DogSkillsLogs { get; set; } = new List<DogSkillsLog>();
    }

}

