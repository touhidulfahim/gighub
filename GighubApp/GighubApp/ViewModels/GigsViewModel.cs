using System.Collections.Generic;
using GighubApp.Models;

namespace GighubApp.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGig { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }

    }
}