using System;
using System.Collections.Generic;
using DubaiSession2.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DubaiSession2.API.Context
{
    public partial class ComXContext : DbContext
    {
        public ComXContext()
        {
        }

        public ComXContext(DbContextOptions<ComXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Attraction> Attractions { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<CancellationPolicy> CancellationPolicies { get; set; } = null!;
        public virtual DbSet<CancellationRefundFee> CancellationRefundFees { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<DimDate> DimDates { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemAmenity> ItemAmenities { get; set; } = null!;
        public virtual DbSet<ItemAttraction> ItemAttractions { get; set; } = null!;
        public virtual DbSet<ItemPicture> ItemPictures { get; set; } = null!;
        public virtual DbSet<ItemPrice> ItemPrices { get; set; } = null!;
        public virtual DbSet<ItemScore> ItemScores { get; set; } = null!;
        public virtual DbSet<ItemType> ItemTypes { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Database=ComX;User Id=sa;password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.IconName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attractions_Areas");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Bookings_Coupons");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Bookings_Transactions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Users");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.IsRefund).HasColumnName("isRefund");

                entity.Property(e => e.ItemPriceId).HasColumnName("ItemPriceID");

                entity.Property(e => e.RefundCancellationPoliciyId).HasColumnName("RefundCancellationPoliciyID");

                entity.Property(e => e.RefundDate).HasColumnType("date");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingDetails_Bookings");

                entity.HasOne(d => d.ItemPrice)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ItemPriceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingDetails_PlacePrices");
            });

            modelBuilder.Entity<CancellationPolicy>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Commission).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CancellationRefundFee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CancellationPolicyId).HasColumnName("CancellationPolicyID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.PenaltyPercentage).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.CancellationPolicy)
                    .WithMany(p => p.CancellationRefundFees)
                    .HasForeignKey(d => d.CancellationPolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CancellationRefundFees_CancellationPolicies");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CouponCode).HasMaxLength(50);

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.MaximimDiscountAmount).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<DimDate>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DayName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsHoliday).HasColumnName("isHoliday");

                entity.Property(e => e.MonthName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApproximateAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.ExactAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.HostRules).HasMaxLength(2000);

                entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Areas");

                entity.HasOne(d => d.ItemType)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Places_PlaceTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Places_Users");
            });

            modelBuilder.Entity<ItemAmenity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmenityId).HasColumnName("AmenityID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Amenity)
                    .WithMany(p => p.ItemAmenities)
                    .HasForeignKey(d => d.AmenityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceAmenities_Amenities");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemAmenities)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceAmenities_Places");
            });

            modelBuilder.Entity<ItemAttraction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttractionId).HasColumnName("AttractionID");

                entity.Property(e => e.Distance).HasColumnType("decimal(5, 1)");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.ItemAttractions)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceSightseeingLocations_SightseeingLocations");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemAttractions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceSightseeingLocations_Places");
            });

            modelBuilder.Entity<ItemPicture>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.PictureFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemPictures)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlacePictures_Places");
            });

            modelBuilder.Entity<ItemPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CancellationPolicyId).HasColumnName("CancellationPolicyID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CancellationPolicy)
                    .WithMany(p => p.ItemPrices)
                    .HasForeignKey(d => d.CancellationPolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlacePrices_CancellationPolicies");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemPrices)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlacePrices_Places");
            });

            modelBuilder.Entity<ItemScore>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ScoreId).HasColumnName("ScoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemScores)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemScores_Items");

                entity.HasOne(d => d.Score)
                    .WithMany(p => p.ItemScores)
                    .HasForeignKey(d => d.ScoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemScores_Scores");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ItemScores)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemScores_Users");
            });

            modelBuilder.Entity<ItemType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GatewayReturnId)
                    .HasMaxLength(50)
                    .HasColumnName("GatewayReturnID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_TransactionTypes");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserTypes");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
