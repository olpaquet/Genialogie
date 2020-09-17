
using gl=Genealogie.DAL.Global.Modeles;
using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Genealogie.DAL.Client.Conversion;
using System.Linq;
using Genealogie.DAL.Client.Modeles;

namespace Genealogie.DAL.Client.Services
{
    public class RoleService: IRoleRepository<Role>
    {

        private IRoleRepository<gl.Role> _rep;

        public RoleService() { this._rep = new RoleRepository(); }

        public bool Activer(int id)
        {
            return _rep.Activer(id);
            throw new NotImplementedException();
        }

        public int Creer(Role e)
        {
            return _rep.Creer(e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Desactiver(int id)
        {
            return _rep.Desactiver(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Donner()
        {
            return _rep.Donner().Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public Role Donner(int id)
        {
            return _rep.Donner(id).VersClient();
            throw new NotImplementedException();
        }

        public IList<Role> Donner(IList<int> l, string[] options = null)
        {
            return _rep.Donner(l, options).Select(r => r.VersClient()).ToList();
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Donner(IEnumerable<int> ie, string[] options = null)
        {
            return _rep.Donner(ie, options).Select(j=>j.VersClient());
            throw new NotImplementedException();
        }

        public int? DonnerParNom(string nom)
        {
            return _rep.DonnerParNom(nom);
            throw new NotImplementedException();
        }

        public bool EstAdmin(int id)
        {
            return _rep.EstAdmin(id);
            throw new NotImplementedException();
        }

        public bool Modifier(int id, Role e)
        {
            return _rep.Modifier(id, e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Supprimer(int id)
        {
            return _rep.Supprimer(id);
            throw new NotImplementedException();
        }
    }
}
