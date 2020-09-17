
using Genealogie.DAL.Client.Modeles;
using gl=Genealogie.DAL.Global.Modeles;
using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Genealogie.DAL.Client.Conversion;
using System.Linq;

namespace Genealogie.DAL.Client.Services
{
    public class UtilisateurService: IUtilisateurRepository<Utilisateur>
    {
        private IUtilisateurRepository<gl.Utilisateur> _rep;

        public UtilisateurService() { this._rep = new UtilisateurRepository(); }

        public bool Activer(int id)
        {
            return _rep.Activer(id);
            throw new NotImplementedException();
        }

        public bool ChangerMotDePasse(string login, string vieuxmotdepasse, string nouveaumotdepasse, string[] option = null)
        {
            return _rep.ChangerMotDePasse(login, vieuxmotdepasse, nouveaumotdepasse, option);
            throw new NotImplementedException();
        }

        public int Creer(Utilisateur e)
        {
            return _rep.Creer(e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Desactiver(int id)
        {
            return _rep.Desactiver(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> Donner()
        {
            return _rep.Donner().Select(k => k.VersClient());
            throw new NotImplementedException();
        }

        public Utilisateur Donner(int id)
        {
            return _rep.Donner(id).VersClient();
            throw new NotImplementedException();
        }

        

        public IEnumerable<Utilisateur> Donner(IEnumerable<int> ie, string[] options = null)
        {
            return _rep.Donner(ie, options).Select(j => j.VersClient());
            throw new NotImplementedException();
        }

        public Utilisateur Donner(string login, string motDePasse)
        {
            return _rep.Donner(login, motDePasse).VersClient();
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

        public bool Modifier(int id, Utilisateur e)
        {
            var xx = e.VersGlobal();
            return _rep.Modifier(id, e.VersGlobal());
            throw new NotImplementedException();
        }

        public bool Supprimer(int id)
        {
            return _rep.Supprimer(id);
            throw new NotImplementedException();
        }

        public bool ValiderUtilisateur(string login, string motdepasse, string[] option = null)
        {
            return _rep.ValiderUtilisateur(login, motdepasse, option); 
            throw new NotImplementedException();
        }
    }
}
