using Genealogie.API.Conversion;
using Genealogie.API.Models;
using Genealogie.DAL.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Genealogie.API.Autentification
{
    public class BasicAuthenticatorAttribute : Attribute, IAuthenticationFilter
    {
        //private List<Utilisateur> _users { get => MockUp.users; }

        private readonly string _indice;
        

        public BasicAuthenticatorAttribute(string attr)
        {
            this._indice = attr;
            
        }

        public bool AllowMultiple { get => false; }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage requeteHttp = context.Request;
            try
            {
                if (requeteHttp.Headers.Authorization is null) throw new UnauthorizedAccessException();
                if (!requeteHttp.Headers.Authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)) throw new UnauthorizedAccessException();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string identifiant = encoding.GetString(Convert.FromBase64String(requeteHttp.Headers.Authorization.Parameter));
                string[] tableauIdentifiant = identifiant.Split(':');
                if (tableauIdentifiant.Length > 2) throw new UnauthorizedAccessException();
                /*string login = tableauIdentifiant[0].Trim();
                string motDePasse = tableauIdentifiant[1].Trim();*/
                string login = tableauIdentifiant[0];
                string motDePasse = tableauIdentifiant[1];
                if (login == "") throw new UnauthorizedAccessException();

                Utilisateur utilisateur = new UtilisateurService().DonnerUtilisateur(login, motDePasse).VersAPI();
                                
                if (utilisateur==null) throw new UnauthorizedAccessException();
                //if (!(utilisateur.IsAdmin && this._indice == "Admin")) throw new UnauthorizedAccessException();
                List<Claim> revendications = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, login)
                };
                ClaimsIdentity identite = new ClaimsIdentity(revendications, "basic");
                ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identite });
                context.Principal = principal;
            }
            catch (UnauthorizedAccessException)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], requeteHttp);
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultatAvecDefi(context.Result, _indice);
            return Task.FromResult(0);
        }

        
    }
}