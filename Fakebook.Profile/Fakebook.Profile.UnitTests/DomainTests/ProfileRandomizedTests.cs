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
        public void ValidUserInfoShouldReturnWithoutErrors(DomainProfile mockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(
                email: mockedProfile.Email,
                firstname: mockedProfile.FirstName,
                lastname: mockedProfile.LastName
            )
            {
                // act
                ProfilePictureUrl = mockedProfile.ProfilePictureUrl,
                PhoneNumber = mockedProfile.PhoneNumber,
                BirthDate = mockedProfile.BirthDate,
                Status = mockedProfile.Status
            };

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidName))]
        public void InvalidUserNameReturnsErrors(DomainProfile mockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(
                email: mockedProfile.Email,
                firstname: mockedProfile.FirstName,
                lastname: mockedProfile.LastName
            )
            {
                // act
                ProfilePictureUrl = mockedProfile.ProfilePictureUrl,
                PhoneNumber = mockedProfile.PhoneNumber,
                BirthDate = mockedProfile.BirthDate,
                Status = mockedProfile.Status
            };

            // assert
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.ThrowsAny<ArgumentNullException>(() => profile.FirstName = null);
            Assert.ThrowsAny<ArgumentNullException>(() => profile.LastName = null);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public void InvalidUserPhoneNumberReturnsErrors(DomainProfile mockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(
                email: mockedProfile.Email,
                firstname: mockedProfile.FirstName,
                lastname: mockedProfile.LastName
            )
            {
                // act
                ProfilePictureUrl = mockedProfile.ProfilePictureUrl,
                FirstName = mockedProfile.FirstName,
                LastName = mockedProfile.LastName,
                BirthDate = mockedProfile.BirthDate,
                Status = mockedProfile.Status
            };

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            // Assert.DoesNotMatch(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.ThrowsAny<ArgumentException>(() => profile.PhoneNumber = GenerateRandom.String());
        }

        [Theory]
        [ClassData(typeof(Create.InvalidEmail))]
        public void InvalidUserEmailReturnsErrors(DomainProfile mockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(
                email: mockedProfile.Email,
                firstname: mockedProfile.FirstName,
                lastname: mockedProfile.LastName
            )
            {
                // act
                ProfilePictureUrl = mockedProfile.ProfilePictureUrl,
                FirstName = mockedProfile.FirstName,
                LastName = mockedProfile.LastName,
                PhoneNumber = mockedProfile.PhoneNumber,
                BirthDate = mockedProfile.BirthDate,
                Status = mockedProfile.Status
            };

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.ThrowsAny<ArgumentException>(() => profile.Email = GenerateRandom.String());
        }

        [Theory]
        [InlineData("test.com", "test")]
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        [InlineData("i.imgur.com", "Lf5S5Sa.jpg")]
        [InlineData("i.imgur.com", "DX5KAnQ.jpg")]
        public void SetUri(string domain, string path)
        {
            // arrange
            DomainProfile profile = new DomainProfile("test@email.com", "First", "Last");

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