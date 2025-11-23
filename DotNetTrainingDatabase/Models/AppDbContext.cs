using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingDatabase.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogTable> BlogTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connection = "Data Source=DESKTOP-UUEE2ME\\SA;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogTable>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("blogTable");

            entity.Property(e => e.BlogId).HasColumnName("Blog_id");
            entity.Property(e => e.BlogAuthor)
                .HasMaxLength(50)
                .HasColumnName("Blog_author");
            entity.Property(e => e.BlogContext).HasColumnName("Blog_context");
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(50)
                .HasColumnName("Blog_title");
            entity.Property(e => e.DeleteFlag).HasColumnName("Delete_flag");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
