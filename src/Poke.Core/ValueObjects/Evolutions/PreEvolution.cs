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
            PokemonEvolutionDTO dto,
            int toPokemonNumber
        )
        {
            return new PreEvolution(
                new PokemonEvolutionDTO
                {
                    ToNumber = toPokemonNumber,
                    FromNumber = dto.FromNumber,
                    EvolutionType = dto.EvolutionType,
                    EvolutionStone = dto.EvolutionStone,
                    PokemonEvolutionLevel = dto.PokemonEvolutionLevel
                }
            );
        }
    }
}
