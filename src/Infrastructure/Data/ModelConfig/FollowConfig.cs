using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.Ignore(l => l.Id);

            builder.HasKey(f => new { f.ProfileId, f.FollowedProfileId });

            builder.HasOne(f => f.Profile)
             .WithMany(p => p.Follows)
             .HasForeignKey(f => f.ProfileId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.FollowedProfile)
                .WithMany(p => p.Followers)
                .HasForeignKey(f => f.FollowedProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
