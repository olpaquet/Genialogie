using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class Theme
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }

    public class ThemeIndex
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public ThemeIndex() { }
        public ThemeIndex(Theme e) { this.actif = e.actif; this.description = e.description; this.titre = e.titre; this.id = e.id; }
    }

    public class ThemeCreation
    {

        public string titre { get; set; }
        public string description { get; set; }


        public ThemeCreation() { }
        public ThemeCreation(Theme e) { this.description = e.description; this.titre = e.titre; }
    }

    public class ThemeModification
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }


        public ThemeModification() { }
        public ThemeModification(Theme e) { this.description = e.description; this.titre = e.titre; this.id = e.id; }
    }

    public class ThemeDetails
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }

        public ThemeDetails() { }
        public ThemeDetails(Theme e) { this.actif = e.actif; this.description = e.description; this.titre = e.titre; this.id = e.id; }
    }


}