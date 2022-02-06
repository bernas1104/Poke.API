using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class PokemonMap : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.ToTable("pokemon", "dbo");

            // builder.HasKey(x => x.Id);

            // builder.HasIndex(x => x.Number);

            builder.HasKey(x => x.Number);

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("integer")
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar");

            builder.Property(x => x.Species)
                .HasColumnName("species")
                .HasColumnType("varchar");

            builder.Property(x => x.Height)
                .HasColumnName("height")
                .HasColumnType("decimal");

            builder.Property(x => x.Weight)
                .HasColumnName("weight")
                .HasColumnType("decimal");

            builder.Property(x => x.ImageUrl)
                .HasColumnName("image_url")
                .HasColumnType("varchar");

            builder.Property(x => x.FirstType)
                .HasColumnName("first_type")
                .HasColumnType("integer");

            builder.Property(x => x.SecondType)
                .HasColumnName("second_type")
                .HasColumnType("integer");

            builder.HasOne<Training>(x => x.Training)
                .WithOne(x => x.Pokemon)
                .HasForeignKey<Training>(x => x.PokemonNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<BaseStats>(x => x.BaseStats)
                .WithOne(x => x.Pokemon)
                .HasForeignKey<BaseStats>(x => x.PokemonNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
