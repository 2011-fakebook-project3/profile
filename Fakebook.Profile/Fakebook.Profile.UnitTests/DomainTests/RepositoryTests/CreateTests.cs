using System;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData;
using Fakebook.Profile.UnitTests.TestData.ProfileTestData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests.RepositoryTests
{

    public class CreateTests
    {
        [Theory]
        [ClassData(typeof(Create.Valid))]
        public async Task CreateUser_ValidData(DomainProfile user)
        {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (var actingContext = new ProfileDbContext(options))
            {
                actingContext.Database.EnsureCreated();

                var repo = new ProfileRepository(actingContext);

                // Create the entity profile with a complete user profile
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new ProfileRepository(assertionContext);
                var userInDB = await repo.GetProfileAsync(user.Email);
                Assert.NotNull(userInDB);
                Assert.Equal(user.Email, userInDB.Email);
                Assert.Equal(user.FirstName, userInDB.FirstName);
                Assert.Equal(user.LastName, userInDB.LastName);
                Assert.Equal(user.BirthDate, userInDB.BirthDate);
            }
        }

        // Invalid split into 3 parts
        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public async Task CreateUser_InvalidPhone(DomainProfile user)
        {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (var actingContext = new ProfileDbContext(options))
            {
                actingContext.Database.EnsureCreated();

                var repo = new ProfileRepository(actingContext);

                // Create the entity profile with an incomplete user profile                                       
                // set it to a phone number that violates the regex (from DomainProfile PhoneNumber)              
                Assert.Throws<ArgumentException>(() => user.PhoneNumber = GenerateRandom.String());
                // phone number is not set, but it is not required, is still valid  
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            { 
                var repo = new ProfileRepository(assertionContext);

                var userInDB = await repo.GetProfileAsync(user.Email);
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