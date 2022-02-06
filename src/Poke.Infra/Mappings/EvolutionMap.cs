using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class EvolutionMap : IEntityTypeConfiguration<AbstractEvolution>
    {
        public void Configure(EntityTypeBuilder<AbstractEvolution> builder)
        {
            builder.ToTable("evolution", "dbo");

            builder.HasKey(x => new { x.Id, x.ToNumber, x.FromNumber });

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.FromNumber)
                .HasColumnName("from_number")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.ToNumber)
                .HasColumnName("to_number")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.EvolutionType)
                .HasColumnName("evolution_type")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(x => x.PokemonEvolutionLevel)
                .HasColumnName("pokemon_evolution_level")
                .HasColumnType("integer")
                .IsRequired(false);

            builder.Property(x => x.EvolutionStone)
                .HasColumnName("evolution_stone")
                .HasColumnType("integer")
                .IsRequired(false);

            builder.HasOne<Pokemon>(x => x.PokemonEvolvesFrom)
                .WithMany(x => x.PokemonsEvolveTo)
                .HasForeignKey(x => x.FromNumber);

            builder.HasOne<Pokemon>(x => x.PokemonEvolvesTo)
                .WithMany(x => x.PokemonsEvolveFrom)
                .HasForeignKey(x => x.ToNumber);
        }
    }
}
