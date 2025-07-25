using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using api.Models;
using Humanizer.DateTimeHumanizeStrategy;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Rocket> Rocket { get; set; }
        public DbSet<Launches> Launches { get; set; }
        public DbSet<Agency> Agency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rocket>(entity =>
            {
                entity.ToTable("Rocket");
                entity.Property(r => r.ID).HasColumnName("ID");
                entity.Property(r => r.Name).HasColumnName("Name");
                entity.Property(r => r.Config).HasColumnName("Config");
                entity.Property(r => r.AgencyID).HasColumnName("AgencyID");
            });
        }
    }
}