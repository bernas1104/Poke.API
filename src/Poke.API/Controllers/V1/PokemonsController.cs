using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poke.Core.Commands.Requests;
using Poke.Core.Commands.Responses;
using Poke.Core.Entities;
using Poke.Core.Queries.Requests;
using Poke.Core.Queries.Response;

namespace Poke.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<List<PokemonQueryResponse>>> GetAllAsync()
        {
            return Ok(
                _mapper.Map<List<PokemonQueryResponse>>(
                    await _mediator.Send<List<Pokemon>>(
                        new GetAllPokemonRequest()
                    )
                )
            );
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

        [HttpGet("{number}")]
        public async Task<ActionResult<PokemonQueryResponse>> GetByNumberAsync(
            [FromRoute] int number
        )
        {
            return Ok(
                _mapper.Map<PokemonQueryResponse>(
                    await _mediator.Send<Pokemon>(
                        new GetPokemonByNumberRequest
                        {
                            Number = number,
                        }
                    )
                )
            );
        }

        [HttpPut("{number}")]
        public async Task<ActionResult> UpdateByNumberAsync(
            [FromRoute] int number,
            [FromBody] UpdatePokemonByNumberRequest request
        )
        {
            request.Number = number;
            await _mediator.Send<Unit>(request);

            return NoContent();
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteByNumberAsync(
            [FromRoute] int number
        )
        {
            await _mediator.Send<Unit>(
                new DeletePokemonByNumberRequest { Number = number, }
            );

            return NoContent();
        }
    }
}
