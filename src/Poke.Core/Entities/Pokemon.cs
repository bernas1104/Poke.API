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
        public PokemonType SecondType { get; private set; }
        public Training Training { get; private set; }
        public BaseStats BaseStats { get; private set; }

        private Pokemon()
        {}

        public Pokemon(
            int number, string name, string species, double height,
            double weight, string image_url, PokemonType first_type,
            PokemonType second_type
        ) : base()
        {
            Number = number;
            Name = name;
            Species = species;
            Height = height;
            Weight = weight;
            ImageUrl = image_url;
            FirstType = first_type;
            SecondType = second_type;

            Validate();
        }

        public Pokemon(
            Guid id, int number, string name, string species,
            double height, double weight, string image_url,
            PokemonType first_type, PokemonType second_type
        ) : base(id)
        {
            Number = number;
            Name = name;
            Species = species;
            Height = height;
            Weight = weight;
            ImageUrl = image_url;
            FirstType = first_type;
            SecondType = second_type;

            Validate();
        }

        protected override void Validate()
        {
            AddNotifications(
                new Contract<Pokemon>()
                    .Requires()
                    .IsGreaterOrEqualsThan(
                        Number, 1, "Pokemon number",
                        "Pokemon number should be greater than one (1)."
                    )
                    .IsLowerOrEqualsThan(
                        Number, 151, "Pokemon number",
                        "Pokemon number should be lower than one hundred fifty two (152)."
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
                        (int)FirstType, 1, "Pokemon first type",
                        "Pokemon first type should be greater than one (1)."
                    )
                    .IsLowerOrEqualsThan(
                        (int)FirstType, 15, "Pokemon first type",
                        "Pokemon first type should be lower than sixteen (16)."
                    )
                    .IsGreaterOrEqualsThan(
                        (int)SecondType, 0, "Pokemon first type",
                        "Pokemon first type should be greater than zero (0)."
                    )
                    .IsLowerOrEqualsThan(
                        (int)SecondType, 15, "Pokemon first type",
                        "Pokemon first type should be lower than sixteen (16)."
                    )
                    .AreNotEquals(
                        (int)FirstType, (int)SecondType, "Pokemon types",
                        "Pokemon second type must be different than first type."
                    )
            );
        }

        public void AddTrainingInformation(Training training)
        {
            Training = training;
        }

        public void AddBaseStatsInformation(BaseStats baseStats)
        {
            BaseStats = baseStats;
        }
    }
}
