using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Commands.Requests
{
    public record CreatePokemonRequest : IRequest<Pokemon>
    {
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public int FirstType { get; init; }
        public int? SecondType { get; init; }
        public CreateBaseStatsRequest BaseStats { get; init; }
        public CreateTrainingRequest Training { get; init; }
    }
}
