namespace Proyecto_Juego;
using NAudio.Wave;
using System.IO;
using Proyecto_Juego;
using Terminal.Gui;
using System.Data;
public class Inventario
{
    public static Window VentanaInventario()
    {
        var top = Application.Top;
        var win = new Window("Tabla tipo Excel")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        Program.BotonesDeJuegoPredeterminado(top, win);
        DataTable tabla = new DataTable();

        tabla.Columns.Add("ID");
        tabla.Columns.Add("Nombre");
        tabla.Columns.Add("Edad");

        tabla.Rows.Add("1", "Raúl", "20");
        tabla.Rows.Add("2", "Bernardino", "21");
        tabla.Rows.Add("3", "Linda", "19");

        var tableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        
        tableView.Table = tabla;

        win.Add(tableView);
        top.Add(win);
        return win;
    }
}