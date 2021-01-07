using System;
using Fakebook.Profile.Domain;
using Fakebook.Profile.Domain.Utility;
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
        * - ProfilePictureUrl: string 
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
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.FirstName = MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
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
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.FirstName = MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.Null(profile.Name);
            Assert.Null(profile.FirstName);
            Assert.Null(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public void InvalidUserPhoneNumberReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.FirstName = MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.DoesNotMatch(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
        }

        [Theory]
        [ClassData(typeof(Create.InvalidPhoneNumber))]
        public void InvalidUserEmailReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.FirstName = MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
            Assert.DoesNotMatch(RegularExpressions.EmailCharacters, profile.Email);
        }

        [Theory]
        [InlineData("test.com", "test")]
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        [InlineData("i.imgur.com", "Lf5S5Sa.jpg")]
        [InlineData("i.imgur.com", "DX5KAnQ.jpg")]
        public void SetUri(string domain, string path)
        {
            // arrange
            DomainProfile profile = new DomainProfile();

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
