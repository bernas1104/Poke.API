
using MediatR;
using Newtonsoft.Json;

namespace Poke.Core.Commands.Requests
{
    public record UpdatePokemonByNumberRequest : IRequest<Unit>
    {
        [JsonIgnore]
        public int Number { get; set; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public int FirstType { get; init; }
        public int? SecondType { get; init; }
        public BaseStatsRequest BaseStats { get; init; }
        public TrainingRequest Training { get; init; }
    }
}
