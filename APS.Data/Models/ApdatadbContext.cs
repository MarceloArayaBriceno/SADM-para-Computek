using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APS.Data.Models;

public partial class ApdatadbContext : DbContext
{
    public ApdatadbContext()
    {
    }

    public ApdatadbContext(DbContextOptions<ApdatadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Aprobacione> Aprobaciones { get; set; }

    public virtual DbSet<Authorization> Authorizations { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<HistorialEquipo> HistorialEquipos { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HQR7UH5;Database=APDatadb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CD35D28E32");

            entity.ToTable("account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_type");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Aprobacione>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aprobaciones");

            entity.Property(e => e.Criterio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criterio");
            entity.Property(e => e.Cumple).HasColumnName("cumple");
        });

        modelBuilder.Entity<Authorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__authoriz__3213E83F9E773328");

            entity.ToTable("authorizations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("equipos");

            entity.Property(e => e.ContraseñaEquipo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña_equipo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.EquipoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("equipo_id");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.GarantiaConLocal).HasColumnName("garantia_con_local");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modelo");
            entity.Property(e => e.MotivoIngreso)
                .HasColumnType("text")
                .HasColumnName("motivo_ingreso");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<HistorialEquipo>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HistorialId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842FA8DDB4C5");

            entity.ToTable("notifications");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
