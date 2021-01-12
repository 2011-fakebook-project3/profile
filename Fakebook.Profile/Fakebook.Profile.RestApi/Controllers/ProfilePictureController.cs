using System;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IStorageService _storageService;
        private readonly ILogger<ProfileController> _logger;

        /// <summary>
        /// Constructor for a new instance of the controller.
        /// </summary>
        /// <param name="storageService">Service for uploading files.</param>
        /// <param name="logger">Logger for logging errors and information.</param>
        public ProfilePictureController(IStorageService storageService, ILogger<ProfileController> logger)
        {
            _logger = logger;
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
        public async Task<ActionResult> Post()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                _logger.LogError("File given to image posting was null.");
                return BadRequest();
            }
            try
            {
                // generate a random guid from the file name
                string extension = file
                    .FileName
                        .Split('.')
                        .Last();
                string newFileName = $"{Request.Form["userId"]}-{Guid.NewGuid()}.{extension}";
                _logger.LogInformation($"New file named to be uploaded, {newFileName}");

                var result = await _storageService.UploadFileContentAsync(
                        file.OpenReadStream(),
                        ProfileConfiguration.BlobContainerName,
                        file.ContentType,
                        newFileName);

                var toReturn = result.AbsoluteUri;
                _logger.LogInformation($"New file uploaded, {newFileName}");

                return Ok(new { path = toReturn });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
