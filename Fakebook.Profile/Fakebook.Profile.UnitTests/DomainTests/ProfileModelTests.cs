using System;
using System.Collections.Generic;
using Fakebook.Profile.Domain;
using Fakebook.Profile.Domain.Utility;
using System.Text.RegularExpressions;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    public class ProfileModelTests
    {
        #region EmailTests
        /// <summary>
        /// Check that emails can't be null.
        /// </summary>
        [Fact]
        public void EmailCantBeNull()
        {
            //arrange
            string email = null;
            //act
            //assert
            Assert.ThrowsAny<ArgumentNullException>(() => new DomainProfile(email, "First", "Last"));
        }

        /// <summary>
        /// Check that invalid emails throw an exception.
        /// </summary>
        /// <param name="email">The email to test. Should be invalid.</param>
        [Theory]
        [InlineData("test1@test")]
        [InlineData("test2.test2")]
        [InlineData("test3.test3@test3")]
        [InlineData("test4")]
        public void InvalidEmailThrowsException(string email)
        {
            //arrange
            //act
            //assert
            Assert.ThrowsAny<ArgumentException>(() => new DomainProfile(email, "First", "Last"));
        }

        /// <summary>
        /// Test that valid emails work.
        /// </summary>
        /// <param name="email">The email to test.</param>
        [Theory]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmail@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void ValidEmailWorks(string email)
        {
            //arrange
            //act
            DomainProfile profile = new(email, "First", "Last");

            //assert
            Assert.NotNull(profile.Email);
            Assert.Equal(email, profile.Email);
        }
        #endregion


        #region PfPTests
        /// <summary>
        /// Insure Uri's can be null.
        /// </summary>
        [Fact]
        public void UrlCanBeNull()
        {
            //arrange
            Uri url = null;
            DomainProfile profile = new("test@test.test", "First", "Last");

            //act
            profile.ProfilePictureUrl = url;

            //assert
            Assert.Null(profile.ProfilePictureUrl);
        }

        /// <summary>
        /// Check that a valid url is accepted.
        /// </summary>
        /// <param name="host">The host of the uri to test. Should be a domain</param>
        /// <param name="path">The path to the resource in the url</param>
        [Theory]
        //https://i.imgur.com/BCeyxdR.jpg
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        //https://i.imgur.com/Lf5S5Sa.jpg
        [InlineData("i.imgur.com", "Lf5S5Sa.png")]
        //https://i.imgur.com/DX5KAnQ.jpg
        [InlineData("i.imgur.com", "DX5KAnQ.jpeg")]
        public void ValidUrlWorks(string host, string path)
        {
            //arrange
            DomainProfile profile = new("test@test.com", "First", "Last");
            UriBuilder uriBuilder = new();
            uriBuilder.Host = host;
            uriBuilder.Path = path;

            //act
            Uri uri = uriBuilder.Uri;
            profile.ProfilePictureUrl = uri;

            //assert
            Assert.NotNull(profile.ProfilePictureUrl);
        }

        /// <summary>
        /// Check that a invalid url is not accepted.
        /// </summary>
        /// <param name="host">The host of the uri to test. Should be a domain</param>
        /// <param name="path">The path to the resource in the url</param>
        [Theory]
        //https://i.imgur.com/BCeyxdR.jpg
        [InlineData("i.imgur.com", "BCeyxdR.log")]
        //https://i.imgur.com/Lf5S5Sa.jpg
        [InlineData("i.imgur.com", "Lf5S5Sa.json")]
        //https://i.imgur.com/DX5KAnQ.jpg
        [InlineData("i.imgur.com", "DX5KAnQ.tim")]
        public void InvalidUrlFails(string host, string path)
        {
            //arrange
            DomainProfile profile = new("test@test.com", "First", "Last");
            UriBuilder uriBuilder = new();
            uriBuilder.Host = host;
            uriBuilder.Path = path;

            //act
            Uri uri = uriBuilder.Uri;

            //assert
            Assert.ThrowsAny<Exception>(() => { profile.ProfilePictureUrl = uri; });
        }
        #endregion


        #region NameTests
        /// <summary>
        /// Check that the name property can format names correctly
        /// </summary>
        /// <param name="first">The first name</param>
        /// <param name="last">The last name</param>
        [Theory]
        [InlineData("first", "last")]
        [InlineData("Something", "Name")]
        public void NameReturnsProperFormat(string first, string last)
        {
            //arrange
            DomainProfile profile = new("example@email.com", "First", "Last");

            profile.FirstName = first;
            profile.LastName = last;

            //act
            string fullname = profile.Name;

            //assert
            Assert.NotNull(fullname);
            Assert.Equal(first + ' ' + last, fullname);
        }



        /// <summary>
        /// Insure Valid characters are accepted
        /// </summary>
        /// <param name="name"> The name to test.</param>
        [Theory]
        [InlineData("Name")]
        [InlineData("name")]
        [InlineData("hypen-name")]
        [InlineData("Na,me")]
        [InlineData("Mr.Name")]
        [InlineData("X Æ A-12")]
        [InlineData("l33t")]
        public void ValidNameAccepted(string name)
        {
            //arrange
            DomainProfile profile = new("email@example.com", "First", "Last");

            //act
            profile.FirstName = name;
            profile.LastName = name;

            //assert
            Assert.Equal(name, profile.FirstName);
            Assert.Equal(name, profile.LastName);
        }


        /// <summary>
        /// Test that invalid names are rejected.
        /// </summary>
        /// <param name="name">The name to test</param>
        [Theory]
        [InlineData("1234567")]
        [InlineData("!@#$%^&*()_+")]
        public void InvalidNameRejected(string name)
        {
            //arrange
            DomainProfile profile = new("email@example.com", "First", "Last");

            //act
            //assert
            Assert.ThrowsAny<ArgumentException>(() => profile.FirstName = name);
            Assert.ThrowsAny<ArgumentException>(() => profile.LastName = name);
        }
        #endregion


        #region PhoneNumberTests
        /// <summary>
        /// Insure valid phone number inputs work.
        /// </summary>
        /// <param name="phoneNumber">Phone number string</param>
        [Theory]
        [InlineData("1234567899")]
        [InlineData("570-386-1123")]
        [InlineData("(267)-424-2243")]
        [InlineData("(267) 424-2243")]
        [InlineData("267 424 2243")]
        public void ValidPhoneNumberWorks(string phoneNumber)
        {
            // arrange
            DomainProfile profile = new("testemail@email.com", "First", "Last");

            // act
            profile.PhoneNumber = phoneNumber;

            // assert
            Assert.True(profile.PhoneNumber.Length <= 14 || profile.PhoneNumber.Length >= 10);
        }

        /// <summary>
        /// Invalid phone numbers don't work.
        /// </summary>
        /// <param name="phoneNumber">The phone number to test.</param>
        [Theory]
        [InlineData("345")]
        [InlineData("77777777777777777777")]
        [InlineData("dw#oi3456")]
        public void InvalidPhoneNumberThrowsException(string phoneNumber)
        {
            // arrange
            DomainProfile profile = new("testemail@email.com", "First", "Last");

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.PhoneNumber = phoneNumber);
        }
        #endregion


        #region BirthdayTests
        /// <summary>
        /// Check if a valid birthdate is in the past
        /// </summary>
        /// <param name="birthDate"> a birthdate string</param>
        [Theory]
        [InlineData("8/18/1880")]
        [InlineData("9/19/1999")]
        [InlineData("12/29/2020")]
        public void ValidBirthDataShouldBeInThePast(string birthDate)
        {
            // arrange
            DomainProfile profile = new("testemail@email.com", "First", "Last");
            DateTime bDate = DateTime.Parse(birthDate);

            // act
            profile.BirthDate = bDate;

            // assert
            Assert.True(profile.BirthDate < DateTime.Now);
        }

        /// <summary>
        /// Check if an invalid birthdate throws an argument exception
        /// </summary>
        /// <param name="birthDate"> a birthdate string</param>
        [Theory]
        [InlineData("5/11/2021")]
        [InlineData("2/28/2022")]
        [InlineData("12/31/2023")]
        public void InvalidBirthdateShouldThrowAnException(string birthDate)
        {
            // arrange
            DomainProfile profile = new("testemail@email.com", "First", "Last");
            DateTime bDate = DateTime.Parse(birthDate);

            // act + assert
            Assert.ThrowsAny<ArgumentException>(() => profile.BirthDate = bDate);
        }

        /// <summary>
        /// Check that one milisecond ago is valid because it's in the past.
        /// </summary>
        [Fact]
        public void EarlierTodayIsValid()
        {
            // arrange
            DomainProfile profile = new("testemail@email.com", "First", "Last");
            DateTime earlierToday = DateTime.Now;
            earlierToday = earlierToday.AddMilliseconds(-1);

            // act
            profile.BirthDate = earlierToday;
            // assert
            Assert.Equal(earlierToday, profile.BirthDate);
        }
        #endregion

        /// <summary>
        /// Check that follower emails have the correct format.
        /// </summary>
        [Fact]
        public void TestAddFollowerEmail() 
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followerEmail = "testermail@gmail.com";

            // act
            profile.AddFollower(followerEmail);

            // assert
            Assert.Contains(followerEmail, profile.FollowerEmails);

        }

        /// <summary>
        /// Check that following emails have the correct format.
        /// </summary>
        [Fact]
        public void TestAddFollowingEmail()
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followingEmail = "testermail@gmail.com";

            // act
            profile.AddFollow(followingEmail);

            // assert
            Assert.Contains(followingEmail, profile.FollowingEmails);
        }

        /// <summary>
        /// Check the method AddFollower throws an exception when the follower email has an invalid format.
        /// </summary>
        [Fact]
        public void TestAddInvalidFollowerEmail()
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followerEmail = "@gmail.com.tester";

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollower(followerEmail));

        }

        /// <summary>
        /// Check the method AddFollow throws an exception when the following email has an invalid format.
        /// </summary>
        [Fact]
        public void TestAddInvalidFollowingEmail()
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followingEmail = "@gmail.com.tester";

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollow(followingEmail));

        }

        /// <summary>
        /// Check the method AddFollower throws an exception when the follower email is empty.
        /// </summary>
        [Fact]
        public void TestEmptyFollowerEmail() 
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followerEmail = "";

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollower(followerEmail));
        }

        /// <summary>
        /// Check the method AddFollow throws an exception when the following email is empty.
        /// </summary>
        [Fact]
        public void TestEmptyFollowingEmail()
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followingEmail = "";

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollow(followingEmail));
        }

        /// <summary>
        /// Check the method AddFollower throws an exception when a duplicate follower email is added to the list of follower emails.
        /// </summary>
        [Fact]
        public void TestDuplicateFollowerEmail() 
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followerEmail = "testermail@outlook.com";
            string duplicateFollowerEmail = followerEmail;
            profile.AddFollower(followerEmail);

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollower(duplicateFollowerEmail));
        }

        /// <summary>
        /// Check the method AddFollow throws an exception when a duplicate following email is added to the list of following emails.
        /// </summary>
        [Fact]
        public void TestDuplicateFollowingEmail()
        {
            // arrange
            DomainProfile profile = new("emailtest@gmail.com", "Bob", "Fields");
            string followingEmail = "testermail@outlook.com";
            string duplicateFollowingEmail = followingEmail;
            profile.AddFollow(followingEmail);

            // act
            // assert
            Assert.ThrowsAny<ArgumentException>(() => profile.AddFollow(duplicateFollowingEmail));
        }

    }
}
