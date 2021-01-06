using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.RestApi.ApiModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Profile.RestApi.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        // PUT: /api/profile/selection/{emails}
        [HttpGet("selection/{emails}")]
        public ActionResult<IEnumerable<ProfileApiModel>> SelectProfiles([FromBody] IEnumerable<string> emails)
        {
            throw new NotImplementedException();
        }

        // GET: /api/profile/{userEmail}
        [HttpGet("{userEmail}")]
        public ActionResult<ProfileApiModel> Get(string userEmail = null)
        {
            throw new NotImplementedException();
        }

        // POST: /api/profile/
        [HttpPost]
        public ActionResult Create([FromBody] ProfileApiModel apiModel)
        {
            throw new NotImplementedException();
        }

        // PUT: /api/profile/
        [HttpPut]
        public ActionResult Update([FromBody] ProfileApiModel apiModel)
        {
            throw new NotImplementedException();
        }
    }
}
