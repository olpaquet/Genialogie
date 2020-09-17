using BoiteAOutil.DB.Standard;
using Genealogie.DAL.Global.Conversion;
using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data.SqlClient;
using System.Text;


namespace Genealogie.DAL.Global.Repository
{
    public class ThemeRepository : BaseRepository, IThemeRepository<Theme>
    {
        private const string CONST_THEME_REQ = "select id,titre,description,actif from Theme";

        public int Creer(Theme e)
        {
            Commande com = new Commande("Theme_cre", true);
            com.AjouterParametre("id", 0, true);
            com.AjouterParametre("titre", e.titre);
            com.AjouterParametre("description", e.description);

            _connexion.ExecuterNonRequete(com);
            return (int)com.Parametres["id"].Valeur;
        }
        public bool Modifier(int id, Theme e)
        {
            Commande com = new Commande("Theme_mod", true);
            com.AjouterParametre("id", e.id);
            com.AjouterParametre("titre", e.titre);
            com.AjouterParametre("description", e.description);

            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Supprimer(int id)
        {
            Commande com = new Commande("Theme_eff", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Activer(int id)
        {
            Commande com = new Commande("Theme_act", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Desactiver(int id)
        {
            Commande com = new Commande("Theme_desact", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public IEnumerable<Theme> Donner()
        {
            Commande com = new Commande(CONST_THEME_REQ);
            return _connexion.ExecuterLecteur(com, x => x.VersTheme());
        }
        public Theme Donner(int id)
        {
            Commande com = new Commande($"{CONST_THEME_REQ} where id=@id");
            com.AjouterParametre("id", id);
            return _connexion.ExecuterLecteur(com, x => x.VersTheme()).SingleOrDefault();
        }
        public IEnumerable<Theme> Donner(IEnumerable<int> ie, string[] options = null)
        {
            string requete = $"{CONST_THEME_REQ} where actif = 1";
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
            return _connexion.ExecuterLecteur(com, j => j.VersTheme());
            throw new NotImplementedException();
        }
        public int? DonnerParNom(string nom)
        {
            if (nom == null) return null;
            Commande com = new Commande($"{CONST_THEME_REQ} where nom = @nom)");
            com.AjouterParametre("nom", nom);
            Theme r = _connexion.ExecuterLecteur(com, j => j.VersTheme()).SingleOrDefault();
            return (r == null) ? (int?)null : (int?)r.id;

        }        
        //throw new NotImplementedException();

    }
}
