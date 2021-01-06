using System;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.Profile.UnitTests.DataAccessTests.RepositoryTests
{
    public class UpdateTest
    {
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
                await repo.UpdateProfileAsync(email: orig.Email, updated);
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
