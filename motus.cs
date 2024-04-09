using System;
using System.Collections.Generic;
using System.IO;

namespace Motus
{
    class motus
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans Motus");

            List<string> mots = ChargerMots("mots.txt");

            do
            {
                string motCache = ChoisirMot(mots);
                int lenMot = motCache.Length;

                Console.WriteLine($"Le mot caché contient {lenMot} caracteres et commence par {motCache[0]}");

                int tentativesRestantes = 6;
                string motSaisi;

                do
                {
                    Console.WriteLine($"Il vous reste {tentativesRestantes} coups à jouer");
                    motSaisi = GetMot(lenMot);
                }
                while (!TestMot(motCache, motSaisi) && --tentativesRestantes > 0);

                if (tentativesRestantes == 0)
                {
                    Console.WriteLine($"Désolé, vous avez perdu ! Le mot caché était : {motCache}");
                }
                else
                {
                    Console.WriteLine($"Bravo, vous avez gagné ! Le mot caché était : {motCache}");
                }

                Console.WriteLine("Voulez-vous rejouer ? (o / n)");
            }
            while (Console.ReadLine().ToLower() == "o");
        }

        static void AfficherCouleur(string texte, ConsoleColor couleur)
        {
            Console.ForegroundColor = couleur;
            Console.Write(texte);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string GetMot(int lenMot)
        {
            string motSaisi;
            do
            {
                Console.Write($"Entrez votre mot de {lenMot} caractères : ");
                motSaisi = Console.ReadLine();
            }
            while (motSaisi.Length != lenMot);

            return motSaisi.ToUpper();
        }

        static int GetNombreAleatoire(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        static List<string> ChargerMots(string fileName)
        {
            List<string> mots = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mots.Add(line.ToUpper());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la lecture du fichier : {e.Message}");
            }
            return mots;
        }

        static string ChoisirMot(List<string> mots)
        {
            int index = GetNombreAleatoire(0, mots.Count - 1);
            return mots[index];
        }

        static bool TestMot(string motCache, string motSaisi)
        {
            bool[] lettreCorrecte = new bool[motCache.Length];

            for (int i = 0; i < motCache.Length; i++)
            {
                if (motCache[i] == motSaisi[i])
                {
                    AfficherCouleur(motSaisi[i].ToString(), ConsoleColor.Red);
                    lettreCorrecte[i] = true;
                }
            }

            for (int i = 0; i < motCache.Length; i++)
            {
                if (!lettreCorrecte[i])
                {
                    if (motCache.Contains(motSaisi[i]))
                    {
                        AfficherCouleur(motSaisi[i].ToString(), ConsoleColor.Yellow);
                    }
                    else
                    {
                        Console.Write(motSaisi[i]);
                    }
                }
            }

            Console.WriteLine();
            return motCache.Equals(motSaisi);
        }
    }
}
