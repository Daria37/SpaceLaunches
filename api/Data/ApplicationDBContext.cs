using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using api.Models;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Rocket> Rocket { get; set; }
        public DbSet<Launches> Launches { get; set; }
        public DbSet<Agency> Agency { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    Id = "1",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    Id = "2",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            
            builder.Entity<Rocket>(entity =>
            {
                entity.ToTable("Rocket");
                entity.Property(r => r.ID).HasColumnName("ID");
                entity.Property(r => r.Name).HasColumnName("Name");
                entity.Property(r => r.Config).HasColumnName("Config");
                entity.Property(r => r.AgencyID).HasColumnName("AgencyID");
            });
            builder.Entity<Launches>(entity =>
            {
                entity.ToTable("Launches");
                entity.Property(r => r.ID).HasColumnName("ID");
                entity.Property(r => r.Name).HasColumnName("Name");
                entity.Property(r => r.CreatedOnUTC).HasColumnName("CreatedOnUTC");
                entity.Property(r => r.RocketID).HasColumnName("RocketID");
                entity.Property(r => r.AgencyID).HasColumnName("AgencyID");
                entity.Property(r => r.CountryCode).HasColumnName("CountryCode");
            });
        }
    }
}