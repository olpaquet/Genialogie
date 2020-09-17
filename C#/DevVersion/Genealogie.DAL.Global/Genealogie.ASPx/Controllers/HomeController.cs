using Genealogie.ASP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genealogie.ASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionUtilisateur.AssignerUtilisateur(null);

            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = "Description de ma page";

            return View();
        }

        public ActionResult Contact()
        {
            /*ViewBag.Connexion = SessionUtilisateur.Connexion;*/
            ViewBag.Message = "Ma page de contact";

            return View();
        }
    }
}