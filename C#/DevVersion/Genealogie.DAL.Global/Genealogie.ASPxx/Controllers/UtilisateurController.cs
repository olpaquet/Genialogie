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
            IEnumerable<UtilisateurIndex> lu = usa.Donner().Select(j => new UtilisateurIndex(j));
            return View(lu);            
        }

        public ActionResult Details(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            UtilisateurDetails u = new UtilisateurDetails(usa.Donner(id));
            return View(u);

        }
    }
}