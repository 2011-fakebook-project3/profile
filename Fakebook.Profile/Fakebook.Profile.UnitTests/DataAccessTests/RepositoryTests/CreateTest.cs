using System;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.Profile.UnitTests.DataAccessTests.RepositoryTests
{
    public class CreateTest
    {
        //create tests
        #region CreationTests
        [Fact]
        public async void CreateValidRepoWorks()
        {
            //arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            Task task;

            //TODO: either make this an arg, and theory function, or fill in data
            DomainProfile newProfile = new DomainProfile();

            //act
            using (var testContext = new ProfileDbContext(options))
            {
                testContext.Database.EnsureCreated();

                var repo = new Repository(testContext);

                // Create the user data
                task = repo.CreateProfileAsync(newProfile);
            }


            //assert
            using (var assertcontext = new ProfileDbContext(options))
            {
                var repo = new Repository(assertcontext);

                DomainProfile profile = await repo.GetProfileAsync(newProfile.Email);

                Assert.NotNull(profile);

                //assert fields are the same
                Assert.Equal(newProfile.BirthDate, profile.BirthDate);
                Assert.Equal(newProfile.Status, profile.Status);
                Assert.Equal(newProfile.FirstName, profile.FirstName);
                Assert.Equal(newProfile.LastName, profile.LastName);
                Assert.Equal(newProfile.PhoneNumber, profile.PhoneNumber);
                Assert.Equal(newProfile.ProfilePictureUrl, profile.ProfilePictureUrl);
            }

        }



        [Fact]
        public void CreateInvalidUserFails()
        {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ProfileDbContext>()
                .UseSqlite(connection)
                .Options;

            //invalid profile
            DomainProfile invalid = new DomainProfile();

            // Act
            using (var actingContext = new ProfileDbContext(options))
            {
                actingContext.Database.EnsureCreated();

                var repo = new Repository(actingContext);

                // Create the user data
                Assert.ThrowsAsync<ArgumentException>(() => repo.CreateProfileAsync(invalid));
            }

            //assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new Repository(assertionContext);

                var profile = repo.GetProfileAsync(invalid.Email);

                Assert.Null(profile);
            }
        }

        #endregion
    }
}
