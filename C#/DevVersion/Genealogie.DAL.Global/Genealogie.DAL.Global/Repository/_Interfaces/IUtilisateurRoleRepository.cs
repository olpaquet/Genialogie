using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IUtilisateurRoleRepository<TE,TR,TU>:IBase2<TE>
    {
        IEnumerable<TR> DonnerRoleParUtilisateur(int idutilisateur);
        IEnumerable<TU> DonnerUtilisateurParRole(int idrole);
        bool EstAdmin(int idUtilisateur);
    }
}
