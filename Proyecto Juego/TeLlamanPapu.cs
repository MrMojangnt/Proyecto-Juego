using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Proyecto_Juego;

public static class TeLlamanPapuContesta
{
    public static bool GameOverActivado = false;

    public static void EvaluarLlamadas()
    {
        for (int i = 0; i < CargandoLasPartidas.ContactosCargados.Count; i++)
        {
            var contacto = CargandoLasPartidas.ContactosCargados[i];

            // Solo evaluar si realmente existe deuda pendiente
            if (!contacto.TienePrestamoActivo || contacto.montoprestado <= 0m)
                continue;

            sbyte presion = Personalidades.Arqueotipos[contacto.idArquetipo].Presion;
            int delay = CalcularDelay(presion);
            int index = CargandoLasPartidas.ContactosCargados.FindIndex(n => n.name == contacto.name);

            if (ManejoDeArchivos.turno >= contacto.UltimoTurnoPrestamo + delay)
            {
                if (contacto.name == "Leah Dávila")
                {
                    if (!contacto.AmenazaFinalEmitida)
                    {
                        // Primera advertencia: marcar turno y avisar
                        contacto.AmenazaFinalEmitida = true;
                        contacto.UltimaAdvertencia = ManejoDeArchivos.turno;
                        if (index != -1)
                            CargandoLasPartidas.ContactosCargados[index] = contacto;
                        GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
                    }
                    else if (ManejoDeArchivos.turno >= contacto.UltimaAdvertencia + 5)
                    {
                        // Han pasado 5 turnos sin pagar → Game Over
                        ManejoDeArchivos.PartidaPerdida = true;
                        ManejoDeArchivos.MotivoGameOver = "leah";
                        ModificarPartidas.Guardarelbalance();
                        GameOverActivado = true;
                        GameOver.VentanaGameOver("Leah", Program.InvInt);
                        return;
                    }
                }
                MostrarLlamada(contacto);
                GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
            }
        }

    }

    static int CalcularDelay(sbyte presion)
    {
        return presion switch
        {
            >= 8 => 1,
            >= 4 => 2,
            >= 0 => 3,
            >= -4 => 4,
            _ => 5
        };
    }
    static int GetNivelPresion(sbyte presion)
    {
        return presion switch
        {
            >= 7 => 3,
            >= 3 => 2,
            >= 0 => 1,
            _ => 0
        };
    }
    static void MostrarLlamada(NPC contacto)
    {
        int index = CargandoLasPartidas.ContactosCargados.FindIndex(n => n.name == contacto.name);

        // Diálogo principal de llamada (estilo menú de llamada)
        var dialogo = new Dialog($"Llamada de {contacto.name}", 72, 26);
        var nombre = new Label(contacto.name) { X = 2, Y = 0 };
        var cuadro = new FrameView("")
        {
            X = 1,
            Y = 2,
            Width = Dim.Fill() - 4,
            Height = 10
        };
        int nivel = GetNivelPresion((sbyte)contacto.PresionActual);
        string mensaje = "";
        if (contacto.name == "Leah Dávila")
        {
            mensaje = Dialogos_de_Contacto.CobroDeuda[4][Random.Shared.Next(Dialogos_de_Contacto.CobroDeuda[4].Length)];
        }
        else
        {
            mensaje = Dialogos_de_Contacto.CobroDeuda[nivel][Random.Shared.Next(Dialogos_de_Contacto.CobroDeuda[nivel].Length)];
        }
        var cobrar = new Label() { X = 1, Y = 1 };
        cobrar.Text = mensaje;
        cuadro.Add(cobrar);

        // Información de deuda (mostramos deuda total por ahora)
        var deudaInfo = new Label($"Deuda total del Inversor: {contacto.montoprestado:F2}   \nBalance de {contacto.name}: {contacto.balance:F2}")
        {
            X = 1,
            Y = Pos.Bottom(cuadro) + 1
        };

        var btnPagar = new Button("Pagar") { X = 1, Y = Pos.AnchorEnd(2) };
        var btnResponder = new Button("Responder") { X = Pos.Right(btnPagar) + 2, Y = Pos.AnchorEnd(2) };
        var btnIgnorar = new Button("Ignorar") { X = Pos.Right(btnResponder) + 2, Y = Pos.AnchorEnd(2) };

        btnPagar.Clicked += () =>
        {
            Application.RequestStop();

            var dlgPago = new Dialog($"Pagar a {contacto.name}", 60, 12);
            var lbl = new Label($"Deuda total: {contacto.montoprestado:F2}") { X = 1, Y = 1 };
            var labelPago = new Label("Monto a pagar:") { X = 1, Y = 3 };
            var campoPago = new TextField("") { X = Pos.Right(labelPago) + 1, Y = 3, Width = 12 };
            campoPago.TextChanging += (e) =>
            {
                if (e.NewText.Length >= 6)
                {
                    e.Cancel = true;
                }
            };
            var btnAceptar = new Button("Aceptar") { X = 1, Y = Pos.AnchorEnd(2) };
            var btnCerrar = new Button("Cerrar") { X = Pos.Right(btnAceptar) + 2, Y = Pos.AnchorEnd(2) };

            btnAceptar.Clicked += () =>
            {
                if (!decimal.TryParse(campoPago.Text.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Query("Error", "Ingrese un monto válido.", "Aceptar");
                    return;
                }

                if (!Tablasdejocksand.PagarDeuda(monto))
                {
                    MessageBox.Query("Error", "No tienes suficiente balance para pagar.", "Aceptar");
                    return;
                }

                // Transferir al contacto y actualizar estado
                contacto.balance += monto;
                contacto.montoprestado -= monto;

                if (contacto.montoprestado <= 0m)
                {
                    contacto.montoprestado = 0;
                    contacto.TienePrestamoActivo = false;
                    contacto.LlamadaPendiente = false;
                    contacto.UltimoTurnoPrestamo = ManejoDeArchivos.turno;

                }

                if (index != -1)
                    CargandoLasPartidas.ContactosCargados[index] = contacto;
                CargandoLasPartidas.RecalcularDeudaEmergencia();
                GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
                ModificarPartidas.Guardarelbalance(); ;

                if (contacto.montoprestado < monto)
                {
                    contacto.Amistad += 1;

                    if (index != -1)
                        CargandoLasPartidas.ContactosCargados[index] = contacto;

                    GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
                }
                MessageBox.Query("Pago", "Pago realizado con éxito.", "Aceptar");
                Application.RequestStop();
            };

            btnCerrar.Clicked += () => Application.RequestStop();

            dlgPago.Add(lbl, labelPago, campoPago, btnAceptar, btnCerrar);
            Application.Run(dlgPago);
        };

        btnResponder.Clicked += () =>
        {
            Application.RequestStop();

            // Abrir un diálogo de conversación parecido al menú de llamar (reusa Plantilla)
            var dlg = new Dialog("", 70, 23);
            var dialogoResp = new FrameView("")
            {
                X = Pos.Center(),
                Y = Pos.AnchorEnd(20),
                Width = Dim.Fill() - 2,
                Height = 8
            };
            var nombreResp = new Label(contacto.name) { X = 2, Y = 0 };
            var textoResp = new Label() { X = 1, Y = 2 };

            dialogoResp.Add(textoResp);
            //LaLlamada.Plantilla(dlg, textoResp, contacto); // usa la interfaz de diálogo existente dentro de esta clase
            dlg.Add(nombreResp, dialogoResp);
            Application.Run(dlg);
        };

        btnIgnorar.Clicked += () =>
        {
            contacto.Amistad -= 1;

            if (index != -1)
                CargandoLasPartidas.ContactosCargados[index] = contacto;

            GeneracionDeContactos.GuardarContactos(Program.InvInt, false);

            Application.RequestStop();
        };

        dialogo.Add(nombre, cuadro, deudaInfo, btnPagar, btnIgnorar);
        Application.Run(dialogo);
    }

}