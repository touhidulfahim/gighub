using System;
using System.ComponentModel.DataAnnotations;

namespace GighubApp.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public DateTime NotificationDate { get; set; }
        public DateTime? OriginalDate { get; set; }
        public NotificationType Type { get; set; }
        public string OriginalVanue { get; set; }

        [Required]
        public Gig Gig { get; set; }
    }
}