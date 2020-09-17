using BoiteAOutil.DB.Standard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;


namespace Genealogie.DAL.Global.Repository
{
    public abstract class BaseRepository

    {
        protected Connexion _connexion;
        private ConnectionStringSettings _ConnectionString { get { return ConfigurationManager.ConnectionStrings["Genealogie.Sql"]; } }
        protected DbProviderFactory _dbpf { get { return _dbpf; } private set { } }

        public BaseRepository()
        {
            
            _connexion = new Connexion(_ConnectionString.ConnectionString, SqlClientFactory.Instance);
        }
    }
}
