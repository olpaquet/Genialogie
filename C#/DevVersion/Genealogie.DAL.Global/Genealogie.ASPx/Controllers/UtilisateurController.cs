using Genealogie.ASP.Models;
using Genealogie.ASP.ServicesAPI;
using Newtonsoft.Json;
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
            APIServiceUtilisateur asu = new APIServiceUtilisateur("http://localhost:61297/api/");

            IEnumerable<Utilisateur> fo = asu.Donner().Select(j=>(Utilisateur)j);
            return View(fo);
        }
    }
}