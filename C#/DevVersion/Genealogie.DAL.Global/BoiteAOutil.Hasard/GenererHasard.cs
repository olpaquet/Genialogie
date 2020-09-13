using System;

namespace BoiteAOutil.Hasard
{
    public static class GenererHasard
    {
        private static Random random = new Random();

        public static string PhraseAleatoire(int longueur = 255)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567891234567890°_*¨£%+=^-";
            string stringChars = default;


            for (int i = 0; i < longueur; i++)
            {
                stringChars += chars.Substring(random.Next(chars.Length), 1);

            }

            return stringChars;
        }

    }
}
