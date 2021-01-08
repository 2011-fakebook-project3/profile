using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Fakebook.Profile.DataAccess.Services.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fakebook.Profile.RestApi.Controllers
{
    [Route("api/ProfilePicture")]
    [ApiController]
    [Authorize]
    public class ProfilePictureController : ControllerBase
    {
        private IStorageService _blobService;

        public ProfilePictureController(IStorageService blobService)
        {

            _blobService = blobService;
        }


        // POST api/<ProfilePictureController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
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

                var result = await _blobService.UploadFileContentAsync(
                        file.OpenReadStream(),
                        "fakebook",
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
