using System;
using Poke.Core.ValueObjects.Nullables;

namespace Poke.Core.Entities.Nullables
{
    public class NullPokemon : Pokemon
    {
        public NullPokemon()
        {
            Number = default;
            Name = default;
            Species = default;
            Height = default;
            Weight = default;
            ImageUrl = default;
            FirstType = default;
            SecondType = default;
            Training = new NullTraining();
            BaseStats = new NullBaseStats();
        }

        public override bool IsNull => true;
    }
}
