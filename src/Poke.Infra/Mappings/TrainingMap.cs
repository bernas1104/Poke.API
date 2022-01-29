using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class TrainingMap : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("training", "dbo");

            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("id")
                .HasColumnType("uuid");
            // builder.HasNoKey();

            builder.Property(x => x.EVYeld)
                .HasColumnName("ev_yeld")
                .HasColumnType("integer");

            builder.Property(x => x.BaseFriendship)
                .HasColumnName("base_friendship")
                .HasColumnType("integer");

            builder.Property(x => x.GrowthRate)
                .HasColumnName("growth_rate")
                .HasColumnType("integer");

            builder.Property(x => x.PokemonId)
                .HasColumnName("pokemon_id")
                .HasColumnType("uuid");

            builder.Ignore(x => x.Pokemon);
        }
    }
}
