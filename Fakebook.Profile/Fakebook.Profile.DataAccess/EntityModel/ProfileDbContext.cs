using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    /// <summary>
    /// DbContext used to define the profile table structure.
    /// </summary>
    public class ProfileDbContext : DbContext
    {
        /// <summary>
        /// Constructs an isntance of this context.
        /// </summary>
        /// <param name="options">The options that the context will be constructed with.</param>
        public ProfileDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        /// <summary>
        /// The table with all the user profiles.
        /// </summary>
        public DbSet<EntityProfile> EntityProfiles { get; set; }

        public DbSet<Follow> Follows { get; set; }

        /// <summary>
        /// Override for generating the model tables.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder object used with building the tables, their properties, and contraints.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityProfile>(entity =>
            {
                entity.ToTable(name: "Profile", schema: "Fakebook");

                entity.Property(e => e.Id)
                    .IsRequired(true);

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

            modelBuilder.Entity<Follow>()
                .HasKey(e => new { e.UserId, e.FollowingId });

            modelBuilder.Entity<Follow>()
                .HasOne(e => e.User)
                .WithMany(e => e.Following)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Follow>()
                .HasOne(e => e.Following)
                .WithMany(e => e.Followers)
                .HasForeignKey(e => e.FollowingId);

            modelBuilder.Entity<EntityProfile>()
                .HasData(new[]
                {
                    new EntityProfile
                    {
                        Id = 1,
                        Email = "john.werner@revature.net",
                        FirstName = "John",
                        LastName = "Werner",
                        BirthDate = new DateTime(1994, 6, 30),
                        ProfilePictureUrl = new Uri("https://images.unsplash.com/photo-1489533119213-66a5cd877091"),
                        Status = "deployed my app feeling good about today's presentation"
                    },
                    new EntityProfile
                    {
                        Id = 2,
                        Email = "testaccount@gmail.com",
                        FirstName = "Jay",
                        LastName = "Shin",
                        BirthDate = new DateTime(1994, 9, 17),
                        ProfilePictureUrl = new Uri("https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800"),
                        Status = null
                    }
                });

            modelBuilder.Entity<Follow>()
                .HasData(new[]
                {
                    new Follow
                    {
                        UserId = 1,
                        FollowingId = 2
                    },
                    new Follow
                    {
                        UserId = 2,
                        FollowingId = 1
                    }
                });
        }
    }
}
