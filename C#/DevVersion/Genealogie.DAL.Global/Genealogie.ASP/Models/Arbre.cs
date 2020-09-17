using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Models
{
    public class Arbre
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int idCreateur { get; set; }
        public DateTime dateCreation { get; set; }
        public int? idBlocage { get; set; }
        public int? idBloqueur { get; set; }
        public DateTime? dateBlocage { get; set; }
    }

    public class ArbreIndex
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool bloque { get; set; }
        

        public ArbreIndex() { }
        public ArbreIndex(Arbre e) {  this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class ArbreCreation
    {

        public string nom { get; set; }
        public string description { get; set; }
        
        


        public ArbreCreation() { }
        public ArbreCreation(Arbre e) { this.description = e.description; this.nom = e.nom; }
    }

    public class ArbreModification
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }

        public ArbreModification() { }
        public ArbreModification(Arbre e) { this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }

    public class ArbreDetails
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool bloque { get; set; }

        public ArbreDetails() { }
        public ArbreDetails(Arbre e) { this.description = e.description; this.nom = e.nom; this.id = e.id; }
    }
}