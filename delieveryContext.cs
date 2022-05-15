using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DelieveryWebApplication
{
    public partial class delieveryContext : DbContext
    {
        public delieveryContext()
        {
        }

        public delieveryContext(DbContextOptions<delieveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Courier> Couriers { get; set; } = null!;
        public virtual DbSet<Delievery> Delieveries { get; set; } = null!;
        public virtual DbSet<DelieveryType> DelieveryTypes { get; set; } = null!;
        public virtual DbSet<DishType> DishTypes { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EFQ1BJ6\\SQLEXPRESS; Database=delievery; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Flat).HasColumnName("flat");

                entity.Property(e => e.House).HasColumnName("house");

                entity.Property(e => e.Name)
                    .HasColumnType("ntext")
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("ntext")
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Street)
                    .HasColumnType("ntext")
                    .HasColumnName("street");

                entity.Property(e => e.Surname)
                    .HasColumnType("ntext")
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.ToTable("Courier");

                entity.Property(e => e.CourierId).HasColumnName("courierID");

                entity.Property(e => e.DelieveryType).HasColumnName("delieveryType");

                entity.Property(e => e.Name)
                    .HasColumnType("ntext")
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("ntext")
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Surname)
                    .HasColumnType("ntext")
                    .HasColumnName("surname");

                entity.HasOne(d => d.DelieveryTypeNavigation)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.DelieveryType)
                    .HasConstraintName("FK_Courier_DelieveryType");
            });

            modelBuilder.Entity<Delievery>(entity =>
            {
                entity.ToTable("Delievery");

                entity.Property(e => e.DelieveryId).HasColumnName("delieveryID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DishId).HasColumnName("dishID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Delieveries)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delievery_Menu");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Delieveries)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delievery_Order");
            });

            modelBuilder.Entity<DelieveryType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("DelieveryType");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Name)
                    .HasColumnType("ntext")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DishType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("DishType");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Name)
                    .HasColumnType("ntext")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.DishId);

                entity.ToTable("Menu");

                entity.Property(e => e.DishId).HasColumnName("dishID");

                entity.Property(e => e.Name)
                    .HasColumnType("ntext")
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_DishType");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.Property(e => e.CourierId).HasColumnName("courierID");

                entity.Property(e => e.DelieveryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("delieveryTime");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Client");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CourierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Courier");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
