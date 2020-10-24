using System.Text.Json;
using System.Threading.Tasks;
using Epsic.Rpg;
using Epsic.Rpg.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Epsic.Rpg.Tests.Controllers
{
    public class ApiControllerTestBase : WebApplicationFactory<Startup>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ApiControllerTestBase()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        protected async Task<T> GetAsync<T>(string url) 
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<T>(body);
        } 

        protected async Task<string> GetAsync(string url) 
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        } 
    }
}