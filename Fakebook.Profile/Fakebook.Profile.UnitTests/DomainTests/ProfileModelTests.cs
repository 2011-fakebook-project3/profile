using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.Domain;
using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    public class ProfileModelTests
    {
        #region NameTests
        [Theory]
        [InlineData("first", "last")]
        [InlineData("Something", "Name")]
        public void NameReturnsProperFormat(string first, string last)
        {
            //arrange
            DomainProfile profile = new DomainProfile("example@email.com");

            profile.FirstName = first;
            profile.LastName = last;

            //act 
            string fullname = profile.Name;

            //assert
            Assert.NotNull(fullname);
            Assert.Equal(first + ' ' + last, fullname);
        }
        #endregion


        #region EmailTests
        [Fact]
        public void EmailCantBeNull()
        {
            //arrange
            string email = null;
            //act
            //assert
            Assert.Throws<ArgumentNullException>(() => new DomainProfile(email));
        }


        [Theory]
        [InlineData("test1@test")]
        [InlineData("test2.test2")]
        [InlineData("test3.test3@test3")]
        public void InvalidEmailThorwsException(string email)
        {
            //arrange
            //act
            //assert
            Assert.Throws<ArgumentException>(() => new DomainProfile(email));
        }


        [Theory]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmai@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void ValidEmailWorks(string email)
        {
            //arrange
            //act
            DomainProfile profile = new DomainProfile(email);

            //assert
            Assert.NotNull(profile.Email);
            Assert.Equal(email, profile.Email);
        }
        #endregion
    }
}
