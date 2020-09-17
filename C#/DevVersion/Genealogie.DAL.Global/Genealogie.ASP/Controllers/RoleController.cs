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
    public class RoleController : Controller
    {
        // GET: Role
        [HttpGet]
        public ActionResult Index()
        {
            RoleServiceAPI rs = new RoleServiceAPI();
            IEnumerable<RoleIndex> ri = rs.Donner().Select(j => new RoleIndex(j));
            return View(ri);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            RoleServiceAPI rsa = new RoleServiceAPI();
            Role r = rsa.Donner(id);
            RoleDetails rd = new RoleDetails(r);
            return View(rd);
        }

        [HttpGet]
        public ActionResult Creer()
        {
            RoleCreation r = new RoleCreation();
            return View(r);
        }

        [HttpPost]
        public ActionResult Creer(RoleCreation e)
        {
            if (ModelState.IsValid)
            {
                RoleServiceAPI rsa = new RoleServiceAPI();
                int i = rsa.Creer(e.VersRole());
                if (i > 0) return RedirectToAction("Index");
            }
            return View(e);

        }

        [HttpGet]
        public ActionResult Modifier(int id)
        {
            RoleServiceAPI rs = new RoleServiceAPI();
            RoleModification r = new RoleModification(rs.Donner(id));
            return View(r);
        }

        [HttpPost]
        public ActionResult Modifier(int id, RoleModification rm)
        {
            if (ModelState.IsValid)
            {
                RoleServiceAPI rsa = new RoleServiceAPI();
                Role r = rm.VersRole();
                bool b = rsa.Modifier(id, r);
                if (b) return RedirectToAction("Index");
            }           

            return View(rm);
        }

        [HttpGet]
        public ActionResult Supprimer(int id)
        {
            RoleServiceAPI rsa = new RoleServiceAPI();
            RoleDetails r = new RoleDetails(rsa.Donner(id));
            return View(r);
        }

        [HttpPost]
        public ActionResult Supprimer(int id,RoleDetails r)
        {
            if (ModelState.IsValid)
            {
                RoleServiceAPI rsa = new RoleServiceAPI();
                bool b = rsa.Supprimer(id);
                if (b) return RedirectToAction("Index");
            }
            return View(r);
        }

        [HttpGet]
        public ActionResult Activer(int id)
        {
            RoleServiceAPI rsa = new RoleServiceAPI();
            bool b = rsa.Activer(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Desactiver(int id)
        {
            RoleServiceAPI rsa = new RoleServiceAPI();
            bool b = rsa.Desactiver(id);
            return RedirectToAction("Index");
        }
    }
}