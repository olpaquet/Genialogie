using Genealogie.API.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Genealogie.API.Conversion;
using Genealogie.DAL.Client.Services;
using Newtonsoft.Json;
using System.Text;
//using Genealogie.DAL.Global.Repository;

namespace Genealogie.API.Controllers
{
    public class UtilisateurController : ApiController /*, IUtilisateurRepository<Utilisateur>*/
    {

        
        [HttpGet]
        public IEnumerable<Utilisateur> Donner()
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner().Select(l => l.VersAPI());          
            
        }

        [HttpGet]
        public Utilisateur Donner(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner(id).VersAPI();
        }

        [HttpGet]
        public Utilisateur DonnerUtilisateur(UtilisateurValide uv)
        {
            UtilisateurService us = new UtilisateurService();
            return us.DonnerUtilisateur(uv.login, uv.motDePasse).VersAPI();
        }

        [HttpPut]
        public bool Activer(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Activer(id);
            
        }

        [HttpPut]
        public bool Desactiver(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Desactiver(id);
        }

        [HttpPut]
        public bool Modifier(int id, Utilisateur e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Modifier(id, e.VersClient());
        }
        [HttpPost]
        public int Creer(Utilisateur e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Creer(e.VersClient());
        }

        [HttpPut]
        public bool ChangerMotDePasse(ChangerMotDePasse e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.ChangerMotDePasse(e.login,e.vieuxMotDePasse, e.nouveauMotDePasse, e.option );
            throw new NotImplementedException();
        }     
        
              
        [HttpGet]
        public bool UtilisateurValide(UtilisateurValide e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.UtilisateurValide(e.login, e.motDePasse,null);
            throw new NotImplementedException();
        }

        [HttpGet]
        public bool EstAdmin(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.EstAdmin(id);
        }
        
        [HttpDelete]
        public bool Supprimer(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Supprimer(id);
            throw new NotImplementedException();
        }

        [HttpGet]
        public IEnumerable<Utilisateur> Donner(ObjetDonnerListe odl)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner(odl.ienum, odl.options).Select(j => j.VersAPI());
            throw new NotImplementedException();
        }
    }

}
