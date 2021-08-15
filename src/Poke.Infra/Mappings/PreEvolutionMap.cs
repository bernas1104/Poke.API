using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class PreEvolutionMap : IEntityTypeConfiguration<PreEvolution>
    {
        public void Configure(EntityTypeBuilder<PreEvolution> builder)
        {
            builder.ToTable("pre_evolution", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer");

            builder.Property(x => x.PkmnPreEvolutionId)
                .HasColumnName("pkmn_pre_evolution_id")
                .HasColumnType("uuid");

            builder.HasOne(x => x.PkmnPreEvolution)
                .WithOne(x => x.PreEvolution)
                .HasForeignKey<PreEvolution>(x => x.PkmnPreEvolutionId);
        }
    }
}
