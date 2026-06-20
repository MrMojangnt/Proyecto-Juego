using Empresas;
using System.IO;
using System;
using Terminal.Gui;

namespace Proyecto_Juego;
public class ManejoDeArchivos
{
    public static bool[] saves = new bool[3];
    public static decimal DeudaEmergencia = 0m; //Se define la variable de la deuda
    public static int turno = 0; //se define el turno en el que está el jugador

    public static string ObtenerCarpeta(int slot)
    {
        string carpeta = Path.Combine(
            AppContext.BaseDirectory,
            "GuardadoDePartidas",
            $"Partida{slot + 1}");

        if( !Directory.Exists( carpeta ))
        {
            Directory.CreateDirectory(carpeta);
        }
        return carpeta;
    }

    public static string rutaPartidas(int slot) 
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "SaveInversor.txt");
        }
    public static string rutaInventario(int slot)
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "Inventario.csv");
    }
    public static string rutaEmpresas(int slot)
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "Empresas.csv");
    }
    public static string rutaBalance(int slot)
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "Balance.csv");
    }
    public static string rutaContactos(int slot)
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "Contactos.csv");
    }
    public static string rutaPeriodico(int slot)
    {
        return Path.Combine(
        ManejoDeArchivos.ObtenerCarpeta(slot),
        "Periodico.csv");
    }
    //CrearPartida


    //Verificando que no esté el archivo abierto
    public static bool ArchivosDisponibles(params string[] archivos)
    {
        foreach (string archivo in archivos)
        {
            try
            {
                using FileStream fs = File.Open(
                    archivo,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.None);
            }
            catch (IOException)
            {
                return false;
            }
        }

        return true;
    }

    static string LeerParteDeUnArchivo(string archivo, int numarchivo, string Busqueda)
    {
        if (archivo == "partidas")
        {
            using (StreamReader guardado = new StreamReader(ManejoDeArchivos.rutaInventario(numarchivo)))
            {
                switch (Busqueda)
                {
                    case "nombre":
                        string nombre = (guardado.ReadLine()??"");
                        nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                        return nombre;
                        
                    case "pais":    
                        string pais = (guardado.ReadLine()??"");
                        pais = pais.Replace("Pais: ", "");
                        return pais;
                    case "carisma":
                        string carisma = (guardado.ReadLine() ?? "");
                        carisma = carisma.Replace("Carisma: ", "");
                        return carisma;
                    case "economia":
                        string economia = (guardado.ReadLine() ?? "");
                        economia = economia.Replace("Economia: ", "");
                        return economia;
                    case "fiscalidad":    
                        string fiscalidad = (guardado.ReadLine() ?? "");
                        fiscalidad = fiscalidad.Replace("Fiscalidad: ", "");
                        return fiscalidad;
                    case "corrupcion":
                        string corrupcion = (guardado.ReadLine() ?? "");
                        corrupcion = corrupcion.Replace("Corrupcion: ", "");
                        return corrupcion;
                    case "balance":
                        string balance = (guardado.ReadLine() ?? "");
                        balance = balance.Replace("Balance: ", "");
                        return balance;
                }
            } 
        }

        if (archivo == "inventario")
        {
            
        }

        return null;
    }

    void CambiarDatos(string archivo, int numarchivo, string Busqueda, int change)
    {
        if (archivo == "partidas")
        {
            using (StreamReader guardado = new StreamReader(ManejoDeArchivos.rutaInventario(numarchivo)))
            using (StreamWriter carga = new StreamWriter(ManejoDeArchivos.rutaInventario(numarchivo)))
            {

            }
        }
    }

   
}