using Microsoft.EntityFrameworkCore;
using RestAprilEducation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {

                p.HasKey(x => x.Id);
                p.Property(x => x.Name).HasColumnName("name").HasMaxLength(256);
                p.Property(x => x.Price).HasPrecision(18, 2);
                p.Property(x => x.Barcode).HasColumnName("barcode").HasMaxLength(128);
                p.ToTable("products");

            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
