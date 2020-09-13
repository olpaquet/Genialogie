using gl=Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Text;
using Genealogie.DAL.Client.Modeles;

namespace Genealogie.DAL.Client.Conversion
{
    public static class Mapper
    {
        public static Role VersClient(this gl.Role e) { if (e == null) { return null; } return new Role { id = e.id, nom = e.nom, description = e.description, actif = e.actif==1?true:false }; }
        public static gl.Role VersGlobal(this Role e) { if (e == null) { return null; } return new gl.Role {id=e.id,nom=e.nom,description=e.description,actif=e.actif?1:0 }; }

        public static Utilisateur VersClient(this gl.Utilisateur e) { if (e == null) { return null; } return new Utilisateur { id = e.id, actif = e.actif == 1 ? true : false, carteDePayement = e.cartedepayement, dateDeNaissance = e.datedenaissance, email = e.email, homme = e.homme==1?true:false, login = e.login, motDePasse = e.motdepasse, nom = e.nom, /*PostSel = e.postsel,*/ prenom = e.prenom/*, PreSel = e.presel*/ }; }
        public static gl.Utilisateur VersGlobal(this Utilisateur e) { if (e == null) { return null; } return new gl.Utilisateur { id = e.id, actif = e.actif?1:0, cartedepayement = e.carteDePayement, datedenaissance = e.dateDeNaissance, email = e.email, homme = e.homme?1:0, login = e.login, motdepasse = e.motDePasse, nom = e.nom, /*postsel = e.PostSel,*/ prenom = e.prenom/*, presel = e.PreSel*/ }; }

        public static UtilisateurRole VersClient(this gl.UtilisateurRole e) { if (e == null) return null; return new UtilisateurRole {idUtilisateur=e.idutilisateur,idrole=e.idrole }; }
        public static gl.UtilisateurRole VersGlobal(this UtilisateurRole e) { if (e == null) return null; return new gl.UtilisateurRole {idutilisateur=e.idUtilisateur,idrole=e.idrole}; }
    }   
}
