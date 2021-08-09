using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Core.ValueObjects;

namespace Poke.Infra.Mappings
{
    public class TraningMap : IEntityTypeConfiguration<Traning>
    {
        public void Configure(EntityTypeBuilder<Traning> builder)
        {
            builder.ToTable("Tranings", "dbo");

            builder.HasKey(x => x.Id);
        }
    }
}