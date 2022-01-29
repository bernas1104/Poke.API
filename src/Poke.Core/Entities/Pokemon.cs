using System;
using Poke.Core.Commands.Requests;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;

namespace Poke.Core.Entities
{
    public class Pokemon : Aggregate
    {
        public int Number { get; protected set; }
        public string Name { get; protected set; }
        public string Species { get; protected set; }
        public double Height { get; protected set; }
        public double Weight { get; protected set; }
        public string ImageUrl { get; protected set; }
        public PokemonType FirstType { get; protected set; }
        public PokemonType SecondType { get; protected set; }
        public Training Training { get; protected set; }
        public BaseStats BaseStats { get; protected set; }

        public Pokemon()
        {}

        public Pokemon(CreatePokemonRequest request) : base()
        {
            Number = request.Number;
            Name = request.Name;
            Species = request.Species;
            Height = request.Height;
            Weight = request.Weight;
            ImageUrl = request.ImageUrl;
            FirstType = (PokemonType)request.FirstType;
            SecondType = (PokemonType)request.SecondType;

            Training = new Training(request.Training);
            BaseStats = new BaseStats(request.BaseStats);
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
