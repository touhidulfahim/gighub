using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GighubApp.Models
{
    public class Gig
    {
        public int GigId { get; set; }
        public bool IsCanceled { get; set; }
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
    }
}