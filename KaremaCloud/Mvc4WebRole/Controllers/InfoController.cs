﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mvc4WebRole.Filters;
using Mvc4WebRole.Models;

namespace Mvc4WebRole.Controllers
{
    [EnhancedAuthorize(Roles = "Reader,Administrator")]
    public class InfoController : Controller
    {
        private readonly RecipeDbContext repository = new RecipeDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            using ( var ctx = new UsersContext() )
            {
                var userList = ctx.UserProfiles.ToList();
                ViewBag.Users = userList.Count();
            }
            ViewBag.Recipes = repository.Recipes.Count();
            ViewBag.Tags = repository.Tags.Count();
            ViewBag.Images = repository.ImageModels.Count();

            return View();
        }

        //
        // GET: /Recipe/
        public ActionResult ByAuthor()
        {
            IEnumerable<IGrouping<String, RecipeModel>> recipes = repository.Recipes.ToList().GroupBy(r => r.Author);
            return View(recipes);
        }

        public ActionResult LastChanged()
        {
            ViewBag.Title = "Zuletzt geändert";
            IEnumerable<RecipeModel> recipes = repository.Recipes.ToList().OrderByDescending(r => r.LastTimeChanged);
            return View("ByDate", recipes);
        }

        public ActionResult LastCreated()
        {
            ViewBag.Title = "Zuletzt erstellt";
            IEnumerable<RecipeModel> recipes = repository.Recipes.ToList().OrderByDescending(r => r.TimeCreated);
            return View("ByDate", recipes);
        }

        public ActionResult Logs()
        {
            return View(SessionLogger.Logs);
        }

        public ActionResult DeleteLogs()
        {
            SessionLogger.Logs.Clear();
            SessionLogger.AddLog("Logs deleted");
            return RedirectToAction("Logs");
        }

    }
}