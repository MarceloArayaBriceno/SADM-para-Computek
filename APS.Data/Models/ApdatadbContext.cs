using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APS.Data.Models
{
    public partial class ApdatadbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApdatadbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApdatadbContext(DbContextOptions<ApdatadbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Aprobacione> Aprobaciones { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<HistorialEquipo> HistorialEquipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, options =>
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5, // Número máximo de reintentos
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo máximo entre reintentos
                        errorNumbersToAdd: null) // Puedes especificar errores específicos a manejar
                );
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la entidad Account
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
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_account_user_id");
            });

            // Configuración para la entidad Aprobacione
            modelBuilder.Entity<Aprobacione>(entity =>
            {
                entity.HasKey(e => e.AprobacionId);
                entity.ToTable("aprobaciones");
                entity.Property(e => e.Criterio)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("criterio");
                entity.Property(e => e.Cumple)
                    .IsRequired()
                    .HasColumnName("cumple");
                entity.Property(e => e.AprobacionId).HasColumnName("AprobacionId");
            });

            // Configuración para la entidad Authorization
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("authorizations");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.Pages).HasColumnName("pages").HasColumnType("nvarchar(max)");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_authorizations_userId");
            });

            // Configuración para la entidad Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);
                entity.ToTable("notifications");
                entity.Property(e => e.NotificationId).HasColumnName("notification_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Message).HasColumnName("message").HasColumnType("text");
                entity.Property(e => e.IsRead).HasColumnName("is_read");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_notifications_user_id");
            });

            // Configuración para la entidad User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("user");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50);
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(255);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(10);
                entity.Property(e => e.IsAuthorized).HasColumnName("isAuthorized");
            });

            // Configuración para la entidad Equipo
            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.EquipoId);
                entity.ToTable("equipos");
                entity.Property(e => e.EquipoId).HasColumnName("equipo_id");
                entity.Property(e => e.Marca).HasColumnName("marca").HasMaxLength(100);
                entity.Property(e => e.Modelo).HasColumnName("modelo").HasMaxLength(100);
                entity.Property(e => e.NombreCliente).HasColumnName("nombre_cliente").HasMaxLength(150);
                entity.Property(e => e.MotivoIngreso).HasColumnName("motivo_ingreso").HasColumnType("text");
                entity.Property(e => e.GarantiaConLocal).HasColumnName("garantia_con_local");
                entity.Property(e => e.ContraseñaEquipo).HasColumnName("contraseña_equipo").HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasColumnType("text");
                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("fecha_ingreso")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            });

            // Configuración para la entidad HistorialEquipo
            modelBuilder.Entity<HistorialEquipo>(entity =>
            {
                entity.HasKey(e => e.HistorialId);
                entity.ToTable("HistorialEquipos");
                entity.Property(e => e.HistorialId).HasColumnName("HistorialId");
                entity.Property(e => e.EquipoId).HasColumnName("EquipoId");
                entity.Property(e => e.DescripcionCambio).HasColumnName("DescripcionCambio").HasColumnType("nvarchar(max)");
                entity.Property(e => e.FechaCambio)
                    .HasColumnName("FechaCambio")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}