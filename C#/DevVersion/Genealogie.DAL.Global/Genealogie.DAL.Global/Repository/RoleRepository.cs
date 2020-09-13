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
    public class RoleRepository : BaseRepository, IRoleRepository<Role>, IParNom
    {
        private const string CONST_ROLE_REQ = "select id,nom,description,actif from role";

        public int Creer(Role e)
        {
            Commande com = new Commande("Role_cre", true);
            com.AjouterParametre("id", null, true);
            com.AjouterParametre("nom", e.nom);
            com.AjouterParametre("description", e.description);
            com.AjouterParametre("actif", e.actif);
            _connexion.ExecuterNonRequete(com);
            return (int)com.Parametres["id"].Valeur;
        }
        public bool Modifier(int id, Role e)
        {
            Commande com = new Commande("Role_mod", true);
            com.AjouterParametre("id", e.id);
            com.AjouterParametre("nom", e.nom);
            com.AjouterParametre("description", e.description);
            com.AjouterParametre("actif", e.actif);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Supprimer(int id)
        {
            Commande com = new Commande("Role_eff", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Activer(int id)
        {
            Commande com = new Commande("Role_act", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public bool Desactiver(int id)
        {
            Commande com = new Commande("Role_desact", true);
            com.AjouterParametre("id", id);
            return (int)_connexion.ExecuterNonRequete(com) > 0;
        }
        public IEnumerable<Role> Donner()
        {
            Commande com = new Commande(CONST_ROLE_REQ);
            return _connexion.ExecuterLecteur(com, x => x.VersRole());
        }
        public Role Donner(int id)
        {
            Commande com = new Commande($"{CONST_ROLE_REQ} where id=@id");
            com.AjouterParametre("id", id);
            return _connexion.ExecuterLecteur(com, x => x.VersRole()).SingleOrDefault();
        }
        public IEnumerable<Role> Donner(IEnumerable<int> ie, string[] options = null)
        {
            string requete = $"{CONST_ROLE_REQ} where actif = 1";
            string clause = "";
            int c = 0;
            Dictionary<string, int> dp = new Dictionary<string, int>();
            foreach (int i in ie)
            {
                c++;
                clause += clause == "" ? "" : " or ";
                clause += "id = @i{c}";
                dp.Add($"i{c}", i);
            }
            if (c > 0) clause = $"or ({clause})";
            Commande com = new Commande($"{requete} {clause}");
            foreach (KeyValuePair<string, int> k in dp)
            {
                com.AjouterParametre(k.Key, k.Key);
            }
            return _connexion.ExecuterLecteur(com, j => j.VersRole());
            throw new NotImplementedException();
        }
        public int? DonnerParNom(string nom)
        {
            if (nom == null) return null;
            Commande com = new Commande($"select nom from ({CONST_ROLE_REQ} where nom = @nom)");
            com.AjouterParametre("nom", nom);
            return (int?)_connexion.ExecuterScalaire(com);
        }

        public bool EstAdmin(int id)
        {
            return id == 1;
            throw new NotImplementedException();
        }
        //throw new NotImplementedException();

    }
}
