using System;
using Flunt.Validations;
using Poke.Core.Enums;
using Poke.Shared.Entities;

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

            Validate();
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

            Validate();
        }

        protected override void Validate()
        {
            AddNotifications(
                new Contract<Training>()
                    .Requires()
                    .IsGreaterOrEqualsThan(
                        EVYeld, 1, "EV Yeld",
                        "EV Yeld should be at least one (1)."
                    )
                    .IsLowerOrEqualsThan(
                        EVYeld, 3, "EV Yeld",
                        "EV Yeld should be at least three (3)."
                    )
                    .IsGreaterOrEqualsThan(
                        BaseFriendship, 50, "Base friendship",
                        "Base friendship should be at least fifty (50)."
                    )
                    .IsLowerOrEqualsThan(
                        BaseFriendship, 140, "Base friendship",
                        "Base friendship should be at most one hundred forty (140)."
                    )
                    .IsGreaterOrEqualsThan(
                        (int)GrowthRate, 0, "Growth Rate",
                        "Growth Rate should be at least zero (0)."
                    )
                    .IsLowerOrEqualsThan(
                        (int)GrowthRate, 5, "Growth Rate",
                        "Growth Rate should be at most five (5)."
                    )
            );
        }
    }
}
