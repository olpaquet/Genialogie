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

        public Utilisateur Donner(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/Donner/{id}").Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return reponse.Content.ReadAsAsync<Utilisateur>().Result;
        }

        public bool Creer(int id, Utilisateur e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Creer/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }

        public bool Modifier(int id, Utilisateur e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Modifier/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }

        public bool Activer(int id)
        {
            /*string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");*/
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Modifier/", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }
        public bool Desactiver(int id)
        {
            /*string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");*/
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Modifier/", null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }

        public bool Supprimer(int id, Utilisateur e)
        {
            string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Supprimer/", contenu).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }
        
        public bool EstAdmin(int id)
        {
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/EstAdmin{id}").Result;
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception des données");
            return true;
        }
    }
}