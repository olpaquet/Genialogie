using gl=Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Text;
using Genealogie.DAL.Global.Repository;

using Genealogie.DAL.Client.Conversion;
using System.Linq;
using Genealogie.DAL.Client.Modeles;

namespace Genealogie.DAL.Client.Services
{
    public class UtilisateurRoleService : IUtilisateurRoleRepository<UtilisateurRole, Role, Utilisateur >
    {
        private IUtilisateurRoleRepository<gl.UtilisateurRole, gl.Role, gl.Utilisateur> _rep;

        public UtilisateurRoleService() { this._rep = new UtilisateurRoleRepository(); }

        public bool Creer(int idutilisateur, int idrole, UtilisateurRole e)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.Creer(idutilisateur, idrole, e.VersGlobal());
            throw new NotImplementedException();
        }

        public IEnumerable<Modeles.UtilisateurRole> Donner()
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.Donner().Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public Modeles.UtilisateurRole Donner(int idutilisateur, int idrole)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.Donner(idutilisateur, idrole).VersClient();
            throw new NotImplementedException();
        }

        public IEnumerable<Role> DonnerRoleParUtilisateur(int idutilisateur)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.DonnerRoleParUtilisateur(idutilisateur).Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> DonnerUtilisateurParRole(int idrole)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.DonnerUtilisateurParRole(idrole).Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public bool EstAdmin(int idUtilisateur)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.EstAdmin(idUtilisateur);
            throw new NotImplementedException();
        }

        public bool Modifier(int idutilisateur, int idrole, UtilisateurRole e)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.Modifier(idutilisateur, idrole, e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Supprimer(int idutilisateur, int idrole)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return urr.Supprimer(idutilisateur, idrole);
            throw new NotImplementedException();
        }
    }
}
