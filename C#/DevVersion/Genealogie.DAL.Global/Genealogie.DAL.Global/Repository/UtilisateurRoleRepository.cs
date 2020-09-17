using BoiteAOutil.DB.Standard;
using Genealogie.DAL.Global.Conversion;
using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public class UtilisateurRoleRepository: BaseRepository, IUtilisateurRoleRepository<UtilisateurRole,Role,Utilisateur>
    {
        private const string CONST_UTILISATEURROLE_REQ = "select idutilisateur, idrole from utilisateurrole";

        public bool Creer(int idutilisateur, int idrole, UtilisateurRole e)
        {
            Commande com = new Commande("utilisateurrole_cre", true);
            com.AjouterParametre("idutilisateur", idutilisateur);
            com.AjouterParametre("idrole", idrole);
            return (int)_connexion.ExecuterNonRequete(com)==1;
            throw new NotImplementedException();
        }

        public IEnumerable<UtilisateurRole> Donner()
        {
            Commande com = new Commande($"{CONST_UTILISATEURROLE_REQ}");
            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateurRole());
            throw new NotImplementedException();
        }

        public UtilisateurRole Donner(int idutilisateur, int idrole)
        {
            Commande com = new Commande($"{CONST_UTILISATEURROLE_REQ} where idutilisateur = @idutilisateur and idrole = @idrole" );
            com.AjouterParametre("idutilisateur", idutilisateur);
            com.AjouterParametre("idrole", idrole);
            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateurRole()).SingleOrDefault();
            throw new NotImplementedException();
        }

        public IEnumerable<Role> DonnerRoleParUtilisateur(int idutilisateur)
        {
            Commande com = new Commande($"{CONST_UTILISATEURROLE_REQ} where idutilisateur = @idutilisateur");
            com.AjouterParametre("idutilisateur", idutilisateur);

            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateurRole())
                .Select(v => 
                {
                    RoleRepository rr = new RoleRepository();
                    return rr.Donner(v.idrole);
                }
                );

            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> DonnerUtilisateurParRole(int idrole)
        {
            Commande com = new Commande($"{CONST_UTILISATEURROLE_REQ} where idrole = @idrole");
            com.AjouterParametre("idrole", idrole);

            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateurRole())
                .Select(v =>
                {
                    UtilisateurRepository ur = new UtilisateurRepository();
                    return ur.Donner(v.idutilisateur);
                }
                );
            throw new NotImplementedException();
        }

        public bool EstAdmin(int idUtilisateur)
        {

            Commande com = new Commande($"{CONST_UTILISATEURROLE_REQ} where idutilisateur = @idutilisateur");
            com.AjouterParametre("idutilisateur", idUtilisateur);
            IEnumerable<int> roles = _connexion.ExecuterLecteur(com, j => j.VersUtilisateurRole()).Select(d=>d.idrole);
            foreach(int i in roles) { RoleRepository rr = new RoleRepository(); if (rr.EstAdmin(i)) return true; }
            return false;
            throw new NotImplementedException();
        }

        public bool Modifier(int idutilisateur, int idrole, UtilisateurRole e)
        {
            Commande com = new Commande("utilisateurrole_mod");
            com.AjouterParametre("idutilisateur", idutilisateur);
            com.AjouterParametre("idrole", idrole);
            return (int)_connexion.ExecuterNonRequete(com)==1;
            throw new NotImplementedException();
        }

        public bool Supprimer(int idutilisateur, int idrole)
        {
            Commande com = new Commande("utilisateurrole_eff");
            com.AjouterParametre("idutilisateur", idutilisateur);
            com.AjouterParametre("idrole", idrole);
            return (int)_connexion.ExecuterNonRequete(com) == 1;
        }
    }
}
