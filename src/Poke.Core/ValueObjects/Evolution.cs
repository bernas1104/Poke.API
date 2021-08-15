using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Poke.Core.Entities;

namespace Poke.Core.ValueObjects
{
    [Table("Evolutions")]
    public class Evolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid PkmnEvolutionId { get; set; }
        public Pokemon PkmnEvolution { get; set; }
    }
}
