﻿using Microsoft.EntityFrameworkCore;
using PigmaAPI.Entities;

namespace PigmaAPI.Infrastructure.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<CompanyContact> CompanyContacts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime"); ;
                entity.Property(e => e.LastUpdated).HasColumnType("datetime"); ;
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Address).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.LastName).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Phone).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Id).HasColumnType("int").HasMaxLength(11);
            });


            modelBuilder.Entity<User>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Password).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.LastName).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Username).HasColumnType("varchar").HasMaxLength(255);
                entity.Property(e => e.Id).HasColumnType("int").HasMaxLength(11);
            

            });
        }


    }
}
