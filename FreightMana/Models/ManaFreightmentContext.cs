using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Models;

public partial class ManaFreightmentContext : DbContext
{
    public ManaFreightmentContext()
    {
    }

    public ManaFreightmentContext(DbContextOptions<ManaFreightmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CusAccount> CusAccounts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Receiver> Receivers { get; set; }

    public virtual DbSet<Sender> Senders { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseAccount> WarehouseAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

        //=> optionsBuilder.UseSqlServer("Data Source=Mew\\SQLEXPRESS;Initial Catalog=manaFreightment;User ID=sa;Password=TheHieuDoan;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

      // => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HUCDGBQ\\SQLEXPRESS;Initial Catalog=manaFreightment;User ID=sa;Password=tt13112003;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    => optionsBuilder.UseSqlServer("Data Source=3043-INS5510;Initial Catalog=manaFreightment;User ID=sa;Password=7777777;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CusAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_orders");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.Cod).HasColumnName("cod");
            entity.Property(e => e.CusId).HasColumnName("cusID");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.NumberOfProduct).HasColumnName("numberOfProduct");
            entity.Property(e => e.Product).HasColumnName("product");
            entity.Property(e => e.ReceiverId).HasColumnName("receiverID");
            entity.Property(e => e.RecordAt)
                .HasColumnType("datetime")
                .HasColumnName("recordAt");
            entity.Property(e => e.SenderId).HasColumnName("senderID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TransportFee).HasColumnName("transportFee");
            entity.Property(e => e.TransportId).HasColumnName("transportID");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouseID");

            entity.HasOne(d => d.Cus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK_orders_accounts");

            entity.HasOne(d => d.Receiver).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_receivers");

            entity.HasOne(d => d.Sender).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_senders");

            entity.HasOne(d => d.Transport).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_transports1");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Orders)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_warehouses");
        });

        modelBuilder.Entity<Receiver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_receivers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Sender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_senders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_shifts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.TimeEnd).HasColumnName("timeEnd");
            entity.Property(e => e.TimeStart).HasColumnName("timeStart");

            entity.HasOne(d => d.Employee).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_shifts_employees");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_employees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouseID");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Staff)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employees_warehouses");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_transports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_warehouses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Hotline)
                .HasMaxLength(50)
                .HasColumnName("hotline");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<WarehouseAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_warehouseAccounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouseID");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseAccounts)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_warehouseAccounts_warehouses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
