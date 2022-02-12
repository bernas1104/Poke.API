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
            PokemonEvolutionDTO dto,
            int fromPokemonNumber
        )
        {
            return new Evolution(
                new PokemonEvolutionDTO
                {
                    ToNumber = dto.ToNumber,
                    FromNumber = fromPokemonNumber,
                    EvolutionStone = dto.EvolutionStone,
                    EvolutionType = dto.EvolutionType,
                    PokemonEvolutionLevel = dto.PokemonEvolutionLevel
                }
            );
        }
    }
}
