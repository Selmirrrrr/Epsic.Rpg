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
            // Act
            var client = _factory.CreateClient();
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
            // Act
            var client = _factory.CreateClient();
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
            // Act
            var client = _factory.CreateClient();
            var response = await client.GetFromJsonAsync<Character>($"characters/getsingle/{id}");

            // Assert
            Assert.AreEqual(id, response.Id);
        }
    }
}
