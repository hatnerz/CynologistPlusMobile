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
    public class ServiceBase
    {
        // Deserialization settings for case insensitivity
        protected JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        protected HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            // Default client settings can be changed
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
