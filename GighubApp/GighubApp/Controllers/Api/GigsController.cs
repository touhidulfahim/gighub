using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GighubApp.Models;
using Microsoft.AspNet.Identity;

namespace GighubApp.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context=new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Single(g => g.GigId == id && g.ArtistId == userId);
            if (gigs.IsCanceled)
            {
                return NotFound();
            }
            gigs.IsCanceled = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}
