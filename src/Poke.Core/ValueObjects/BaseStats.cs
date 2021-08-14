using System;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities;

namespace Poke.Core.ValueObjects
{
    [Table("BaseStats")]
    public class BaseStats
    {
        public int Id { get; set; }
        public int HitPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public Guid PokemonId { get; set; }
        [NotMapped]
        public Pokemon Pokemon { get; set; }
    }
}
