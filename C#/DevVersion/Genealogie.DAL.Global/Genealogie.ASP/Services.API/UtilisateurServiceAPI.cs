using Genealogie.ASP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
//using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Genealogie.ASP.Services.API
{

    /* pour faire fonctionner ReadAsAsync, il faut installer la dernière version de Microsoft.AspNet.WebApi.Client */
    public class UtilisateurServiceAPI : BaseServiceAPI
    {
        
        [HttpGet]
        public IEnumerable<Utilisateur> Donner()
        {
            HttpResponseMessage reponse = _client.GetAsync("Utilisateur/Donner/").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            
            return reponse.Content.ReadAsAsync<IEnumerable<Utilisateur>>().Result;

        }

        [HttpGet]
        public Utilisateur Donner(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/Donner/{id}").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return reponse.Content.ReadAsAsync<Utilisateur>().Result;
        }

        public int Creer(Utilisateur e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Creer/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToInt32(reponse.Content.ReadAsStringAsync().Result);
        }

        public bool Modifier(int id, Utilisateur e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync($"Utilisateur/Modifier/{id}", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean((reponse.Content.ReadAsStringAsync().Result));
            /*Boolean.TryParse("", cha);
            return cha;*/
        }

        public bool Activer(int id)
        {
            /*string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");*/
            HttpResponseMessage reponse = _client.PutAsync($"Utilisateur/Activer/{id}", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean(reponse.Content.ReadAsStringAsync().Result);
        }
        public bool Desactiver(int id)
        {
            
            HttpResponseMessage reponse = _client.PutAsync($"Utilisateur/Desactiver/{id}", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean(reponse.Content.ReadAsStringAsync().Result);
        }

        public bool Supprimer(int id)
        {
            
            HttpResponseMessage reponse = _client.DeleteAsync($"Utilisateur/Supprimer/{id}").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return Convert.ToBoolean(reponse.Content.ReadAsStringAsync().Result);
        }
        
        public bool EstAdmin(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/EstAdmin{id}").Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception des données");
            return Convert.ToBoolean(reponse.Content.ReadAsStringAsync().Result);
        }

        public bool ValiderUtilisateur(UtilisateurConnexion uc)
        {
            /* !!!! putasync !!! */
            string contenuJson = JsonConvert.SerializeObject(uc, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync("Utilisateur/ValiderUtilisateur/", contenu).Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception des données");
            string t = reponse.Content.ReadAsStringAsync().Result;
            return Convert.ToBoolean(t);
        }

        public Utilisateur DonnerUtilisateur(UtilisateurConnexion uc)
        {
            string contenuJson = JsonConvert.SerializeObject(uc, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PutAsync("Utilisateur/DonnerUtilisateur/", contenu).Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception de données");
            return reponse.Content.ReadAsAsync<Utilisateur>().Result; 

        }
    }
}