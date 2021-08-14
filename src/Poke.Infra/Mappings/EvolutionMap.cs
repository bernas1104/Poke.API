using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.Entities;

namespace Poke.Infra.Mappings
{
    public class EvolutionMap : IEntityTypeConfiguration<Evolution>
    {
        public void Configure(EntityTypeBuilder<Evolution> builder)
        {
            builder.ToTable("Evolutions", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(x => x.PokemonEvolutionId)
                .HasColumnName("pokemon_evolution_id");

            builder.Property(x => x.PokemonPreEvolutionId)
                .HasColumnName("pokemon_pre_evolution_id");

            builder.HasOne(x => x.PokemonPreEvolution)
                .WithMany()
                .HasForeignKey(x => x.PokemonPreEvolutionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PokemonEvolution)
                .WithMany(x => x.Evolutions)
                .HasForeignKey(x => x.PokemonEvolutionId);
        }
    }
}
