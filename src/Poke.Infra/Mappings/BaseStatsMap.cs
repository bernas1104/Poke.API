using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.Entities;

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
                .HasColumnType("uuid");

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

            builder.Property(x => x.PokemonId)
                .HasColumnName("pokemon_id")
                .HasColumnType("uuid");

            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.TotalPoints);

            builder.OwnsOne(x => x.HitPoints).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.HitPoints).Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Attack).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.Attack).Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Defense).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.Defense).Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.SpecialAttack).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.SpecialAttack).Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.SpecialDefense).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.SpecialDefense).Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Speed).Ignore(x => x.Notifications);
            builder.OwnsOne(x => x.Speed).Ignore(x => x.IsValid);
        }
    }
}
