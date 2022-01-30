using Poke.Core.DTOs;

namespace Poke.Core.ValueObjects.Evolutions
{
    public class Evolution : AbstractEvolution
    {
        public Evolution()
        {
            //
        }

        public Evolution(PokemonEvolutionDTO dto) : base(dto)
        {
            //
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
