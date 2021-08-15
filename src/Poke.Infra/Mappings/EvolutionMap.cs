using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class EvolutionMap : IEntityTypeConfiguration<Evolution>
    {
        public void Configure(EntityTypeBuilder<Evolution> builder)
        {
            builder.ToTable("Evolutions", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.HasOne(x => x.PkmnEvolution)
                .WithMany(x => x.Evolutions)
                .HasForeignKey(x => x.PkmnEvolutionId);
        }
    }
}
