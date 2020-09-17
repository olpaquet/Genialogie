using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class Blocage
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }

    public class BlocageIndex
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public BlocageIndex() { }
        public BlocageIndex(Blocage e) { this.actif = e.actif; this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class BlocageCreation
    {

        public string nom { get; set; }
        public string description { get; set; }


        public BlocageCreation() { }
        public BlocageCreation(Blocage e) { this.description = e.description; this.nom = e.nom; }
    }

    public class BlocageModification
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }


        public BlocageModification() { }
        public BlocageModification(Blocage e) { this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class BlocageDetails
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public BlocageDetails() { }
        public BlocageDetails(Blocage e) { this.actif = e.actif; this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }
}