using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain;
using Fakebook.Profile.RestApi.ApiModel;
using Fakebook.Profile.RestApi.Controllers;
using Fakebook.Profile.UnitTests.TestData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.APITests
{
    public class ProfilePictureControllerTests
    {
        [Fact]
        public async Task uploadValidImageSize()
        {
            // arrange
            Mock<IConfiguration> mockedStorageConfiguration = new();
            Mock<IStorageService> mockedStorageService = new();
            mockedStorageService
                .Setup(x => x.UploadFileContentAsync(It.IsAny<Stream>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .Returns(Task.FromResult(new Uri("https://www.fake.com")));
            byte[] data = new byte[1000];
            var stream = new MemoryStream(data);
            var file = new FormFile(stream, 0, 0, "Data", "dummy.jpeg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            var controller = new ProfilePictureController(mockedStorageService.Object, new NullLogger<ProfileController>(), mockedStorageConfiguration.Object)
            {
                ControllerContext = controllerContext
            };

            // act
            var result = await controller.uploadProfilePicture();

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task uploadInvalidImageSize()
        {
            // arrange
            Mock<IConfiguration> mockedStorageConfiguration = new();
            Mock<IStorageService> mockedStorageService = new();
            mockedStorageService
                .Setup(x => x.UploadFileContentAsync(It.IsAny<Stream>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .Returns(Task.FromResult(new Uri("https://www.fake.com")));
            byte[] data = new byte[2 * 1024 * 1024];
            var stream = new MemoryStream(data);
            var file = new FormFile(stream, 0, 1000, "Data", "dummy.jpeg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            file.ContentType = "image/jpeg";

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            var controller = new ProfilePictureController(mockedStorageService.Object, new NullLogger<ProfileController>(), mockedStorageConfiguration.Object)
            {
                ControllerContext = controllerContext
            };

            // act
            var result = await controller.uploadProfilePicture();
        }
    }
}
