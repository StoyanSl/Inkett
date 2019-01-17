using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasOne(a => a.Profile)
                .WithMany(p => p.Albums)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
