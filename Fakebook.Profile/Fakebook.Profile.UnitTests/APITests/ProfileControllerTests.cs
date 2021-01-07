using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Moq;
using Fakebook.Profile.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Fakebook.Profile.Domain;
using Fakebook.Profile.UnitTests.TestData;
using Fakebook.Profile.RestApi.ApiModel;
using System.Collections;

namespace Fakebook.Profile.UnitTests.APITests
{
    public class ProfileControllerTests
    {
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0

        #region GetProfile
        /// <summary>
        /// Getting a specific profile from the controller works.
        /// </summary>
        /// <returns>A Task</returns>
        [Fact]
        public async Task GetSpecificProfileWorks()
        {
            string email = GenerateRandom.Email();
            DomainProfile profile = new DomainProfile
            {
                Email = email,
                PhoneNumber = GenerateRandom.PhoneNumber(),
                FirstName = GenerateRandom.String(),
                LastName = GenerateRandom.String(),
                BirthDate = DateTime.Now
            };

            //arrange
            var mockedProfileRepository = new Mock<IRepository>();
            var controller = new ProfileController(mockedProfileRepository.Object);
            mockedProfileRepository
                .Setup(mpr => mpr.GetProfileAsync(It.IsAny<string>()))
                .ReturnsAsync(profile);

            //act
            var result = await controller.GetAsync(email);

            //assert
            Assert.NotNull(result);

            var model = Assert.IsAssignableFrom<ProfileApiModel>(result);
            Assert.Equal(email, model.Email);
            Assert.Equal(profile.BirthDate, model.BirthDate);
            Assert.Equal(profile.FirstName, model.FirstName);
            Assert.Equal(profile.LastName, model.LastName);
        }

        private static readonly string[] emails =
        {
            GenerateRandom.Email(),
            GenerateRandom.Email(),
            GenerateRandom.Email()
        };

        /// <summary>
        /// ensure you can get multiple profiles from the controller
        /// </summary>
        [Fact]
        public async void GetSetofProfilesWorks()
        {
            //arrange
            var mockedProfileRepository = new Mock<IRepository>();
            var controller = new ProfileController(mockedProfileRepository.Object);
            var expectedResults = new List<DomainProfile>();

            string[] emails = new[]
            {
                GenerateRandom.Email(),
                GenerateRandom.Email(),
                GenerateRandom.Email(),
                GenerateRandom.Email(),
                GenerateRandom.Email()
            };

            foreach (var email in emails)
            {
                //generate profile, add to list
                expectedResults.Add(
                        //generate new domain profile
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            Email = email,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime()
                        }
                    );
            }

            // Set it up to mimic the profile found
            mockedProfileRepository
                .Setup(mpr => mpr.GetProfilesByEmailAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(expectedResults);

            //act
            var result = await controller.SelectProfilesAsync(emails);

            //assert
            Assert.NotNull(result);

            //assert 200 ok
            Assert.IsType<OkResult>(result);

            var value = result.Value.ToList();

            //assert data matches what it should
            foreach (DomainProfile profile in expectedResults)
            {
                //get the matching profile
                var resultprofile = result.Value.FirstOrDefault(entry => entry.Email == profile.Email);

                //assert equals
                Assert.Equal(profile.Email, resultprofile.Email);
                Assert.Equal(profile.BirthDate, resultprofile.BirthDate);
                Assert.Equal(profile.FirstName, resultprofile.FirstName);
                Assert.Equal(profile.LastName, resultprofile.LastName);
            }
        }
        #endregion

        #region CreateProfile
        //CreateAsync
        /// <summary>
        /// Tests that you can create a profile with valid data.
        /// </summary>
        [Fact]
        public async void CreateProfileWorksForValidData()
        {
            //arrange
            DomainProfile profile = new DomainProfile(GenerateRandom.Email());

            var mockedProfileRepository = new Mock<IRepository>();
            mockedProfileRepository
                .Setup(repo => repo.CreateProfileAsync(It.IsAny<DomainProfile>()))
                .Verifiable();

            var controller = new ProfileController(mockedProfileRepository.Object);
            
            //act
            var result = await controller.CreateAsync(null); // a profile API model

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            //mockedProfileRepository.Verify();
            mockedProfileRepository.Verify(x => x.CreateProfileAsync(It.IsAny<DomainProfile>()), Times.Once);
        }

        /// <summary>
        /// Creating an invalid profile fails because of the data not meeting constraints.
        /// </summary>
        [Fact]
        public void CreateProfileReturnsErrorForInvalidData()
        {
            //arrange
            var mockedProfileRepository = new Mock<IRepository>();

            mockedProfileRepository.Setup(x => x.CreateProfileAsync(It.IsAny<DomainProfile>()))
                .Throws(new ArgumentException());

            var controller = new ProfileController(mockedProfileRepository.Object);

            ProfileApiModel badProfile = new ProfileApiModel();

            //act
            var result = controller.CreateAsync(badProfile); 

            //assert
            Assert.NotNull(result);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        #endregion

        //UpdateAsync
        #region Update
        [Fact]
        public void UpdateExistingProfileWorks()
        {
            //arrange
            string email = GenerateRandom.Email();
            DomainProfile origProfile = new DomainProfile(email);
            origProfile.FirstName = "bob";
            origProfile.LastName = "lastname";

            /*
             * neccissary???
            DomainProfile changedProfile = new DomainProfile(email);
            changedProfile.FirstName = "Bob";
            changedProfile.LastName = "Person";
            */

            var mockedProfileRepository = new Mock<IRepository>();
            mockedProfileRepository.Setup(repo => repo.UpdateProfileAsync(email, origProfile))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new ProfileController(mockedProfileRepository.Object);

            //act
            var result = controller.SelectProfilesAsync(null); //list of emails

            //assert
            Assert.NotNull(result);
            //assert response code is error or something.
            //200 ok + obj in body matches
        }

        /// <summary>
        /// trying to update a profile not in the repository won't work. 404 not found error.
        /// </summary>
        [Fact]
        public void UpdatingNonexistProfileFails()
        {
            //arrange
            var mockedProfileRepository = new Mock<IRepository>();
            var controller = new ProfileController(mockedProfileRepository.Object);

            //act
            var result = controller.SelectProfilesAsync(null); 

            //assert
            Assert.NotNull(result);
            var notFoundRequestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<SerializableError>(notFoundRequestResult.Value);
        }

        /// <summary>
        /// Calling update with invalid information will fail, and return an error code.
        /// </summary>
        [Fact]
        public void UpdatingWithInvalidInformationFails()
        {
            //arrange
            var mockedProfileRepository = new Mock<IRepository>();
            mockedProfileRepository.Setup(obj => obj.GetProfileAsync(It.IsAny<string>()))
                .Throws(new ArgumentException("Invalid argument (from moq)"));

            var controller = new ProfileController(mockedProfileRepository.Object);

            string email = GenerateRandom.Email();

            ProfileApiModel badmodel = new ProfileApiModel();
            badmodel.Email = email;

            //act
            var result = controller.UpdateAsync(email, badmodel);

            //assert
            Assert.NotNull(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        #endregion
    }
}
