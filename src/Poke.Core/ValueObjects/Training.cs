using System;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities;
using Poke.Core.Entities.Enumerations;

namespace Poke.Core.ValueObjects
{
    [Table("trainings")]
    public class Training
    {
        public int Id { get; set; }
        public int EVYeld { get; set; }
        public double CatchRate { get; set; }
        public int BaseFriendship { get; set; }
        public int BaseExperience { get; set; }
        public GrowthRate GrowthRate { get; set; }
        public Guid PokemonId { get; set; }
        [NotMapped]
        public Pokemon Pokemon { get; set; }
    }
}
