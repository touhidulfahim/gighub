using System;
using GighubApp.Models;

namespace GighubApp.Dtos
{
    public class NotificationDto
    {
        public DateTime NotificationDate { get; set; }
        public DateTime? OriginalDate { get; set; }
        public NotificationType Type { get; set; }
        public string OriginalVanue { get; set; }
        public GigDto Gig { get; set; }

    }
}