using System;
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
    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ItemsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateItemResponse>> CreateItemAsync(
            [FromBody] CreateItemRequest request
        )
        {
            var item = _mapper.Map<CreateItemResponse>(
                await _mediator.Send<Item>(request)
            );

            return Created($"/api/v1/items/{item.Name}", item);
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemQueryResponse>>> GetAllAsync()
        {
            return Ok(
                _mapper.Map<List<ItemQueryResponse>>(
                    await _mediator.Send<List<Item>>(new GetAllItemsRequest())
                )
            );
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ItemQueryResponse>> GetByNameAsync(
            [FromRoute] string name
        )
        {
            return Ok(
                _mapper.Map<ItemQueryResponse>(
                    await _mediator.Send<Item>(
                        new GetItemByNameRequest
                        {
                            Name = name
                        }
                    )
                )
            );
        }
    }
}
