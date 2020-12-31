using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Profile.RestApi.Controllers
{
    //TODO: uncomment when okta is set up 
    [Authorize]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {

        // GET: Profile/Create
        [HttpGet]
        [Route("/Details/{email}")]
        public ActionResult Details([FromRoute] string email)
        {
            var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;

            // return default ok for now
            return Ok();
        }

        // GET: Profile/Create
        [HttpPost]
        public ActionResult Create() {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;

            // return default ok for now
            return Ok();
        }


        // POST: Profile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            // return default ok for now
            return Ok();
        }
    }
}
