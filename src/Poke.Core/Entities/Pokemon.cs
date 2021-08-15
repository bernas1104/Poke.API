using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities.Enumerations;
using Poke.Core.ValueObjects;

namespace Poke.Core.Entities
{
    [Table("Pokemons")]
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string ImageUrl { get; set; }
        public PokemonType FirstType { get; set; }
        public PokemonType? SecondType { get; set; }
        public Training Traning { get; set; }
        public BaseStats BaseStats { get; set; }
        public PreEvolution PreEvolution { get; set; }
        public IEnumerable<Evolution> Evolutions { get; set; }
    }
}
