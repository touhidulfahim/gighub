using System.Collections.Generic;
using GighubApp.Models;

namespace GighubApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingGig { get; set; }
        public bool ShowActions { get; set; }
    }
}