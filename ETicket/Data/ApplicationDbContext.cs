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
        public virtual DbSet<PerformerCategories> PerformerCategories { get; set; }
        public virtual DbSet<Performers> Performers { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Tours> Tours { get; set; }
        public virtual DbSet<PhotoEvents> PhotoEvents { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
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

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_ToPlaces");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Events_ToTours");
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

            modelBuilder.Entity<PhotoEvents>(entity =>
            {
                entity.HasKey(e => e.PhotoEventId);

                entity.Property(e => e.PhotoEventDefault)
                    .IsRequired();

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.PhotoEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhotoEvents_ToEvents");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.PhotoEvents)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhotoEvents_ToPhotos");
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasMaxLength(250);
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

                entity.Property(e => e.PurchaseTicketDate).HasColumnType("datetime");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToDeliveries");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToTickets");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_ToApplicationUsers");
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