using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poke.Core.Commands.Requests;
using Poke.Core.Commands.Responses;
using Poke.Core.Entities;

namespace Poke.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/{controller}")]
    [Produces("application/json")]
    public class PokemonsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PokemonsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePokemonResponse>> CreatePokemonAsync(
            [FromBody] CreatePokemonRequest request
        )
        {
            var pokemon = await _mediator.Send<Pokemon>(request);
            return Created(
                $"api/v1/pokemon/{pokemon.Id}",
                _mapper.Map<CreatePokemonResponse>(pokemon)
            );
        }
    }
}
