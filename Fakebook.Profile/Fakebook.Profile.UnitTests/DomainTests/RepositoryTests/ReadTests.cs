using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Fakebook.Profile.UnitTests.TestData.ProfileTestData;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests.RepositoryTests
{
    public class ReadTests
    {
        [Theory]
        [ClassData(typeof(Read.Valid))]
        public async Task Profile_Read_Valid(List<DomainProfile> profiles, string userEmail)
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

                foreach (var profile in profiles)
                {
                    await repo.CreateProfileAsync(profile);
                }
            }

            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new Repository(assertionContext);

                var result = await repo.GetProfileAsync(userEmail);

                Assert.NotNull(result);
                Assert.Equal(result.Email, userEmail);
            }
        }

        [Theory]
        [ClassData(typeof(Read.Invalid))]
        public async Task Profile_Read_Invalid(List<DomainProfile> profiles, string userEmail)
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

                foreach (var profile in profiles)
                {
                    await repo.CreateProfileAsync(profile);
                }
            }

            // Assert
            using (var assertionContext = new ProfileDbContext(options))
            {
                var repo = new Repository(assertionContext);

                await Assert.ThrowsAsync<ArgumentException>(async () => await repo.GetProfileAsync(userEmail));
            }
        }
    }
}
