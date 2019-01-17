using Inkett.ApplicationCore.Entitites;
using Inkett.Infrastructure.Data.ModelConfig;
using Microsoft.EntityFrameworkCore;

namespace Inkett.Infrastructure.Data
{
    public class InkettContext : DbContext
    {
        public InkettContext(DbContextOptions<InkettContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Tattoo> Tattoos { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<TattooStyle> TattooStyles { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TattooConfig());
            modelBuilder.ApplyConfiguration(new AlbumConfig());
            modelBuilder.ApplyConfiguration(new TattooStyleConfig());
            modelBuilder.ApplyConfiguration(new LikeConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new NotificationConfig());

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
