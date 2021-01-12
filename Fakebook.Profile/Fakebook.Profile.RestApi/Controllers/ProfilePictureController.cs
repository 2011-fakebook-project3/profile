using System;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// constructor for a new instance of the controller/
        /// </summary>
        /// <param name="storageService">Service for uploading files.</param>
        public ProfilePictureController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        // POST api/ProfilePicture
        /// <summary>
        /// Endpoint for uploading an image to the service's storage.
        /// </summary>
        /// <returns>An Http response</returns>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> Post()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
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

                var result = await _storageService.UploadFileContentAsync(
                        file.OpenReadStream(),
                        ProfileConfiguration.BlobContainerName,
                        file.ContentType,
                        newFileName);

                var toReturn = result.AbsoluteUri;

                return Ok(new { path = toReturn });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
