using Genealogie.API.Conversion;
using Genealogie.API.Models;
using Genealogie.DAL.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Genealogie.API.Controllers
{
    public class UtilisateurRoleController : ApiController
    {
        [HttpGet]
        public IEnumerable<Role> DonnerRoles(int id)
        {
            UtilisateurRoleService urs = new UtilisateurRoleService();
            return urs.DonnerRoleParUtilisateur(id).Select(j => j.VersAPI());
        }

        [HttpGet]
        public IEnumerable<Utilisateur> DonnerUtilisateurs(int id)
        {
            UtilisateurRoleService urs = new UtilisateurRoleService();
            return urs.DonnerUtilisateurParRole(id).Select(j => j.VersAPI());
        }
    }
}
