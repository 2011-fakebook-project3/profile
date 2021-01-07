using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


namespace Fakebook.Profile.DataAccess.EntityModel
{
    /// <summary>
    /// DbContext used to define the profile table structure
    /// </summary>
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        public DbSet<EntityProfile> StorageProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityProfile>(entity =>
            {
                // Table | Schema
                entity.ToTable("Profile", "Fakebook");

                entity.Property(e => e.Email)
                    .IsRequired(true);

                entity.Property(e => e.ProfilePictureUrl)
                    .IsRequired(false);

                entity.Property(e => e.FirstName)
                    .IsRequired(true);

                entity.Property(e => e.LastName)
                    .IsRequired(true);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired(false);

                entity.Property(e => e.BirthDate)
                    .IsRequired(true);

                entity.Property(e => e.Status)
                    .IsRequired(false);
            });

            // 
            modelBuilder.Entity<EntityFollow>(entity =>
            {
                entity.ToTable("Follow", "Fakebook");

                entity.HasKey(e => new { e.FollowerEmail, e.FolloweeEmail })
                      .HasName("Pk_FollowEntity");

                entity.HasOne(e => e.Followee)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(f => f.FolloweeEmail)
                    .HasConstraintName("Fk_Follow_Followee")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Follower)
                    .WithMany(f => f.Followees)
                    .HasForeignKey(f => f.FollowerEmail)
                    .HasConstraintName("Fk_Follow_Follower")
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
