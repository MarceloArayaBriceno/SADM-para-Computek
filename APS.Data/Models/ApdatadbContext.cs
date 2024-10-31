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

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<HistorialEquipo> HistorialEquipos { get; set; }

   public virtual DbSet<Producto> Producto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-WAYNER;Database=APDatadb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CDB9FFDFD3");

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
            entity.HasKey(e => e.AprobacionId).HasName("PK__aprobaci__841444415F806074");

            entity.ToTable("aprobaciones");

            entity.Property(e => e.AprobacionId).HasColumnName("aprobacion_id");
            entity.Property(e => e.Criterio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criterio");
            entity.Property(e => e.Cumple).HasColumnName("cumple");
            entity.Property(e => e.EquipoId).HasColumnName("equipo_id");

            entity.HasOne(d => d.Equipo).WithMany(p => p.Aprobaciones)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aprobacio__equip__6D0D32F4");
        });

        modelBuilder.Entity<Authorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__authoriz__3213E83F41813066");

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
            entity.HasKey(e => e.EquipoId).HasName("PK__equipos__50EAD2BF9783599C");

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

            entity.HasKey(e => e.EquipoId);

            entity.HasMany(e => e.HistorialEquipos)
                  .WithOne(h => h.Equipo)
                  .HasForeignKey(h => h.EquipoId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842FAF406580");

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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370F4435B8CB");

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

        modelBuilder.Entity<HistorialEquipo>(entity =>
        {
            entity.HasKey(e => e.HistorialId);

            entity.Property(e => e.DescripcionCambio)
                  .IsRequired()
                  .HasMaxLength(int.MaxValue);

            entity.Property(e => e.FechaCambio)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Equipo)
                  .WithMany(p => p.HistorialEquipos)
                  .HasForeignKey(d => d.EquipoId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
