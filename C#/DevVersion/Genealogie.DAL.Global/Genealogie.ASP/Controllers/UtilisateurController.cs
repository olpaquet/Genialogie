﻿using Genealogie.ASP.Conversion;
using Genealogie.ASP.Models;
using Genealogie.ASP.Securite;
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
        [HttpGet]
        public ActionResult Index()
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            IEnumerable<UtilisateurIndex> ieui = usa.Donner().Select(j => new UtilisateurIndex(j));
            return View(ieui);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {            
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            Utilisateur u = usa.Donner(id);            
            UtilisateurDetails ud = new UtilisateurDetails(u);
            ud.SLIRoles = usa.DonnerSLIRoles(id).ToList();

            return View(ud);
        }

        [HttpGet]
        public ActionResult Creer()
        {            
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            UtilisateurCreation u = new UtilisateurCreation();
            u.SLIRoles = usa.DonnerSLIRoles((int?)null).ToList();
            return View(u);
        }
        [HttpPost]
        public ActionResult Creer(UtilisateurCreation u)
        {
            if (ModelState.IsValid)
            {
                UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
                Utilisateur ch = u.VersUtilisateur();
                ch.lRoles = u.SLIRoles.Where(j => j.Selected).Select(k => Int32.Parse(k.Value)).VersListePypee();
                int b = usa.Creer(ch);
                if (b >= 1) return RedirectToAction("Index");
            }
            return View(u);
        }

        [HttpGet]
        public ActionResult Modifier(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            Utilisateur u = usa.Donner(id);
            UtilisateurModification um = u.VersUtilisateurModification();
            um.SLIRoles = usa.DonnerSLIRoles(id).ToList();
            return View(um);
        }

        [HttpPost]
        public ActionResult Modifier(int id, UtilisateurModification um)
        {
            if (ModelState.IsValid)
            {
                UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
                UtilisateurModification vum = usa.Donner(id).VersUtilisateurModification();
                um.login = vum.login;
                Utilisateur u = um.VersUtilisateur();
                u.lRoles = um.SLIRoles.Where(j => j.Selected).Select(l => Int32.Parse(l.Value)).VersListePypee();
                if (usa.Modifier(id, u)) return RedirectToAction("Index");
            }
            return View(um);
        }

        [HttpGet]
        public ActionResult Connexion()
        {
            return View(new UtilisateurConnexion());
            
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Connexion(UtilisateurConnexion uc)
        {
            if (ModelState.IsValid)
            {
                UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
                Utilisateur u = usa.DonnerUtilisateur(uc);
                
                if (u!=null)
                {
                    SessionUtilisateur.AssignerUtilisateur(u);
                    return RedirectToAction("Index","Home");
                }
            }
            return View(uc);
        }

        [HttpGet]
        public ActionResult Deconnexion()
        {
            SessionUtilisateur.AssignerUtilisateur(null);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Supprimer(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            UtilisateurDetails u = usa.Donner(id).VersUtilisateurDetails();
            return View(u);
        }
        [HttpPost]
        public ActionResult Supprimer(int id, UtilisateurDetails u)
        {
            if (ModelState.IsValid)
            {
                UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
                if (!(usa.Supprimer(id))) return RedirectToAction("Index");
            }
            return View(u);
        }

        [HttpGet]
        public ActionResult Desactiver(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            usa.Desactiver(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Activer(int id)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            usa.Activer(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Enregistrer()
        {
            UtilisateurEnregistrement ur = new UtilisateurEnregistrement();
            return View(ur);
        }
        [HttpPost]
        public ActionResult Enregistrer(UtilisateurEnregistrement uc)
        {
            UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
            int i = usa.Creer(uc.VersUtilisateur());
            if (i > 0)
            {
                SessionUtilisateur.AssignerUtilisateur(new UtilisateurServiceAPI().Donner(i));
                return RedirectToAction("Index", "Home");
            }
            return View(uc);

        }
        
    }
}