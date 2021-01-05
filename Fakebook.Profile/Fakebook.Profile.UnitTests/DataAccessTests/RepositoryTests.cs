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

namespace Fakebook.Profile.UnitTests.DataAccsessTests
{
    public class RepositoryTests
    {
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
            //await results of the create

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
    }
}
