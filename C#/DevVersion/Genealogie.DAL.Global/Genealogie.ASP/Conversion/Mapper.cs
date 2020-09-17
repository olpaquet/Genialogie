using Genealogie.ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Conversion
{
    public static class Mapper
    {
        public static string VersListePypee(this IEnumerable<int> e) { if (e == null) { return null; }
            int compteur = 0;
            string ret = "";
            foreach(int i in e) { compteur++; ret = ret + (compteur != 1 ? "," : "") + i.ToString(); }
            return ret;

        }


        public static Utilisateur VersUtilisateur(this UtilisateurCreation e) { if (e==null) { return null; } return new Utilisateur { cartedepayement = e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, login=e.login, motDePasse=e.motDePasse, nom=e.nom, prenom=e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurModification e) { if (e == null) { return null; } return new Utilisateur { cartedepayement = e.cartedepayement, dateDeNaissance = e.dateDeNaissance, email = e.email, homme = e.homme, login = e.login, nom = e.nom, prenom = e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurDetails e) { if (e == null) { return null; } return new Utilisateur { actif=e.actif, cartedepayement=e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, id=e.id, login=e.login, nom=e.nom, prenom=e.prenom }; }
        public static Utilisateur VersUtilisateur(this UtilisateurEnregistrement e) { if (e == null) { return null; } return new Utilisateur { dateDeNaissance=e.dateDeNaissance, cartedepayement=e.cartedepayement, email=e.email, homme=e.homme, login=e.login,motDePasse= e.motDePasse, nom=e.nom, prenom=e.prenom }; }

        public static UtilisateurModification VersUtilisateurModification(this Utilisateur e) { if (e == null) { return null; } return new UtilisateurModification(e); }
        public static UtilisateurDetails VersUtilisateurDetails(this Utilisateur e) { if (e == null) { return null; } return new UtilisateurDetails { actif = e.actif, cartedepayement=e.cartedepayement, dateDeNaissance=e.dateDeNaissance, email=e.email, homme=e.homme, id=e.id, login=e.login, nom=e.nom, prenom=e.prenom }; }


        /*Role*/
        public static Role VersRole(this RoleCreation e) { if (e == null) { return null; } return new Role {  description=e.description, nom=e.nom }; }
        public static Role VersRole(this RoleModification e) { if (e == null) { return null; } return new Role { id=e.id,  description = e.description, nom = e.nom }; }
        public static Role VersRole(this RoleDetails e) { if (e == null) { return null; } return new Role { id=e.id, actif = e.actif, description = e.description, nom = e.nom }; }

        /*Blocage*/
        public static Blocage VersBlocage(this BlocageCreation e) { if (e == null) { return null; } return new Blocage { description = e.description, nom = e.nom }; }
        public static Blocage VersBlocage(this BlocageModification e) { if (e == null) { return null; } return new Blocage { id = e.id, description = e.description, nom = e.nom }; }
        public static Blocage VersBlocage(this BlocageDetails e) { if (e == null) { return null; } return new Blocage { id = e.id, actif = e.actif, description = e.description, nom = e.nom }; }



    }
}