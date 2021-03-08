using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Data
{
    public class PlantAppDbContext : IdentityDbContext
    {
        public PlantAppDbContext() { }

        public PlantAppDbContext(DbContextOptions<PlantAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RRQS078\\SQLEXPRESS01;Database=PlantAppDb;Integrated Security=true").UseLazyLoadingProxies();
            }
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.WateringPeriod)
                    .IsRequired();

                entity.Property(e => e.LastWateredOn)
                    .IsRequired();

                entity.HasOne(p => p.User)
                .WithMany(u => u.Plants)
                .HasForeignKey(p => p.UserId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}