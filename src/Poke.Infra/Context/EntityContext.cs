using Microsoft.EntityFrameworkCore;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;
using Poke.Infra.Mappings;

namespace Poke.Infra.Context
{
    public class EntityContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<BaseStats> BaseStats { get; set; }
        public DbSet<Training> Tranings { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<PreEvolution> PreEvolutions { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PokemonMap());
            modelBuilder.ApplyConfiguration(new EvolutionMap());
            modelBuilder.ApplyConfiguration(new PreEvolutionMap());
            modelBuilder.ApplyConfiguration(new BaseStatsMap());
            modelBuilder.ApplyConfiguration(new TrainingMap());
        }
    }
}
