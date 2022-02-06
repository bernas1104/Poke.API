using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.Entities;

namespace Poke.Infra.Mappings
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("item", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("text");

            builder.Property(x => x.HeldItem)
                .HasColumnName("held_item")
                .HasColumnType("boolean");

            builder.Property(x => x.ItemType)
                .HasColumnName("item_type")
                .HasColumnType("integer");
        }
    }
}
