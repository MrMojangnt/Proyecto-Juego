using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;
using Terminal.Gui;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Proyecto_Juego;

public static class LaLlamada
{
    static int estado = 0;
    //Aquí se crean los labels y botones que se utilizarán en las otras funciones, cuando se toque una opcion, cuando cambie el dialogo y asi
    public static void Plantilla(Dialog dial, Label texto, NPC contacto)
    {
        estado = 0;
        //Llamar(Dialogos[contacto.idArquetipo], contacto);

        var Prestamo = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(8)
        };
        var bt1 = new Button()
        {
            X = Pos.Right(Prestamo),
            Y = Pos.AnchorEnd(8)
        };

        var Consejo = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(6)
        };
        var bt2 = new Button()
        {
            X = Pos.Right(Consejo),
            Y = Pos.AnchorEnd(6)
        };
        var Charlar = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(4)
        };
        var bt3 = new Button()
        {
            X = Pos.Right(Charlar),
            Y = Pos.AnchorEnd(4)
        };
        var colgar = new Label("¿Sabes qué?, mejor colgaré, gracias.")
        {
            X = 2,
            Y = Pos.AnchorEnd(2)
        };
        var cerrar = new Button()
        {
            X = Pos.Right(colgar),
            Y = Pos.AnchorEnd(2)
        };
        var CosoPrestamo = new TextField("")
        {
            X = Pos.X(Prestamo),
            Y = Pos.Y(Prestamo),
            Width = 10,
        };
        CosoPrestamo.Visible = false;
        CosoPrestamo.Enabled = false;
        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        bt1.Clicked += () => OnOpcion(1, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar, dial, CosoPrestamo);
        bt2.Clicked += () => OnOpcion(2, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar, dial, CosoPrestamo);
        bt3.Clicked += () => OnOpcion(3, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar, dial, CosoPrestamo);
        dial.Add(Prestamo, Consejo, Charlar, bt1, bt2, bt3, colgar, cerrar, CosoPrestamo);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(texto, Prestamo, bt1, Consejo, bt2, Charlar, bt3, contacto, Prestamo, Consejo, Charlar, colgar, cerrar);
            return false;
        });
    }
    //Esto es lo que permite que el texto se escriba letra por letra todo bonito
    static void EscribirBonito(string[] dialogos, Label texto, Button[] TodosLosBotones, Label[] TodosLosLabels,
                               Label colgar, Button cerrar, Button[] botones, Label[] labels)
    {
        int indice = Random.Shared.Next(dialogos.Length); //escoge un indice aleatorio cuyo máximo es la cantidad de elementos que contiene el array
        string frase = dialogos[indice];

        texto.Text = "";
        string acumulado = "";//se hace un string porque para el += no acepta caracteres parece, o me daba ese error al menos
        //se hacen invisibles porque se veria feo que mientras se escribe aparezcan botones sin contexto
        foreach (Button boton in TodosLosBotones)
        {
            boton.Visible = false;
        }

        foreach (Label label in TodosLosLabels)
        {
            label.Visible = false;
        }

        colgar.Visible = false;
        cerrar.Visible = false;


        int pos = 0;

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(30), _ =>
        {
            if (pos >= frase.Length) //es decir, si ya sobrepasó la cantidad de caracteres que contiene la frase (el diálogo)
            {
                foreach (Button boton in botones)
                {
                    boton.Visible = true;
                }
                foreach (Label label in labels) 
                { 
                    label.Visible = true;
                }
                colgar.Visible = true;
                cerrar.Visible = true;

                
                return false; // termina el temporizador
            }
             
            acumulado += frase[pos];
            texto.Text = acumulado;
            texto.SetNeedsDisplay();
            pos++;

            return true; // sigue ejecutándose
        });
    }


    //Pues esto se refiere a lo primero que aparece en la llamada. El diálogo de cuando te contesta y las opciones que tenés para responder
    static void PrimerDialogo( Label texto, Label Prestamo, Button bt1, Label Consejo, Button bt2, Label Charlar, Button bt3, NPC contacto,
        Label op1, Label op2, Label op3, Label colgar, Button cerrar)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoContestaLaLlamada[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3], colgar, cerrar, 
            [bt1, bt2, bt3],[op1, op2, op3]); // se llama la funcion para escribir letra por letra
        Prestamo.Text = "¿Cómo va todo?"; // El texto que lleva a otro


        Consejo.Text = "Necesito un consejo."; 

        Charlar.Text = "Solo quería charlar.";

    }
    static void OnOpcion(int op, Label texto,
    Label op1, Label op2, Label op3,
    Button bt1, Button bt2, Button bt3,
    NPC contacto,
    Label colgar, Button cerrar,
    Dialog dial, TextField CosoPrestamo)
    {
        switch (op)
        {
            case 1: //boton 1
                switch (estado)
                {
                    case 0:
                        DespuesDelComoTeVa(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar);
                        break;

                    case 1:
                        MenuPrestamo(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar, CosoPrestamo);
                        break;
                    case 2:
                        FinalConsejo(op1, op2, op3, contacto, texto, colgar, cerrar);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar, CosoPrestamo);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                }
                break;

            case 2: //boton 2
                switch (estado)
                {
                    case 0:
                        Consejo(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar);
                        break;

                    case 1:
                        Consejo(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar);
                        break;

                    case 2:
                        FinalConsejo(op1, op2, op3, contacto, texto, colgar, cerrar);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar, CosoPrestamo);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                }
                break;

            case 3: // boton 3
                switch (estado)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        FinalConsejo(op1, op2, op3, contacto, texto, colgar, cerrar);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar, CosoPrestamo);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                }
                break;
        }
    }
    

    //Lo que sale cuando el usuario presiona como te va
    static void DespuesDelComoTeVa(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRespondeAComoTeVa[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            colgar, cerrar, [bt1, bt2, bt3], [op1, op2, op3]);
        op1.Text = "Necesito un préstamo.";

        op2.Text = "Estoy pasando un mal momento financiero.";
        
        op3.Text = "¿Podrías ayudarme con algo de dinero?";
        estado = 1;
    }
    static void MenuPrestamo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, 
        NPC contacto, Label texto, Label colgar, Button cerrar, TextField CosoPrestamo
        )
    {
        
        if (contacto.Amistad >= 0)
        {
            EscribirBonito(Dialogos_de_Contacto.DialogosCuandoAceptaranPrestamo[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            colgar, cerrar, [bt1], []);

            CosoPrestamo.Visible = true;
            CosoPrestamo.Enabled = true;
            bt1.X = Pos.Right(CosoPrestamo);
            bt1.Text = "Confirmar";
            estado = 3;

        }
        else
        {
            EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRechazaranPrestamo[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            colgar, cerrar, [bt1], []);
            CosoPrestamo.Visible = false;
            CosoPrestamo.Enabled = false;
            
        }

    }
    static void CuandoYaPedisteLaPlata(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3,
        NPC contacto, Label texto, Label colgar, Button cerrar,
        TextField CosoPrestamo)
    {
        var top = Application.Top;
        if (decimal.TryParse(CosoPrestamo.Text.ToString(), out decimal cantidty) && cantidty > 0)
        {
            CosoPrestamo.Enabled = false;
            CosoPrestamo.Visible = false;
            if (cantidty < (contacto.balance / 2))
            {
                MessageBox.Query(
                "Préstamo Exitoso",
                $"Se han transferido {cantidty}$ a tu cuenta.",
                "Aceptar");
                Program.AplicarPrestamoEmergencia(cantidty); // el prestamo
                Program.Guardarelbalance(); // se guarda el balance del jugador
                contacto.balance -= cantidty;
                contacto.TienePrestamoActivo = true;
                contacto.LlamadaPendiente = true;
                contacto.UltimoTurnoPrestamo = Program.turno;
                contacto.PresionActual = Personalidades.Arqueotipos[contacto.idArquetipo].Presion;
                int index = Program.ContactosCargados.FindIndex(n => n.name == contacto.name);

                if (index != -1)
                {
                    Program.ContactosCargados[index] = contacto;
                }
                GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
                
                top.RemoveAll(); // se actualiza la pantalla de inicio
                Program.Inicio(top); // se actualiza la pantalla de inicio
                

                EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPrestanDinero[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
                    colgar, cerrar, [bt1], [op1]);
                
                op1.Text = "Gracias por el préstamo, me será muy útil.";
                bt1.X = Pos.Right(op1) + 3;
                bt1.Text = "";
                estado = 4;
            }
            else
            {
                MessageBox.Query(
                "Error",
                "No te prestará más de la mitad de su balance",
                "Aceptar");
            }
        }
        else
        {
            MessageBox.Query(
                "Error",
                "Escriba número válidos",
                "Aceptar");
        }
    }
    static void Consejo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPidenConsejo[contacto.idArquetipo],
            texto, [bt1, bt2, bt3], [op1, op2, op3],
            colgar, cerrar, [bt1, bt2, bt3], [op1, op2, op3]);

        SwitchRespuestaConsejo(contacto, op1, op2, op3);

        estado = 2;
    }
    static void FinalConsejo(Label op1, Label op2, Label op3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
        texto.Text = "La llamada está por terminar...";

        op1.Text = "Entendido.";
        op2.Text = "No estoy seguro.";
        op3.Text = "Colgar.";

        colgar.Visible = false;
        cerrar.Visible = false;
        estado = 4;
    }
    static void SwitchRespuestaConsejo(NPC contacto, Label op1, Label op2, Label op3)
    {
        switch (contacto.idArquetipo)
        {
            case 0:
                op1.Text = "Gracias… necesitaba escuchar eso.";
                op2.Text = "No sé si voy a poder con esto solo.";
                op3.Text = "Me alivia un poco que lo digas así.";
                break;
            case 1:
                op1.Text = "Eso no ayuda mucho…";
                op2.Text = "Ok, entendido.";
                op3.Text = "Ya veo, da igual entonces.";
                break;
            case 2:
                op1.Text = "Eso suena sospechoso…";
                op2.Text = "¿No me estás ocultando algo?";
                op3.Text = "Cada vez suena peor esto…";
                break;
            case 3:
                op1.Text = "Te estás yendo muy lejos, pero lo entiendo.";
                op2.Text = "No sé si eso me ayuda o me confunde más.";
                op3.Text = "Suena profundo… pero no sé qué hacer con eso.";
                break;
            case 4:
                op1.Text = "¿Y esto qué te cuesta a ti?";
                op2.Text = "Seguro hay algo que no estás diciendo.";
                op3.Text = "Nada es gratis contigo, ¿verdad?";
                break;
            case 5:
                op1.Text = "No sé si esto me sirve… pero gracias.";
                op2.Text = "Creo que lo entendí, más o menos.";
                op3.Text = "Perdón, estoy un poco perdido.";
                break;
            case 6:
                op1.Text = "Ok, ya entendí.";
                op2.Text = "Podrías ser menos complicado.";
                op3.Text = "No era tan necesario decir todo eso.";
                break;
            case 7:
                op1.Text = "Eso se puede aprovechar…";
                op2.Text = "Interesante… ya veo por dónde vas.";
                op3.Text = "Esto se puede usar a mifavor.";
                break;
            case 8:
                op1.Text = "Tiene sentido si lo pienso como estrategia.";
                op2.Text = "No quiero perder tiempo en algo inútil.";
                op3.Text = "Necesito algo más concreto.";
                break;
            case 9:
                op1.Text = "Solo dime si esto me conviene o no.";
                op2.Text = "¿Esto me hace perder dinero o no?";
                op3.Text = "Necesito números, no ideas.";
                break;
        }
    }

    static void TerminarLlamada()
    {
        estado = 0;
        Application.RequestStop();
    }
    public static void Llamar(NPC contacto)
    {


        var dialog = new Dialog("", 70, 23);
        var dialogo = new FrameView("")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(20),
            Width = Dim.Fill() - 2,
            Height = 8
        };
        var nombre = new Label(contacto.name)
        {
            X = 2,
            Y = 0
        };
        var texto = new Label()
        {
            X = 1,
            Y = 2
        };
        //dialogo respuesta
        

        
        dialogo.Add(texto);
        Plantilla(dialog, texto, contacto);

        dialog.Add(nombre, dialogo);

        Application.Run(dialog);
    }


}

public static class TeLlamanPapuContesta
{
    public static void EvaluarLlamadas()
    {
        for (int i = 0; i < Program.ContactosCargados.Count; i++)
        {
            var contacto = Program.ContactosCargados[i];

            if (!contacto.TienePrestamoActivo)
                continue;

            sbyte presion = Personalidades.Arqueotipos[contacto.idArquetipo].Presion;

            int delay = CalcularDelay(presion);

            if (Program.turno >= contacto.UltimoTurnoPrestamo + delay)
            {
                MostrarLlamada(contacto);
            }

            Program.ContactosCargados[i] = contacto; 
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

    static void MostrarLlamada(NPC contacto)
    {
        int index = Program.ContactosCargados.FindIndex(n => n.name == contacto.name);

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
        var texto = new Label() { X = 1, Y = 1 };
        cuadro.Add(texto);

        // Información de deuda (mostramos deuda total por ahora)
        var deudaInfo = new Label($"Deuda total del jugador: {Program.DeudaEmergencia:F2}   Balance contacto: {contacto.balance:F2}")
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
            var lbl = new Label($"Deuda total: {Program.DeudaEmergencia:F2}") { X = 1, Y = 1 };
            var labelPago = new Label("Monto a pagar:") { X = 1, Y = 3 };
            var campoPago = new TextField("") { X = Pos.Right(labelPago) + 1, Y = 3, Width = 12 };
            var btnAceptar = new Button("Aceptar") { X = 1, Y = Pos.AnchorEnd(2) };
            var btnCerrar = new Button("Cerrar") { X = Pos.Right(btnAceptar) + 2, Y = Pos.AnchorEnd(2) };

            btnAceptar.Clicked += () =>
            {
                if (!decimal.TryParse(campoPago.Text.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Query("Error", "Ingrese un monto válido.", "Aceptar");
                    return;
                }

                if (!Program.PagarDeuda(monto))
                {
                    MessageBox.Query("Error", "No tienes suficiente balance para pagar.", "Aceptar");
                    return;
                }

                // Transferir al contacto y actualizar estado
                contacto.balance += monto;
                if (Program.DeudaEmergencia == 0m)
                    contacto.TienePrestamoActivo = false;
                contacto.LlamadaPendiente = false;

                if (index != -1)
                    Program.ContactosCargados[index] = contacto;

                Program.Guardarelbalance();
                GeneracionDeContactos.GuardarContactos(Program.InvInt, false);

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
            LaLlamada.Plantilla(dlg, textoResp, contacto); // usa la interfaz de diálogo existente dentro de esta clase
            dlg.Add(nombreResp, dialogoResp);
            Application.Run(dlg);
        };

        btnIgnorar.Clicked += () => Application.RequestStop();

        dialogo.Add(nombre, cuadro, deudaInfo, btnPagar, btnResponder, btnIgnorar);
        Application.Run(dialogo);
    }

}


