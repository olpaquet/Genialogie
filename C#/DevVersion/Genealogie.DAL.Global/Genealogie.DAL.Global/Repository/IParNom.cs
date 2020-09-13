using System;
using System.Collections.Generic;
using System.Text;

namespace Genealogie.DAL.Global.Repository
{
    public interface IParNom
    {
        int? DonnerParNom(string nom);
    }
}
