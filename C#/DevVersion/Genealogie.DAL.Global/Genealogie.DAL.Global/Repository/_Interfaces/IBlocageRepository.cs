using Genealogie.DAL.Global.Modeles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IBlocageRepository<Blocage> : IBase<Blocage>, IAct<Blocage>, IParNom
    {
    }
}
