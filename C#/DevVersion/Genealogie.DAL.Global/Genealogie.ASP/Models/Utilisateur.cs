

using Genealogie.ASP.Services.API;
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
        public bool actif { get; set; }

        public IEnumerable<Role> roles 
        {
            get
            {
                return null;
            }
        }
        public int nombreDeRoles
        {
            get { return 0; }
        }
        public string nomAffichage { get { return $"{this.prenom.Trim()} {this.nom.Trim()}".Trim(); } }
        public bool estAdmin
        {
            get
            {
                UtilisateurServiceAPI usa = new UtilisateurServiceAPI();
                return usa.EstAdmin(this.id);
            }
        }
    }

    public class UtilisateurIndex
    {
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public bool actif { get; set; }

        public UtilisateurIndex() { }
        public UtilisateurIndex(Utilisateur e)
        {
            id = e.id; login = e.login; nom = e.nom; prenom = e.prenom; email = e.email;            
            actif = e.actif;
        }
    }

    public class UtilisateurDetails
    {
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? dateDeNaissance { get; set; }
        public bool homme { get; set; }
        public string cartedepayement { get; set; }
        /*public string PreSel { get; set; }
        public string PostSel { get; set; }*/
        public bool actif { get; set; }

        public UtilisateurDetails() { }
        public UtilisateurDetails(Utilisateur e)
        {
            id = e.id; login = e.login; nom = e.nom; prenom = e.prenom; email = e.email;
            dateDeNaissance = e.dateDeNaissance; homme = e.homme; cartedepayement = e.cartedepayement;
            actif = e.actif;
        }
    }

}