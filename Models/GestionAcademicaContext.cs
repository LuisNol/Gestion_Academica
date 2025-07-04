using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial3_sumaran.Models;

public partial class GestionAcademicaContext : DbContext
{
    public GestionAcademicaContext()
    {
    }

    public GestionAcademicaContext(DbContextOptions<GestionAcademicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC0771374924");

            entity.ToTable("Curso");

            entity.Property(e => e.Ciclo).HasMaxLength(50);
            entity.Property(e => e.Curso1)
                .HasMaxLength(150)
                .HasColumnName("Curso");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdDocente)
                .HasConstraintName("FK__Curso__IdDocente__398D8EEE");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Docente__3214EC07C04B6F16");

            entity.ToTable("Docente");

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Profesion).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
