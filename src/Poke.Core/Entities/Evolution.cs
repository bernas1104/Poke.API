using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Core.Entities
{
    [Table("Evolutions")]
    public class Evolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid PokemonEvolutionId { get; set; }
        public Guid PokemonPreEvolutionId { get; set; }
        public Pokemon PokemonEvolution { get; set; }
        public Pokemon PokemonPreEvolution { get; set; }
    }
}
