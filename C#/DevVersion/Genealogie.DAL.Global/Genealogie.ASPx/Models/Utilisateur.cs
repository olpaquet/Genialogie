using Genealogie.ASP.ServicesAPI;
using Genealogie.DAL.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class Utilisateur
    {
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? dateDeNaissance { get; set; }
        public bool homme { get; set; }
        public string cartedepayement { get; set; }
        public string motDePasse { get; set; }
        /*public string PreSel { get; set; }
        public string PostSel { get; set; }*/
        public bool cctif { get; set; }

        public IEnumerable<Role> roles { get; set; }
        public string nomAffichage { get { return $"{this.prenom.Trim()} {this.nom.Trim()}".Trim(); } }
        public bool estAdmin { get 
            {
                UtilisateurService usa = new UtilisateurService();
                return usa.EstAdmin(this.id);
            } }
    }
}