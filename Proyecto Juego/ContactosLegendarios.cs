using System;
using System.Collections.Generic;
using System.Data;
using Empresas;
using Terminal.Gui;

namespace Proyecto_Juego;

public abstract class ContactoLegendarioBase
{
    public string Nombre { get; }
    public string Rol { get; }
    public string Habilidad { get; }

    protected ContactoLegendarioBase(string nombre, string rol, string habilidad)
    {
        Nombre = nombre;
        Rol = rol;
        Habilidad = habilidad;
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
        var dialogo = new Dialog("Jocksand Valladares", 70, 22);

        var titulo = new Label("Elige un sector para inflarlo un 50%:")
        {
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

        var detalle = new Label("El movimiento se aplicará a todas las empresas del sector.")
        {
            X = 1,
            Y = 14
        };

        var aplicar = new Button("Aplicar")
        {
            X = 1,
            Y = 17
        };

        var cancelar = new Button("Cancelar")
        {
            X = 15,
            Y = 17
        };

        aplicar.Clicked += () =>
        {
            int indice = listaSectores.SelectedItem;
            if (indice < 0 || indice >= sectores.Count)
            {
                MessageBox.Query("Jocksand Valladares", "Elegí un sector válido.", "Aceptar");
                return;
            }

            string sector = sectores[indice];
            Program.AplicarImpactoSector(sector, 1.5m);
            MessageBox.Query("Jocksand Valladares", $"Sector afectado: {sector}\nEl valor bursátil subió un 50%.", "Aceptar");
            Application.RequestStop();
        };

        cancelar.Clicked += () => Application.RequestStop();

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
        var dialogo = new Dialog("Raul Castillo", 70, 18);

        var titulo = new Label("Monto del préstamo de emergencia:")
        {
            X = 1,
            Y = 1
        };

        var monto = new TextField("100000")
        {
            X = 1,
            Y = 3,
            Width = 25
        };

        var detalle = new Label("El préstamo aumenta tu balance y suma deuda.")
        {
            X = 1,
            Y = 5
        };

        var otorgar = new Button("Otorgar")
        {
            X = 1,
            Y = 8
        };

        var cancelar = new Button("Cancelar")
        {
            X = 15,
            Y = 8
        };

        otorgar.Clicked += () =>
        {
            if (!decimal.TryParse(monto.Text.ToString(), out decimal cantidad) || cantidad <= 0m)
            {
                MessageBox.Query("Raul Castillo", "Ingresá un monto válido.", "Aceptar");
                return;
            }

            Program.AplicarPrestamoEmergencia(cantidad);
            MessageBox.Query(
                "Raul Castillo",
                $"Préstamo aprobado.\nRecibiste ${cantidad:F2}\nDeuda actual: ${Program.DeudaEmergencia:F2}",
                "Aceptar");
            Application.RequestStop();
        };

        cancelar.Clicked += () => Application.RequestStop();

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
        var dialogo = new Dialog("Francisco Alvarez", 90, 25);

        var titulo = new Label("Pronóstico exacto del mercado:")
        {
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

        tabla.Table = Program.ObtenerPronosticoMercado();

        var cerrar = new Button("Cerrar")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        cerrar.Clicked += () => Application.RequestStop();

        dialogo.Add(titulo, tabla, cerrar);
        Application.Run(dialogo);
    }
}

public static class ContactosLegendariosMenu
{
    private static readonly ContactoLegendarioBase[] Contactos =
    {
        new JocksandValladaresLegendario(),
        new RaulCastilloLegendario(),
        new FranciscoAlvarezLegendario()
    };

    public static void MostrarMenu()
    {
        var dialogo = new Dialog("Contactos Legendarios", 84, 24);

        var lista = new ListView(Array.ConvertAll(Contactos, contacto => $"{contacto.Nombre} - {contacto.Rol}"))
        {
            X = 1,
            Y = 1,
            Width = 40,
            Height = 10
        };

        var detalle = new Label("Seleccioná un contacto para usar su habilidad.")
        {
            X = 1,
            Y = 12
        };

        lista.SelectedItemChanged += e =>
        {
            var contacto = Contactos[e.Item];
            detalle.Text = $"{contacto.Nombre}: {contacto.Habilidad}";
        };

        var usar = new Button("Usar")
        {
            X = 1,
            Y = 16
        };

        var cerrar = new Button("Cerrar")
        {
            X = 15,
            Y = 16
        };

        usar.Clicked += () =>
        {
            int seleccionado = lista.SelectedItem;
            if (seleccionado < 0 || seleccionado >= Contactos.Length)
            {
                MessageBox.Query("Contactos Legendarios", "Elegí un contacto válido.", "Aceptar");
                return;
            }

            Contactos[seleccionado].Ejecutar();
        };

        cerrar.Clicked += () => Application.RequestStop();

        dialogo.Add(lista, detalle, usar, cerrar);
        Application.Run(dialogo);
    }
}
