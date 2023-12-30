using CynologistPlusMobile.Model;
using CynologistPlusMobile.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CynologistPlusMobile.Interfaces
{
    public interface IDogService
    {
        Task<IEnumerable<Dog>> GetAllDogs();
        Task<Dog> GetDog(int id);
        Task<IEnumerable<DogSkill>> GetDogSkills(int dogId);
        Task<IEnumerable<DogSkillsLog>> GetDogSkillLog(int dogId, int skillId);
        Task<bool> ChangeDogSkill(DogSkill dogSkill);
    }
}
