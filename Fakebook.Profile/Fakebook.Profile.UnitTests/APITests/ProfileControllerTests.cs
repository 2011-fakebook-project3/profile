using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain;
using Fakebook.Profile.RestApi.ApiModel;
using Fakebook.Profile.RestApi.Controllers;
using Fakebook.Profile.UnitTests.TestData;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.APITests
{
    public class ProfileControllerTests
    {
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0

        // all dummy data
        readonly string dummyEmail = "test@gmail.com";
        readonly string[] dummyEmails = { "test1@gmail.com", "test2@gmail.com" };
        readonly ProfileApiModel dummyInvalidProfile = new ProfileApiModel();

        /// <summary>
        /// Get's a valid profile with dummy data for the repo.
        /// </summary>
        /// <remarks>
        /// Intended for getting data to try and retrieve from a moq object durring testing.
        /// </remarks>
        /// <returns>A valid user profile.</returns>
        private static DomainProfile GetValidDummyProfile()
        {
            DomainProfile profile = new DomainProfile(
                email: GenerateRandom.Email(),
                firstName: GenerateRandom.String(),
                lastName: GenerateRandom.String()
            )
            {
                PhoneNumber = GenerateRandom.PhoneNumber(),
                LastName = GenerateRandom.String(),
                BirthDate = DateTime.Now
            };
            return profile;
        }

        /// <summary>
        /// Get's a valid API profile with dummy data for testing.
        /// </summary>
        /// <remarks>
        /// Intended to be returned by moq objects for testing.
        /// </remarks>
        /// <returns>A valid API profile.</returns>
        private static ProfileApiModel GetValidAPIDummy()
        {
            ProfileApiModel profile = new ProfileApiModel
            {
                Email = GenerateRandom.Email(),
                PhoneNumber = GenerateRandom.PhoneNumber(),
                FirstName = GenerateRandom.String(),
                LastName = GenerateRandom.String(),
                BirthDate = DateTime.Now
            };
            return profile;
        }


        #region GetProfile
        /// <summary>
        /// Getting a specific profile from the controller works.
        /// </summary>
        /// <returns>A Task</returns>
        [Fact]
        public async Task GetSpecificProfileWorks()
        {
            // arrange
            var mockedProfileRepository = new Mock<IProfileRepository>();
            var mockedStorageService = new Mock<IStorageService>();
            var controller = new ProfileController(
                mockedProfileRepository.Object,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );

            DomainProfile dummy = GetValidDummyProfile();
            mockedProfileRepository
                .Setup(mpr => mpr.GetProfileAsync(It.IsAny<string>()))
                .ReturnsAsync(dummy);

            // act
            var result = await controller.GetAsync(dummyEmail);

            // assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<ProfileApiModel>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<ProfileApiModel>(okObjectResult.Value);
            Assert.Equal(dummy.Email, returnValue.Email);
            Assert.Equal(dummy.BirthDate, returnValue.BirthDate);
            Assert.Equal(dummy.FirstName, returnValue.FirstName);
            Assert.Equal(dummy.LastName, returnValue.LastName);
        }

        /// <summary>
        /// ensure you can get multiple profiles from the controller
        /// </summary>
        [Fact]
        public async Task GetSetofProfilesWorks()
        {
            // arrange
            var mockedProfileRepository = new Mock<IProfileRepository>();
            var mockedStorageService = new Mock<IStorageService>();
            var controller = new ProfileController(
                mockedProfileRepository.Object,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );

            var expectedResults = new List<DomainProfile>();
            for (int i = 0; i < 5; i++)
            {
                expectedResults.Add(GetValidDummyProfile());
            }

            // Set it up to mimic the profile found
            mockedProfileRepository
                .Setup(mpr => mpr.GetProfilesByEmailAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(expectedResults);

            // act
            var result = await controller.SelectProfilesAsync(dummyEmails);

            // assert    
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProfileApiModel>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValues = Assert.IsAssignableFrom<IEnumerable<ProfileApiModel>>(okObjectResult.Value);

            foreach (DomainProfile dummy in expectedResults)
            {
                //get the matching profile
                var matchedProfile = returnValues.FirstOrDefault(entry => entry.Email == dummy.Email);

                //assert equals
                Assert.Equal(dummy.BirthDate, matchedProfile.BirthDate);
                Assert.Equal(dummy.FirstName, matchedProfile.FirstName);
                Assert.Equal(dummy.LastName, matchedProfile.LastName);
            }
        }
        #endregion

        #region CreateProfile
        /// <summary>
        /// Tests that you can create a profile with valid data.
        /// </summary>
        [Fact]
        public async Task CreateProfileWorksForValidData()
        {
            // arrange
            var mockedProfileRepository = new Mock<IProfileRepository>();
            var mockedStorageService = new Mock<IStorageService>();
            var controller = new ProfileController(
                mockedProfileRepository.Object,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );

            ProfileApiModel dummy = GetValidAPIDummy();
            mockedProfileRepository
                .Setup(repo => repo.CreateProfileAsync(It.IsAny<DomainProfile>()))
                .Verifiable();

            // act
            var result = await controller.CreateAsync(dummy);

            // assert
            Assert.NotNull(result);
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            // no return type -> no.Result, no.Value
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            mockedProfileRepository.Verify(x => x.CreateProfileAsync(It.IsAny<DomainProfile>()), Times.Once);
        }

        /// <summary>
        /// Creating an invalid profile fails because of the data not meeting constraints.
        /// </summary>
        [Fact]
        public async Task CreateProfileReturnsErrorForInvalidData()
        {
            // arrange
            var mockedProfileRepository = new Mock<IProfileRepository>();
            var mockedStorageService = new Mock<IStorageService>();
            var controller = new ProfileController(
                mockedProfileRepository.Object,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );

            // throw set up will be shortcircuited by converter
            // remove replicated exception handling
            mockedProfileRepository.Setup(x => x.CreateProfileAsync(It.IsAny<DomainProfile>()))
                .ThrowsAsync(new ArgumentException()) // never reached
                .Verifiable();

            // act
            var result = await controller.CreateAsync(dummyInvalidProfile);

            // assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            // shortcircuited by converter
            mockedProfileRepository.Verify(x => x.CreateProfileAsync(It.IsAny<DomainProfile>()), Times.Never);
        }
        #endregion
    }
}