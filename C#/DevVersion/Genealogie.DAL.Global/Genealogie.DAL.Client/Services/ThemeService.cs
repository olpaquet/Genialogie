
using gl = Genealogie.DAL.Global.Modeles;
using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Genealogie.DAL.Client.Conversion;
using System.Linq;
using Genealogie.DAL.Client.Modeles;

namespace Genealogie.DAL.Client.Services
{
    public class ThemeService : IThemeRepository<Theme>
    {

        private IThemeRepository<gl.Theme> _rep;

        public ThemeService() { this._rep = new ThemeRepository(); }

        public bool Activer(int id)
        {
            return _rep.Activer(id);
            throw new NotImplementedException();
        }

        public int Creer(Theme e)
        {
            return _rep.Creer(e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Desactiver(int id)
        {
            return _rep.Desactiver(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Theme> Donner()
        {
            return _rep.Donner().Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public Theme Donner(int id)
        {
            return _rep.Donner(id).VersClient();
            throw new NotImplementedException();
        }

        public IList<Theme> Donner(IList<int> l, string[] options = null)
        {
            return _rep.Donner(l, options).Select(r => r.VersClient()).ToList();
            throw new NotImplementedException();
        }

        public IEnumerable<Theme> Donner(IEnumerable<int> ie, string[] options = null)
        {
            return _rep.Donner(ie, options).Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public int? DonnerParNom(string nom)
        {
            return _rep.DonnerParNom(nom);
            throw new NotImplementedException();
        }

        public bool Modifier(int id, Theme e)
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
