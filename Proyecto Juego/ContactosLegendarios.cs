using System;
using System.Collections.Generic;
using System.Data;
using Empresas;
using Terminal.Gui;
using Terminal.Gui.Views;
using Terminal.Gui.ViewBase;

namespace Proyecto_Juego;

public abstract class ContactoLegendarioBase
{
    public string Nombre { get; }
    public string Rol { get; }
    public string Habilidad { get; }
    public int UsosRestantes { get; set; } = 3;   // set público para poder cargarlo desde archivo

    protected ContactoLegendarioBase(string nombre, string rol, string habilidad)
    {
        Nombre = nombre;
        Rol = rol;
        Habilidad = habilidad;
    }

    public bool PuedeUsarse => UsosRestantes > 0;

    public void ConsumirUso()
    {
        if (UsosRestantes > 0)
        {
            UsosRestantes--;
            ContactosLegendariosMenu.GuardarUsos();   // persiste automáticamente al gastar un uso
        }
    }


    public abstract void Ejecutar();
}

public sealed class JocksandValladaresLegendario : ContactoLegendarioBase
{
    public JocksandValladaresLegendario()
        : base("Jocksand Valladares", "Manipulador del mercado", "Hace que un sector suba un 50%")
    {
    }

    public override void Ejecutar()
    {
        var sectores = new List<string>(Indices.Nombre_Sectores_Variables.Keys);
        var dialogo = new Dialog() { Title = "Jocksand Valladares", Width = 70, Height = 22 };

        var titulo = new Label()
        {
            Text = "Elige un sector para inflarlo un 50%:",
            X = 1,
            Y = 1
        };

        var listaSectores = new ListView(sectores)
        {
            X = 1,
            Y = 3,
            Width = 32,
            Height = 10
        };

        var detalle = new Label()
        {
            Text = "El movimiento se aplicará a todas las empresas del sector.",
            X = 1,
            Y = 14
        };

        var aplicar = new Button()
        {
            Text = "Aplicar",
            X = 1,
            Y = 17
        };

        var cancelar = new Button()
        {
            Text = "Cancelar",
            X = 15,
            Y = 17
        };

        aplicar.Accepting += (s,e) =>
        {
            int indice = listaSectores.SelectedItem;
            if (indice < 0 || indice >= sectores.Count)
            {
                MessageBox.Query("Jocksand Valladares", "Elegí un sector válido.", "Aceptar");
                return;
            }

            string sector = sectores[indice];
            ModificarPartidas.AplicarImpactoSector(sector, 1.5m);
            ConsumirUso();
            MessageBox.Query("Jocksand Valladares", $"Sector afectado: {sector}\nEl valor bursátil subió un 50%.", "Aceptar");
            Application.RequestStop();
        };

        cancelar.Accepting += (s,e) => Application.RequestStop();

        dialogo.Add(titulo, listaSectores, detalle, aplicar, cancelar);
        Application.Run(dialogo);
    }
}

public sealed class RaulCastilloLegendario : ContactoLegendarioBase
{
    public RaulCastilloLegendario()
        : base("Raul Castillo", "Inversionista Ángel", "Otorga préstamos masivos de emergencia")
    {
    }

    public override void Ejecutar()
    {
        var top = Application.Top;
        var dialogo = new Dialog() { Title = "Raul Castillo", Width = 70, Height = 18 };

        var titulo = new Label()
        {
            Text = "Monto del préstamo de emergencia:",
            X = 1,
            Y = 1
        };

        var monto = new TextField()
        {
            Text = "100000",
            X = 1,
            Y = 3,
            Width = 25
        };
        monto.TextChanging += (e) =>
        {
            if (e.NewText.Length >= 6)
            {
                e.Cancel = true;
            }
        };

        var detalle = new Label()
        {
            Text = "El préstamo aumenta tu balance y suma deuda.",
            X = 1,
            Y = 5
        };

        var otorgar = new Button()
        {
            Text = "Otorgar",
            X = 1,
            Y = 8
        };

        var cancelar = new Button()
        {
            Text = "Cancel",
            X = 15,
            Y = 8
        };

        otorgar.Accepting += (s,e) =>
        {
            if (!decimal.TryParse(monto.Text.ToString(), out decimal cantidad) || cantidad <= 0m)
            {
                MessageBox.Query("Raul Castillo", "Ingresá un monto válido.", "Aceptar");
                return;
            }

            ModificarPartidas.AplicarPrestamoEmergencia(cantidad);
            ConsumirUso();
            ModificarPartidas.Guardarelbalance();
            MessageBox.Query(
                "Raul Castillo",
                $"Préstamo aprobado.\nRecibiste ${cantidad:F2}\nDeuda actual: ${ManejoDeArchivos.DeudaEmergencia:F2}",
                "Aceptar");

            top.RemoveAll();
            Program.Inicio(top);
            Application.RequestStop();
        };

        cancelar.Accepting += (s,e) => Application.RequestStop();

        dialogo.Add(titulo, monto, detalle, otorgar, cancelar);
        Application.Run(dialogo);
    }
}

public sealed class FranciscoAlvarezLegendario : ContactoLegendarioBase
{
    public FranciscoAlvarezLegendario()
        : base("Francisco Alvarez", "Database Admin / Insider Trading", "Predice exactamente el mercado de mañana")
    {
    }

    public override void Ejecutar()
    {
        var dialogo = new Dialog(){Title = "Francisco Alvarez", Width = 90, Height = 25};

        var titulo = new Label()
        {
            Text = "Pronóstico exacto del mercado:",
            X = 1,
            Y = 1
        };

        var tabla = new TableView()
        {
            X = 1,
            Y = 3,
            Width = Dim.Fill() - 2,
            Height = 16
        };

        tabla.Table = new DataTableSource(Tablasdefrancisco.ObtenerPronosticoMercado());

        var cerrar = new Button()
        {
            Text = "Cerrar",
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        cerrar.Accepting += (s,e) => Application.RequestStop();

        dialogo.Add(titulo, tabla, cerrar);
        ConsumirUso();
        Application.Run(dialogo);
    }
}

public static class ContactosLegendariosMenu
{
    public static readonly ContactoLegendarioBase[] Contactos =
    {
        new JocksandValladaresLegendario(),
        new RaulCastilloLegendario(),
        new FranciscoAlvarezLegendario()
    };

    // Ruta GLOBAL compartida entre todas las partidas
    private static string RutaGlobal =>
        Path.Combine(AppContext.BaseDirectory, "GuardadoDePartidas", "Legendarios_Global.txt");

    public static void CargarUsos()
    {
        if (!File.Exists(RutaGlobal)) return; // primera vez: se quedan con su valor default (3)

        foreach (string linea in File.ReadAllLines(RutaGlobal))
        {
            string[] partes = linea.Split(';');
            if (partes.Length < 2) continue;

            string nombre = partes[0];
            if (!int.TryParse(partes[1], out int usos)) continue;

            var legendario = Array.Find(Contactos, l => l.Nombre == nombre);
            if (legendario != null) legendario.UsosRestantes = usos;
        }
    }

    public static void GuardarUsos()
    {
        var lineas = new List<string>();
        foreach (var l in Contactos)
            lineas.Add($"{l.Nombre};{l.UsosRestantes}");

        string dir = Path.GetDirectoryName(RutaGlobal)!;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllLines(RutaGlobal, lineas);
    }

}