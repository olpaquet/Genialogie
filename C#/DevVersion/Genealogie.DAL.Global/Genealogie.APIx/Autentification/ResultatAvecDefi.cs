using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Genealogie.API.Autentification
{
    public class ResultatAvecDefi : IHttpActionResult
    {
        private readonly IHttpActionResult _suivant;
        private readonly string _indice;

        public ResultatAvecDefi(IHttpActionResult suivant, string indice)
        {
            this._suivant = suivant;
            this._indice = indice;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken tokenAnnulation)
        {
            var result = await _suivant.ExecuteAsync(tokenAnnulation);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._indice));
            }
            return result;
        }
    }
}