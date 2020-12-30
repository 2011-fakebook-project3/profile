using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    // unit testing domain profile model
    public class ProfileTests
    {
        [Theory]
        [ClassData(typeof(Fakebook.Profile.UnitTests.TestData.UserTestData.Create.Valid)]
        public void ValidUserInformationShouldReturnNoErrors(DomainProfile profile)
        {
            // arrange
            // act
            // assert
            Assert.True



            
        }

        [Theory]
        [ClassData(typeof(Fakebook.Profile.UnitTests.TestData.UserTestData.Create.Invalid)]
        public void CreateUser_Invalid(DomainProfile user)
        {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new 

            
        }
    }
}
