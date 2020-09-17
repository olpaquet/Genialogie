using Genealogie.ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genealogie.ASP.Infrastructure
{
    public static class SessionUtilisateur
    {
        public static Utilisateur Utilisateur
        {
            get { return (Utilisateur)HttpContext.Current.Session["utilisateur"]; }
            set
            {
                Utilisateur = value;
                if (value == null)
                {
                    HttpContext.Current.Session["id"] = null;
                    HttpContext.Current.Session["nomaffichage"] = null;
                    HttpContext.Current.Session["admin"] = null;
                }
                else
                {
                    HttpContext.Current.Session["id"] = ((Utilisateur)value).id;
                    HttpContext.Current.Session["nomaffichage"] = ((Utilisateur)value).nomAffichage;
                    HttpContext.Current.Session["admin"] = (Utilisateur)value;
                }
            }
        }

        public static int id { get { return (int)HttpContext.Current.Session["id"]; } }
        public static string nomAffichage { get { return (string)HttpContext.Current.Session["nomaffichage"]; } }

        public static void AssignerUtilisateur(Utilisateur u)
        {
            SessionUtilisateur.Utilisateur = u;
        }
    }
        
}