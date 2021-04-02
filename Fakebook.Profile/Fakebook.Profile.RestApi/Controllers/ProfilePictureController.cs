using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fakebook.Profile.RestApi.Controllers
{
    /// <summary>
    /// A controller for image uploading. 
    /// </summary>
    [Route("api/ProfilePicture")]
    [ApiController]
    [Authorize]
    public class ProfilePictureController : ControllerBase
    {
        /// <summary>
        /// Service for talking to the backend.
        /// </summary>
        private readonly IStorageService _storageService;
        private readonly ILogger<ProfileController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for a new instance of the controller.
        /// </summary>
        /// <param name="storageService">Service for uploading files.</param>
        /// <param name="logger">Logger for logging errors and information.</param>
        public ProfilePictureController(IStorageService storageService, ILogger<ProfileController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _storageService = storageService;
        }

        // POST api/ProfilePicture
        /// <summary>
        /// Endpoint for uploading an image to the service's storage.
        /// </summary>
        /// <returns>An Http response.</returns>
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadProfilePicture()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                _logger.LogError("File given to image posting was null.");
                return BadRequest();
            }

            string extension = file.FileName
                    .Split('.')
                    .Last()
                    .ToUpper();

            var validExtensions = new List<string> { "PNG", "JPEG", "GIF", "JPG", "SVG", "WEBP", "AVIF", "APNG" };
            // validate file extension to be valid image
            if (!validExtensions.Contains(extension))
            {
                _logger.LogError("File is not a valid image.");
                return BadRequest();
            }

            // generate a random guid from the file name
            var newFileName = $"{Request.Form["userId"]}-{Guid.NewGuid()}.{extension}";
            _logger.LogInformation($"New file named to be uploaded, {newFileName}");

            // use the stream, and allow for it to close once this scope exits
            using var stream = file.OpenReadStream();

            var containerName = _configuration[ProfileConfigOptions.ProfileConfig];

            var result = await _storageService.UploadFileContentAsync(
                stream,
                containerName,
                file.ContentType,
                newFileName);

            _logger.LogInformation($"New file uploaded, {newFileName}");

            return Created(result, null);
        }
    }
}
