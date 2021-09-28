using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poke.Application.Dtos.InputModels;
using Poke.Application.Dtos.ViewModels;
using Poke.Application.Services.Interfaces;

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

        [HttpPost]
        public async Task<ActionResult<PokemonViewModel>> CreateAsync(
            [FromBody] PokemonInputModel pokemon
        )
        {
            var createdPokemon = await _pokemonsService.CreatePokemonAsync(
                pokemon
            );

            return Created(
                $"/api/v1/pokemon/{createdPokemon.Id.ToString()}",
                pokemon
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonViewModel>>> GetAllAsync()
        {
            return Ok(await _pokemonsService.GetAllAsync());
        }
    }
}
