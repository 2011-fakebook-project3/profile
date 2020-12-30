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
        public void InvalidEmailThrowsException(string email)
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


        #region PfPTests
        [Fact]
        public void UrlCanBeNull()
        {
            //arrange
            Uri url = null;
            DomainProfile profile = new DomainProfile("test@test.test");

            //act
            profile.ProfilePictureUrl = url;

            //assert
            Assert.Null(profile.ProfilePictureUrl);
        }

        [Theory]
        //https://imgur.com/t/photography/nOfAU66
        [InlineData("imgur.com","t/photography/nOfAU66")]
        public void ValidUrlWorks(string host, string path)
        {
            //arrange
            DomainProfile profile = new DomainProfile("test@test.com");
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = host;
            uriBuilder.Path = path;

            //act
            Uri uri = uriBuilder.Uri;

            //assert
            Assert.NotNull(profile.ProfilePictureUrl);
        }

        #endregion


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


        //name test - only valid characters
        [Theory]
        [InlineData("Name")]
        [InlineData("name")]
        [InlineData("hypen-name")]
        [InlineData("Na,me")]
        [InlineData("Mr.Name")] // <- forgot periods
        public void ValidNameAccepted(string name)
        {
            //arrange
            DomainProfile profile = new DomainProfile("email@example.com");

            //act
            profile.FirstName = name;
            profile.LastName = name;

            //assert
            Assert.Equal(name, profile.FirstName);
            Assert.Equal(name, profile.LastName);
        }

        [Theory]
        [InlineData("X Æ A-12")]
        [InlineData("1234567")]
        [InlineData("l33t")]
        [InlineData("!@#$%^&*()_+")]
        public void InvalidNameRejected(string name)
        {
            //arrange
            DomainProfile profile = new DomainProfile("email@example.com");

            //act
            //assert
            Assert.Throws<ArgumentException>(() => profile.FirstName = name);
            Assert.Throws<ArgumentException>(() => profile.LastName = name);
        }
        #endregion

        #region PhoneNumberTests

        [Theory]
        [InlineData("1234567899")]
        [InlineData("570-386-1123")]
        [InlineData("(267)-424-2243")]
        [InlineData("(267) 424-2243")]
        [InlineData("267 424 2243")]
        public void ValidPhoneNumberWorks(string phoneNumber)
        {
            // arrange
            DomainProfile profile = new DomainProfile("testemail@email.com");

            // act
            profile.PhoneNumber =  phoneNumber;
           
            // assert
            Assert.True(profile.PhoneNumber.Length <= 14 || profile.PhoneNumber.Length >= 10);
        }

        [Theory]
        [InlineData("345")]
        [InlineData("77777777777777777777")]
        [InlineData("dw#oi3456")]
        public void InvalidPhoneNumberThrowsException(string phoneNumber)
        {
            // arrange
            DomainProfile profile = new DomainProfile("testemail@email.com");

            // act
            // assert
            Assert.Throws<ArgumentException>(() => profile.PhoneNumber =  phoneNumber);
        }


        #endregion

        #region BirthdayTests
        [Theory]
        [InlineData("1888-08-08")]
        [InlineData("1999-09-09")]
        [InlineData("2020-12-29")]
        public void ValidBirthDataShouldBeInThePast(DateTime BirthDate)
        {
            // arrange
            DomainProfile profile = new DomainProfile("testemail@email.com");

            // act
            profile.BirthDate = BirthDate;

            // assert
            Assert.True(profile.BirthDate < DateTime.Now);

        }

        [Theory]
        [InlineData("2021-05-11")]
        [InlineData("2022-02-28")]
        [InlineData("2023-12-31")]
        public void InvalidBirthdateShouldThrowAnException(DateTime BirthDate)
        {
            // arrange 
            DomainProfile profile = new DomainProfile("testemail@email.com");

            // act
            profile.BirthDate = BirthDate;

            // assert
            Assert.Throws<ArgumentException>(() => profile.BirthDate = BirthDate);

        }
        #endregion


    }
}
