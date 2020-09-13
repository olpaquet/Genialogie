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
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool Activer(int id)
        {
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
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool Desactiver(int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public IEnumerable<Utilisateur> DonnerX(IEnumerable<int> ie, string[] options = null)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Utilisateur DonnerUtilisateur(string login, string motDePasse)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public bool EstAdmin(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public bool Modifier(int id, Utilisateur e)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public bool Supprimer(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public bool UtilisateurValide(string login, string motdepasse, string[] option = null)
        {
            throw new NotImplementedException();
        }
        
    }

}
