using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IRoleRepository<TE> : IBase<TE>, IAct<TE>
    {
        bool EstAdmin(int id);
    }
}
