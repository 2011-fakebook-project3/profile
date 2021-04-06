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

                entity.HasMany(e => e.Following).WithOne();
            });

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
        }
    }
}
