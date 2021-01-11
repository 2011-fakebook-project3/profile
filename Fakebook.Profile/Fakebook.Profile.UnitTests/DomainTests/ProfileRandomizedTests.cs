using System;
using Fakebook.Profile.Domain;
using Fakebook.Profile.Domain.Utility;
using Fakebook.Profile.UnitTests.TestData;
using Fakebook.Profile.UnitTests.TestData.ProfileTestData;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    // unit testing domain profile model
    public class ProfileRandomizedTests
    {
        /*
        * User:
        * - Email: string
        * - ProfilePictureUrl: Uri
        * - Name : string
        * - FirstName: string
        * - Lastname: string              
        * - PhoneNumber: string
        * - BirthDate: DateTime
        * - Status: string
        */

        [Theory]
        [ClassData(typeof(Create.Valid))]
        public void ValidUserInfoShouldReturnWithoutErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(MockedProfile.Email, MockedProfile.FirstName, MockedProfile.LastName);

            // act
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidName))]
        public void InvalidUserNameReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(MockedProfile.Email, "first", "last");

            // act
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;
            
            // assert
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.ThrowsAny<ArgumentNullException>(() => profile.FirstName = null);
            Assert.ThrowsAny<ArgumentNullException>(() => profile.LastName = null);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public void InvalidUserPhoneNumberReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(MockedProfile.Email, MockedProfile.FirstName, MockedProfile.LastName);

            // act
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.ThrowsAny<ArgumentException>(() => profile.PhoneNumber = GenerateRandom.String());
        }

        [Theory]
        [ClassData(typeof(Create.InvalidEmail))]
        public void InvalidUserEmailReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile("valid@email.com", MockedProfile.FirstName, MockedProfile.LastName);

            // act
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Email);
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.ThrowsAny<ArgumentException>(() => profile.Email = GenerateRandom.String());
        }


        [Theory]
        [ClassData(typeof(Create.InvalidEmail))]
        public void InvalidUserEmailReturnsErrorsInConstructor(DomainProfile MockedProfile)
        {
            // arrange
            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => new DomainProfile(GenerateRandom.String(), MockedProfile.FirstName, MockedProfile.LastName));
        }


        [Theory]
        [InlineData("test.com", "test")]
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        [InlineData("i.imgur.com", "Lf5S5Sa.jpg")]
        [InlineData("i.imgur.com", "DX5KAnQ.jpg")]
        public void SetUri(string domain, string path)
        {
            // arrange
            DomainProfile profile = new DomainProfile("email@test.com", "first", "last");

            //act
            var builder = new UriBuilder();
            builder.Host = domain;
            builder.Path = path;
            profile.ProfilePictureUrl = builder.Uri;

            //assert
            Assert.NotNull(profile.ProfilePictureUrl);
            Assert.Equal(domain, profile.ProfilePictureUrl.Host);
            Assert.EndsWith(path, profile.ProfilePictureUrl.AbsoluteUri);
        }
    }
}
