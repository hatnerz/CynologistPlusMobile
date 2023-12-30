using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Model;
using CynologistPlusMobile.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;
using Xamarin.Essentials;

namespace CynologistPlusMobile.Services
{
    public class DogService : ServiceBase, IDogService
    {
        readonly string apiUrl = Preferences.Get("ApiUrl", "") + "/api/Dog";

        public async Task<IEnumerable<Dog>> GetAllDogs()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(apiUrl);
            return JsonSerializer.Deserialize<IEnumerable<Dog>>(result, options);
        }

        public async Task<Dog> GetDog(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{apiUrl}/{id}");
            return JsonSerializer.Deserialize<Dog>(result, options);
        }

        public async Task<IEnumerable<DogSkill>> GetDogSkills(int dogId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{apiUrl}/skill/{dogId}");
            return JsonSerializer.Deserialize<IEnumerable<DogSkill>>(result, options);
        }

        public async Task<IEnumerable<DogSkillsLog>> GetDogSkillLog(int dogId, int skillId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{apiUrl}/skill/{dogId}/{skillId}");
            return JsonSerializer.Deserialize<IEnumerable<DogSkillsLog>>(result, options);
        }

        public async Task<bool> ChangeDogSkill(DogSkill dogSkill)
        {
            HttpClient client = GetClient();
            DogSkillChangeModel model = new DogSkillChangeModel
            {
                DogId = dogSkill.DogId,
                SkillId = dogSkill.Skill.Id,
                Value = dogSkill.Value
            };
            string serializedModel = JsonSerializer.Serialize(model);
            var response = await client.PutAsync($"{apiUrl}/skill",
                new StringContent(
                    serializedModel,
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            return true;
        }
    }
}
