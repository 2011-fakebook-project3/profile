using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.RestApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.ApiTests
{
    public class ProfilePictureControllerTests
    {
        [Theory]
        [InlineData("jpeg")]
        [InlineData("png")]
        [InlineData("gif")]
        [InlineData("jpg")]
        [InlineData("svg")]
        [InlineData("webp")]
        [InlineData("avif")]
        [InlineData("apng")]
        [InlineData("jfif")]
        [InlineData("pjpeg")]
        [InlineData("pjp")]
        public async Task UploadValidImageExtension(string extension)
        {
            await UploadImageExtension(extension, true);
        }

        [Theory]
        [InlineData("json")]
        [InlineData("xml")]
        [InlineData("bmp")]
        public async Task UploadInvalidImageExtension(string extension)
        {
            await UploadImageExtension(extension, false);
        }

        internal static async Task UploadImageExtension(string extension, bool successExpected)
        {
            // arrange
            Mock<IConfiguration> mockedStorageConfiguration = new();
            Mock<IStorageService> mockedStorageService = new();
            mockedStorageService
                .Setup(x => x.UploadFileContentAsync(It.IsAny<Stream>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .Returns(Task.FromResult(new Uri("https://www.fake.com")));
            byte[] data = new byte[1000];
            var stream = new MemoryStream(data);
            var file = new FormFile(stream, 0, 1000, "Data", $"dummy.{extension}")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
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
            var result = await controller.UploadProfilePicture();

            // assert
            if (successExpected)
            {
                Assert.IsAssignableFrom<CreatedResult>(result);
            }
            else
            {
                Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            }
        }
    }
}