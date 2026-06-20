using Empresas;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
namespace Proyecto_Juego;

public class CargandoLasPartidas
{

    public static List<Companias> Companiass = new List<Companias>(); //Una lista de struct porque se tomarán en cuenta varias empresas
    public static List<NPC> ContactosCargados = new List<NPC>(); // Una lista de los contactos cargados (máximo 4, + 1 un contacto legendario si te toca)
    public static List<Periodicos> noticiash = new List<Periodicos>();



    public static void CargarPartida(int slot, Toplevel top, Window VentanaCargarPartida)
    {
        Program.InvInt = slot;
        if (File.Exists(ManejoDeArchivos.rutaPartidas(slot)) && File.Exists(ManejoDeArchivos.rutaInventario(slot))
            && File.Exists(ManejoDeArchivos.rutaEmpresas(slot)) && File.Exists(ManejoDeArchivos.rutaBalance(slot)) &&
            File.Exists(ManejoDeArchivos.rutaContactos(slot)) && File.Exists(ManejoDeArchivos.rutaPeriodico(slot)))
        {
                if (!ManejoDeArchivos.ArchivosDisponibles(
                    ManejoDeArchivos.rutaPartidas(slot),
                    ManejoDeArchivos.rutaInventario(slot),
                    ManejoDeArchivos.rutaEmpresas(slot),
                    ManejoDeArchivos.rutaBalance(slot),
                    ManejoDeArchivos.rutaContactos(slot),
                    ManejoDeArchivos.rutaPeriodico(slot)))
                {
                    MessageBox.Query(
                        "Error",
                        "Uno o más archivos de la partida están abiertos en otro programa.",
                        "Aceptar");

                    return;
                }
            CargarTxt(slot);
                

                Companiass = CargarEmpresa(slot);
                CambiosDelMercado.PrepararPronosticoMercado(Companiass);
                ContactosCargados = CargarContactos(slot);
                RecalcularDeudaEmergencia();

                top.Remove(VentanaCargarPartida);
                Program.Inicio(top);
            
        }
        else
        {
                MessageBox.Query(
                    "Error",
                    "No tienes partida guardada",
                    "Aceptar");
        }
    }

    //Se carga el archivo partidas.txt
    //Incluye nombre, país, balance, deuda y turno
    static void CargarTxt(int slot)
    {
        Program.InvInt = slot;
        using (StreamReader save = new StreamReader(ManejoDeArchivos.rutaPartidas(slot), Encoding.UTF8))
        {
            string nombre = (save.ReadLine() ?? "");
            nombre = nombre.Replace("Nombre: ", "");
            string pais = (save.ReadLine() ?? "");
            pais = pais.Replace("Pais: ", "");
            _ = decimal.TryParse((save.ReadLine() ?? "").Replace("Balance: ", ""), out decimal balance);
            _ = decimal.TryParse((save.ReadLine() ?? "").Replace("DeudaEmergencia: ", ""), out decimal deuda);
            _ = int.TryParse((save.ReadLine() ?? "").Replace("Turno: ", ""), out int turnos);
            Program.pd.name = nombre;
            Program.pd.pais = pais;
            Program.pd.balance = balance;
            ManejoDeArchivos.DeudaEmergencia = deuda;
            ManejoDeArchivos.turno = turnos;
        }
    }

    //Se carga el archivo empresas.csv
    //Incluye todos los datos de la empresa:
    //ID, Nombre, País, Rubro, Capital Bursátil, Accionistas, Ganancias Trimestrales, Gastos en Marketing, Mantenimiento e Investigacion,
    //El porcentaje de participación que tiene la empresa en el sector y su balance
    public static List<Companias> CargarEmpresa(int indice)
    {
        List<Companias> Comp = new List<Companias>();
        char[] delimitadores = { ';', '\n', '|', '\r' };
        using (StreamReader savecompani = new StreamReader(ManejoDeArchivos.rutaEmpresas(indice), Encoding.UTF8))
        {
            string[] encabezados = (savecompani.ReadLine() ?? "").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            while (!savecompani.EndOfStream)
            {
                Proyecto_Juego.Companias compitas = new Companias();
                compitas.productos = new string[10];

                string[] lineas = (savecompani.ReadLine() ?? "").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
                compitas.id = int.Parse(lineas[0]);
                compitas.name = lineas[1];
                lineas[2] = lineas[2].Replace("(predeterminado)", ""); //reemplaza "M" por ""
                compitas.pais = lineas[2];
                compitas.rubro = lineas[3];
                lineas[4] = lineas[4].Replace("M", ""); //reemplaza "M" por ""
                compitas.capbursatil = decimal.Parse(lineas[4]);
                compitas.accionistas = int.Parse(lineas[5]);
                int p = 6;
                for (int i = 0; i < compitas.productos.Length; i++)
                {
                    compitas.productos[i] = lineas[p];
                    p++;
                }
                lineas[16] = lineas[16].Replace("M", ""); //reemplaza "M" por ""
                compitas.GananciasTrimestrales = decimal.Parse(lineas[16]);
                lineas[17] = lineas[17].Replace("M", ""); //reemplaza "M" por ""
                compitas.marketing = decimal.Parse(lineas[17]);
                lineas[18] = lineas[18].Replace("M", ""); //reemplaza "M" por ""
                compitas.investigacion = decimal.Parse(lineas[18]);
                lineas[19] = lineas[19].Replace("M", ""); //reemplaza "M" por ""
                compitas.mantenimiento = decimal.Parse(lineas[19]);
                lineas[20] = lineas[20].Replace("%", ""); //reemplaza "%" por ""
                compitas.participacion = decimal.Parse(lineas[20]);
                lineas[21] = lineas[21].Replace("M", ""); //reemplaza "M" por ""
                compitas.balance = decimal.Parse(lineas[21]);

                Comp.Add(compitas);
            }

        }
        return Comp;
    }

    //Se carga el archivo contactos.csv
    //Incluye nombre, sexo, edad, sector dominante, balance, id de arquetipo de personalidad, nivel de amistad con el inversor,
    //UltimoTurnoLlamado (para que no se pueda llamar más de una vez)
    //Un bool TienePrestamoActivo por si el jugador le pidió un préstamo al contacto
    //PresionActual, que es la presion y define cuantas veces va a estar llamando al jugador para cobrarle el prestamo
    //Montoprestado, que es cuanto le prestó el contacto al jugador
    public static List<NPC> CargarContactos(int indice)//FUNCION QUE CARGA LOS CONTACTOS
    {
        List<NPC> ConNPC = new List<NPC>();
        NPC ContactosCargados = new NPC();
        char[] delimitadores = { ';', '\n', '\r' };

        using (StreamReader savecontactos = new StreamReader(ManejoDeArchivos.rutaContactos(indice), Encoding.UTF8))
        {
            string[] encabezados = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            while (!savecontactos.EndOfStream)
            {
                string[] lineas = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

                int len = lineas.Length;
                ContactosCargados.name = len > 0 ? lineas[0] : string.Empty;
                ContactosCargados.masculino = len > 1 && bool.TryParse(lineas[1], out bool m) ? m : false;
                ContactosCargados.edad = len > 2 && int.TryParse(lineas[2], out int e) ? e : 0;
                ContactosCargados.sector_dominante = len > 3 ? lineas[3] : string.Empty;
                ContactosCargados.balance = len > 4 && decimal.TryParse(lineas[4], out decimal b) ? b : 0m;
                ContactosCargados.idArquetipo = len > 5 && int.TryParse(lineas[5], out int id) ? id : 0;
                ContactosCargados.Amistad = len > 6 && sbyte.TryParse(lineas[6], out sbyte a) ? a : (sbyte)0;
                ContactosCargados.UltimoTurnoLlamado = len > 7 && int.TryParse(lineas[7], out int utl) ? utl : -1;
                ContactosCargados.TienePrestamoActivo = len > 8 && bool.TryParse(lineas[8], out bool tpa) ? tpa : false;
                ContactosCargados.UltimoTurnoPrestamo = len > 9 && int.TryParse(lineas[9], out int utp) ? utp : -1;
                ContactosCargados.PresionActual = len > 10 && int.TryParse(lineas[10], out int pa) ? pa : 0;
                ContactosCargados.montoprestado = len > 11 && decimal.TryParse(lineas[11], out decimal mp) ? mp : 0m;

                ConNPC.Add(ContactosCargados);
            }

        }
        return ConNPC;
    }

    //Recalcula la deuda, puede ser por un contacto legendario o por un contacto normal
    // Utilidad: recalcular deuda total desde los contactos (llamar después de CargarContactos)
    public static void RecalcularDeudaEmergencia()
    {
        decimal total = 0m;
        foreach (var c in ContactosCargados)
            total += c.montoprestado;
        ManejoDeArchivos.DeudaEmergencia = total;
    }

    public static void VerificarNoHayDatosGuardadosLabel(FrameView[] Slots)
    {
        for (int i = 0; i < ManejoDeArchivos.saves.Length; i++)
        {
            if (ManejoDeArchivos.saves[i])
            {
                StreamReader save = new StreamReader(ManejoDeArchivos.rutaPartidas(i), Encoding.UTF8);
                string nombre = (save.ReadLine() ?? "");
                nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                save.Close();

                Slots[i].Add(new Label("Nombre:\n" + nombre)
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                });
                Program.InvInt = i;
            }
            else
            {

                Slots[i].Add(new Label("No hay \ndatos guardados")
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                });
            }
        }
    }


}
