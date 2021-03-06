﻿using BoiteAOutil.DB.Standard;
using BoiteAOutil.Hasard;
using Genealogie.DAL.Global.Conversion;
using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public class UtilisateurRepository : BaseRepository, IUtilisateurRepository<Utilisateur>
    {

        private const string CONST_UTILISATEUR_REQ = "select id,login,nom,prenom,email,datedenaissance,homme,cartedepayement, actif from Utilisateur";
        public int Creer(Utilisateur e)
        {                        
            Commande com = new Commande("Utilisateur_cre", true);
            com.AjouterParametre("id", 0, true);
            com.AjouterParametre("login", e.login);
            com.AjouterParametre("nom", e.nom);
            com.AjouterParametre("prenom", e.prenom);
            com.AjouterParametre("email", e.email);
            com.AjouterParametre("datedenaissance", e.datedenaissance);
            com.AjouterParametre("homme", e.homme);
            com.AjouterParametre("cartedepayement", e.cartedepayement);
            com.AjouterParametre("motdepasse", e.motdepasse);
            com.AjouterParametre("presel", GenererHasard.PhraseAleatoire());
            com.AjouterParametre("postsel", GenererHasard.PhraseAleatoire());
            com.AjouterParametre("roles", e.lroles);

            try
            {
                _connexion.ExecuterNonRequete(com);
            }
            catch (Exception)
            {
                throw;
                
            }
            return (int)(com.Parametres["id"].Valeur);
        }
        public bool Modifier(int id, Utilisateur e)
        {
            Commande com = new Commande("Utilisateur_mod", true);
            com.AjouterParametre("id", id);
            com.AjouterParametre("nom", e.nom);
            com.AjouterParametre("prenom", e.prenom);
            com.AjouterParametre("email", e.email);
            com.AjouterParametre("datedenaissance", e.datedenaissance);
            com.AjouterParametre("homme", e.homme);
            com.AjouterParametre("cartedepayement", e.cartedepayement);
            com.AjouterParametre("roles", e.lroles);

            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Supprimer(int id)
        {
            Commande com = new Commande("Utilisateur_eff", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Activer(int id)
        {
            Commande com = new Commande("Utilisateur_act", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Desactiver(int id)
        {
            Commande com = new Commande("Utilisateur_desact", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public IEnumerable<Utilisateur> Donner()
        {
            Commande com = new Commande(CONST_UTILISATEUR_REQ);
            return _connexion.ExecuterLecteur(com, x => x.VersUtilisateur());
        }
        public Utilisateur Donner(int id)
        {
            Commande com = new Commande($"{CONST_UTILISATEUR_REQ} where id=@id");
            com.AjouterParametre("id", id);
            return _connexion.ExecuterLecteur(com, x => x.VersUtilisateur()).SingleOrDefault();
        }
        public IEnumerable<Utilisateur> Donner(IEnumerable<int> ie, string[] options = null)
        {
            string requete = $"{CONST_UTILISATEUR_REQ} where actif = 1";
            string clause = "";
            int c = 0;
            Dictionary<string, int> dp = new Dictionary<string, int>();
            if (ie != null)
            {
                foreach (int i in ie)
                {
                    c++;
                    clause += clause == "" ? "" : " or ";
                    clause += $"id = @i{c}";
                    dp.Add($"i{c}", i);
                }
            }
            if (c > 0) clause = $"or ({clause})";
            Commande com = new Commande($"{requete} {clause}");
            foreach (KeyValuePair<string, int> k in dp)
            {
                com.AjouterParametre(k.Key, k.Value);
            }
            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateur());
            throw new NotImplementedException();
        }

        public bool ValiderUtilisateur(string login, string motdepasse, string[] options = null)
        {
            if (login == null) return false;
            Commande com = new Commande("select dbo.ControlerUtilisateur(@login,@motdepasse,@option)");
            com.AjouterParametre("login", login);
            com.AjouterParametre("motdepasse", motdepasse);
            com.AjouterParametre("option", "");
            int j = (int)_connexion.ExecuterScalaire(com);
            return j > 0;
            throw new NotImplementedException();
            
        }

        public bool ChangerMotDePasse(string login, string vieuxmotdepasse, string nouveaumotdepasse, string[] option = null)
        {
            /*
             * (@login varchar(50), @vieuxmotdepasse varchar(50), 
             * @motdepasse varchar(50), @reponse 0 false, 1 true
            int out, @option nvarchar(max))
             * */
            Commande com = new Commande("pchangermotdepasse",true);
            com.AjouterParametre("xreponse", 0, true);
            com.AjouterParametre("xlogin", login);
            com.AjouterParametre("vieuxmotdepasse", vieuxmotdepasse);
            com.AjouterParametre("motdepasse", nouveaumotdepasse);
            com.AjouterParametre("xoption", null);
            _connexion.ExecuterNonRequete(com);
            //return (int)_connexion.ExecuterScalaire(com)==1;
            return (int)com.Parametres["xreponse"].Valeur==1;
            throw new NotImplementedException();
        }

        public Utilisateur Donner(string login, string motDePasse)
        {
            Commande com = new Commande($"{CONST_UTILISATEUR_REQ} where login = @login and dbo.ConstruireHMotdepasse(@motdepasse, presel,postsel) = motdepasse");
            com.AjouterParametre("login", login);
            com.AjouterParametre("motdepasse", motDePasse);
            return _connexion.ExecuterLecteur(com, j => j.VersUtilisateur()).SingleOrDefault();
            throw new NotImplementedException();
        }

        public bool EstAdmin(int id)
        {
            UtilisateurRoleRepository urr = new UtilisateurRoleRepository();
            return (urr.EstAdmin(id));
            throw new NotImplementedException();
        }

        public int? DonnerParNom(string login)
        {
            Commande com = new Commande($"{CONST_UTILISATEUR_REQ} where login = @login");
            com.AjouterParametre("login", login);
            Utilisateur u = _connexion.ExecuterLecteur(com, j => j.VersUtilisateur()).SingleOrDefault();
            return (u == null) ? (int?)null : (int?)u.id;
            throw new NotImplementedException();
        }
    }
}
