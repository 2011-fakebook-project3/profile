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

    public class CreateTests
    {
        /// <summary>
        /// Check if an valid user gets created and stored in the database.
        /// </summary>
        /// <param name="user">valid data for the domain profile.</param>
        [Theory]
        [ClassData(typeof(Create.Valid))]
        public async Task CreateUser_ValidData(DomainProfile user)
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

                // Create the entity profile with a complete user profile
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);
                var userInDB = await repo.GetProfileAsync(user.Email);
                Assert.NotNull(userInDB);
                Assert.Equal(user.Email, userInDB.Email);
                Assert.Equal(user.FirstName, userInDB.FirstName);
                Assert.Equal(user.LastName, userInDB.LastName);
                Assert.Equal(user.BirthDate, userInDB.BirthDate);
            }
        }

        // Invalid split into 3 parts
        /// <summary>
        /// Test that creating a user with an invalid phone number throws an execption.
        /// </summary>
        /// <param name="user">The valid data for the test.</param>
        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public async Task CreateUser_InvalidPhone(DomainProfile user)
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

                // Create the entity profile with an incomplete user profile
                // set it to a phone number that violates the regex (from DomainProfile PhoneNumber)
                Assert.Throws<ArgumentException>(() => user.PhoneNumber = GenerateRandom.String());
                // phone number is not set, but it is not required, is still valid
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                var userInDB = await repo.GetProfileAsync(user.Email.ToUpper());
                Assert.Equal(userInDB.ProfilePictureUrl, user.ProfilePictureUrl);
                Assert.Equal(userInDB.FirstName, user.FirstName);
                Assert.Equal(userInDB.LastName, user.LastName);
                Assert.Null(userInDB.PhoneNumber);
                Assert.Equal(userInDB.BirthDate, user.BirthDate);
                Assert.Equal(userInDB.Status, user.Status);
            }
        }
    }
}
