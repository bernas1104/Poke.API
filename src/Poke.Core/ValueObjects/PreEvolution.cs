using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities;

namespace Poke.Core.ValueObjects
{
    [Table("pre_evolution")]
    public class PreEvolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid PkmnPreEvolutionId { get; set; }
        public Pokemon PkmnPreEvolution { get; set; }
    }
}
