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

            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("id")
                .HasColumnType("uuid");
            // builder.HasNoKey();

            builder.OwnsOne(x => x.HitPoints)
                .Property(x => x.Value)
                .HasColumnName("hitpoints")
                .HasColumnType("integer");

            builder.OwnsOne(x => x.Attack)
                .Property(x => x.Value)
                .HasColumnName("attack")
                .HasColumnType("integer");

            builder.OwnsOne(x => x.Defense)
                .Property(x => x.Value)
                .HasColumnName("defense")
                .HasColumnType("integer");

            builder.OwnsOne(x => x.SpecialAttack)
                .Property(x => x.Value)
                .HasColumnName("special_attack")
                .HasColumnType("integer");

            builder.OwnsOne(x => x.SpecialDefense)
                .Property(x => x.Value)
                .HasColumnName("special_defense")
                .HasColumnType("integer");

            builder.OwnsOne(x => x.Speed)
                .Property(x => x.Value)
                .HasColumnName("speed")
                .HasColumnType("integer");

            builder.Property(x => x.PokemonNumber)
                .HasColumnName("pokemon_number")
                .HasColumnType("int");

            builder.Ignore(x => x.TotalPoints);
            builder.Ignore(x => x.Pokemon);
        }
    }
}
