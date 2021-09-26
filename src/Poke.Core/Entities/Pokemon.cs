using System;
using Flunt.Validations;
using Poke.Core.Enums;
using Poke.Shared.Entities;

namespace Poke.Core.Entities
{
    public class Pokemon : Aggregate
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Species { get; private set; }
        public double Height { get; private set; }
        public double Weight { get; private set; }
        public string ImageUrl { get; private set; }
        public PokemonType FirstType { get; private set; }
        public PokemonType? SecondType { get; private set; }
        public Training Training { get; private set; }
        public BaseStats BaseStats { get; private set; }

        private Pokemon()
        {}

        public Pokemon(
            int number, string name, string species, double height,
            double weight, string imageUrl, PokemonType firstType,
            Training training, BaseStats baseStats,
            PokemonType? secondType = null
        ) : base()
        {
            Number = number;
            Name = name;
            Species = species;
            Height = height;
            Weight = weight;
            ImageUrl = imageUrl;
            FirstType = firstType;
            SecondType = secondType;
            Training = training;
            BaseStats = baseStats;

            Validate();
        }

        public Pokemon(
            Guid id, int number, string name, string species, double height,
            double weight, string imageUrl, PokemonType firstType,
            Training training, BaseStats baseStats,
            PokemonType? secondType = null
        ) : base(id)
        {
            Number = number;
            Name = name;
            Species = species;
            Height = height;
            Weight = weight;
            ImageUrl = imageUrl;
            FirstType = firstType;
            SecondType = secondType;
            Training = training;
            BaseStats = baseStats;

            Validate();
        }

        protected override void Validate()
        {
            AddNotifications(
                Training,
                BaseStats,
                new Contract<Pokemon>()
                    .Requires()
                    .IsGreaterOrEqualsThan(
                        Number, 1, "Pokemon number",
                        "Pokemon number should be greater than one (1)."
                    )
                    .IsLowerOrEqualsThan(
                        Number, 151, "Pokemon number",
                        "Pokemon number should be greater than one hundred fifty one (151)."
                    )
                    .IsNotNullOrEmpty(
                        Name, "Pokemon name",
                        "Pokemon name should not be null or empty."
                    )
                    .IsNotNullOrEmpty(
                        Species, "Pokemon species",
                        "Pokemon species should not be null or empty."
                    )
                    .IsGreaterThan(
                        Height, 0d, "Pokemon height",
                        "Pokemon height should be greater than zero (0)."
                    )
                    .IsGreaterThan(
                        Weight, 0d, "Pokemon weight",
                        "Pokemon weight should be greater than zero (0)."
                    )
                    .IsUrl(
                        ImageUrl, "Pokemon image URL",
                        "Pokemon image should be a valid URL."
                    )
                    .IsNotNullOrEmpty(
                        ImageUrl, "Pokemon image URL",
                        "Pokemon image URL should not be null or empty."
                    )
                    .IsGreaterOrEqualsThan(
                        (int)FirstType, 0, "Pokemon first type",
                        "Pokemon first type should be greater than zero (0)."
                    )
                    .IsLowerOrEqualsThan(
                        (int)FirstType, 14, "Pokemon first type",
                        "Pokemon first type should be lower than fourteen (14)."
                    )
            );

            if (SecondType is not null)
            {
                AddNotifications(
                    new Contract<Pokemon>()
                        .Requires()
                        .IsGreaterOrEqualsThan(
                            (int)SecondType, 0, "Pokemon first type",
                            "Pokemon first type should be greater than zero (0)."
                        )
                        .IsLowerOrEqualsThan(
                            (int)SecondType, 14, "Pokemon first type",
                            "Pokemon first type should be lower than fourteen (14)."
                        )
                );
            }
        }
    }
}
