

using Genealogie.ASP.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string lRoles { get; set; }

        public IEnumerable<Role> roles 
        {get{UtilisateurServiceAPI usa = new UtilisateurServiceAPI();return usa.DonnerRoles(this.id);}}
        public int nombreDeRoles() { return this.roles.Count(); }
        public string nomAffichage() { string pr = this.prenom ?? ""; return $"{pr.Trim()} {this.nom.Trim()}".Trim();}
        public bool estAdmin(){   UtilisateurServiceAPI usa = new UtilisateurServiceAPI();return usa.EstAdmin(this.id);}
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

        public IList<SelectListItem> SLIRoles { get; set; }

        public UtilisateurDetails() { }
        public UtilisateurDetails(Utilisateur e)
        {
            id = e.id; login = e.login; nom = e.nom; prenom = e.prenom; email = e.email;
            dateDeNaissance = e.dateDeNaissance; homme = e.homme; cartedepayement = e.cartedepayement;
            actif = e.actif;
        }                
    }

    public class UtilisateurConnexion
    {
        public string login { get; set; }
        public string motDePasse { get; set; }        
    }

    public class UtilisateurEnregistrement
    {
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? dateDeNaissance { get; set; }
        public bool homme { get; set; }
        public string cartedepayement { get; set; }
        public string motDePasse { get; set; }
        public string motDePasseConfirmation { get; set; }

    }

    public class UtilisateurCreation
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
        public IList<SelectListItem> SLIRoles { get; set; }

    }

    public class UtilisateurModification
    {
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? dateDeNaissance { get; set; }
        public bool homme { get; set; }
        public string cartedepayement { get; set; }
        public IList<SelectListItem> SLIRoles { get; set; }

        public UtilisateurModification() { }
        public UtilisateurModification(Utilisateur u) 
        {
            this.cartedepayement = u.cartedepayement;
            this.dateDeNaissance = u.dateDeNaissance;
            this.email = u.email;
            this.homme = u.homme;
            this.prenom = u.prenom;
            this.login = u.login;
            this.nom = u.nom;
            
        }

    }

}