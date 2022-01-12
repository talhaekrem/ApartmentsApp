using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApartmentsApp.DB.Entities;

#nullable disable

namespace ApartmentsApp.DB.Entities.ApartmentsAppDbContext
{
    public partial class ApartmentsAppContext : DbContext
    {
        //Scaffold-DbContext "Server=TALHAEKREM;Database=ApartmentsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -ContextDir Entities/ApartmentsAppDbContext -Context ApartmentsAppContext -Project ApartmentsApp.DB -StartupProject ApartmentsApp.DB -NoPluralize -Force
        public ApartmentsAppContext()
        {
        }

        public ApartmentsAppContext(DbContextOptions<ApartmentsAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<ElectricBill> ElectricBill { get; set; }
        public virtual DbSet<GasBill> GasBill { get; set; }
        public virtual DbSet<HomeBill> HomeBill { get; set; }
        public virtual DbSet<Homes> Homes { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WaterBill> WaterBill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TALHAEKREM;Database=ApartmentsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Bills>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HomeId).HasColumnName("homeId");

                entity.HasOne(d => d.Home)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.HomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bills_Homes");
            });

            modelBuilder.Entity<ElectricBill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("billDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BillsId).HasColumnName("billsId");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Bills)
                    .WithMany(p => p.ElectricBill)
                    .HasForeignKey(d => d.BillsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectricBill_Bills");
            });

            modelBuilder.Entity<GasBill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("billDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BillsId).HasColumnName("billsId");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Bills)
                    .WithMany(p => p.GasBill)
                    .HasForeignKey(d => d.BillsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GasBill_Bills");
            });

            modelBuilder.Entity<HomeBill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("billDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BillsId).HasColumnName("billsId");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Bills)
                    .WithMany(p => p.HomeBill)
                    .HasForeignKey(d => d.BillsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeBill_Bills");
            });

            modelBuilder.Entity<Homes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("blockName");

                entity.Property(e => e.DoorNumber).HasColumnName("doorNumber");

                entity.Property(e => e.FloorNumber).HasColumnName("floorNumber");

                entity.Property(e => e.HomeType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("homeType");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsOwned).HasColumnName("isOwned");

                entity.Property(e => e.OwnerId).HasColumnName("ownerId");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Homes_Users");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsReceiverReaded).HasColumnName("isReceiverReaded");

                entity.Property(e => e.IsSenderReaded).HasColumnName("isSenderReaded");

                entity.Property(e => e.MessageTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("messageTitle");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

                entity.Property(e => e.ReceiverMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("receiverMessage");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.SenderMessage)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("senderMessage");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessagesReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Users1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessagesSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email, "UK_Email")
                    .IsUnique();

                entity.HasIndex(e => e.TcNo, "UK_tcNo")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarPlate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("carPlate");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(125)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("surName");

                entity.Property(e => e.TcNo)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("tcNo");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<WaterBill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("billDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BillsId).HasColumnName("billsId");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Bills)
                    .WithMany(p => p.WaterBill)
                    .HasForeignKey(d => d.BillsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaterBill_Bills");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
