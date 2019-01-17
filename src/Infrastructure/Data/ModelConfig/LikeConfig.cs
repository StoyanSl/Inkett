using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class LikeConfig : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {

            builder.Ignore(l => l.Id);

            builder.HasKey(l => new { l.TattooId, l.ProfileId });

            builder.HasOne(l => l.Tattoo)
             .WithMany(t => t.Likes)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Profile)
             .WithMany(p => p.Likes)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
