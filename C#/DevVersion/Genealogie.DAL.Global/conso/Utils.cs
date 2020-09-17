using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conso
{
    public static class ConnexionUtilitaire
    {        
        /*public static readonly ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["Genealogie.Sql"];*/
        public static readonly DbProviderFactory UsineGenealogie = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Genealogie.Sql"].ProviderName);
    }
}
