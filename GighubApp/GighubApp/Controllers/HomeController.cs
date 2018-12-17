using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using GighubApp.Models;
using GighubApp.ViewModels;

namespace GighubApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcominggig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g=>g.Genre)
                .Where(g => g.GigDate > DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpcomingGig = upcominggig,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gig"
            };

            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}