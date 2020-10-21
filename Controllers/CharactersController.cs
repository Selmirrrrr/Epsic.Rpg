using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Info3e.Rpg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Epsic.Info3e.Rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var perso = new Character();
            return Ok(perso);
        }
    }
}
