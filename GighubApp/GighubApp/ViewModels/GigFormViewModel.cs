﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GighubApp.Controllers;
using GighubApp.Models;

namespace GighubApp.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }

        public string Action
        {

           get
            {
                Expression<Func<GigsController, ActionResult>> update =
                (u => u.Update(this));

                Expression<Func<GigsController, ActionResult>> create =
                                (u => u.Create(this));
                var action=(Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }
    }
}