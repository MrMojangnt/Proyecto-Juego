using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Terminal.Gui;
using System.Globalization;
namespace Proyecto_Juego;

public class GameOver
{
    public static void VentanaGameOver(string MotivosDeGameOver, int slot)
    {
        var top = Application.Top;

        var Ventanaperdio = new Window
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Program.colores[Program.colora]
        };
        CargandoLasPartidas.RecalcularDeudaEmergencia();
        var perdio = new Label(@"╔══════════════════════════════════════════════════════════════╗
║                                                              ║
║             ██████╗  █████╗ ███╗   ███╗███████╗              ║
║            ██╔════╝ ██╔══██╗████╗ ████║██╔════╝              ║
║            ██║  ███╗███████║██╔████╔██║█████╗                ║
║            ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝                ║
║            ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗              ║
║             ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝              ║
║                                                              ║
║             ██████╗ ██╗   ██╗███████╗██████╗                 ║
║            ██╔═══██╗██║   ██║██╔════╝██╔══██╗                ║
║            ██║   ██║██║   ██║█████╗  ██████╔╝                ║
║            ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗                ║
║            ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║                ║
║             ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝                ║
║                                                              ║
║           ""El mercado siguió adelante sin ti.""               ║
║                                                              ║
╚══════════════════════════════════════════════════════════════╝")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 15
        };

        var agregados = new Label(@$"Inversor: {Program.pd.name}
Balance final: {Program.pd.balance.ToString("N0", CultureInfo.InvariantCulture)}
Turnos sobrevividos: {ManejoDeArchivos.turno}
Causa del colapso: {MotivosDeGameOver}")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(perdio) + 2
        };

        var VolverAlMenu = new Button("Volver al Menú")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(agregados) + 5
        };

        VolverAlMenu.Clicked += () =>
        {
            top.Remove(Ventanaperdio);
            top.Add(Program.VentanaPrincipal);
        };

        Ventanaperdio.Add(perdio, agregados, VolverAlMenu );

        top.RemoveAll();
        top.Add(Ventanaperdio);
    }
}
