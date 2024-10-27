using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Chubb.Prueba.Entities.Entities;

public partial class ChubbDbContext : DbContext
{
    public ChubbDbContext()
    {
    }

    public ChubbDbContext(DbContextOptions<ChubbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asegurado> Asegurados { get; set; }

    public virtual DbSet<AseguradoSeguro> AseguradoSeguros { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Seguro> Seguros { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asegurado>(entity =>
        {
            entity.HasKey(e => e.IdAsegurado);

            entity.ToTable("ASEGURADO");

            entity.Property(e => e.IdAsegurado).HasColumnName("idAsegurado");
            entity.Property(e => e.ApellidoAsegurado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoAsegurado");
            entity.Property(e => e.CedulaAsegurado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedulaAsegurado");
            entity.Property(e => e.EdadAsegurado).HasColumnName("edadAsegurado");
            entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.NombreAsegurado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreAsegurado");
            entity.Property(e => e.TelefonoAsegurado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefonoAsegurado");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Asegurados)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_ASEGURADO_ESTADO");
        });

        modelBuilder.Entity<AseguradoSeguro>(entity =>
        {
            entity.HasKey(e => e.IdAseguradoSeguro);

            entity.ToTable("ASEGURADO_SEGURO");

            entity.Property(e => e.IdAseguradoSeguro).HasColumnName("idAseguradoSeguro");
            entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro");
            entity.Property(e => e.IdAsegurado).HasColumnName("idAsegurado");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.IdSeguro).HasColumnName("idSeguro");

            entity.HasOne(d => d.IdAseguradoNavigation).WithMany(p => p.AseguradoSeguros)
                .HasForeignKey(d => d.IdAsegurado)
                .HasConstraintName("FK_SEGURO_ASEGURADO");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.AseguradoSeguros)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_ASEGURADO_SEGURO_ESTADO");

            entity.HasOne(d => d.IdSeguroNavigation).WithMany(p => p.AseguradoSeguros)
                .HasForeignKey(d => d.IdSeguro)
                .HasConstraintName("FK_ASEGURADO_SEGURO");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("ESTADO");

            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Seguro>(entity =>
        {
            entity.HasKey(e => e.IdSeguro);

            entity.ToTable("SEGURO");

            entity.Property(e => e.IdSeguro).HasColumnName("idSeguro");
            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("codigoSeguro");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.NombreSeguro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombreSeguro");
            entity.Property(e => e.Prima).HasColumnName("prima");
            entity.Property(e => e.SumaAsegurada).HasColumnName("sumaAsegurada");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Seguros)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_SEGURO_ESTADO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
