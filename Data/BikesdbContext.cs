using System;
using System.Collections.Generic;
using Crudtoso_api.Model;
using Microsoft.EntityFrameworkCore;

namespace Crudtoso_api.Data;

public partial class BikesdbContext : DbContext
{
    public BikesdbContext()
    {
    }

    public BikesdbContext(DbContextOptions<BikesdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BikeDb> BikeDbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=crudtososerverdb.database.windows.net;Initial Catalog=bikesdb;User ID=projectadmin;Password=Something_!?; Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BikeDb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BIKE_DB");

            entity.Property(e => e.Category)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DateAdded).HasColumnType("date");
            entity.Property(e => e.Price)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(21)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(24)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
