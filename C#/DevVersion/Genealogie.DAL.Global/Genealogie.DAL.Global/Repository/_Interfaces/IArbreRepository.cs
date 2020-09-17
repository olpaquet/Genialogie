using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IArbreRepository<TE> : IBase<TE>, IAct<TE>, IParNom
    {
    }
}
