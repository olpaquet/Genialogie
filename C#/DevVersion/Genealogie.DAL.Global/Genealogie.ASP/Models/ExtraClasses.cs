using Genealogie.ASP.Securite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class ChangerMotDePasse
    {
        public string login { get; set; }
        public string vieuxMotDePasse { get; set; }
        public string nouveauMotDePasse { get; set; }
        public string[] option { get; set; }
    }

    public class UtilisateurValide
    {
        public string login { get; set; }
        public string motDePasse { get; set; }
        public string[] options { get; set; }
    }

    public class ObjetDonnerListe
    {
        public IEnumerable<int> ienum { get; set; }
        public string[] options { get; set; }
    }

    public class ChercherPar
    {
        public string recherche { get; set; }
    }

}