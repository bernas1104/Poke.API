using System;
using Poke.Core.Enums;

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
        }

        public override bool IsValid()
        {
            return false;
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
