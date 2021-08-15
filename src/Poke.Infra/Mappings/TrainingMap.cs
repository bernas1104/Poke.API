using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class TrainingMap : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("traning", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(x => x.EVYeld)
                .HasColumnName("ev_yeld")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.CatchRate)
                .HasColumnName("catch_rate")
                .HasColumnType("decimal")
                .IsRequired(true);

            builder.Property(x => x.BaseFriendship)
                .HasColumnName("base_friendship")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.BaseExperience)
                .HasColumnName("base_experience")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.GrowthRate)
                .HasColumnName("growth_rate")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.PokemonId)
                .HasColumnName("pokemon_id");
        }
    }
}
