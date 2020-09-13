using Genealogie.ASP.Models;
using Genealogie.ASP.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genealogie.ASP.Controllers
{
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        public ActionResult Index()
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            IEnumerable<UtilisateurIndex> ieui = usa.Donner().Select(j => new UtilisateurIndex(j));
            return View(ieui);
        }

        public ActionResult Details(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            UtilisateurDetails ud = new UtilisateurDetails(usa.Donner(id));
            return View(ud);
        }
    }
}