using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class PokemonMap : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.ToTable("pokemon", "dbo");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Number).IsUnique(true);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("integer")
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .IsRequired(true);

            builder.Property(x => x.Species)
                .HasColumnName("species")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Height)
                .HasColumnName("height")
                .HasColumnType("decimal")
                .IsRequired(true);

            builder.Property(x => x.Weight)
                .HasColumnName("weight")
                .HasColumnType("decimal")
                .IsRequired(true);

            builder.Property(x => x.ImageUrl)
                .HasColumnName("image_url")
                .HasColumnType("text")
                .IsRequired(true);

            builder.Property(x => x.FirstType)
                .HasColumnName("first_type")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired(true);

            builder.Property(x => x.SecondType)
                .HasColumnName("second_type")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired(false);

            builder.HasOne(x => x.Traning)
                .WithOne(x => x.Pokemon)
                .HasForeignKey<Training>(x => x.PokemonId)
                .IsRequired(true);

            builder.HasOne(x => x.BaseStats)
                .WithOne(x => x.Pokemon)
                .HasForeignKey<BaseStats>(x => x.PokemonId)
                .IsRequired(true);
        }
    }
}
