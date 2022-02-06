using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;

namespace Poke.Core.ValueObjects.Evolutions
{
    public class PreEvolution : AbstractEvolution
    {
        public PreEvolution()
        {
            //
        }

        public PreEvolution(PokemonEvolutionDTO dto) : base(dto)
        {
            //
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public static PreEvolution FromCreatePokemonEvolutionRequest(
            CreatePokemonEvolutionRequest request,
            int toPokemonNumber
        )
        {
            return new PreEvolution(
                new PokemonEvolutionDTO
                {
                    ToNumber = toPokemonNumber,
                    FromNumber = request.FromNumber.Value,
                    EvolutionType = request.EvolutionType,
                    EvolutionStone = request.EvolutionStone,
                    PokemonEvolutionLevel = request.PokemonEvolutionLevel
                }
            );
        }
    }
}
