using Genealogie.DAL.Global.Repository;
using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace conso
{
    class Program
    {
        static void Main(string[] args)
        {
            creerrole("Samantha Fox");
            donner();
            creerutilisateur("johnny hallyday");
            controlersuperadmin();
             changermotdepasseadmin("1", "2");
            Console.ReadKey();
        }

        public static void controlersuperadmin()
        {
            Console.WriteLine("*** controle super admin ***");
            UtilisateurRepository ur = new UtilisateurRepository();
            Console.WriteLine($"{ur.UtilisateurValide("admin","1")}");
        }
        public static void donner()
        {
            //string _connectionString = ConfigurationManager.ConnectionStrings["Genealogie.Sql"].ConnectionString;
            

            Console.WriteLine("****donner****");

            RoleRepository rr = new RoleRepository();

            IEnumerable<Role> roles = rr.Donner();
            foreach (Role r in roles) { Console.WriteLine($"{r.id} : {r.nom}"); }
         
            


            Console.WriteLine("**************");
        }

        public static void creerutilisateur(string login)
        {

            Utilisateur u = new Utilisateur();
            u.email = "email@e.be";
            u.login = login;
            u.nom = "coucou";
            u.postsel = "postsel";
            u.presel = "presel";
            u.motdepasse = "2";

            UtilisateurRepository ur = new UtilisateurRepository();
            Console.WriteLine($"j'ai créé utilisateur {login} : {ur.Creer(u)}");     

        }

        private static void changermotdepasseadmin(string ancien, string nouveau)
        {
            UtilisateurRepository ur = new UtilisateurRepository();
            Console.WriteLine($"changé? {ur.ChangerMotDePasse("admin",ancien,nouveau)}");

        }
        private static void creerrole(string xnom)
        {
            RoleRepository rr = new RoleRepository();
            Role r = new Role { description = $"description de {xnom}", nom = xnom };

            Console.WriteLine($"Créé? {rr.Creer(r)}");


        }
    }
} 
