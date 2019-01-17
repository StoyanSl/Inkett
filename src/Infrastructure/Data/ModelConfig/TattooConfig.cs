using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class TattooConfig : IEntityTypeConfiguration<Tattoo>
    {
        public void Configure(EntityTypeBuilder<Tattoo> builder)
        {
            builder.HasOne(b => b.Album)
                .WithMany(a => a.Tattoos)
                 .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Profile)
               .WithMany(a => a.Tattoos)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
