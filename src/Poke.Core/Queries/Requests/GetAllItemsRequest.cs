using System.Collections.Generic;
using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Queries.Requests
{
    public record GetAllItemsRequest : IRequest<List<Item>>
    {
        //
    }
}
