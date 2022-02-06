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

            builder.Property(x => x.EVYeld)
                .HasColumnName("ev_yeld")
                .HasColumnType("integer");

            builder.Property(x => x.BaseFriendship)
                .HasColumnName("base_friendship")
                .HasColumnType("integer");

            builder.Property(x => x.GrowthRate)
                .HasColumnName("growth_rate")
                .HasColumnType("integer");

            builder.Property(x => x.PokemonNumber)
                .HasColumnName("pokemon_number")
                .HasColumnType("int");

            builder.Ignore(x => x.Pokemon);
        }
    }
}
