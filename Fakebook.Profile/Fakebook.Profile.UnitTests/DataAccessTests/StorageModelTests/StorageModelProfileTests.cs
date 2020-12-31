using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.StorageModel;

using Xunit;

namespace Fakebook.Profile.DataAccess.StorageModel
{
    public class StorageModelProfileTests
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
            StorageProfile bf = new StorageProfile();

            //act
            bf.Email = email;

            //assert
            Assert.NotNull(bf.Email);
            Assert.Equal(email, bf.Email);
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
            StorageProfile bf = new StorageProfile();
            bf.Email = "someOtherEmail@email.com";

            //act
            bf.Email = email;

            //assert
            Assert.NotNull(bf.Email);
            Assert.Equal(email, bf.Email);
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
            StorageProfile pf = new StorageProfile();
            var builder = new UriBuilder();

            //act
            builder.Host = host;
            builder.Path = path;
            pf.ProfilePictureUrl = builder.Uri;

            //assert
            Assert.NotNull(pf.ProfilePictureUrl);
            Assert.Equal(host, pf.ProfilePictureUrl.Host);
            Assert.EndsWith(path, pf.ProfilePictureUrl.AbsolutePath);
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
            StorageProfile pf = new StorageProfile();
            //set to an inital uri, since this is testing that it can change when not null.
            pf.ProfilePictureUrl = new UriBuilder().Uri;
            var builder = new UriBuilder();

            //act
            builder.Host = host;
            builder.Path = path;
            pf.ProfilePictureUrl = builder.Uri;

            //assert
            Assert.NotNull(pf.ProfilePictureUrl);
            Assert.Equal(host, pf.ProfilePictureUrl.Host);
            Assert.EndsWith(path, pf.ProfilePictureUrl.AbsolutePath);
        }


        //names
        [Theory]
        [InlineData("Name")]
        public void SettingFirstNameShouldWork(string name)
        {
            //arrange
            StorageProfile pf = new StorageProfile();

            //act
            pf.FirstName = name;

            //assert
            Assert.NotNull(pf.FirstName);
            Assert.Equal(name, pf.FirstName);
        }


        [Theory]
        [InlineData("Name")]
        [InlineData("name")]
        [InlineData("hypen-name")]
        [InlineData("Na,me")]
        [InlineData("Mr.Name")]
        public void SettingLastNameShouldWork(string name)
        {
            //arrange
            StorageProfile pf = new StorageProfile();

            //act
            pf.LastName = name;

            //assert
            Assert.NotNull(pf.LastName);
            Assert.Equal(name, pf.LastName);
        }

    }
}
