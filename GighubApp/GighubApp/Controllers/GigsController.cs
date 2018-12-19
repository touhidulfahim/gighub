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
            _context = new ApplicationDbContext();
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId &&
                 g.GigDate > DateTime.Now &&
                !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
        }


        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGig = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I am Attending"
            };
            return View("Gigs", viewModel);
        }



        [HttpGet]
        public ActionResult Create()
        {
            var viewMolde = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a gig"
            };
            return View("GigForm", viewMolde);
        }

        [HttpGet]
        public ActionResult EditGigs(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.GigId == id && g.ArtistId == userId);
            var viewMolde = new GigFormViewModel
            {
                Id = gig.GigId,
                Genres = _context.Genres.ToList(),
                Date = gig.GigDate.ToString("d MMM yyyy"),
                Time = gig.GigDate.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Update a gig"
            };
            return View("GigForm", viewMolde);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                formViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", formViewModel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                GigDate = formViewModel.GetDateTime(),
                GenreId = formViewModel.Genre,
                Venue = formViewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                formViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", formViewModel);
            }
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.GigId == formViewModel.Id && g.ArtistId == userId);

            gigs.Modify(formViewModel.GetDateTime(), formViewModel.Venue, formViewModel.Genre);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

    }
}