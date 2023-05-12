using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPF_EF_MSSQL_eCatalog.Models;
namespace WPF_EF_MSSQL_eCatalog.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\local;Initial Catalog=davidECatalogDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Categories_CategoryId").IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Products_ProductId").IsUnique();
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_ProductCategories_CategoryId");

            entity.HasIndex(e => e.ProductId, "IX_ProductCategories_ProductId");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories).HasForeignKey(d => d.ProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
