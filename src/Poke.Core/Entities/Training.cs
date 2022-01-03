using System;
using Poke.Core.Enums;

namespace Poke.Core.Entities
{
    public class Training : Aggregate
    {
        public int EVYeld { get; private set; }
        public int BaseFriendship { get; private set; }
        public GrowthRate GrowthRate { get; private set; }
        public Guid PokemonId { get; private set; }
        public Pokemon Pokemon { get; private set; }

        private Training()
        {}

        public Training(
            int evYeld, int baseFrindship, GrowthRate growthRate
        )
        {
            EVYeld = evYeld;
            BaseFriendship = baseFrindship;
            GrowthRate = growthRate;
        }

        public Training(
            Guid id, int evYeld, int baseFrindship,
            GrowthRate growthRate, Guid pokemonId
        ) : base(id)
        {
            EVYeld = evYeld;
            BaseFriendship = baseFrindship;
            GrowthRate = growthRate;
            PokemonId = pokemonId;
        }

        public override bool IsValid()
        {
            return false;
        }
    }
}
