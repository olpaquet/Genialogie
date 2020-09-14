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
//using Genealogie.DAL.Global.Repository;

namespace Genealogie.API.Controllers
{
    public class UtilisateurController : ApiController /*, IUtilisateurRepository<Utilisateur>*/
    {

        [HttpGet]
        /*[ActionName("Donner")]*/
        public IEnumerable<Utilisateur> Donner()
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner().Select(l => l.VersAPI());
        }
        
        [HttpGet]
        /*[ActionName("Donnerz")]*/
        //[Route("api/utilisateur/donner/{id:int}")]
        public Utilisateur Donner(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner(id).VersAPI();
            throw new NotImplementedException();
        }
        
        [HttpPut]
        public bool Activer(int id)
        {
            UtilisateurService us = new UtilisateurService();

            return us.Activer(id);
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool ChangerMotDePasse(string login, string vieuxmotdepasse, string nouveaumotdepasse, string[] option = null)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public int Creer(Utilisateur e)
        {
            UtilisateurService us = new UtilisateurService();
            var yes = e.VersClient();
            return us.Creer(yes);
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool Desactiver(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Desactiver(id);
            throw new NotImplementedException();
        }
        
        [HttpGet]
        
        public IEnumerable<Utilisateur> DonnerL(ObjetDonnerListe odl)
        {
            UtilisateurService rs = new UtilisateurService();
            return rs.Donner(odl.ienum, odl.options).Select(j=>j.VersAPI());
            throw new NotImplementedException();
        }

        

        [HttpGet]
        public bool EstAdmin(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.EstAdmin(id);
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool Modifier(int id, Utilisateur e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Modifier(id, e.VersClient());
            throw new NotImplementedException();
        }

        [HttpDelete]
        public bool Supprimer(int id)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Supprimer(id);
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool ValiderUtilisateur(UtilisateurValide uv)
        {
            UtilisateurService us = new UtilisateurService();
            return us.ValiderUtilisateur(uv.login, uv.motDePasse, uv.options);
            throw new NotImplementedException();
        }

        [HttpPut]
        public Utilisateur DonnerUtilisateur(UtilisateurValide uv)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Donner(uv.login, uv.motDePasse).VersAPI();
        }

        [HttpPost]
        public int Enregistrer(Utilisateur e)
        {
            UtilisateurService us = new UtilisateurService();
            return us.Creer(e.VersClient());
        }
        

        
        
        
    }

}
