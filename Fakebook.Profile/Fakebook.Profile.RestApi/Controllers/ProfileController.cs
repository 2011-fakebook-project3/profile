using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain;
using Fakebook.Profile.RestApi.ApiModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fakebook.Profile.RestApi.Controllers
{
    /// <summary>
    /// Controller that handles routes/actions relating to profiles
    /// </summary>
    [Route("api/profiles")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _repository;
        private readonly IStorageService _storageService;

        private readonly ILogger<ProfileController> _logger;

        /// <summary>
        /// Contructor method for creating a Profile Controller
        /// </summary>
        /// <param name="repository">Instance of an IRepository interface that allows for the class to store through different mediums</param>
        public ProfileController(IProfileRepository repository, IStorageService storageService, ILogger<ProfileController> logger)
        {
            _repository = repository;
            _storageService = storageService;
            _logger = logger;
        }

        /// <summary>
        /// Helper method for getting the password of a currently authenticated user
        /// </summary>
        /// <returns>The email of the user, if they are logged in</returns>
        private string GetUserEmail()
        {
            try
            {
                return User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Action method that handles getting multiple profiles via their emails;
        /// PUT: /api/profiles/selection/{emails}
        /// </summary>
        /// <param name="emails">A collection of emails as strings to get the profiles</param>
        /// <returns>A collection of profiles converted to API Models</returns>
        [HttpGet("selection/{emails}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfileApiModel>>> SelectProfilesAsync([FromBody] IEnumerable<string> emails)
        {
            var results = await _repository.GetProfilesByEmailAsync(emails);

            // convert them to the ApiModel
            return Ok(results
                .Select(p => new ProfileApiModel(p))
                .ToList());
        }


        /// <summary>
        /// Action method that handles getting multiple profiles via their names;
        /// PUT: /api/profiles/search/{name}
        /// </summary>
        /// <param name="name">A name you want to search for to get the profiles</param>
        /// <returns>A collection of profiles converted to API Models</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfileApiModel>>> SelectProfilesByNameAsync([FromQuery] string name)
        {
            if(name == null)
            {
                return BadRequest();
            }
            var results = await _repository.GetProfilesByNameAsync(name);

            // convert them to the ApiModel
            return Ok(results
                .Select(p => new ProfileApiModel(p))
                .ToList());
        }




        /// <summary>
        /// Action method that handles getting a single user by their email;
        /// GET: /api/profiles/{profileEmail}
        /// </summary>
        /// <param name="profileEmail">The email of the user being retrieved</param>
        /// <returns>If found, a profile API model version of the profile; if not, it returns a NotFound()</returns>
        [HttpGet("")]
        [HttpGet("{profileEmail}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileApiModel>> GetAsync(string profileEmail = null)
        {
            // redundant or incorrect??
            string email = profileEmail is not null ? profileEmail : GetUserEmail();

            if (email is null)
            {
                _logger.LogError($"Could not find current user's email. {profileEmail}");
                throw new ArgumentException("Could not find current user's email");
            }

            var result = await _repository.GetProfileAsync(email);
            return Ok(new ProfileApiModel(result));
        }

        /// <summary>
        /// Action method that is used for creating a new profile;
        /// POST: /api/profiles/
        /// </summary>
        /// <param name="apiModel">The data of the profile to be created</param>
        /// <returns>Created if the model was created successfully, otherwise a 400-based status code</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync([FromBody] ProfileApiModel apiModel)
        {
            try
            {
                var domainProfile = apiModel.ToDomainProfile();
                await _repository.CreateProfileAsync(domainProfile);
                return CreatedAtAction(nameof(GetAsync), new { email = apiModel.Email });
            }
            catch (Exception e)
            {
                // return this because the profile could not be created.
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a user profile with a given email;
        /// PUT: /api/profiles/
        /// </summary>
        /// <param name="apiModel">The data to update the currect user with, if it exists</param>
        /// <returns>200 Ok if the process goes successfully; elsewise a 400-based status code</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] ProfileApiModel apiModel)
        {
            string email = GetUserEmail();
            if (email is null)
            {
                return NotFound(email);
            }
            try
            {
                await _repository.UpdateProfileAsync(email, apiModel.ToDomainProfile());
                return Ok();
            }
            catch (ArgumentNullException)
            {
                // cannot find the profile to update
                return NotFound(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                //should be 404?
                return BadRequest();
            }
        }

        [HttpPost("follow/{followEmail}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Follow([EmailAddress] string followEmail)
        {
            // Get emails of each user
            string thisUserEmail = GetUserEmail();
            if (thisUserEmail is null)
            {
                return Unauthorized(thisUserEmail);
            }
            // Get users into domain models
            DomainProfile thisUser;
            DomainProfile followUser;
            try
            {
                var usersQuery = await _repository.GetProfilesByEmailAsync(new List<string> { thisUserEmail, followEmail });
                thisUser = usersQuery.Single(x => x.Email == thisUserEmail);
                followUser = usersQuery.Single(x => x.Email == followEmail);
                // Add following relationships
                thisUser.AddFollow(followEmail);
                followUser.AddFollower(thisUserEmail);
                // Update in the database
                await _repository.UpdateProfileAsync(thisUserEmail, thisUser);
                await _repository.UpdateProfileAsync(followEmail, followUser);
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                return BadRequest();
            }
        }

        [HttpPost("unfollow/{unfollowEmail}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Unfollow([EmailAddress] string unfollowEmail)
        {
            // Get emails of each user
            string thisUserEmail = GetUserEmail();
            if (thisUserEmail is null)
            {
                return Unauthorized(thisUserEmail);
            }
            // Get users into domain models
            DomainProfile thisUser;
            DomainProfile unfollowUser;
            try
            {
                var usersQuery = await _repository.GetProfilesByEmailAsync(new List<string> { thisUserEmail, unfollowEmail });
                thisUser = usersQuery.Single(x => x.Email == thisUserEmail);
                unfollowUser = usersQuery.Single(x => x.Email == unfollowEmail);
                // Add following relationships
                thisUser.RemoveFollowing(unfollowEmail);
                unfollowUser.RemoveFollower(thisUserEmail);
                // Update in the database
                await _repository.UpdateProfileAsync(thisUserEmail, thisUser);
                await _repository.UpdateProfileAsync(unfollowEmail, unfollowUser);
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                return BadRequest();
            }
        }
    }
}
