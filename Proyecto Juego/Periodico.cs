using System;
using Terminal.Gui;
using System.IO;
namespace Proyecto_Juego;

public class Events
{
    public static string[] titulosBuenos =
    {
        "Aumento del consumo interno",
        "Reducción de impuestos empresariales",
        "Boom tecnológico impulsa inversiones",
        "Exportaciones alcanzan récord histórico",
        "Descenso de la inflación",
        "Nuevos acuerdos comerciales internacionales",
        "Crecimiento económico superior al esperado",
        "Mayor confianza de los consumidores",
        "Descubrimiento de recursos estratégicos",
        "Bancos anuncian créditos más accesibles"
    };

    public static string[] descripcionesBuenas =
    {
        "El aumento del gasto de los consumidores impulsa las ventas y mejora las expectativas de las empresas.",
        "El gobierno reduce la carga fiscal sobre las compañías, aumentando sus beneficios netos.",
        "Las empresas tecnológicas lideran una ola de inversiones que fortalece al mercado.",
        "Las exportaciones nacionales baten récords y mejoran los ingresos de numerosas industrias.",
        "La inflación cae a niveles bajos, favoreciendo la estabilidad económica y el consumo.",
        "Nuevos tratados comerciales abren mercados adicionales para las empresas locales.",
        "La economía crece más de lo previsto por los analistas, impulsando la confianza inversora.",
        "Los consumidores muestran optimismo sobre el futuro económico y aumentan sus compras.",
        "El hallazgo de recursos valiosos atrae inversión nacional e internacional.",
        "Las entidades financieras facilitan el acceso al crédito para empresas y ciudadanos."
    };

    public static string[] titulosMalos =
    {
        "Caída del consumo interno",
        "Aumento de impuestos empresariales",
        "Crisis en el sector tecnológico",
        "Fuerte descenso de las exportaciones",
        "Repunte de la inflación",
        "Fracaso de negociaciones comerciales",
        "Desaceleración económica inesperada",
        "Pérdida de confianza de los consumidores",
        "Escasez de materias primas",
        "Restricción del crédito bancario"
    };

    public static string[] descripcionesMalas =
    {
        "Los consumidores reducen sus gastos, afectando negativamente las ventas de las empresas.",
        "Las compañías enfrentan mayores costos fiscales que reducen su rentabilidad.",
        "Varias empresas tecnológicas reportan pérdidas y provocan incertidumbre en el mercado.",
        "Las exportaciones disminuyen significativamente, afectando los ingresos de múltiples sectores.",
        "La inflación aumenta y reduce el poder adquisitivo de los ciudadanos.",
        "Las negociaciones comerciales fracasan y limitan las oportunidades de expansión empresarial.",
        "La economía muestra señales de enfriamiento que preocupan a los inversionistas.",
        "Los consumidores reducen sus expectativas económicas y retrasan compras importantes.",
        "La falta de materias primas provoca retrasos y mayores costos de producción.",
        "Los bancos endurecen los requisitos para otorgar préstamos, frenando nuevas inversiones."
    };

    public static void Periodico(Toplevel top)
    {
        var VentanaPeriodico = new Window()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        top.Add(VentanaPeriodico);
    }

    public static void PasarTurnoPeriodico()
    {
        bool BuenaNoticia = false;
        Random rnd = new Random();
        BuenaNoticia = rnd.Next(2) == 1;
        if (BuenaNoticia)
        {
            Random rnd2 = new Random();
            int numNoticia = rnd.Next(descripcionesBuenas.Length);
             
        }
    }
}
