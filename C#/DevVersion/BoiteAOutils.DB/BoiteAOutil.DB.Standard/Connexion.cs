using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace BoiteAOutil.DB.Standard
{
    public class Connexion
    {
        private string _connectionString;

        private DbProviderFactory _factory;

        public delegate T Conversion<T>(IDataRecord lecteur); // Func<IDataRecord,T>

        public Connexion(string connectionString, DbProviderFactory dbpf /*string invariantName = "System.Data.SqlClient"*/)
        {
            _connectionString = connectionString;
            _factory = dbpf /* DbProviderFactories.GetFactory(invariantName)*/;
        }

        private DbConnection CreerDBConnection()
        {
            DbConnection connection = _factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) throw new Exception("Impossible d'ouvrir la connection.");
            return connection;
        }

        private DbCommand CreerDBCommand(DbConnection connection, Commande commande)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = commande.SqlRequete;
            dbCommand.CommandType = (commande.EstProcedureStockee) ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
            foreach (Parametre parametre in commande.Parametres.Values)
            {
                DbParameter dbParameter = _factory.CreateParameter();
                dbParameter.ParameterName = parametre.Nom;
                dbParameter.Value = parametre.Valeur;
                dbParameter.Direction = parametre.Direction;
                dbParameter.Size = parametre.Taille;
                dbCommand.Parameters.Add(dbParameter);
            }
            return dbCommand;
        }

        public int ExecuterNonRequete(Commande commande)
        {
            int result;
            using (DbConnection connection = CreerDBConnection())
            {
                using (DbCommand dbCommand = CreerDBCommand(connection, commande))
                {
                    result = dbCommand.ExecuteNonQuery();
                    AjusterValeurParametres(dbCommand, commande);
                }
            }
            return result;
        }

        public object ExecuterScalaire(Commande commande)
        {
            object result;
            using (DbConnection connection = CreerDBConnection())
            {
                using (DbCommand dbCommand = CreerDBCommand(connection, commande))
                {
                    result = dbCommand.ExecuteScalar();
                    AjusterValeurParametres(dbCommand, commande);
                }
            }
            return result;
        }
        public DataTable DonnerTableDonnees(Commande commande)
        {
            DataTable table = new DataTable();
            using (DbConnection connection = CreerDBConnection())
            {
                using (DbCommand dbCommand = CreerDBCommand(connection, commande))
                {
                    DbDataAdapter adapter = _factory.CreateDataAdapter();
                    adapter.SelectCommand = dbCommand;
                    adapter.Fill(table);
                    AjusterValeurParametres(dbCommand, commande);
                }
            }
            return table;
        }


        public IEnumerable<T> ExecuterLecteur<T>(Commande commande, Conversion<T> conv) //ConvertMethod<T> est le même type que le délégué générique de Func<IDataRecord,T>
        {
            //List<T> list = new List<T>();
            using (DbConnection connection = CreerDBConnection())
            {
                using (DbCommand dbCommand = CreerDBCommand(connection, commande))
                {
                    using (DbDataReader lect = dbCommand.ExecuteReader())
                    {
                        AjusterValeurParametres(dbCommand, commande);
                        while (lect.Read())
                        {
                            yield return conv(lect);
                            //list.Add(item);
                        }
                    }
                }
            }
            //return list;
        }

        private void AjusterValeurParametres(DbCommand dbCommand, Commande commande)
        {
            foreach (DbParameter p in dbCommand.Parameters)
            {
                if (p.Direction == ParameterDirection.Output)
                {
                    commande.Parametres[p.ParameterName].Valeur = (p.Value == DBNull.Value) ? null : p.Value;
                }
            }
        }
    }
}
