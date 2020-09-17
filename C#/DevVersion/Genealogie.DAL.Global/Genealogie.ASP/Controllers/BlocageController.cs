using Genealogie.ASP.Conversion;
using Genealogie.ASP.Models;
using Genealogie.ASP.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genealogie.ASP.Controllers
{
    public class BlocageController : Controller
    {
        // GET: Blocage
        [HttpGet]
        public ActionResult Index()
        {
            BlocageServiceAPI rs = new BlocageServiceAPI();
            IEnumerable<BlocageIndex> ri = rs.Donner().Select(j => new BlocageIndex(j));
            return View(ri);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BlocageServiceAPI rsa = new BlocageServiceAPI();
            Blocage r = rsa.Donner(id);
            BlocageDetails rd = new BlocageDetails(r);
            return View(rd);
        }

        [HttpGet]
        public ActionResult Creer()
        {
            BlocageCreation r = new BlocageCreation();
            return View(r);
        }

        [HttpPost]
        public ActionResult Creer(BlocageCreation e)
        {
            if (ModelState.IsValid)
            {
                BlocageServiceAPI rsa = new BlocageServiceAPI();
                int i = rsa.Creer(e.VersBlocage());
                if (i > 0) return RedirectToAction("Index");
            }
            return View(e);

        }

        [HttpGet]
        public ActionResult Modifier(int id)
        {
            BlocageServiceAPI rs = new BlocageServiceAPI();
            BlocageModification r = new BlocageModification(rs.Donner(id));
            return View(r);
        }

        [HttpPost]
        public ActionResult Modifier(int id, BlocageModification rm)
        {
            if (ModelState.IsValid)
            {
                BlocageServiceAPI rsa = new BlocageServiceAPI();
                Blocage r = rm.VersBlocage();
                bool b = rsa.Modifier(id, r);
                if (b) return RedirectToAction("Index");
            }

            return View(rm);
        }

        [HttpGet]
        public ActionResult Supprimer(int id)
        {
            BlocageServiceAPI rsa = new BlocageServiceAPI();
            BlocageDetails r = new BlocageDetails(rsa.Donner(id));
            return View(r);
        }

        [HttpPost]
        public ActionResult Supprimer(int id, BlocageDetails r)
        {
            if (ModelState.IsValid)
            {
                BlocageServiceAPI rsa = new BlocageServiceAPI();
                bool b = rsa.Supprimer(id);
                if (b) return RedirectToAction("Index");
            }
            return View(r);
        }

        [HttpGet]
        public ActionResult Activer(int id)
        {
            BlocageServiceAPI rsa = new BlocageServiceAPI();
            bool b = rsa.Activer(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Desactiver(int id)
        {
            BlocageServiceAPI rsa = new BlocageServiceAPI();
            bool b = rsa.Desactiver(id);
            return RedirectToAction("Index");
        }
    }
}