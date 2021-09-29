using System;
using Flunt.Validations;
using Poke.Core.ValueObjects;
using Poke.Shared.Entities;

namespace Poke.Core.Entities
{
    public class BaseStats : Aggregate
    {
        public Point HitPoints { get; private set; }
        public Point Attack { get; private set; }
        public Point Defense { get; private set; }
        public Point SpecialAttack { get; private set; }
        public Point SpecialDefense { get; private set; }
        public Point Speed { get; private set; }
        public int TotalPoints {
            get => HitPoints.Value + Attack.Value + Defense.Value +
                SpecialAttack.Value + SpecialDefense.Value + Speed.Value;
        }
        public Guid PokemonId { get; private set; }
        public Pokemon Pokemon { get; private set; }

        private BaseStats()
        {}

        public BaseStats(
            Point hitPoints, Point attack, Point defense,
            Point specialAttack, Point specialDefense, Point speed
        )
        {
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;

            Validate();
        }

        public BaseStats(
            Guid id, Point hitPoints, Point attack, Point defense,
            Point specialAttack, Point specialDefense, Point speed,
            Guid pokemonId
        ) : base(id)
        {
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
            PokemonId = pokemonId;

            Validate();
        }


        protected override void Validate()
        {
            AddNotifications(
                HitPoints,
                Attack,
                Defense,
                SpecialAttack,
                SpecialDefense,
                Speed,
                new Contract<BaseStats>()
                    .Requires()
                    .IsGreaterOrEqualsThan(
                        TotalPoints, 175, "Total Points",
                        "Base Stats sum must be at least one hundred seventy five (175)."
                    )
                    .IsLowerOrEqualsThan(
                        TotalPoints, 590, "Total Points",
                        "Base Stats sum must be at most five hundred ninety (590)."
                    )
            );
        }
    }
}
