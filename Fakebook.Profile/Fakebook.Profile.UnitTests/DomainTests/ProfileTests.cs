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
        [ClassData(typeof(Fakebook.Profile.UnitTests.TestData.UserData.Create.Valid))]
        public void ValidUserInfoShouldReturnWithNoErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile(); 

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.Name = MockedProfile.Name;
            profile.FirstName = MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate = MockedProfile.BirthDate;
            Profile.Status = MockedProfile.Status;

            // assert
            Assert.NotNull(profile.Name);
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.Matches(RegularExpressions.EmailCharacters,profile.Email);
            Assert.Matches(RegularExpressions.PhoneNumberCharacters, profile.PhoneNumber);
   
        }

        [Theory]
        [ClassData(typeof(Fakebook.Profile.UnitTests.TestData.UserData.Create.Invalid))]
        public void InvalidUserReturnsErrors(DomainProfile MockedProfile)
        {
            // arrange
            DomainProfile profile = new DomainProfile();

            // act
            profile.Email = MockedProfile.Email;
            profile.ProfilePictureUrl = MockedProfile.ProfilePictureUrl;
            profile.Name =  MockedProfile.Name;
            profile.FirstName =  MockedProfile.FirstName;
            profile.LastName = MockedProfile.LastName;
            profile.PhoneNumber = MockedProfile.PhoneNumber;
            profile.BirthDate =  MockedProfile.BirthDate;
            profile.Status = MockedProfile.Status;

            // assert
            //Assert.N;


        }
    }
}

