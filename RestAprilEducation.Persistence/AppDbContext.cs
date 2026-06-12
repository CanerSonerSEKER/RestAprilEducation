using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAprilEducation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) 
        : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

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

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
