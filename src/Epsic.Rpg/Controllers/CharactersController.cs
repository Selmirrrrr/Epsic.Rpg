using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Rpg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Epsic.Rpg.Controllers
{
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private IList<Character> _characters = new List<Character> 
        {
            new Character { Id = 1, Name = "Pierre"},
            new Character { Id = 2, Name = "Paul"},
            new Character { Id = 3, Name = "Jacques"},
        };

        [HttpGet("characters")]
        public IActionResult Get()
        {
            return Ok(_characters.FirstOrDefault());
        }
    }
}
