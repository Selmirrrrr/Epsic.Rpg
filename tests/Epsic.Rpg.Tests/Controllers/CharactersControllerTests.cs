using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Rpg.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epsic.Rpg.Tests.Controllers
{
    [TestClass]
    public class CharactersControllerTests : WebApplicationFactory<Startup>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CharactersControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }
        [TestMethod, TestCategory("Ex1")]
        public async Task CharactersGetAll()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<IList<Character>>("/characters/getall");

            // Assert
            Assert.AreEqual(3, response.Count);
        }

        [TestMethod, TestCategory("Ex1")]
        [DataRow("P", 2)]
        [DataRow("Pierre", 1)]
        [DataRow("cques", 1)]
        public async Task CharactersPersonnagesNom_Ok(string nom, int expectedCount)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<IList<Character>>($"/personnages?nom={nom}");

            // Assert
            Assert.AreEqual(expectedCount, response.Count);
        }

        [TestMethod, TestCategory("Ex1")]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task CharactersGetSinleId_Ok(int id)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<Character>($"characters/getsingle/{id}");

            // Assert
            Assert.AreEqual(id, response.Id);
        }

        [TestMethod, TestCategory("Ex2")]
        public async Task CharactersCreate()
        {
            // Arrange
            var id = 4;
            var name = "TestPlayer";
            var hitPoints = 4;
            var character = new Character { Id = id, Name = name, HitPoints = hitPoints };
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.PostAsJsonAsync<Character>("/characters", character);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Character>();

            // Assert
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(hitPoints, result.HitPoints);
        }

        [TestMethod, TestCategory("Ex2")]
        public async Task CharactersPut()
        {
            // Arrange
            var name = "TestPlayer";
            var id = 3;
            var character = new Character { Id = id, Name = name, HitPoints = 6 };
            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync<Character>("/characters", character);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Character>();
            
            // Assert
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(6, result.HitPoints);
        }

        [TestMethod, TestCategory("Ex2")]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task CharactersDelete(int id)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/characters/{id}");

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
