using Genealogie.ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Conversion
{
    public static class Mapper
    {
        public static Utilisateur VersUtilisateur(this UtilisateurCreation e) { if (e==null) { return null; } return new Utilisateur { cartedepayement = e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, login=e.login, motDePasse=e.motDePasse, nom=e.nom, prenom=e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurModification e) { if (e == null) { return null; } return new Utilisateur { cartedepayement = e.cartedepayement, dateDeNaissance = e.dateDeNaissance, email = e.email, homme = e.homme, login = e.login, nom = e.nom, prenom = e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurDetails e) { if (e == null) { return null; } return new Utilisateur { actif=e.actif, cartedepayement=e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, id=e.id, login=e.login, nom=e.nom, prenom=e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurEnregistrement e) { if (e == null) { return null; } return new Utilisateur { dateDeNaissance=e.dateDeNaissance, cartedepayement=e.cartedepayement, email=e.email, homme=e.homme, login=e.login,motDePasse= e.motDePasse, nom=e.nom, prenom=e.prenom }; }

        public static UtilisateurModification VersUtilisateurModification(this Utilisateur e) { if (e == null) { return null; } return new UtilisateurModification(e); }
        public static UtilisateurDetails VersUtilisateurDetails(this Utilisateur e) { if (e == null) { return null; } return new UtilisateurDetails { actif = e.actif, cartedepayement=e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, id=e.id, login=e.login, nom=e.nom, prenom=e.prenom }; }

    }
}