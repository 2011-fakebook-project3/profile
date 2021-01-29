using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData.ProfileTestData;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests.RepositoryTests
{
    public class ReadTests
    {
        /// <summary>
        /// Test if an valid profile can be read correctly
        /// </summary>
        /// <param name="users">randomized profile data</param>
        /// <param name="userEmail">a targeted email</param>
        /// <returns></returns>
        [Theory]
        [ClassData(typeof(Read.Valid))]
        public async Task GetOneProfile_ValidData(List<DomainProfile> users, string userEmail)
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
                users.ForEach(async user => await repo.CreateProfileAsync(user));

            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                var usersActual = await repo.GetAllProfilesAsync();
                Assert.True(usersActual.Any());
                Assert.NotNull(usersActual);

                var userData = users.First(x => x.Email == userEmail);
                var userActual = await repo.GetProfileAsync(userData.Email);
                Assert.NotNull(userActual);

                Assert.Equal(userData.FirstName, userActual.FirstName);
                Assert.Equal(userData.LastName, userActual.LastName);
                Assert.Equal(userData.BirthDate, userActual.BirthDate);
            }
        }

        /// <summary>
        /// Given valid profile data, test if a random invalid email can still be accessed incorrectly
        /// </summary>
        /// <param name="users">randomized profile data</param>
        /// <param name="userEmail">a targeted email</param>
        /// <returns></returns>
        [Theory]
        [ClassData(typeof(Read.Invalid))]
        public async Task GetOneProfile_InvalidData(List<DomainProfile> users, string userEmail)
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
                var repo = new ProfileRepository(actingContext);

                // Create valid user data
                users.ForEach(async user => await repo.CreateProfileAsync(user));
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                var usersActual = await repo.GetAllProfilesAsync();
                Assert.True(usersActual.Any());
                // 0. only emails are invalid
                // 1. invalid emails that don't fit regex -> ArgumentException (from DomainProfile Email)
                // 2. null -> ArgumentNullException (from ProfileRepository mapper)
                // throwsany catches parent and derived
                await Assert.ThrowsAnyAsync<ArgumentException>(async () => await repo.GetProfileAsync(userEmail));
            }
        }

        /// <summary>
        /// Test if all valid profiles can be read correctly
        /// </summary>
        /// <param name="users">randomized profile data</param>
        /// <param name="userEmail">a targeted email</param>
        /// <returns></returns>
        [Theory]
        [ClassData(typeof(Read.ValidCollection))]
        public async Task GetAllProfiles_ValidData(List<DomainProfile> users)
        {
            // Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            int initialCount;

            // Act
            using (ProfileDbContext actingContext = new(options))
            {
                actingContext.Database.EnsureCreated();
                initialCount = actingContext.EntityProfiles.Count();
                ProfileRepository repo = new(actingContext);
                users.ForEach(async user => await repo.CreateProfileAsync(user));
            }

            // Assert
            using (ProfileDbContext assertionContext = new(options))
            {
                ProfileRepository repo = new(assertionContext);

                var usersActual = await repo.GetAllProfilesAsync();
                Assert.True(usersActual.Any());
                Assert.True(usersActual.Count() == initialCount + users.Count);

                // testing the first element
                var userExpected = users.First();
                var userActual = usersActual.Single(u => u.Email == userExpected.Email);
                Assert.Equal(userExpected.ProfilePictureUrl, userActual.ProfilePictureUrl);
                Assert.Equal(userExpected.FirstName, userActual.FirstName);
                Assert.Equal(userExpected.LastName, userActual.LastName);
                Assert.Equal(userExpected.PhoneNumber, userActual.PhoneNumber);
                Assert.Equal(userExpected.BirthDate, userActual.BirthDate);
                Assert.Equal(userExpected.Status, userActual.Status);

                // testing the last element
                var userLastExpected = users.Last();
                var userLastActual = usersActual.Single(u => u.Email == userLastExpected.Email);
                Assert.Equal(userLastExpected.ProfilePictureUrl, userLastActual.ProfilePictureUrl);
                Assert.Equal(userLastExpected.FirstName, userLastActual.FirstName);
                Assert.Equal(userLastExpected.LastName, userLastActual.LastName);
                Assert.Equal(userLastExpected.PhoneNumber, userLastActual.PhoneNumber);
                Assert.Equal(userLastExpected.BirthDate, userLastActual.BirthDate);
                Assert.Equal(userLastExpected.Status, userLastActual.Status);
            }
        }
    }
}
