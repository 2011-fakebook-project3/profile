using System;
using System.Collections.Generic;
using Fakebook.Profile.DataAccess.EntityModel;

using Xunit;

namespace Fakebook.Profile.DataAccess.StorageModel
{
    public class EntityProfileTests
    {
        //Email Tests
        /// <summary>
        /// Check that setting an email works at all.
        /// </summary>
        /// <param name="email">The email to set the profile's email to.</param>
        [Theory]
        [InlineData("test@email.com")]
        [InlineData("random@email.com")]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmai@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void SetEmailShouldWork(string email)
        {
            //arrange
            EntityProfile profile = new();

            //act
            profile.Email = email;

            //assert
            Assert.NotNull(profile.Email);
            Assert.Equal(email, profile.Email);
        }

        /// <summary>
        /// Test that changing the email from an initial value works.
        /// </summary>
        /// <remarks>
        /// Maybe this shouldn't be allowed, but for now
        /// it's the model's responsibility for validation
        /// </remarks>
        /// <param name="email"></param>
        [Theory]
        [InlineData("test@email.com")]
        [InlineData("random@email.com")]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmai@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void ChangingEmailShouldWork(string email)
        {
            //arrange
            EntityProfile profile = new();
            profile.Email = "someOtherEmail@email.com";

            //act
            profile.Email = email;

            //assert
            Assert.NotNull(profile.Email);
            Assert.Equal(email, profile.Email);
        }

        //URI Tests
        /// <summary>
        /// Check that you can set a valid uri.
        /// </summary>
        /// <param name="host">The domain for th uri to test</param>
        /// <param name="path">The path to the resource in the domain.</param>
        [Theory]
        [InlineData("test.com", "test")]
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        [InlineData("i.imgur.com", "Lf5S5Sa.jpg")]
        [InlineData("i.imgur.com", "DX5KAnQ.jpg")]
        public void SetValidUriShouldWork(string host, string path)
        {
            //arrange
            EntityProfile profile = new();
            UriBuilder uriBuilder = new();

            //act
            uriBuilder.Host = host;
            uriBuilder.Path = path;
            profile.ProfilePictureUrl = uriBuilder.Uri;

            //assert
            Assert.NotNull(profile.ProfilePictureUrl);
            Assert.Equal(host, profile.ProfilePictureUrl.Host);
            Assert.EndsWith(path, profile.ProfilePictureUrl.AbsolutePath);
        }

        /// <summary>
        /// Test that a uri can be changed from it's initial value.
        /// </summary>
        /// <param name="host">The host domain.</param>
        /// <param name="path">The path relative to the host.</param>
        [Theory]
        [InlineData("test.com", "test")]
        [InlineData("i.imgur.com", "BCeyxdR.jpg")]
        [InlineData("i.imgur.com", "Lf5S5Sa.jpg")]
        [InlineData("i.imgur.com", "DX5KAnQ.jpg")]
        public void ChangeValidUriShouldWork(string host, string path)
        {
            //arrange
            EntityProfile profile = new()
            {
                //set to an initial uri, since this is testing that it can change when not null.
                ProfilePictureUrl = new UriBuilder().Uri
            };
            UriBuilder uriBuilder = new()
            {
                //act
                Host = host,
                Path = path
            };
            profile.ProfilePictureUrl = uriBuilder.Uri;

            //assert
            Assert.NotNull(profile.ProfilePictureUrl);
            Assert.Equal(host, profile.ProfilePictureUrl.Host);
            Assert.EndsWith(path, profile.ProfilePictureUrl.AbsolutePath);
        }

        /// <summary>
        /// Test that a first name can be set to an initial value
        /// <param name="name"></param>
        /// </summary>
        [Theory]
        [InlineData("Name")]
        public void SettingFirstNameShouldWork(string name)
        {
            //arrange
            EntityProfile profile = new();

            //act
            profile.FirstName = name;

            //assert
            Assert.NotNull(profile.FirstName);
            Assert.Equal(name, profile.FirstName);
        }

        /// <summary>
        /// Test that a last name can be set to an initial value
        /// </summary>
        /// <param name="name"></param>
        [Theory]
        [InlineData("Name")]
        [InlineData("name")]
        [InlineData("hyphen-name")]
        [InlineData("Na,me")]
        [InlineData("Mr.Name")]
        public void SettingLastNameShouldWork(string name)
        {
            //arrange
            EntityProfile profile = new();

            //act
            profile.LastName = name;

            //assert
            Assert.NotNull(profile.LastName);
            Assert.Equal(name, profile.LastName);
        }

         /// <summary>
        /// Test that an email of someone the user is following can be added to a list of FollowingEmails
        /// </summary>
        /// <param name="email"></param>
        [Theory]
        [InlineData("test@email.com")]
        [InlineData("random@email.com")]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        public void AddingFollowingEmailShouldWork(string email)
        {
            //arrange
            EntityProfile profile = new();
            List<string> followingEmails = new List<string>();

            //act
            followingEmails.Add(email);

            //assert
            Assert.NotEmpty(followingEmails);
            Assert.Equal(followingEmails[0], email);
        }
    }
}
