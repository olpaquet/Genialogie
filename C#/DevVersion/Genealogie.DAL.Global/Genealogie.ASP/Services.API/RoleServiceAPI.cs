using Genealogie.ASP.Models;
using Newtonsoft.Json;
//using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Genealogie.ASP.Services.API
{
    public class RoleServiceAPI : BaseServiceAPI /*, IRoleRepository<Role>*/
    {
        public bool Activer(int id)
        {            
            HttpResponseMessage reponse = _client.PutAsync($"Role/Activer/{id}", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean((reponse.Content.ReadAsStringAsync().Result));
            throw new NotImplementedException();
        }

        public int Creer(Role e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PostAsync($"Role/Creer/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToInt32((reponse.Content.ReadAsStringAsync().Result));
            throw new NotImplementedException();
        }

        public bool Desactiver(int id)
        {
            HttpResponseMessage reponse = _client.PutAsync($"Role/Desactiver/{id}", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean((reponse.Content.ReadAsStringAsync().Result));
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Donner()
        {
            HttpResponseMessage reponse = _client.GetAsync($"Role/Donner/").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return reponse.Content.ReadAsAsync<IEnumerable<Role>>().Result;
            throw new NotImplementedException();
        }

        public Role Donner(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Role/Donner/{id}").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return reponse.Content.ReadAsAsync<Role>().Result;
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Donner(ObjetDonnerListe e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync($"Role/Donner/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return reponse.Content.ReadAsAsync<IEnumerable<Role>>().Result;
            throw new NotImplementedException();
        }

        public int? DonnerParNom(ChercherPar cp)
        {
            string contenuJson = JsonConvert.SerializeObject(cp, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync($"Role/DonnerParNom/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            var x = reponse.Content.ReadAsStringAsync().Result;
            if (x == null) return null;
            else return int.Parse(reponse.Content.ReadAsStringAsync().Result);
            throw new NotImplementedException();
        }
        
        public bool Modifier(int id, Role e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync($"Role/Modifier/{id}", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            var x = reponse.Content.ReadAsStringAsync().Result;
            return bool.Parse(x);
            throw new NotImplementedException();
        }

        public bool Supprimer(int id)
        {            
            HttpResponseMessage reponse = _client.DeleteAsync($"Role/Supprimer/{id}").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            var x = reponse.Content.ReadAsStringAsync().Result;
            return bool.Parse(x);
            throw new NotImplementedException();
        }
    }
}