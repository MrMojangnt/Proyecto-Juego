using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Juego;

public class CambiosDelMercado
{
    //Relacionado a Noticias y Periodico
    public static Dictionary<int, decimal> PronosticoMercado = new Dictionary<int, decimal>();
    public static string Titulo;
    public static string Descripcion;

    public static void PrepararPronosticoMercado(List<Companias> Companiass)
    {
        PronosticoMercado.Clear();

        foreach (Companias empresa in Companiass)
        {
            decimal cambioBase = (decimal)(Random.Shared.NextDouble() * 0.20 - 0.10);
            decimal cambioNoticias = 0m;
            Events.PasarTurnoPeriodico(ref Titulo, ref Descripcion, Program.InvInt, ref cambioNoticias);
            PronosticoMercado[empresa.id] = Math.Round(cambioBase + cambioNoticias, 4);
        }
    }

}
