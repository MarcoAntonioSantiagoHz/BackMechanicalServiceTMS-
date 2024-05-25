using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tokesServiceWebApi.Models;

public partial class TokesBdContext : DbContext
{
    public TokesBdContext()
    {
    }

    public TokesBdContext(DbContextOptions<TokesBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=LAPTOP-PJ6IFQK6; Database=tokes_BD; User Id=sa; Password=marco; TrustServerCertificate=True;");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Clients__A6A610D4A283484E");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.AddressClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address_client");
            entity.Property(e => e.EmailClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_client");
            entity.Property(e => e.LastModification)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_modification");
            entity.Property(e => e.NameClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_client");
            entity.Property(e => e.PhoneClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_client");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__B7C92638EEE438A6");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053485BA4576").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleUser)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.IdVehicle).HasName("PK__Vehicles__B5E24754F167FCC4");

            entity.Property(e => e.IdVehicle).HasColumnName("idVehicle");
            entity.Property(e => e.CarType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("car_type");
            entity.Property(e => e.DateEntry)
                .HasColumnType("datetime")
                .HasColumnName("date_entry");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.LastModification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_modification");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("licensePlate");
            entity.Property(e => e.Mark)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mark");
            entity.Property(e => e.MechanicalRevisionBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mechanical_revision_by");
            entity.Property(e => e.Observations)
                .HasColumnType("text")
                .HasColumnName("observations");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TechnicalComments)
                .HasColumnType("text")
                .HasColumnName("technical_comments");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Vehicles__id_cli__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
