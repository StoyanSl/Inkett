using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkett.Infrastructure.Data.ModelConfig
{
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Tattoo)
            .WithMany(a => a.Comments)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
