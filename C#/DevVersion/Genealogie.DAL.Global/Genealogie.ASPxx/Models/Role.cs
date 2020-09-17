using Genealogie.DAL.Client.Services;
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

        public IEnumerable<Utilisateur> utilisateurs
        {
            get
            {
                return null;
            }
        }

        public int nombreDUtilisateurs 
        { 
            get
            {
                UtilisateurRoleService urs = new UtilisateurRoleService();
                return urs.DonnerUtilisateurParRole(this.id).Count();
            }
        }
    }
}