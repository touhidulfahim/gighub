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
            var notification = new Notification
            {
                NotificationDate = DateTime.Now,
                Gig = gigs,
                Type = NotificationType.GigCanceled
            };
            var attendees = _context.Attendances
                .Where(a => a.GigId == gigs.GigId)
                .Select(a => a.Attendee)
                .ToList();
            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }
            gigs.IsCanceled = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}
