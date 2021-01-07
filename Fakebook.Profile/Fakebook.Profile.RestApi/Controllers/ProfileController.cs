using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Fakebook.Profile.Domain;
using Fakebook.Profile.RestApi.ApiModel;

namespace Fakebook.Profile.RestApi.Controllers
{
    /// <summary>
    /// Controller that handles routes/actions relating to profiles
    /// </summary>
    [Route("api/profiles")]
    //TODO: uncomment when okta is set up 
    //[Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileRepository _repository;

        /// <summary>
        /// Contructor method for creating a Profile Controller
        /// </summary>
        /// <param name="repository">Instance of an IRepository interface that allows for the class to store through different mediums</param>
        public ProfileController(IProfileRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Action method that handles getting multiple profiles via their emails;
        /// PUT: /api/profiles/selection/{emails}
        /// </summary>
        /// <param name="emails">A collection of emails as strings to get the profiles</param>
        /// <returns>A collection of profiles converted to API Models</returns>
        [HttpGet("selection/{emails}")]
        public async Task<ActionResult<IEnumerable<ProfileApiModel>>> SelectProfilesAsync([FromBody] IEnumerable<string> emails)
        {
            //var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Action method that handles getting a single user by their email;
        /// GET: /api/profiles/{profileEmail}
        /// </summary>
        /// <param name="profileEmail">The email of the user being retrieved</param>
        /// <returns>If found, a profile API model version of the profile; if not, it returns a NotFound()</returns>
        [HttpGet("{profileEmail}")]
        public async Task<ActionResult<ProfileApiModel>> GetAsync(string profileEmail = null)
        {
            // var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Action method that is used for creating a new profile;
        /// POST: /api/profiles/
        /// </summary>
        /// <param name="apiModel">The data of the profile to be created</param>
        /// <returns>Created if the model was created successfully, otherwise a 400-based status code</returns>
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProfileApiModel apiModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a user profile with a given email;
        /// PUT: /api/profiles/{profileEmail}
        /// </summary>
        /// <param name="apiModel">The data to update the currect user with, if it exists</param>
        /// <returns>200 Ok if the process goes successfully; elsewise a 400-based status code</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(string profileEmail, [FromBody] ProfileApiModel apiModel)
        {
            throw new NotImplementedException();
        }
    }
}
