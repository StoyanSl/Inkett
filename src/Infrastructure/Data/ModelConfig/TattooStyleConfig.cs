using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class TattooStyleConfig : IEntityTypeConfiguration<TattooStyle>
    {
        public void Configure(EntityTypeBuilder<TattooStyle> builder)
        {
            builder.Ignore(ts => ts.Id);

            builder.HasKey(ts => new { ts.StyleId, ts.TattooId });

            builder.HasOne(ts => ts.Tattoo)
                .WithMany(t => t.TattooStyles)
                .HasForeignKey(ts => ts.TattooId);

            builder.HasOne(ts => ts.Style)
                .WithMany(t => t.TattooStyles)
                .HasForeignKey(ts => ts.StyleId);
        }
    }
}
