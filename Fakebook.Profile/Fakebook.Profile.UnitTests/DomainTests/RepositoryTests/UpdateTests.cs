using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData;
using Fakebook.Profile.UnitTests.TestData.ProfileTestData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests.RepositoryTests
{
    public class UpdateTests
    {
        [Theory]
        [ClassData(typeof(Update.Valid))]
        public async Task UpdateProfile_ValidData(DomainProfile user, DomainProfile userUpdates)
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

                // Create the user data
                await repo.CreateProfileAsync(user);
            }

            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new ProfileRepository(assertionContext);

                await repo.UpdateProfileAsync(user.Email, userUpdates);
                var alteredUser = await repo.GetProfileAsync(user.Email);
                Assert.NotEqual(user.FirstName, alteredUser.FirstName);
                Assert.NotEqual(user.LastName, alteredUser.LastName);
                Assert.NotEqual(user.Status, alteredUser.Status);
            }
        }

        [Theory]
        [ClassData(typeof(Update.InvalidPhone))]
        public async Task UpdateProfile_InvalidPhone(DomainProfile user, DomainProfile userUpdate)
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

                // Create the user data
                await repo.CreateProfileAsync(user);
            }

            // Assert          
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new ProfileRepository(assertionContext);

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

        [Theory]
        [ClassData(typeof(Update.Invalid))]
        public async Task UpdateProfile_InvalidName(DomainProfile user, DomainProfile userUpdate)
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

                // Create the user data
                await repo.CreateProfileAsync(user);
            }

            // Assert          
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new ProfileRepository(assertionContext);

                // Create the entity profile with an incomplete user profile                                      
                // first name and last name are not set -> invalid (from Repository UpdateProfileAsync)
                await Assert.ThrowsAsync<ArgumentException>( async() => await repo.UpdateProfileAsync(user.Email, userUpdate));

                var userActual = await repo.GetProfileAsync(user.Email);
                Assert.Equal(user.ProfilePictureUrl, userActual.ProfilePictureUrl);
                Assert.Equal(user.FirstName, userActual.FirstName);
                Assert.Equal(user.LastName, userActual.LastName);
                Assert.Equal(user.PhoneNumber, userActual.PhoneNumber);
                Assert.Equal(user.BirthDate, userActual.BirthDate);
                Assert.Equal(user.Status, userActual.Status);
            }
        }
    }
}