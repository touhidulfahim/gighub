using System.Linq;
using System.Web.Http;
using GighubApp.Dtos;
using GighubApp.Models;
using Microsoft.AspNet.Identity;

namespace GighubApp.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context=new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(g=>g.AttendeeId==userId && g.GigId==dto.GigId))
            {
                return BadRequest("The attendance already exists!");
            }
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}