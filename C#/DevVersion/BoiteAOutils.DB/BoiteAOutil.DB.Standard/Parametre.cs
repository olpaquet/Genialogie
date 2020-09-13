using System;
using System.Data;

namespace BoiteAOutil.DB.Standard
{
    public class Parametre
    {
        public string Nom { get; set; }
        public object Valeur { get; set; }
        public System.Data.ParameterDirection Direction { get; set; }
        public int Taille { get; set; }
    }
}
