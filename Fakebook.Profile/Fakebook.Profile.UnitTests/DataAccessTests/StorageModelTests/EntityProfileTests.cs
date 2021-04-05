using System;

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
        /// Test that EntityProfile constructor works
        /// </summary>
        [Fact]
        public void ProfileMinimumConstructor_Pass()
        {
            // arrange
            const string email = "hi@gmail.com";
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act
            EntityProfile profile = new EntityProfile(email, firstName, lastName, dob);
            
            // assert
            Assert.NotNull(profile);
        }

        /// <summary>
        /// Test that an empty email will throw an exception
        /// </summary>
        [Fact]
        public void EmptyEmail_Exception()
        {
            // arrange
            const string email = "";
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);
            
            // assert
            Assert.Throws<ArgumentNullException>(constructProfile);
        }

        /// <summary>
        /// Test that an empty FirstName will throw an exception
        /// </summary>
        [Fact]
        public void EmptyFirstName_Exception()
        {
            // arrange
            const string email = "test@someemail.com";
            const string firstName = "";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);
            
            // assert
            Assert.Throws<ArgumentNullException>(constructProfile);
        }

        /// <summary>
        /// Test that an empty LastName will throw an exception
        /// </summary>
        [Fact]
        public void EmptyLastName_Exception()
        {
            // arrange
            const string email = "test@someemail.com";
            const string firstName = "SpongeBob";
            const string lastName = "";
            DateTime dob = new DateTime(1988, 8, 8);

            // act
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);
            
            // assert
            Assert.Throws<ArgumentNullException>(constructProfile);
        }

        /// <summary>
        /// Test that an empty BirthDate will throw an exception
        /// </summary>
        [Fact]
        public void EmptyDOB_Exception()
        {
            // arrange
            const string email = "test@someemail.com";
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dobDateTime = new DateTime();

            // act
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dobDateTime);
            
            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that setting an email greater than 45 characters fails
        /// </summary>
        /// <param name="email"></param>
        [Theory]
        [InlineData("reallyreallyreallyreallyreallyreallyreallylongemail@email.com")]
        [InlineData("aaaaaaaaaaaaabbbbbbbbbbbcccccccccccccccccccccccccc@zmail.com")]
        public void TooLongEmail_Fails(string email) 
        {
            // arrange
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that setting an email shorter than 5 characters fails
        /// </summary>
        /// <param name="email"></param>
        [Theory]
        [InlineData("1@.a")]
        [InlineData("g@m.")]
        public void TooShortEmail_Fails(string email) 
        {
            // arrange
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that setting a FirstName longer than 25 characters fails
        /// </summary>
        /// <param name="firstName"></param>
        [Theory]
        [InlineData("JohnJacobJenkleHeimerSchmidt")]
        [InlineData("Anotherreallylongfirstname")]
        public void TooLongFirstName_Fails(string firstName) 
        {
            // arrange
            const string email = "SpongeBob@network.com";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that setting a LastName longer than 25 characters fails
        /// </summary>
        /// <param name="lastName"></param>
        [Theory]
        [InlineData("Reallyaveryveryveryverylongfirstname")]
        [InlineData("Markantonyceasaraugustusthefourth")]
        public void TooLongLastName_Fails(string lastName) 
        {
            // arrange
            const string email = "SpongeBob@network.com";
            const string firstName = "SpongeBob";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that setting a LastName shorter than 2 characters fails
        /// </summary>
        /// <param name="lastName"></param>
        [Theory]
        [InlineData("J")]
        [InlineData("K")]
        public void TooShortLastName_Fails(string lastName) 
        {
            // arrange
            const string email = "SpongeBob@network.com";
            const string firstName = "SpongeBob";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// If user is less than 13 years old throws an argument exception
        /// </summary>
        /// <param name="birthDate">date of birth as a string</param>
        [Theory]
        [InlineData("5/11/2011")]
        [InlineData("2/28/2010")]
        [InlineData("12/31/2009")]
        public void InvalidBirthdate_ThrowsException(string birthDate)
        {
            // arrange
            const string email = "test@someemail.com";
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";

            // act
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, DateTime.Parse(birthDate));
            
            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }

        /// <summary>
        /// Test that EntityProfile status works
        /// </summary>
        [Theory]
        [InlineData("Here is a random status")]
        [InlineData("Something something coffee something")]
        public void ProfileStatus_Pass(string status)
        {
            // arrange
            EntityProfile profile = new EntityProfile();

            // act
            profile.Status = status;
            
            // assert
            Assert.Equal(status, profile.Status);
        }

        /// <summary>
        /// Test that an invalid (too long) EntityProfile status throws exception
        /// </summary>
        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur vel ultrices tellus, sed malesuada eros. Proin sed sollicitudin tortor. Fusce dictum lorem non tellus imperdiet rhoncus. Nunc maximus justo non enim vestibulum, eu pellentesque augue accumsan. Donec purus elit, dignissim id varius elementum, suscipit vitae nibh.")]
        public void ProfileStatusTooLong_ThrowsException(string status)
        {
            // arrange
            const string email = "SpongeBob@network.com";
            const string firstName = "SpongeBob";
            const string lastName = "SquarePants";
            DateTime dob = new DateTime(1988, 8, 8);

            // act 
            EntityProfile constructProfile() => new EntityProfile(email, firstName, lastName, dob, status);

            // assert
            Assert.Throws<ArgumentException>(constructProfile);
        }
    }
}
