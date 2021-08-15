using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class PreEvolutionMap : IEntityTypeConfiguration<PreEvolution>
    {
        public void Configure(EntityTypeBuilder<PreEvolution> builder)
        {
            builder.ToTable("PreEvolutions", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.HasOne(x => x.PkmnPreEvolution)
                .WithOne(x => x.PreEvolution)
                .HasForeignKey<PreEvolution>(x => x.PkmnPreEvolutionId);
        }
    }
}
