using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IAct<TE>
    {
        bool Activer(int id);
        bool Desactiver(int id);
        
        IEnumerable<TE> Donner(IEnumerable<int> ie, string[] options = null);
    }
}
