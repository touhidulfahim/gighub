using System;
using System.ComponentModel.DataAnnotations;

namespace GighubApp.Models
{
    public class Notification
    {
        public int NotificationId { get; private set; }
        public DateTime NotificationDate { get; private set; }
        public DateTime? OriginalDate { get; private set; }
        public NotificationType Type { get; private set; }
        public string OriginalVanue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
            
        }
        

        private Notification(NotificationType type, Gig gig)
        {
            if (gig==null)
            throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            NotificationDate=DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }
        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification= new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDate = originalDateTime;
            notification.OriginalVanue = originalVenue;
            return notification;
        }
        
        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

    }
}