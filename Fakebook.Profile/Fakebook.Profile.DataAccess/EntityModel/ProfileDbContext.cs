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
        /// <param name="options">The optinos that the context will be constructed with.</param>
        public ProfileDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        /// <summary>
        /// The table with all the user profiles.
        /// </summary>
        public DbSet<EntityProfile> EntityProfiles { get; set; }

        /// <summary>
        /// Override for generatingthe model tables.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder object used with building the tables, their properties, and contraints.</param>
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
        }
    }
}
