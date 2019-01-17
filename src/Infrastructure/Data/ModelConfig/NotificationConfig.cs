using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Inkett.Infrastructure.Data.ModelConfig
{
    class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(f => f.Profile)
               .WithMany(p => p.Notifications)
               .HasForeignKey(f => f.ProfileId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
