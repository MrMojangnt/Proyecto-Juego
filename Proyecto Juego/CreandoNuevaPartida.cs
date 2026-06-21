using Empresas;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_Juego;

public class CreandoNuevaPartida
{

    public static void VerificarPartidaAntesDeVentanainicio(TextField casillaNombre, string PaisSeleccionado, bool nombre, bool guardado, int slot,
        Toplevel top, Window VentanaCreacionPersonaje)
    {
        //Comprobar nombre vacio
        if (casillaNombre.Text.IsEmpty)
        {
            MessageBox.Query(
                "ERROR",
                "Ingresa un nombre correcto",
                "Introduce nuevos datos");

        }
        else
        {
            nombre = true;
        }

        //Comprobar pais
        //Comprobacion final
        if (nombre)
        {
            Program.pd.name = casillaNombre.Text.ToString()!;
            Program.pd.pais = PaisSeleccionado;
            Program.pd.balance = 50000;


            guardado = GuardarPartida(slot);

            if (guardado)
            {
                top.Remove(VentanaCreacionPersonaje);//Cuando se pulsa el botón desaparece la ventana
                Program.MostrarTutorial = true;
                Program.Inicio(top);
                Tutorial.EvaluarTutorial();
            }
        }
    }

    public static void VerificarSave()
    {
        for (int i = 0; i < ManejoDeArchivos.saves.Length; i++)
        {
            string carpeta = ManejoDeArchivos.ObtenerCarpeta(i);

            ManejoDeArchivos.saves[i] =
                Directory.Exists(carpeta) &&
                File.Exists(Path.Combine(carpeta, "SaveInversor.txt")) &&
                File.Exists(Path.Combine(carpeta, "Inventario.csv")) &&
                File.Exists(Path.Combine(carpeta, "Empresas.csv")) &&
                File.Exists(Path.Combine(carpeta, "Balance.csv")) &&
                File.Exists(Path.Combine(carpeta, "Contactos.csv"));
        }
    }

    public static bool GuardarPartida(int slot)
    {
        bool guardado = false;
        var top = Application.Top;
        VerificarSave();
        ManejoDeArchivos.turno = 0;


        for (int i = 0; i < ManejoDeArchivos.saves.Length; i++)
        {
            if (!ManejoDeArchivos.saves[i])
            {
                using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaPartidas(i), false, Encoding.UTF8))
                {
                    save.WriteLine($"Nombre: {Program.pd.name} \nPais: {Program.pd.pais} \nBalance: {Program.pd.balance}");
                    // nueva línea que persiste la deuda total
                    save.WriteLine($"DeudaEmergencia: {ManejoDeArchivos.DeudaEmergencia}");
                    save.WriteLine($"DeudaLegendaria: {ManejoDeArchivos.DeudaLegendaria}");

                }

                using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaInventario(i), false, Encoding.UTF8))
                {

                    save.WriteLine(Program.pd.name);
                    save.WriteLine($"ID,Nombre,Costo_Compra,CostoActual,TipoAccion,Cantidad");
                }

                InicializarHistorialBalance(i);
                Guardarempresa(i, true);

                // preparar estado y generar contactos para la nueva partida
                CargandoLasPartidas.Companiass = CargandoLasPartidas.CargarEmpresa(i);
                CambiosDelMercado.PrepararPronosticoMercado(CargandoLasPartidas.Companiass);
                CargandoLasPartidas.ContactosCargados = GeneracionDeContactos.GenerarPersonas();
                CargandoLasPartidas.RecalcularDeudaEmergencia();
                GeneracionDeContactos.GuardarContactos(i, true);

                slot = i;
                Program.InvInt = i;
                guardado = true;
                break;
            }
        }

        using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaPartidas(slot), true))
        {
            save.WriteLine($"Turno: {ManejoDeArchivos.turno}");
            save.WriteLine($"GameOver: {ManejoDeArchivos.PartidaPerdida}");
        }

        if (!guardado)
        {
            int fullSlots = MessageBox.Query("Slots llenos",
                "Ha superado el límite de partidas guardadas, ¿quiere sobreescribir alguna?",
                "Sí", "No");

            if (fullSlots == 0)
            {
                ModificarPartidas.SobreescribirPartida(top);
            }
        }
        return guardado;
    }
    public static void Guardarempresa(int i, bool zzz)
    {
        using (StreamWriter save_empresas = new StreamWriter(ManejoDeArchivos.rutaEmpresas(i), zzz, Encoding.UTF8))
        {
            save_empresas.WriteLine("IdEmpresa; Empresa; Pais; Sector; Capital Bursátil; Accionistas; Productos; Ganancias; Gastos Marketing;Gastos Investigación; Gastos Mantenimiento; Participacion; Balance");
            CargandoLasPartidas.Companiass = Indices.GenerarIndicesEmpresas();
            for (int p = 0; p < CargandoLasPartidas.Companiass.Count; p++)
            {
                save_empresas.WriteLine(CargandoLasPartidas.Companiass[p]);

            }


        }
    }

    //Se actualizan los datos del balance del inversor
    public static void InicializarHistorialBalance(int indice)
    {
        using (StreamWriter historial = new StreamWriter(ManejoDeArchivos.rutaBalance(indice), false, Encoding.UTF8))
        {
            historial.WriteLine("Tipo;EmpresaId;Empresa;Sector;Cantidad;PrecioUnitario;Total;BalanceDespues;Turno");
        }
    }

}