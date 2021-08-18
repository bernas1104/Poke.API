using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poke.Application.Services.Interfaces;
using Poke.Core.Entities;

namespace Poke.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/{controller}")]
    [Produces("application/json")]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonsService _pokemonsService;

        public PokemonsController(IPokemonsService pokemonsService)
        {
            _pokemonsService = pokemonsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetAll()
        {
            return Ok(await _pokemonsService.GetAllAsync());
        }
    }
}
