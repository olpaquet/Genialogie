using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IBase2<TE>
    {
        bool Creer(int id1, int id2, TE e);
        bool Modifier(int id1, int id2, TE e);
        bool Supprimer(int id1, int id2);

        IEnumerable<TE> Donner();
        TE Donner(int id1, int id2);
    }
}
