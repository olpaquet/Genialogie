using Genealogie.ASP.Securite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Genealogie.ASP.Services.API
{
    

    public class BaseServiceAPI
    {
        protected HttpClient _client;
        protected string _baseUrl = ConnexionUtilisateur.baseUrl;
        private string _login = ConnexionUtilisateur.login;
        private string _motDePasse = ConnexionUtilisateur.motDePasse;

        public BaseServiceAPI( )
        {
            NetworkCredential identifiant;
            HttpClientHandler gestionnaire = null;
            if (!(_login is null && _motDePasse is null))
            {
                identifiant = new NetworkCredential(_login, _motDePasse);
                gestionnaire = new HttpClientHandler { Credentials = identifiant };
            }
            _client = (gestionnaire is null) ? new HttpClient() : new HttpClient(gestionnaire);
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}