using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Genealogie.DAL.Global.Conversion
{
    internal static class Mapper
    {
        public static object SiNul(this object o) { return o == DBNull.Value ? null : o; }

        public static Abonnement VersAbonnement(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Abonnement
            {
                id = (int)idr[nameof(Abonnement.id)]
            ,
                nom = (string)idr[nameof(Abonnement.nom)]
            ,
                description = (string)idr[nameof(Abonnement.description)]
            ,
                duree = (int)idr[nameof(Abonnement.duree)]
            ,
                prix = (decimal)idr[nameof(Abonnement.prix)]
            ,
                nombremaxarbres = (int)idr[nameof(Abonnement.nombremaxarbres)]
            ,
                nombremaxpersonnes = (int)idr[nameof(Abonnement.nombremaxpersonnes)]
            ,
                actif = (int)idr[nameof(Abonnement.actif)]
            };
        }
        public static Arbre VersArbre(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Arbre
            {
                id = (int)idr[nameof(Arbre.id)]
            ,
                nom = (string)idr[nameof(Arbre.nom)]
            ,
                description = (string)idr[nameof(Arbre.description)]
            ,
                idcreateur = (int)idr[nameof(Arbre.idcreateur)]
            ,
                datecreation = (DateTime)idr[nameof(Arbre.datecreation)]
            ,
                idblocage = (int?)idr[nameof(Arbre.idblocage)].SiNul()
            ,
                idbloqueur = (int?)idr[nameof(Arbre.idbloqueur)].SiNul()
            ,
                dateblocage = (DateTime?)idr[nameof(Arbre.dateblocage)].SiNul()
            };
        }
        public static Blocage VersBlocage(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Blocage
            {
                id = (int)idr[nameof(Blocage.id)]
            ,
                nom = (string)idr[nameof(Blocage.nom)]
            ,
                description = (string)idr[nameof(Blocage.description)]
            ,
                actif = (int)idr[nameof(Blocage.actif)]
            };
        }
        public static Conversation VersConversation(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Conversation
            {
                id = (int)idr[nameof(Conversation.id)]
            ,
                date = (DateTime)idr[nameof(Conversation.date)]
            };
        }
        public static Couple VersCouple(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Couple
            {
                idpersonne = (int)idr[nameof(Couple.idpersonne)]
            ,
                idpartenaire = (int)idr[nameof(Couple.idpartenaire)]
            ,
                datedebut = (DateTime)idr[nameof(Couple.datedebut)]
            ,
                datefin = (DateTime?)idr[nameof(Couple.datefin)].SiNul()
            };
        }
        public static Message VersMessage(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Message
            {
                id = (int)idr[nameof(Message.id)]
            ,
                sujet = (string)idr[nameof(Message.sujet)]
            ,
                texte = (string)idr[nameof(Message.texte)]
            ,
                idemetteur = (int)idr[nameof(Message.idemetteur)]
            ,
                idconversation = (int)idr[nameof(Message.idconversation)]
            };
        }
        public static MessageEfface VersMessageEfface(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new MessageEfface
            {
                idmessage = (int)idr[nameof(MessageEfface.idmessage)]
            ,
                ideffaceur = (int)idr[nameof(MessageEfface.ideffaceur)]
            ,
                date = (DateTime?)idr[nameof(MessageEfface.date)]
            };
        }
        public static MessageForum VersMessageForum(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new MessageForum
            {
                id = (int)idr[nameof(MessageForum.id)]
            ,
                sujet = (string)idr[nameof(MessageForum.sujet)]
            ,
                texte = (string)idr[nameof(MessageForum.texte)]
            ,
                idtheme = (int)idr[nameof(MessageForum.idtheme)]
            ,
                idpublicateur = (int)idr[nameof(MessageForum.idpublicateur)]
            ,
                datepublication = (DateTime?)idr[nameof(MessageForum.datepublication)]
            ,
                actif = (int)idr[nameof(MessageForum.actif)]
            };
        }
        public static MessageLu VersMessageLu(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new MessageLu
            {
                idmessage = (int)idr[nameof(MessageLu.idmessage)]
            ,
                idlecteur = (int)idr[nameof(MessageLu.idlecteur)]
            ,
                date = (DateTime?)idr[nameof(MessageLu.date)]
            };
        }
        public static Nouvelle VersNouvelle(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Nouvelle
            {
                id = (int)idr[nameof(Nouvelle.id)]
            ,
                titre = (string)idr[nameof(Nouvelle.titre)]
            ,
                description = (string)idr[nameof(Nouvelle.description)]
            ,
                actif = (int)idr[nameof(Nouvelle.actif)]
            };
        }
        public static Personne VersPersonne(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Personne
            {
                id = (int)idr[nameof(Personne.id)]
            ,
                nom = (string)idr[nameof(Personne.nom)]
            ,
                prenom = (string)idr[nameof(Personne.prenom)].SiNul()
            ,
                datedenaissance = (DateTime?)idr[nameof(Personne.datedenaissance)].SiNul()
            ,
                datededeces = (DateTime?)idr[nameof(Personne.datededeces)].SiNul()
            ,
                idarbre = (int)idr[nameof(Personne.idarbre)]
            ,
                dateajout = (DateTime)idr[nameof(Personne.dateajout)]
            ,
                idpere = (int?)idr[nameof(Personne.idpere)].SiNul()
            ,
                idmere = (int?)idr[nameof(Personne.idmere)].SiNul()
            };
        }
        public static Role VersRole(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Role
            {
                id = (int)idr[nameof(Role.id)]
            ,
                nom = (string)idr[nameof(Role.nom)]
            ,
                description = (string)idr[nameof(Role.description)]
            ,
                actif = (int)idr[nameof(Role.actif)]
            };
        }
        public static Theme VersTheme(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Theme
            {
                id = (int)idr[nameof(Theme.id)]
            ,
                titre = (string)idr[nameof(Theme.titre)]
            ,
                description = (string)idr[nameof(Theme.description)]
            ,
                actif = (int)idr[nameof(Theme.actif)]
            };
        }
        public static Utilisateur VersUtilisateur(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new Utilisateur
            {
                id = (int)idr[nameof(Utilisateur.id)]
            ,
                login = (string)idr[nameof(Utilisateur.login)]
            ,
                nom = (string)idr[nameof(Utilisateur.nom)]
            ,
                prenom = (string)idr[nameof(Utilisateur.prenom)].SiNul()
            ,
                email = (string)idr[nameof(Utilisateur.email)]
            ,
                datedenaissance = (DateTime?)idr[nameof(Utilisateur.datedenaissance)].SiNul()
            ,
                homme = (int)idr[nameof(Utilisateur.homme)]
            ,
                cartedepayement = (string)idr[nameof(Utilisateur.cartedepayement)].SiNul()
            ,
                motdepasse = "" /*(varbinary)idr[nameof(Utilisateur.motdepasse)]*/
            /*,
                presel = (string)idr[nameof(Utilisateur.presel)]
            ,
                postsel = (string)idr[nameof(Utilisateur.postsel)]*/
            ,
                actif = (int)idr[nameof(Utilisateur.actif)]
            };
        }
        public static UtilisateurAbonnement VersUtilisateurAbonnement(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new UtilisateurAbonnement
            {
                idabonne = (int)idr[nameof(UtilisateurAbonnement.idabonne)]
            ,
                idabonnement = (int)idr[nameof(UtilisateurAbonnement.idabonnement)]
            ,
                dateabonnement = (DateTime)idr[nameof(UtilisateurAbonnement.dateabonnement)]
            ,
                cartedepayement = (string)idr[nameof(UtilisateurAbonnement.cartedepayement)]
            };
        }
        public static UtilisateurNouvelle VersUtilisateurNouvelle(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new UtilisateurNouvelle
            {
                idpublicateur = (int)idr[nameof(UtilisateurNouvelle.idpublicateur)]
            ,
                idnouvelle = (int)idr[nameof(UtilisateurNouvelle.idnouvelle)]
            ,
                datepublication = (DateTime)idr[nameof(UtilisateurNouvelle.datepublication)]
            };
        }
        public static UtilisateurRole VersUtilisateurRole(this IDataRecord idr)
        {
            if (idr == null) return null;
            return new UtilisateurRole
            {
                idutilisateur = (int)idr[nameof(UtilisateurRole.idutilisateur)]
            ,
                idrole = (int)idr[nameof(UtilisateurRole.idrole)]
            };
        }



    }
}
