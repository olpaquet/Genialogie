using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Client.Modeles
{
    
    public class Abonnement
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Duree { get; set; }
        public decimal Prix { get; set; }
        public int NombreMaxArbres { get; set; }
        public int NombreMaxPersonnes { get; set; }
        public int Actif { get; set; }
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
    public class Blocage
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }

    public class Conversation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
    public class Couple
    {
        public int IdPersonne { get; set; }
        public int IdPartenaire { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }
    public class Message
    {
        public int Id { get; set; }
        public string Sujet { get; set; }
        public string Texte { get; set; }
        public int IdEmetteur { get; set; }
        public int IdConversation { get; set; }
    }
    public class MessageEfface
    {
        public int IdMessage { get; set; }
        public int IdEffaceur { get; set; }
        public DateTime? Date { get; set; }
    }
    public class MessageForum
    {
        public int Id { get; set; }
        public string Sujet { get; set; }
        public string Texte { get; set; }
        public int IdTheme { get; set; }
        public int IdPublicateur { get; set; }
        public DateTime? DatePublication { get; set; }
        public int Actif { get; set; }
    }
    public class MessageLu
    {
        public int IdMessage { get; set; }
        public int IdLecteur { get; set; }
        public DateTime? Date { get; set; }
    }
    public class Nouvelle
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int Actif { get; set; }
    }
    public class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateDeNaissance { get; set; }
        public DateTime? DateDeDeces { get; set; }
        public int IdArbre { get; set; }
        public DateTime DateAjout { get; set; }
        public int? IdPere { get; set; }
        public int? IdMere { get; set; }
    }
    public class Role
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }
    public class Theme
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public bool actif { get; set; }
    }
    public class Utilisateur
    {
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime? dateDeNaissance { get; set; }
        public bool homme { get; set; }
        public string carteDePayement { get; set; }
        public string motDePasse { get; set; }
        /*public string PreSel { get; set; }
        public string PostSel { get; set; }*/
        public bool actif { get; set; }
        public string lRoles { get; set; }
    }
    public class UtilisateurAbonnement
    {
        public int idAbonne { get; set; }
        public int idAbonnement { get; set; }
        public DateTime dateAbonnement { get; set; }
        public string carteDePayement { get; set; }
    }
    public class UtilisateurNouvelle
    {
        public int idPublicateur { get; set; }
        public int idNouvelle { get; set; }
        public DateTime datePublication { get; set; }
    }
    public class UtilisateurRole
    {
        public int idUtilisateur { get; set; }
        public int idrole { get; set; }
    }


    

}
