using Genealogie.API.Conversion;
using Genealogie.API.Models;
using Genealogie.DAL.Client.Services;
//using Genealogie.DAL.Global.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Genealogie.API.Controllers
{
    public class RoleController : ApiController /*, IRoleRepository<Role>*/
    {
        [HttpGet]
        public IEnumerable<Role> DonnerTout()
        {
            RoleService us = new RoleService();

            return us.Donner().Select(j => j.VersAPI());
        }

        [HttpGet]
        public Role Donner(int id)
        {
            RoleService us = new RoleService();
            return us.Donner(id).VersAPI();
        }

        [HttpPut]
        public bool Activer(int id)
        {
            RoleService us = new RoleService();
            return us.Activer(id);

        }

        [HttpPut]
        public bool Desactiver(int id)
        {
            RoleService us = new RoleService();
            return us.Desactiver(id);
        }

        [HttpPut]
        public bool Modifier(int id, Role e)
        {
            RoleService us = new RoleService();
            return us.Modifier(id, e.VersClient());
        }
        [HttpPost]
        public int Creer(Role e)
        {
            RoleService us = new RoleService();
            return us.Creer(e.VersClient());
        }

        [HttpGet]
        public bool EstAdmin(int id)
        {
            RoleService rs = new RoleService();
            return rs.EstAdmin(id);
        }

        [HttpDelete]
        public bool Supprimer(int id)
        {
            RoleService rs = new RoleService();
            return rs.Supprimer(id);
        }

        [HttpGet]
        public IEnumerable<Role> DonnerX(ObjetDonnerListe odl)
        {
            RoleService rs = new RoleService();
            return rs.Donner(odl.ienum, odl.options).Select(j => j.VersAPI());
            throw new NotImplementedException();
        }
    }
}
