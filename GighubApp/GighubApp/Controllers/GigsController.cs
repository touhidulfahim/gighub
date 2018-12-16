using System;
using System.Collections.Generic;
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
            return RedirectToAction("Index","Home");
        }
    }
}