using System;
using System.Collections.Generic;
using System.Text;
using FarmAppV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FarmAppV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {           
        }

        public DbSet<Sheep> Sheep { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SheepVaccine>()
                .HasKey(bc => new { bc.SheepId, bc.VaccineId });
            modelBuilder.Entity<SheepVaccine>()
                .HasOne(bc => bc.Sheep)
                .WithMany(b => b.SheepVaccines)
                .HasForeignKey(bc => bc.SheepId);
            modelBuilder.Entity<SheepVaccine>()
                .HasOne(bc => bc.Vaccine)
                .WithMany(c => c.SheepVaccines)
                .HasForeignKey(bc => bc.VaccineId);
        }

     
    }
}
