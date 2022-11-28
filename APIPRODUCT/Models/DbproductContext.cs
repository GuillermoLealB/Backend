using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIPRODUCT.Models;

public partial class DbproductContext : DbContext
{
    public DbproductContext()
    {
    }

    public DbproductContext(DbContextOptions<DbproductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ProductsInf> ProductsInfs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
#warning

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory)
                .ValueGeneratedNever()
                .HasColumnName("idCategory");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductsInf>(entity =>
        {
            entity.ToTable("ProductsInf");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.oCategory).WithMany(p => p.ProductsInfs)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_ProductsInf_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
