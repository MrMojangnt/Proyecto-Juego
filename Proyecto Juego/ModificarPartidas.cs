using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Proyecto_Juego;

public class ModificarPartidas
{
    public static void SobreescribirPartida(Toplevel top)
    {

        Button[] SobreSlot = new Button[3];
        var Sobreescribir = new Dialog(
    "Sobreescribir partida",
    60,
    20
);
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            string linea = LeerNombre(index);
            SobreSlot[index] = new Button($"Nombre: {linea}")
            {
                X = 2,
                Y = i + 2
            };

            // Reemplaza el handler de SobreSlot[index].Clicked en SobreescribirPartida(...) por esta versión
            SobreSlot[index].Clicked += () =>
            {
                using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaPartidas(index), false, Encoding.UTF8))
                {
                    save.WriteLine(Program.pd.ToString());
                    save.WriteLine($"DeudaEmergencia: {ManejoDeArchivos.DeudaEmergencia}");
                    save.WriteLine($"DeudaLegendaria: {ManejoDeArchivos.DeudaLegendaria}");
                    save.WriteLine($"Turno: {ManejoDeArchivos.turno}");
                }

                using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaInventario(index), false, Encoding.UTF8))
                {
                    save.WriteLine(Program.pd.name);
                }

                CreandoNuevaPartida.InicializarHistorialBalance(index);
                CreandoNuevaPartida.Guardarempresa(index, false);

                // Guardar la lista de contactos generada o existente
                CargandoLasPartidas.ContactosCargados = GeneracionDeContactos.GenerarPersonas();
                GeneracionDeContactos.GuardarContactos(index, false);

                CargandoLasPartidas.Companiass = CargandoLasPartidas.CargarEmpresa(index);
                CambiosDelMercado.PrepararPronosticoMercado(CargandoLasPartidas.Companiass);
                CargandoLasPartidas.ContactosCargados = CargandoLasPartidas.CargarContactos(index);

                Application.RequestStop();
                top.RemoveAll();
                Program.Inicio(top);
            };

            Sobreescribir.Add(SobreSlot[index]);

        }

        var cancelar = new Button("Cancelar")
        {
            X = 20,
            Y = 6
        };
        cancelar.Clicked += () =>
        {
            Application.RequestStop();
            top.RemoveAll();
            top.Add(Program.VentanaPrincipal);
        };
        Sobreescribir.Add(cancelar);
        Application.Run(Sobreescribir);
    }

    static string LeerNombre(int i)
    {
        if (!ManejoDeArchivos.ArchivosDisponibles(
                    ManejoDeArchivos.rutaPartidas(i),
                    ManejoDeArchivos.rutaInventario(i),
                    ManejoDeArchivos.rutaEmpresas(i),
                    ManejoDeArchivos.rutaBalance(i),
                    ManejoDeArchivos.rutaContactos(i),
                    ManejoDeArchivos.rutaPeriodico(i)))
        {
            MessageBox.Query(
                "Error",
                "Uno o más archivos de la partida están abiertos en otro programa.",
                "Aceptar");

            return "";
        }
        else
        {

            using (StreamReader save = new StreamReader(ManejoDeArchivos.rutaPartidas(i), Encoding.UTF8))
            {
                string linea = (save.ReadLine() ?? "");
                linea = linea.Replace("Nombre:", "");//reemplaza "Nombre" por ""
                return linea;
            }
        }
    }

    public static void EliminarPartida(int i)
    {
        CreandoNuevaPartida.VerificarSave();

        int Eliminar = MessageBox.Query("Eliminar",
        "¿Está seguro que desea eliminar esta partida?",
        "Sí", "No");

        if (Eliminar == 0)
        {
            if (!ManejoDeArchivos.ArchivosDisponibles(
                    ManejoDeArchivos.rutaPartidas(i),
                    ManejoDeArchivos.rutaInventario(i),
                    ManejoDeArchivos.rutaEmpresas(i),
                    ManejoDeArchivos.rutaBalance(i),
                    ManejoDeArchivos.rutaContactos(i),
                    ManejoDeArchivos.rutaPeriodico(i)))
            {
                MessageBox.Query(
                    "Error",
                    "Uno o más archivos de la partida están abiertos en otro programa.",
                    "Aceptar");

                return;
            }
            else
            {

                MessageBox.Query("Eliminar",
                    "Partida eliminada con éxito",
                    "Aceptar");

                File.Delete(ManejoDeArchivos.rutaPartidas(i));
                File.Delete(ManejoDeArchivos.rutaInventario(i));
                File.Delete(ManejoDeArchivos.rutaEmpresas(i));
                File.Delete(ManejoDeArchivos.rutaBalance(i));
                File.Delete(ManejoDeArchivos.rutaPeriodico(i));
                File.Delete(ManejoDeArchivos.rutaContactos(i));
            }
        }

    }

    public static void GuardarEmpresasActualizadas()
    {
        using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaEmpresas(Program.InvInt), false, Encoding.UTF8))
        {
            save.WriteLine("IdEmpresa; Empresa; Pais; Sector; Capital Bursátil; Accionistas; Productos; Ganancias; Gastos Marketing;Gastos Investigación; Gastos Mantenimiento; Participacion; Balance");

            foreach (Companias empresa in CargandoLasPartidas.Companiass)
            {
                save.WriteLine(empresa.ToString());
            }
        }
    }
    public static void ActualizarPreciosInventario()
    {
        if (!File.Exists(ManejoDeArchivos.rutaInventario(Program.InvInt)))
            return;

        List<string> lineas = File.ReadAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt)).ToList();

        for (int i = 2; i < lineas.Count; i++)
        {
            string[] datos = lineas[i].Split(",");

            int idEmpresa = int.Parse(datos[0]);

            int indiceEmpresa = CargandoLasPartidas.Companiass.FindIndex(e => e.id == idEmpresa);

            if (indiceEmpresa != -1)
            {
                decimal nuevoPrecio =
                    (CargandoLasPartidas.Companiass[indiceEmpresa].capbursatil * 1000000m)
                    / 50000000m;

                // Columna CostoActual
                datos[3] = nuevoPrecio.ToString();

                lineas[i] = string.Join(",", datos);
            }
        }

        File.WriteAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt), lineas);
    }
    public static void AplicarImpactoSector(string sector, decimal multiplicador)
    {
        for (int i = 0; i < CargandoLasPartidas.Companiass.Count; i++)
        {
            Companias empresa = CargandoLasPartidas.Companiass[i];

            if (empresa.rubro == sector)
            {
                empresa.capbursatil = Math.Round(empresa.capbursatil * multiplicador, 2);
                empresa.balance = Math.Round(empresa.balance * multiplicador, 2);
                CargandoLasPartidas.Companiass[i] = empresa;
            }
        }

        GuardarEmpresasActualizadas();
        ActualizarPreciosInventario();
    }

    public static void AplicarPrestamoEmergencia(decimal monto)
    {
        Program.pd.balance += monto;
        ManejoDeArchivos.DeudaLegendaria += monto;
        CargandoLasPartidas.RecalcularDeudaEmergencia();
    }
    public static void RegistrarMovimientoBalance(string tipo, Companias empresa, int cantidad, decimal precioUnitario, decimal total)
    {
        Tablasdefrancisco.AsegurarHistorialBalance(Program.InvInt);

        using (StreamWriter historial = new StreamWriter(ManejoDeArchivos.rutaBalance(Program.InvInt), true, Encoding.UTF8))
        {
            historial.WriteLine($"{tipo};{empresa.id};{empresa.name};{empresa.rubro};{cantidad};{precioUnitario:F2};" +
                $"{total:F2};{Program.pd.balance:F2};{ManejoDeArchivos.turno}");
        }
    }
    public static void Guardarelbalance()
    {
        using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaPartidas(Program.InvInt), false, Encoding.UTF8))
        {
            save.WriteLine($"Nombre: {Program.pd.name} \nPais: {Program.pd.pais} \nBalance: {Program.pd.balance}");
            save.WriteLine($"DeudaEmergencia: {ManejoDeArchivos.DeudaEmergencia}");
            save.WriteLine($"DeudaLegendaria: {ManejoDeArchivos.DeudaLegendaria}");
            save.WriteLine($"Turno: {ManejoDeArchivos.turno}");

        }
    }
    public static void PasarTurno(Toplevel top)
    {
        ManejoDeArchivos.turno++;
        TeLlamanPapuContesta.EvaluarLlamadas();
        GeneracionDeContactos.EvaluarAparicionLegendario();
        GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
        if (CambiosDelMercado.PronosticoMercado.Count != CargandoLasPartidas.Companiass.Count)
        {
            CambiosDelMercado.PrepararPronosticoMercado(CargandoLasPartidas.Companiass);
        }

        for (int i = 0; i < CargandoLasPartidas.Companiass.Count; i++)
        {
            Companias empresa = CargandoLasPartidas.Companiass[i];
            decimal cambio = 0m;
            CambiosDelMercado.PronosticoMercado.TryGetValue(empresa.id, out cambio);
            empresa.capbursatil += empresa.capbursatil * cambio;

            CargandoLasPartidas.Companiass[i] = empresa;
        }

        GuardarEmpresasActualizadas();
        ActualizarPreciosInventario();
        CambiosDelMercado.PrepararPronosticoMercado(CargandoLasPartidas.Companiass);
        string[] lineas = File.ReadAllLines(ManejoDeArchivos.rutaPartidas(Program.InvInt));

        for (int i = 0; i < lineas.Length; i++)
        {
            if (lineas[i].StartsWith("Turno:"))
            {
                lineas[i] = $"Turno: {ManejoDeArchivos.turno}";
                break;
            }
        }

        File.WriteAllLines(ManejoDeArchivos.rutaPartidas(Program.InvInt), lineas);


        MessageBox.Query(
            "Turno",
            "Se actualizaron los capitales bursátiles",
            "Aceptar");
        decimal balancepormientras = Program.pd.balance;
        Events.GestorDeEventos(ref balancepormientras);
        Program.pd.balance = balancepormientras;
        top.RemoveAll();
        Program.Inicio(top);
        Events.Apuestas(top);
    }
}