using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities.Enumerations;
using Poke.Core.ValueObjects;

namespace Poke.Core.Entities
{
    public class Pokemon
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string ImageUrl { get; set; }
        public PokemonType FirstType { get; set; }
        public PokemonType? SecondType { get; set; }
        public Traning Traning { get; set; }
        public BaseStats BaseStats { get; set; }
        
        [NotMapped]
        public Pokemon PreEvolution { get; set; }
        [NotMapped]
        public IEnumerable<Pokemon> Evolutions { get; set; }
    }
}