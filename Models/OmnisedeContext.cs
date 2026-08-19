using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OmniSedeBackend.Models;

public partial class OmnisedeContext : DbContext
{
    public OmnisedeContext()
    {
    }

    public OmnisedeContext(DbContextOptions<OmnisedeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aziende> Aziendes { get; set; }

    public virtual DbSet<Documenti> Documentis { get; set; }

    public virtual DbSet<Ruoli> Ruolis { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Utenti> Utentis { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aziende>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aziende__3213E83F9A27B324");

            entity.ToTable("Aziende");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartitaIva)
                .HasMaxLength(255)
                .HasColumnName("partita_iva");
            entity.Property(e => e.RagioneSociale)
                .HasMaxLength(255)
                .HasColumnName("ragione_sociale");
        });

        modelBuilder.Entity<Documenti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3213E83FC998B287");

            entity.ToTable("Documenti");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovatoDa).HasColumnName("approvato_da");
            entity.Property(e => e.CaricatoDa).HasColumnName("caricato_da");
            entity.Property(e => e.DataApprovazione)
                .HasColumnType("datetime")
                .HasColumnName("data_approvazione");
            entity.Property(e => e.DataCaricamento)
                .HasColumnType("datetime")
                .HasColumnName("data_caricamento");
            entity.Property(e => e.DataModifica)
                .HasColumnType("datetime")
                .HasColumnName("data_modifica");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(255)
                .HasColumnName("descrizione");
            entity.Property(e => e.NomeFile)
                .HasMaxLength(255)
                .HasColumnName("nome_file");
            entity.Property(e => e.SedeId).HasColumnName("sede_id");

            entity.HasOne(d => d.ApprovatoDaNavigation).WithMany(p => p.DocumentiApprovatoDaNavigations)
                .HasForeignKey(d => d.ApprovatoDa)
                .HasConstraintName("FK__Documenti__appro__440B1D61");

            entity.HasOne(d => d.CaricatoDaNavigation).WithMany(p => p.DocumentiCaricatoDaNavigations)
                .HasForeignKey(d => d.CaricatoDa)
                .HasConstraintName("FK__Documenti__caric__4316F928");

            entity.HasOne(d => d.Sede).WithMany(p => p.Documentis)
                .HasForeignKey(d => d.SedeId)
                .HasConstraintName("FK__Documenti__sede___4222D4EF");
        });

        modelBuilder.Entity<Ruoli>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ruoli__3213E83F50ABF3C9");

            entity.ToTable("Ruoli");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sede__3213E83F702CAD61");

            entity.ToTable("Sede");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AziendaId).HasColumnName("azienda_id");
            entity.Property(e => e.Indirizzo)
                .HasMaxLength(255)
                .HasColumnName("indirizzo");

            entity.HasOne(d => d.Azienda).WithMany(p => p.Sedes)
                .HasForeignKey(d => d.AziendaId)
                .HasConstraintName("FK__Sede__azienda_id__412EB0B6");
        });

        modelBuilder.Entity<Utenti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utenti__3213E83FF062D1BF");

            entity.ToTable("Utenti");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cognome)
                .HasMaxLength(255)
                .HasColumnName("cognome");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RuoloId).HasColumnName("ruolo_id");
            entity.Property(e => e.SedeId).HasColumnName("sede_id");

            entity.HasOne(d => d.Ruolo).WithMany(p => p.Utentis)
                .HasForeignKey(d => d.RuoloId)
                .HasConstraintName("FK__Utenti__ruolo_id__3F466844");

            entity.HasOne(d => d.Sede).WithMany(p => p.Utentis)
                .HasForeignKey(d => d.SedeId)
                .HasConstraintName("FK__Utenti__sede_id__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
