using Genealogie.ASP.Models;
using Genealogie.DAL.Global.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Genealogie.ASP.Services.API
{
    public class UtilisateurRoleServiceAPI : BaseServiceAPI /*, IUtilisateurRoleRepository<UtilisateurRole, Role, Utilisateur>*/
    {
        /*public bool Creer(int id1, int id2, UtilisateurRole e)
        {
            throw new NotImplementedException();
        }*/

        /*public IEnumerable<UtilisateurRole> Donner()
        {
            throw new NotImplementedException();
        }*/

        /*public UtilisateurRole Donner(int id1, int id2)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<Role> DonnerRolesParUtilisateur(int id)
        {
            
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/DonnerRoles/{id}").Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception de données");
            return reponse.Content.ReadAsAsync<IEnumerable<Role>>().Result;

            throw new NotImplementedException();
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> DonnerUtilisateursParRole(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/DonnerUtilisateurs/{id}").Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception de données");
            return reponse.Content.ReadAsAsync<IEnumerable<Utilisateur>>().Result;

            throw new NotImplementedException();
        }

        /*public bool EstAdmin(int idUtilisateur)
        {
            throw new NotImplementedException();
        }*/

        /*public bool Modifier(int id1, int id2, UtilisateurRole e)
        {
            throw new NotImplementedException();
        }*/

        /*public bool Supprimer(int id1, int id2)
        {
            throw new NotImplementedException();
        }*/
    }
}