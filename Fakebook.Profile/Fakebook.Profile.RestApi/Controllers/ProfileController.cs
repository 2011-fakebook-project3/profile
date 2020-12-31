﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Profile.RestApi.Controllers
{
    //TODO: uncomment when okta is set up 
    //[Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile/Create
        public ActionResult Create() {
            // return default ok for now
            return Ok();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
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
