using Genealogie.DAL.Client.Modeles;
using gl = Genealogie.DAL.Global.Modeles;
using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Genealogie.DAL.Client.Conversion;

namespace Genealogie.DAL.Client.Services
{
    public class ArbreService : IArbreRepository<Arbre>
    {

        private IArbreRepository<gl.Arbre> _rep;

        public ArbreService() { this._rep = new ArbreRepository(); }

        public bool Activer(int id)
        {
            return _rep.Activer(id);
            throw new NotImplementedException();
        }

        public int Creer(Arbre e)
        {
            return _rep.Creer(e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Desactiver(int id)
        {
            return _rep.Desactiver(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Arbre> Donner()
        {
            return _rep.Donner().Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public Arbre Donner(int id)
        {
            return _rep.Donner(id).VersClient();
            throw new NotImplementedException();
        }

        public IList<Arbre> Donner(IList<int> l, string[] options = null)
        {
            return _rep.Donner(l, options).Select(r => r.VersClient()).ToList();
            throw new NotImplementedException();
        }

        public IEnumerable<Arbre> Donner(IEnumerable<int> ie, string[] options = null)
        {
            return _rep.Donner(ie, options).Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public int? DonnerParNom(string nom)
        {
            return _rep.DonnerParNom(nom);
            throw new NotImplementedException();
        }

        public bool Modifier(int id, Arbre e)
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
