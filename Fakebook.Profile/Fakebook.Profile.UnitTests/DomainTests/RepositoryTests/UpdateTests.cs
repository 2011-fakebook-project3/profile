using System;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData;
using Fakebook.Profile.UnitTests.TestData.ProfileTestData;
using Fakebook.Profile.DataAccess;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests.RepositoryTests
{
    public class UpdateTests
    {
        /// <summary>
        /// Test if an valid profile can be updated correctly
        /// </summary>
        /// <param name="user">profile data</param>
        /// <param name="userUpdates">profile that contains updates</param>
        [Theory]
        [ClassData(typeof(Update.Valid))]
        public async Task UpdateProfile_ValidData(DomainProfile user, DomainProfile userUpdates)
        {
            // Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (ProfileDbContext actingContext = new(options))
            {
                actingContext.Database.EnsureCreated();
                ProfileRepository repo = new(actingContext);

                // Create the user data
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                await repo.UpdateProfileAsync(user.Email, userUpdates);
                var alteredUser = await repo.GetProfileAsync(user.Email);
                Assert.NotEqual(user.FirstName, alteredUser.FirstName);
                Assert.NotEqual(user.LastName, alteredUser.LastName);
                Assert.NotEqual(user.Status, alteredUser.Status);
            }
        }

        /// <summary>
        /// Test if a profile with an invalid phone number can be updated
        /// </summary>
        /// <param name="user">profile data</param>
        /// <param name="userUpdates">profile that contains updates</param>
        [Theory]
        [ClassData(typeof(Update.InvalidPhone))]
        public async Task UpdateProfile_InvalidPhone(DomainProfile user, DomainProfile userUpdate)
        {
            // Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (ProfileDbContext actingContext = new(options))
            {
                actingContext.Database.EnsureCreated();
                ProfileRepository repo = new(actingContext);

                // Create the user data
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                // Create the entity profile with an incomplete user profile
                // set it to a phone number that violates the regex (from DomainProfile PhoneNumber)
                Assert.Throws<ArgumentException>(() => userUpdate.PhoneNumber = GenerateRandom.String());
                // phone number is not set, but it is not required, is still valid
                await repo.UpdateProfileAsync(user.Email, userUpdate);

                // used to be Equal
                var userActual = await repo.GetProfileAsync(user.Email);
                Assert.NotEqual(user.ProfilePictureUrl, userActual.ProfilePictureUrl);
                Assert.NotEqual(user.FirstName, userActual.FirstName);
                Assert.NotEqual(user.LastName, userActual.LastName);
                Assert.NotEqual(user.PhoneNumber, userActual.PhoneNumber);
                Assert.NotEqual(user.BirthDate, userActual.BirthDate);
                Assert.NotEqual(user.Status, userActual.Status);
            }
        }
    }
}
