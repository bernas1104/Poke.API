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
    }
}
