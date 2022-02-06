using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;

namespace Poke.Core.ValueObjects.Evolutions
{
    public class Evolution : AbstractEvolution
    {
        public Evolution()
        {
            //
        }

        protected Evolution(PokemonEvolutionDTO dto) : base(dto)
        {
            //
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public static Evolution FromCreatePokemonEvolutionRequest(
            CreatePokemonEvolutionRequest request,
            int fromPokemonNumber
        )
        {
            return new Evolution(
                new PokemonEvolutionDTO
                {
                    ToNumber = request.ToNumber.Value,
                    FromNumber = fromPokemonNumber,
                    EvolutionStone = request.EvolutionStone,
                    EvolutionType = request.EvolutionType,
                    PokemonEvolutionLevel = request.PokemonEvolutionLevel
                }
            );
        }
    }
}
