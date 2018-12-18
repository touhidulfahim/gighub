using System.Linq;
using System.Web.Http;
using GighubApp.Dtos;
using GighubApp.Models;
using Microsoft.AspNet.Identity;

namespace GighubApp.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController() 
        {
            _context=new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId==f.FollowerId))
            {
                return BadRequest("Following already exists!");
            }
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
        
    }
}
