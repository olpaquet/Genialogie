using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Modeles
{
    public class Abonnement
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int duree { get; set; }
        public decimal prix { get; set; }
        public int nombremaxarbres { get; set; }
        public int nombremaxpersonnes { get; set; }
        public int actif { get; set; }
    }
    public class Arbre
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int idcreateur { get; set; }
        public DateTime datecreation { get; set; }
        public int? idblocage { get; set; }
        public int? idbloqueur { get; set; }
        public DateTime? dateblocage { get; set; }
    }
    public class Blocage
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int actif { get; set; }
    }
    public class Conversation
    {
        public int id { get; set; }
        public DateTime date { get; set; }
    }
    public class Couple
    {
        public int idpersonne { get; set; }
        public int idpartenaire { get; set; }
        public DateTime datedebut { get; set; }
        public DateTime? datefin { get; set; }
    }
    public class Message
    {
        public int id { get; set; }
        public string sujet { get; set; }
        public string texte { get; set; }
        public int idemetteur { get; set; }
        public int idconversation { get; set; }
    }
    public class MessageEfface
    {
        public int idmessage { get; set; }
        public int ideffaceur { get; set; }
        public DateTime? date { get; set; }
    }
    public class MessageForum
    {
        public int id { get; set; }
        public string sujet { get; set; }
        public string texte { get; set; }
        public int idtheme { get; set; }
        public int idpublicateur { get; set; }
        public DateTime? datepublication { get; set; }
        public int actif { get; set; }
    }
    public class MessageLu
    {
        public int idmessage { get; set; }
        public int idlecteur { get; set; }
        public DateTime? date { get; set; }
    }
    public class Nouvelle
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public int actif { get; set; }
    }
    public class Personne
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public DateTime? datedenaissance { get; set; }
        public DateTime? datededeces { get; set; }
        public int idarbre { get; set; }
        public DateTime dateajout { get; set; }
        public int? idpere { get; set; }
        public int? idmere { get; set; }
    }
    public class Role
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int actif { get; set; }
    }
    public class Theme
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public int actif { get; set; }
    }
    public class Utilisateur
    {
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? datedenaissance { get; set; }
        public int homme { get; set; }
        public string cartedepayement { get; set; }
        public string motdepasse { get; set; }
        public string presel { get; set; }
        public string postsel { get; set; }
        public int actif { get; set; }

        public string lroles { get; set; }
    }
    public class UtilisateurAbonnement
    {
        public int idabonne { get; set; }
        public int idabonnement { get; set; }
        public DateTime dateabonnement { get; set; }
        public string cartedepayement { get; set; }
    }
    public class UtilisateurNouvelle
    {
        public int idpublicateur { get; set; }
        public int idnouvelle { get; set; }
        public DateTime datepublication { get; set; }
    }
    public class UtilisateurRole
    {
        public int idutilisateur { get; set; }
        public int idrole { get; set; }
    }


}
