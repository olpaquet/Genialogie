
using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IUtilisateurRepository<TE>: IBase<TE>, IAct<TE>
    {
        bool UtilisateurValide(string login, string motdepasse, string[] option = null);
        bool ChangerMotDePasse(string login, string vieuxmotdepasse, string nouveaumotdepasse, string[] option = null);
        TE DonnerUtilisateur(string login, string motDePasse);
        bool EstAdmin(int id);
    }
}
