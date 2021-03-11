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
                //optionsBuilder.UseSqlServer("Server=DESKTOP-RRQS078\\SQLEXPRESS01;Database=PlantAppDb;Integrated Security=true").UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("Server=SQL5080.site4now.net;Database=DB_A70974_StoianDardzhikov;User Id=DB_A70974_StoianDardzhikov_admin;Password=rootroot1Bg!").UseLazyLoadingProxies();
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