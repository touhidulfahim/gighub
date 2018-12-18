using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GighubApp.Models;
using GighubApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace GighubApp.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context=new ApplicationDbContext();
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId==userId && g.GigDate > DateTime.Now)
                .Include(g=>g.Genre)
                .ToList();
            return View(gigs);
        }
        

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGig = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading="Gigs I am Attending"
            };
            return View("Gigs", viewModel);
        }



       [HttpGet]
        public ActionResult AddGigs()
        {
            var viewMolde = new GigViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewMolde);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGigs(GigViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                GigDate = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine","Gigs");
        }
    }
}