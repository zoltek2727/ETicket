using System;
using System.Collections.Generic;
using System.Text;
using ETicket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Deliveries> Deliveries { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<HotelReservations> HotelReservations { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<PerformerCategories> PerformerCategories { get; set; }
        public virtual DbSet<Performers> Performers { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Reliefs> Reliefs { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Sectors> Sectors { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Tours> Tours { get; set; }
        public virtual DbSet<TransportReservations> TransportReservations { get; set; }
        public virtual DbSet<Transports> Transports { get; set; }
        public new virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_ToCountries");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Deliveries>(entity =>
            {
                entity.HasKey(e => e.DeliveryId);

                entity.Property(e => e.DeliveryType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventDescription).HasMaxLength(500);

                entity.Property(e => e.EventEnd).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EventStart).HasColumnType("datetime");

                entity.HasOne(d => d.HotelReservation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.HotelReservationId)
                    .HasConstraintName("FK_Events_ToHotelReservations");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_ToPlaces");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Events_ToTours");

                entity.HasOne(d => d.TransportReservation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TransportReservationId)
                    .HasConstraintName("FK_Events_ToTransportReservations");
            });

            modelBuilder.Entity<HotelReservations>(entity =>
            {
                entity.HasKey(e => e.HotelReservationId);

                entity.Property(e => e.HotelReservationEnd).HasColumnType("datetime");

                entity.Property(e => e.HotelReservationStart).HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.HotelReservations)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelReservations_ToEvents");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.HotelReservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelReservations_ToRooms");
            });

            modelBuilder.Entity<Hotels>(entity =>
            {
                entity.HasKey(e => e.HotelId);

                entity.Property(e => e.HotelAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HotelDescription).HasMaxLength(500);

                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HotelPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotels_ToCities");
            });

            modelBuilder.Entity<PerformerCategories>(entity =>
            {
                entity.HasKey(e => e.PerformerCategoryId);

                entity.Property(e => e.PerformerCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Performers>(entity =>
            {
                entity.HasKey(e => e.PerformerId);

                entity.Property(e => e.PerformerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PerformerCategory)
                    .WithMany(p => p.Performers)
                    .HasForeignKey(d => d.PerformerCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Performers_ToPerformerCategories");
            });

            modelBuilder.Entity<Places>(entity =>
            {
                entity.HasKey(e => e.PlaceId);

                entity.Property(e => e.PlaceAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PlaceDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Places_ToCities");
            });

            modelBuilder.Entity<Purchases>(entity =>
            {
                entity.HasKey(e => e.PurchaseId);

                entity.Property(e => e.PurchaseSelectedRow).HasMaxLength(10);

                entity.Property(e => e.PurchaseSelectedRowSeat).HasMaxLength(10);

                entity.Property(e => e.PurchaseTicketDate).HasColumnType("datetime");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToDeliveries");

                entity.HasOne(d => d.HotelReservation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.HotelReservationId)
                    .HasConstraintName("FK_Purchases_ToHotelReservations");

                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.ReliefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToReliefs");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToTickets");

                entity.HasOne(d => d.TransportReservation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.TransportReservationId)
                    .HasConstraintName("FK_Purchases_ToTransportReservations");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToApplicationUsers");
            });

            modelBuilder.Entity<Reliefs>(entity =>
            {
                entity.HasKey(e => e.ReliefId);

                entity.Property(e => e.ReliefPercent).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.ReliefType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.Property(e => e.RoomDescription).HasMaxLength(500);

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoomPriceForNight).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_ToHotels");
            });

            modelBuilder.Entity<Sectors>(entity =>
            {
                entity.HasKey(e => e.SectorId);

                entity.Property(e => e.SectorName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Sectors)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Sectors_ToPlaces");
            });

            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.Property(e => e.TicketDescription).HasMaxLength(500);

                entity.Property(e => e.TicketName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TicketPrice).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_ToEvents");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("FK_Tickets_ToSectors");
            });

            modelBuilder.Entity<Tours>(entity =>
            {
                entity.HasKey(e => e.TourId);

                entity.Property(e => e.TourDescription).HasMaxLength(500);

                entity.Property(e => e.TourName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.PerformerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_ToPerformers");
            });

            modelBuilder.Entity<TransportReservations>(entity =>
            {
                entity.HasKey(e => e.TransportReservationId);

                entity.Property(e => e.TransportReservationAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransportReservationEnd).HasColumnType("datetime");

                entity.Property(e => e.TransportReservationStart).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TransportReservations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransportReservations_ToCities");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.TransportReservations)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransportReservations_ToEvents");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.TransportReservations)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransportReservations_ToTransports");
            });

            modelBuilder.Entity<Transports>(entity =>
            {
                entity.HasKey(e => e.TransportId);

                entity.Property(e => e.TransportDescription).HasMaxLength(50);

                entity.Property(e => e.TransportName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.UserAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserBirthdate).HasColumnType("date");

                entity.Property(e => e.UserCity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserCreated).HasColumnType("datetime");

                entity.Property(e => e.UserFirstname).HasMaxLength(50);

                entity.Property(e => e.UserLastLog).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhone).HasMaxLength(20);

                entity.Property(e => e.UserSex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UserSurname).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_ToCountries");
            });
        }
    }
}