using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace agenda_web_api.Models
{
    public partial class agendaContext : DbContext
    {
        public agendaContext()
        {
        }

        public agendaContext(DbContextOptions<agendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<SocialMedia> SocialMedia { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserContact> UserContact { get; set; }
        public virtual DbSet<UserPhone> UserPhone { get; set; }
        public virtual DbSet<UserSm> UserSm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.ContactId).IsUnicode(false);

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.NotificationContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__notificat__conta__17036CC0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NotificationUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__notificat__user___160F4887");
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__user__AB6E61648CA4C8C4")
                    .IsUnique();

                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.AddressCity).IsUnicode(false);

                entity.Property(e => e.AddressCountry).IsUnicode(false);

                entity.Property(e => e.AddressStreet).IsUnicode(false);

                entity.Property(e => e.Business).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Pass).IsUnicode(false);

                entity.HasOne(d => d.BusinessNavigation)
                    .WithMany(p => p.InverseBusinessNavigation)
                    .HasForeignKey(d => d.Business)
                    .HasConstraintName("FK__user__business__36B12243");
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.UserId })
                    .HasName("PK__user_con__E9D599F669E33486");

                entity.Property(e => e.ContactId).IsUnicode(false);

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.Nickname).IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.UserContactContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_cont__conta__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContactUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_cont__user___403A8C7D");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.HasKey(e => e.Phone)
                    .HasName("PK__user_pho__B43B145EDCA46D02");

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPhone)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_phon__user___44FF419A");
            });

            modelBuilder.Entity<UserSm>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SmId })
                    .HasName("PK__user_sm__A9D329A23A6C7663");

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.SmId).IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);

                entity.HasOne(d => d.Sm)
                    .WithMany(p => p.UserSm)
                    .HasForeignKey(d => d.SmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_sm__sm_id__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSm)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_sm__user_id__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
