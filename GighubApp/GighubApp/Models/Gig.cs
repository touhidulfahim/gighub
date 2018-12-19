using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GighubApp.Models
{
    public class Gig
    {
        public int GigId { get; set; }
        public bool IsCanceled { get; private set; }
        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime GigDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }
        
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances=new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;
            var notification = Notification.GigCanceled(this);
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notofy(notification);
            }
        }

        public void Modify(DateTime datetime, string venue, byte genre)
        {
            var notification= Notification.GigUpdated(this, GigDate, Venue);

            Venue = venue;
            GigDate = datetime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a=>a.Attendee))
            {
                attendee.Notofy(notification);
            }
        }
    }
}