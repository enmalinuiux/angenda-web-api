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

        public virtual DbSet<SocialMedia> SocialMedia { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserContacto> UserContacto { get; set; }
        public virtual DbSet<UserPhone> UserPhone { get; set; }
        public virtual DbSet<UserSm> UserSm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-9IB9T0EQ\\MYSERVER;Database=agenda;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.ToTable("social_media");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressCity)
                    .IsRequired()
                    .HasColumnName("address_city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressCountry)
                    .IsRequired()
                    .HasColumnName("address_country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressStreet)
                    .IsRequired()
                    .HasColumnName("address_street")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Birth)
                    .HasColumnName("birth")
                    .HasColumnType("date");

                entity.Property(e => e.Business)
                    .HasColumnName("business")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.HasOne(d => d.BusinessNavigation)
                    .WithMany(p => p.InverseBusinessNavigation)
                    .HasForeignKey(d => d.Business)
                    .HasConstraintName("FK__user__business__37A5467C");
            });

            modelBuilder.Entity<UserContacto>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.UserId })
                    .HasName("PK__user_con__E9D599F6016EB745");

                entity.ToTable("user_contacto");

                entity.Property(e => e.ContactId)
                    .HasColumnName("contact_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduledDate)
                    .HasColumnName("scheduled_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.UserContactoContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_cont__conta__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContactoUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_cont__user___412EB0B6");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.HasKey(e => e.Phone)
                    .HasName("PK__user_pho__B43B145E7EA5330A");

                entity.ToTable("user_phone");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPhone)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_phon__user___45F365D3");
            });

            modelBuilder.Entity<UserSm>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SmId })
                    .HasName("PK__user_sm__A9D329A2ED0BAFE7");

                entity.ToTable("user_sm");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SmId)
                    .HasColumnName("sm_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sm)
                    .WithMany(p => p.UserSm)
                    .HasForeignKey(d => d.SmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_sm__sm_id__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSm)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_sm__user_id__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
