using System;
using System.Collections.Generic;
using GaleriaOnline.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaOnline.WebApi.DbContextImagem;

public partial class GaleriaOnlineDbContext : DbContext
{
    public GaleriaOnlineDbContext()
    {
    }

    public GaleriaOnlineDbContext(DbContextOptions<GaleriaOnlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Imagem> Imagens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NOTE11-S28\\SQLEXPRESS;Database=GaleriaOnlineDB;user ID = sa; Pwd= Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Imagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Imagens__3214EC0738DF8966");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
