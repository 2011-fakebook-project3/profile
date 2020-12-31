using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Profile.RestApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class ProfileController : ControllerBase
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
