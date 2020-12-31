using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData;
using Moq;


using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    // unit testing domain profile model
    public class ProfileTests
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
        [ClassData(typeof(UserData.Create.Valid))]
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
            Assert.Matches(RegularExpressions.EmailCharacters,profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber); 
        }

        [Theory]
        [ClassData(typeof(UserData.Create.InvalidName))]
        public void InvalidUserNameReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.FirstName =  MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate =  MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            Assert.Null(profile.Name);
            Assert.Null(profile.FirstName);
            Assert.Null(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters, profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
        }

        [Theory]
        [ClassData(typeof(UserData.Create.InvalidPhoneNumber))]
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
        [ClassData(typeof(UserData.Create.InvalidPhoneNumber))]
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
    }
}

