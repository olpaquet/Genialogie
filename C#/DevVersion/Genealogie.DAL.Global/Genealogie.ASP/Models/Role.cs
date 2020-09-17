using Genealogie.ASP.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class Role
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        /*public IEnumerable<Utilisateur> utilisateurs
        {get{ UtilisateurRoleServiceAPI ursa = new UtilisateurRoleServiceAPI(); return ursa.DonnerUtilisateursParRole(id);}}

        public int nombreDUtilisateurs
        { get { return this.utilisateurs.Count(); }}*/
    }

    public class RoleIndex
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public RoleIndex() { }
        public RoleIndex(Role e) { this.actif = e.actif; this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class RoleCreation
    {
        
        public string nom { get; set; }
        public string description { get; set; }
        

        public RoleCreation() { }
        public RoleCreation(Role e) {  this.description = e.description; this.nom = e.nom;  }
    }

    public class RoleModification
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        

        public RoleModification() { }
        public RoleModification(Role e) { this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class RoleDetails
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public RoleDetails() { }
        public RoleDetails(Role e) { this.actif = e.actif; this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }


}