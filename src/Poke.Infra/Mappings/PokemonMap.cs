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

            builder.HasIndex(x => x.Number);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid");

            builder.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("integer")
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
                .HasColumnType("text");

            builder.Property(x => x.FirstType)
                .HasColumnName("first_type")
                .HasColumnType("varchar");

            builder.Property(x => x.SecondType)
                .HasColumnName("second_type")
                .HasColumnType("varchar")
                .IsRequired(false);

            builder.HasOne<Training>(x => x.Training);

            builder.HasOne<BaseStats>(x => x.BaseStats);

            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);
        }
    }
}
