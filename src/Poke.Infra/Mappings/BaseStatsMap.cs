using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class BaseStatsMap : IEntityTypeConfiguration<BaseStats>
    {
        public void Configure(EntityTypeBuilder<BaseStats> builder)
        {
            builder.ToTable("base_stats", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(x => x.HitPoints)
                .HasColumnName("hitpoints")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.Attack)
                .HasColumnName("attack")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.Defense)
                .HasColumnName("defense")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.SpecialAttack)
                .HasColumnName("special_attack")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.SpecialDefense)
                .HasColumnName("special_defense")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.Speed)
                .HasColumnName("speed")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.PokemonId)
                .HasColumnName("pokemon_id");
        }
    }
}
