using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fakebook.Profile.Domain;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    public class RepositoryTests
    {
        [Theory]
        [ClassData(typeof(UserData.Create.Valid))]
        public void CreateUser_Valid(User user)
        {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>().
                UseSqlite(connection)
                .Options;

            int result;

            // Act
            using(var actingContext = new FakebookContext(options))
            {
                actingContext.Database.EnsureCreated();

                var repo = new Repository(actingContext);

                result = repo.CreateUser(user).Result;
            }

            Assert.True(result != -1, "Failed to create user.");

        }

    }
}
