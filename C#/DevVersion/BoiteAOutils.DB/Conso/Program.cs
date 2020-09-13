



using BoiteAOutil.DB.Standard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conso
{
    class Program
    {
        static void Main(string[] args)
        {

            ConnectionStringSettings st = ConfigurationManager.ConnectionStrings["Genealogie.Sql"];
            DbProviderFactory dbf = DbProviderFactories.GetFactory(st.ProviderName);
            Connexion conn = new Connexion(st.ConnectionString, dbf);

            
            ajouterrole("P.Lion",conn);
            
            listerroles(conn);

            nouvelutilisateur("Pippa", conn);


            
            
            


            Console.ReadKey();
        }

        private static void listerroles(Connexion conn)
        {
            Commande comm = new Commande("select * from role");
            IEnumerable<Role> roles = conn.ExecuterLecteur(comm, j => new Role { id = (int)j["id"], nom = (string)j["nom"], description = (string)j["description"], actif = (int)j["actif"] });

            Console.WriteLine("Roles....");
            foreach (Role r in roles)
            {
                Console.WriteLine($"({r.actif}){r.id} : {r.nom} *** {r.description}");
            }
        }
        private static void ajouterrole(string nom, Connexion conn)
        {
            Commande comm = new Commande("role_cre", true);
            comm.AjouterParametre("nom", nom);
            comm.AjouterParametre("description", $"{nom} je regarde tout le temps...");
            comm.AjouterParametre("id", 0, true);

            try
            {
                conn.ExecuterNonRequete(comm);
            }
            catch (Exception)
            {


            }
            Console.WriteLine($"sortie : {(int?)comm.Parametres["id"].Valeur}");
        }
        private static void nouvelutilisateur(string login, Connexion conn)
        {
            /* creer utilisateur */
            Commande com = new Commande("utilisateur_cre", true);
            com.AjouterParametre("id", 0, true);
            com.AjouterParametre("login", login);
            com.AjouterParametre("nom", "Paquet");
            com.AjouterParametre("prenom", null);
            com.AjouterParametre("email", "olivier@paquet.com");
            com.AjouterParametre("datedenaissance", new DateTime(2005,10,20));
            com.AjouterParametre("homme", 1);
            com.AjouterParametre("cartedepayement", "lmqkjfmqj");
            com.AjouterParametre("motdepasse", "1");
            com.AjouterParametre("presel", "presel");
            com.AjouterParametre("postsel", "postsel");

            try
            {
                conn.ExecuterNonRequete(com);
            }
            catch (Exception)
            {

                
            }

            Console.WriteLine($"utilisateur créé {com.Parametres["id"].Valeur}");

        }
    }
    public class Role
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int actif { get; set; }
    }
}
