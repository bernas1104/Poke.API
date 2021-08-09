using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class BaseStatsMap : IEntityTypeConfiguration<BaseStats>
    {
        public void Configure(EntityTypeBuilder<BaseStats> builder)
        {
            builder.ToTable("BaseStats", "dbo");

            builder.HasKey(x => x.Id);
        }
    }
}