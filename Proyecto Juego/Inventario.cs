namespace Proyecto_Juego;
using NAudio.Wave;
using Proyecto_Juego;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using Terminal.Gui;

public class Inventario
{
    public static Window VentanaInventario(Toplevel top)
    {
        var win = new Window("Tabla tipo Excel")
        {
            X = 0,
            Y = 0,
            ColorScheme = Program.colores[Program.colora],
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        DataTable tabla = new DataTable();//ID,Nombre,Costo_Compra,CostoActual,TipoAccion,Cantidad
        tabla.Columns.Add("ID");
        tabla.Columns.Add("Nombre");
        tabla.Columns.Add("Costo de Compra");
        tabla.Columns.Add("Costo Actual");
        tabla.Columns.Add("Tipo de Acción");
        tabla.Columns.Add("Cantidad");

        Program.Accioneshh = CargarInventario(Program.InvInt);
        foreach (Acciones i in Program.Accioneshh)
        {
            tabla.Rows.Add(
                i.id,
                i.name,
                $"{i.CostoDeCompra:F2}" ,
                $"{i.CostoActual:F2}",
                i.TipoDeAccion,
                i.cantidad
            );

        }

        var tableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = 10
        };
        Program.BotonesDeJuegoPredeterminado(top, win);
        tableView.Table = tabla;

        win.Add(tableView);
        top.Add(win);
        return win;
    }

    public static List<Acciones> CargarInventario(int indice)
    {
        List<Acciones> Accioneshh = new List<Acciones>();
        Proyecto_Juego.Acciones AccionesStruct = new Acciones(); //structttttttttttt de acciones
        char[] delimitadores = { ',', '\n', '|', '\r' };
        using (StreamReader saveacciones = new StreamReader(ManejoDeArchivos.rutaInventario(indice), Encoding.UTF8))
        {
            string[] Nombre = (saveacciones.ReadLine()??"").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            string Encabezados = (saveacciones.ReadLine() ?? "");
            while (!saveacciones.EndOfStream)
            {
                string[] lineas = (saveacciones.ReadLine() ?? "").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
                AccionesStruct.id = int.Parse(lineas[0]);
                AccionesStruct.name = lineas[1];
                AccionesStruct.CostoDeCompra = (decimal.Parse(lineas[2]));
                AccionesStruct.CostoActual = (decimal.Parse(lineas[3]));
                AccionesStruct.TipoDeAccion = bool.Parse(lineas[4]);
                AccionesStruct.cantidad = int.Parse(lineas[5]);

                Accioneshh.Add(AccionesStruct);
            }

        }
        return Accioneshh;
    }

}