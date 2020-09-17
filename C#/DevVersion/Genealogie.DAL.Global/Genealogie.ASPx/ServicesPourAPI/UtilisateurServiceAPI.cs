﻿
//using Genealogie.ASP.Models;
using Genealogie.ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Net.Http;
//using System.Net.Http;
using System.Net;
//using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Genealogie.ASP.ServicesAPI
{
    public class UtilisateurServiceAPI
    {
        private HttpClient _client;

        private string _base_uri;

        public UtilisateurServiceAPI(string base_url, string login = null, string motdepasse = null)
        {
            NetworkCredential identifiant;
            HttpClientHandler gestionnaire = null;
            if (!(login is null && motdepasse is null))
            {
                identifiant = new NetworkCredential(login, motdepasse);
                gestionnaire = new HttpClientHandler { Credentials = identifiant };
            }
            _client = (gestionnaire is null) ? new HttpClient() : new HttpClient(gestionnaire);
            _base_uri = base_url;
            _client.BaseAddress = new Uri(_base_uri);
        }

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

        [HttpPost]
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

        [HttpPut]
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

        [HttpPut]
        public bool Activer(int id)
        {
            /*string contenuJson = JsonConvert.SerializeObject(e, Formatting.Indented);
            StringContent contenu = new StringContent(contenuJson, Encoding.UTF8, "application/json");*/
            HttpResponseMessage reponse = _client.PostAsync("Utilisateur/Modifier/",null).Result;
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return true;
        }
        [HttpPut]
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

        [HttpDelete]
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
            HttpResponseMessage reponse = _client.GetAsync($"Utilisateur/EstAdmin{id}" );
            if (!reponse.IsSuccessStatusCode) throw new Exception("Echec de la réception des données");
            return true;
        }
    }
}