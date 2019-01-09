using Inkett.ApplicationCore.Entitites;
using Inkett.Infrastructure.Data.ModelConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.Infrastructure.Data
{
    public class InkettContext : DbContext
    {
        public InkettContext(DbContextOptions<InkettContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Tattoo> Tattoos { get; set; }

        public DbSet<TattooStyle> TattooStyles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>().HasMany(a => a.Albums).WithOne();

            modelBuilder.Entity<TattooStyle>().Ignore(ts => ts.Id);
            modelBuilder.Entity<TattooStyle>()
                .HasKey(ts => new { ts.StyleId, ts.TattooId });
            modelBuilder.Entity<TattooStyle>()
                .HasOne(ts => ts.Tattoo)
                .WithMany(t => t.TattooStyles)
                .HasForeignKey(ts => ts.TattooId);
            modelBuilder.Entity<TattooStyle>()
                .HasOne(ts => ts.Style)
                .WithMany(t => t.TattooStyles)
                .HasForeignKey(ts => ts.StyleId);

            modelBuilder.Entity<Tattoo>()
                .HasOne(b => b.Album)
                .WithMany(a => a.Tattoos)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Tattoo>()
               .HasOne(t => t.Profile)
               .WithMany(a => a.Tattoos)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
              .HasOne(c => c.Tattoo)
              .WithMany(a => a.Comments)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>().Ignore(l => l.Id);
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.TattooId, l.ProfileId });
            modelBuilder.Entity<Like>()
             .HasOne(l => l.Tattoo)
             .WithMany(t => t.Likes)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
             .HasOne(l => l.Profile)
             .WithMany(p => p.Likes)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>().Ignore(l => l.Id);
            modelBuilder.Entity<Follow>()
             .HasKey(f => new { f.ProfileId, f.FollowedProfileId });

            modelBuilder.Entity<Follow>()
             .HasOne(f=>f.Profile)
             .WithMany(p=>p.Follows)
             .HasForeignKey(f=>f.ProfileId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.FollowedProfile)
                .WithMany(p => p.Followers)
                .HasForeignKey(f => f.FollowedProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
               .HasOne(f => f.Profile)
               .WithMany(p => p.Notifications)
               .HasForeignKey(f => f.ProfileId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
