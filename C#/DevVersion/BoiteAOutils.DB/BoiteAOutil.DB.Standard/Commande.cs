using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BoiteAOutil.DB.Standard
{
    public class Commande
    {
        public string SqlRequete { get; private set; }

        public bool EstProcedureStockee { get; private set; }

        public Dictionary<string, Parametre> Parametres { get; private set; }

        public Commande(string sqlQuery, bool isStoredProcedure = false)
        {
            SqlRequete = sqlQuery;
            EstProcedureStockee = isStoredProcedure;
            Parametres = new Dictionary<string, Parametre>();
        }

        public void AjouterParametre(string parameterName, object value, bool isOutput = false, int size = 0)
        {
            if (Parametres.TryGetValue(parameterName, out _)) throw new ArgumentException("Paramètre déjà présent.", nameof(parameterName));
            Parametre param = new Parametre();
            param.Nom = parameterName;
            param.Valeur = value ?? DBNull.Value;
            param.Direction = (isOutput) ? System.Data.ParameterDirection.Output : System.Data.ParameterDirection.Input;
            param.Taille = size;
            Parametres.Add(parameterName, param);
        }
    }
}
