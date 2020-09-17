using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.API.Models
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
    }
    
    public class Role
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }

    public class Arbre
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int IdCreateur { get; set; }
        public DateTime DateCreation { get; set; }
        public int? IdBlocage { get; set; }
        public int? IdBloqueur { get; set; }
        public DateTime? DateBlocage { get; set; }
    }
}