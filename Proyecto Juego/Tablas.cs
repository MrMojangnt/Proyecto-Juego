using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Terminal.Gui;

namespace Proyecto_Juego;

public class Tablasdefrancisco
{
    //MOSTRAR REPORTE DE BALANCE
    public static void MostrarReporteBalance(Toplevel top, List<ColorScheme> colores, int colora)
    {
        AsegurarHistorialBalance(Program.InvInt);

        decimal valorCartera;
        decimal costoBase;
        int totalAcciones;
        DataTable tablaPosiciones = CrearTablaPosicionesJugador(out valorCartera, out costoBase, out totalAcciones);
        DataTable tablaMovimientos = CrearTablaMovimientosBalance();
        decimal patrimonioEstimado = Program.pd.balance + valorCartera - ManejoDeArchivos.DeudaEmergencia;
        decimal gananciaFlotante = valorCartera - costoBase;

        var VentanaBalance = new Window("Reporte de Balance")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };

        var labelResumen = new Label(
            $@"Inversor: {Program.pd.name} | País: {Program.pd.pais} | Turno: {ManejoDeArchivos.turno}
Efectivo: ${Program.pd.balance:F2} | Cartera: ${valorCartera:F2} | Deuda: ${ManejoDeArchivos.DeudaEmergencia:F2} | Patrimonio estimado: ${patrimonioEstimado:F2}
Costo base: ${costoBase:F2} | Ganancia/Pérdida flotante: ${gananciaFlotante:+0.00;-0.00;0.00} | Acciones: {totalAcciones}")
        {
            X = 1,
            Y = 1
        };

        var marcoPosiciones = new FrameView("Posiciones actuales")
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaPosicionesView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaPosicionesView.Table = tablaPosiciones;
        marcoPosiciones.Add(tablaPosicionesView);

        var marcoMovimientos = new FrameView("Compras y ventas")
        {
            X = 1,
            Y = 15,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaMovimientosView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaMovimientosView.Table = tablaMovimientos;
        marcoMovimientos.Add(tablaMovimientosView);

        var botonVolver = new Button("Volver al Inicio")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        botonVolver.Clicked += () =>
        {
            top.RemoveAll();
            Program.Inicio(top);
        };

        VentanaBalance.Add(labelResumen, marcoPosiciones, marcoMovimientos, botonVolver);
        top.Add(VentanaBalance);
    }

    public static DataTable CrearTablaPosicionesJugador(out decimal valorCartera, out decimal costoBase, out int totalAcciones)
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Costo compra");
        tabla.Columns.Add("Precio actual");
        tabla.Columns.Add("Valor actual");
        tabla.Columns.Add("Ganancia/Pérdida");

        valorCartera = 0m;
        costoBase = 0m;
        totalAcciones = 0;

        if (!File.Exists(ManejoDeArchivos.rutaInventario(Program.InvInt)))
        {
            return tabla;
        }

        List<Acciones> acciones = Inventario.CargarInventario(Program.InvInt);
        foreach (Acciones accion in acciones)
        {
            Companias empresa;
            decimal precioActual = accion.CostoActual;
            string sector = "Desconocido";
            string nombreEmpresa = accion.name;

            if (TryObtenerEmpresa(accion.id, out empresa))
            {
                precioActual = ObtenerPrecioAccion(empresa);
                sector = empresa.rubro;
                nombreEmpresa = empresa.name;
            }

            decimal valorActual = precioActual * accion.cantidad;
            decimal costoRegistrado = accion.CostoDeCompra * accion.cantidad;
            decimal diferencia = valorActual - costoRegistrado;

            valorCartera += valorActual;
            costoBase += costoRegistrado;
            totalAcciones += accion.cantidad;

            tabla.Rows.Add(
                nombreEmpresa,
                sector,
                accion.cantidad,
                $"{accion.CostoDeCompra:F2}",
                $"{precioActual:F2}",
                $"{valorActual:F2}",
                $"{diferencia:+0.00;-0.00;0.00}");
        }

        return tabla;
    }
    public static DataTable CrearTablaMovimientosBalance()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Tipo");
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Precio");
        tabla.Columns.Add("Total");
        tabla.Columns.Add("Balance");
        tabla.Columns.Add("Turno");

        if (!File.Exists(ManejoDeArchivos.rutaBalance(Program.InvInt)))
        {
            return tabla;
        }

        string[] lineas = File.ReadAllLines(ManejoDeArchivos.rutaBalance(Program.InvInt), Encoding.UTF8);
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(';');
            if (datos.Length < 9)
            {
                continue;
            }

            tabla.Rows.Add(
                datos[0],
                datos[2],
                datos[3],
                datos[4],
                datos[5],
                datos[6],
                datos[7],
                datos[8]);
        }

        return tabla;
    }

    static decimal ObtenerPrecioAccion(Companias empresa)
    {
        return (empresa.capbursatil * 1000000m) / 50000000m;
    }

    static bool TryObtenerEmpresa(int id, out Companias empresa)
    {
        foreach (Companias actual in CargandoLasPartidas.Companiass)
        {
            if (actual.id == id)
            {
                empresa = actual;
                return true;
            }
        }

        empresa = default;
        return false;
    }
    public static void AsegurarHistorialBalance(int indice)
    {
        if (!File.Exists(ManejoDeArchivos.rutaBalance(indice)))
        {
            CreandoNuevaPartida.InicializarHistorialBalance(indice);
        }
    }

}
