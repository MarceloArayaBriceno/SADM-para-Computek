﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APS.Data.Models;

public partial class DbComputekContext : DbContext
{
    public DbComputekContext()
    {
    }

    public DbComputekContext(DbContextOptions<DbComputekContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Aprobacione> Aprobaciones { get; set; }

    public virtual DbSet<Authorization> Authorizations { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<HistorialEquipo> HistorialEquipos { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Prueba> Pruebas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=dbcomputek.database.windows.net;Database=dbComputek;User Id=adminsqlCK;Password=rZ61Y4>fOfi;Encrypt=True;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CD8381E4DF");

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

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_account_user_id");
        });

        modelBuilder.Entity<Aprobacione>(entity =>
        {
            entity.HasKey(e => e.AprobacionId).HasName("PK__aprobaci__29259AFD3877699D");

            entity.ToTable("aprobaciones");

            entity.Property(e => e.Criterio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criterio");
            entity.Property(e => e.Cumple).HasColumnName("cumple");
        });

        modelBuilder.Entity<Authorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__authoriz__3213E83F07333FB5");

            entity.ToTable("authorizations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Authorizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_authorizations_userId");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK_Equipos");

            entity.ToTable("equipos");

            entity.Property(e => e.EquipoId).HasColumnName("equipo_id");
            entity.Property(e => e.ContraseñaEquipo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña_equipo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
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
            entity.HasKey(e => e.HistorialId);

            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842FE38D5DF7");

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

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_notifications_user_id");
        });

        modelBuilder.Entity<Prueba>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRUEBAS");

            entity.Property(e => e.Datos)
                .ValueGeneratedOnAdd()
                .HasColumnName("DATOS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370F79898370");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ_user_email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_user_username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsAuthorized).HasColumnName("isAuthorized");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
