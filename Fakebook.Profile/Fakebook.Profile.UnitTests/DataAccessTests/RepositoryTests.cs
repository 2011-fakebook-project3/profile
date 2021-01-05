using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fakebook.Profile.UnitTests.DataAccessTests
{
    public class RepositoryTests
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


        #region DeletionTests
        /*
        [Theory]
        [ClassData(typeof(TestData.ProfileTestData.Read))]
        public async void DeletingRealEntryWorks(List<DomainProfile> profiles, string emailId)
        {

        }
        */

        #endregion


        #region stuff
        [Theory]
        [ClassData(typeof(TestData.ProfileTestData.Update.Valid))]
        public async void UpdatingRealProfileWorks(DomainProfile orig, DomainProfile updated)
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

                var repo = new Repository(actingContext);

                // Create the user data
                Task result =  repo.UpdateProfileAsync(email: orig.Email, updated);
            }


            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new Repository(assertionContext);

                var updateResult = repo.UpdateProfileAsync(orig.Email, updated);

                //Assert.True(updateResult, "Unable to update the user.");
                var alteredUser = await repo.GetProfileAsync(orig.Email);

                Assert.NotEqual(orig.FirstName, alteredUser.FirstName);
                Assert.NotEqual(orig.LastName, alteredUser.LastName);
                Assert.NotEqual(orig.Status, alteredUser.Status);
            }
        }


        #endregion

    }
}
