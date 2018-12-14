using Inkett.ApplicationCore.Entitites.Profile;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
