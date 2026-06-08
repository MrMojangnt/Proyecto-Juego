using System;
using System.IO;
namespace Proyecto_Juego;

public class zzz
{
    public static void Contry()
    {
        Random rnd = new Random();

        string[] paises =
        {
            "Estados Unidos",
            "España",
            "China",
            "Nicaragua",
            "Japón",
            "Alemania"
        };

        string archivo = "DatosPaises.txt";

        StreamWriter escritor = new StreamWriter(archivo);

        escritor.WriteLine("===== DATOS GENERADOS DEL MUNDO =====");
        escritor.WriteLine();

        for (int i = 0; i < paises.Length; i++)
        {
            double pib = 0;
            double desempleo = 0;
            double inflacion = 0;
            int poblacion = 0;

            switch (paises[i])
            {
                case "Estados Unidos":
                    pib = rnd.Next(25000, 32001);
                    desempleo = rnd.Next(3, 8);
                    inflacion = rnd.Next(1, 6);
                    poblacion = rnd.Next(330, 345);
                    break;

                case "España":
                    pib = rnd.Next(1400, 1901);
                    desempleo = rnd.Next(8, 16);
                    inflacion = rnd.Next(1, 8);
                    poblacion = rnd.Next(47, 50);
                    break;

                case "China":
                    pib = rnd.Next(16000, 22001);
                    desempleo = rnd.Next(3, 7);
                    inflacion = rnd.Next(1, 5);
                    poblacion = rnd.Next(1400, 1420);
                    break;

                case "Nicaragua":
                    pib = rnd.Next(15, 25);
                    desempleo = rnd.Next(2, 8);
                    inflacion = rnd.Next(2, 10);
                    poblacion = rnd.Next(6, 8);
                    break;

                case "Japón":
                    pib = rnd.Next(3500, 5501);
                    desempleo = rnd.Next(2, 5);
                    inflacion = rnd.Next(1, 5);
                    poblacion = rnd.Next(120, 126);
                    break;

                case "Alemania":
                    pib = rnd.Next(3800, 5501);
                    desempleo = rnd.Next(2, 6);
                    inflacion = rnd.Next(1, 5);
                    poblacion = rnd.Next(82, 86);
                    break;
            }

            escritor.WriteLine("País: " + paises[i]);
            escritor.WriteLine("PIB (Miles de millones USD): " + pib);
            escritor.WriteLine("Desempleo (%): " + desempleo);
            escritor.WriteLine("Inflación (%): " + inflacion);
            escritor.WriteLine("Población (Millones): " + poblacion);
            escritor.WriteLine("-------------------------------------");
        }

        escritor.Close();

        Console.WriteLine("Archivo generado correctamente.");
        Console.WriteLine("Revise el archivo: " + archivo);

        Console.ReadKey();
    }
}