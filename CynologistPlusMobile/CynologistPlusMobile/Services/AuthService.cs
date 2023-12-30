using CynologistPlusMobile.DTO;
using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Model;
using CynologistPlusMobile.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace CynologistPlusMobile.Services
{
    /// <summary>
    /// Represents the service responsible for handling authentication-related operations.
    /// Extends <see cref="ServiceBase"/> and implements <see cref="IAuthService"/>.
    /// </summary>
    public class AuthService : ServiceBase, IAuthService
    {
        // Getting api URL from local preferences
        readonly string apiUrl = Preferences.Get("ApiUrl", "") + "/api/Auth";

        public async Task<bool> SignIn(string username, string password)
        {
            AuthModel model = new AuthModel()
            {
                Login = username,
                Password = password,
                Role = "cynologist"
            };

            HttpClient client = GetClient();
            string serializedModel = JsonSerializer.Serialize(model);
            var response = await client.PostAsync($"{apiUrl}/login",
                new StringContent(
                    serializedModel,
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                AuthResponse authResponseContentModel = JsonSerializer.Deserialize<AuthResponse>(responseContent, options);
                Preferences.Set("token", authResponseContentModel.Token);
                return true;
            }  
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                return false;
            else
                // Exception throws if http response code incorrect
                throw new Exception();
        }

        // JWT is stored in local preferences after authorization
        public int GetCynologistIdFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            string jsonTokenString = Preferences.Get("token", "");
            if (jsonTokenString == "")
                return -1;
            var jsonToken = handler.ReadToken(jsonTokenString) as JwtSecurityToken;
            foreach (var claim in jsonToken.Claims)
            {
                string type = claim.Type;
                string value = claim.Value;
                if (type == "userId")
                    return Convert.ToInt32(value);
            }
            return -1;
        }

        public async Task<Cynologist> GetCynologistById(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{Preferences.Get("ApiUrl", "")}/api/Client/cynologist/{id}");
            return JsonSerializer.Deserialize<Cynologist>(result, options);
        } 
    }
}
