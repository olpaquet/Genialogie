using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IBase<TE>
    {
        int Creer(TE e);
        bool Modifier(int id, TE e);
        bool Supprimer(int id);

        IEnumerable<TE> Donner();
        TE Donner(int id);
    }
}
