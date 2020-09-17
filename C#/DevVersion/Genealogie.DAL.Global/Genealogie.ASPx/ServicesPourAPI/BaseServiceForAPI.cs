using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Genealogie.ASP.ServicesPourAPI
{
    public class BaseServiceForAPI
    {
        private HttpClient _client;
        private string _base_uri;

        public BaseServiceForAPI(string base_uri, string login = null, string motDePasse = null)
        {
            NetworkCredential credential;
            HttpClientHandler handler = null;
            if (!(login is null && password is null))
            {
                credential = new NetworkCredential(login, password);
                handler = new HttpClientHandler { Credentials = credential };
            }
            _client = (handler is null) ? new HttpClient() : new HttpClient(handler);
            _base_uri = base_uri;
            _client.BaseAddress = new Uri(_base_uri);
        }
    }

}