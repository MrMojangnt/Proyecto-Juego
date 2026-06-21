using Empresas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;
using Terminal.Gui;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Proyecto_Juego;

public class LaLlamada
{
    static int estado = 0;
    //Aquí se crean los labels y botones que se utilizarán en las otras funciones, cuando se toque una opcion, cuando cambie el dialogo y asi

    //label de opciones
    static Label op1 = new(); //default prestamo
    static Label op2 = new();//default consejo
    static Label op3 = new();//default interes en el mercado

    //Label para colgar
    static Label colgar = new();

    //Boton para colgar
    static Button cerrar = new();

    //Textfield para el prestamo
    static TextField CosoPrestamo = new("");


    public static void Plantilla(Dialog dial, Label texto, NPC contacto)
    {
        estado = 0;
        estado = 0;

        //Llamar(Dialogos[contacto.idArquetipo], contacto);

        //Definiendo la posición de la opción(label) del "Cómo te va" (prestamo)
        op1.Text = "";
        op1.X = 2;
        op1.Y = Pos.AnchorEnd(8);

        //Definiendo la posición del botón (Button) del "Cómo te va" (préstamo)
        var bt1 = new Button()
        {
            X = Pos.Right(op1),
            Y = Pos.AnchorEnd(8)
        };

        //Definiendo la posición de la opción (label) del "Necesito un consejo" (consejo)
        op2.Text = "";
        op2.X = 2;
        op2.Y = Pos.AnchorEnd(6);

        //Definiendo la posición del botón (Button) del "Ncesito un consejo" (consejo)
        var bt2 = new Button()
        {
            X = Pos.Right(op2),
            Y = Pos.AnchorEnd(6)
        };

        //Definiendo la posición de la opción (Label) del "Interés en el mercado" (interés en el mercado)
        op3.Text = "";
        op3.X = 2;
        op3.Y = Pos.AnchorEnd(4);

        //Definiendo la posición del botón (Button) del "Interés en el mercado" (interés en el mercado)
        var bt3 = new Button()
        {
            X = Pos.Right(op3),
            Y = Pos.AnchorEnd(4)
        };

        //Definiendo la posición de la opción para colgar
        colgar.Text = "¿Sabes qué?, mejor colgaré, gracias.";
        colgar.X = 2;
        colgar.Y = Pos.AnchorEnd(2);

        //Definiendo la posición del botón para colgar
        cerrar.X = Pos.Right(colgar);
        cerrar.Y = Pos.AnchorEnd(2);

        //Definiendo la posicion y tamaño del textfield para poner el prestamo
        CosoPrestamo.Text = "";
        CosoPrestamo.X = Pos.X(op1);
        CosoPrestamo.Y = Pos.Y(op1);
        CosoPrestamo.Width = 10;
       
        //Declarando lo que pasará cuando se presione el boton cerrar
        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };

        //
        bt1.Clicked += () => OnOpcion(1, texto, contacto, dial, bt1, bt2, bt3);
        bt2.Clicked += () => OnOpcion(2, texto, contacto, dial, bt1, bt2, bt3);
        bt3.Clicked += () => OnOpcion(3, texto, contacto, dial, bt1, bt2, bt3);
        dial.Add(op1,op2,op3, bt1, bt2, bt3, colgar, cerrar, CosoPrestamo);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(texto, contacto, bt1, bt2, bt3);
            return false;
        });
    }
    //Esto es lo que permite que el texto se escriba letra por letra todo bonito
    static void EscribirBonito(string[] dialogos, Label texto, Button[] TodosLosBotones, Label[] TodosLosLabels,
                              Button[] botones, Label[] labels, 
                              bool SiPresta, bool SiCuelga)
    {
        int indice = Random.Shared.Next(dialogos.Length); //escoge un indice aleatorio cuyo máximo es la cantidad de elementos que contiene el array
        string frase = dialogos[indice];

        texto.Text = "";
        string acumulado = "";//se hace un string porque para el += no acepta caracteres parece, o me daba ese error al menos
        //se hacen invisibles porque se veria feo que mientras se escribe aparezcan botones sin contexto

        CosoPrestamo.Visible = false;
        CosoPrestamo.Enabled = false;
        foreach (Button boton in TodosLosBotones)
        {
            boton.Visible = false;
            boton.Enabled = false;
        }

        foreach (Label label in TodosLosLabels)
        {
            label.Visible = false;
            label.Enabled = false;
        }

        colgar.Visible = false;
        colgar.Enabled = false;
        cerrar.Visible = false;
        cerrar.Enabled = false;


        int pos = 0;

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(30), _ =>
        {
            if (pos >= frase.Length) //es decir, si ya sobrepasó la cantidad de caracteres que contiene la frase (el diálogo)
            {
                
                foreach (Button boton in botones)
                {
                    boton.Visible = true;
                    boton.Enabled = true;
                }
                foreach (Label label in labels) 
                { 
                    label.Visible = true;
                    label.Enabled = true;
                }
                if (SiPresta)
                {
                    CosoPrestamo.Visible = true;
                    CosoPrestamo.Enabled = true;

                }
                if (SiCuelga)
                {
                    colgar.Enabled = true;
                    colgar.Visible = true;
                    cerrar.Visible = true;
                    cerrar.Enabled = true;
                }
                
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
    static void PrimerDialogo( Label texto, NPC contacto, Button bt1, Button bt2, Button bt3)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoContestaLaLlamada[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3], 
            [bt1, bt2, bt3],[op1, op2, op3], false, true); // se llama la funcion para escribir letra por letra
        op1.Text = "¿Cómo va todo?"; // El texto que lleva a otro
        bt1.X = Pos.Right(op1);

        op2.Text = "Necesito un consejo.";
        bt2.X = Pos.Right(op2);

        op3.Text = "Me gustaría invertir en el mercado.";
        bt3.X = Pos.Right(op3);

    }
    static void OnOpcion(int op, Label texto,
    NPC contacto,
    Dialog dial,
    Button bt1, Button bt2, Button bt3)
    {
        var top = Application.Top;
        switch (op)
        {
            case 1: //boton 1
                switch (estado)
                {
                    case 0:
                        DespuesDelComoTeVa(contacto, texto, bt1, bt2, bt3);
                        break;

                    case 1:
                        MenuPrestamo(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 2:
                        FinalConsejo(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                    case 5:
                        IrseALasEmpresas(contacto, texto, bt1, bt2, bt3);
                        estado = 2;
                        break;
                }
                break;

            case 2: //boton 2
                switch (estado)
                {
                    case 0:
                        int index = CargandoLasPartidas.ContactosCargados
                            .FindIndex(n => n.name == contacto.name);

                        if (index != -1)
                        {
                            contacto.Amistad += 1;
                            CargandoLasPartidas.ContactosCargados[index] = contacto;
                        }
                        Consejo(contacto, texto, bt1, bt2, bt3);
                        break;

                    case 1:
                        MenuPrestamo(contacto, texto, bt1, bt2, bt3);
                        break;

                    case 2:
                        FinalConsejo(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                    case 5:
                        FinalConsejo(contacto, texto, bt1, bt2, bt3); 
                        break;
                }
                break;

            case 3: // boton 3
                switch (estado)
                {
                    case 0:
                        InteraccionSector(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 1:
                        MenuPrestamo(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 2:
                        FinalConsejo(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 3:
                        CuandoYaPedisteLaPlata(contacto, texto, bt1, bt2, bt3);
                        break;
                    case 4:
                        TerminarLlamada();
                        break;
                    case 5:
                        FinalConsejo(contacto, texto, bt1, bt2, bt3);
                        break;
                }
                break;
        }
    }
    

    //Lo que sale cuando el usuario presiona como te va
    static void DespuesDelComoTeVa(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRespondeAComoTeVa[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            [bt1, bt2, bt3], [op1, op2, op3],
            false, true);
        op1.Text = "Necesito un préstamo.";
        bt1.X = Pos.Right(op1);

        op2.Text = "Estoy pasando un mal momento financiero.";
        bt2.X = Pos.Right(op2);

        op3.Text = "¿Podrías ayudarme con algo de dinero?";
        bt3.X = Pos.Right(op3);
        estado = 1;
    }
    static void MenuPrestamo(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
    {
        
        if (contacto.Amistad >= 0)
        {
            EscribirBonito(Dialogos_de_Contacto.DialogosCuandoAceptaranPrestamo[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            [bt1], [op2],
            true, true);

   
            bt1.X = Pos.Right(CosoPrestamo);
            op2.Text = "¿Podrías prestarme esta cantidad?";
            estado = 3;

        }
        else
        {
            EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRechazaranPrestamo[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
            [], [],
            false, true);
            
            
        }

    }
    static void CuandoYaPedisteLaPlata(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
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
                ModificarPartidas.AplicarPrestamoEmergencia(cantidty); // el prestamo
                ModificarPartidas.Guardarelbalance(); // se guarda el balance del jugador
                contacto.balance -= cantidty; //se resta el monto que se pidió prestado del monto del contacto
                contacto.TienePrestamoActivo = true; //para recordar que tiene prestamo activo
                contacto.LlamadaPendiente = true; //recordar que tiene llamada pendiente po rlo del prestamo
                contacto.UltimoTurnoPrestamo = ManejoDeArchivos.turno;
                contacto.PresionActual = Personalidades.Arqueotipos[contacto.idArquetipo].Presion;
                contacto.montoprestado += cantidty; // acumula lo que te debe ese contacto
                int index = CargandoLasPartidas.ContactosCargados.FindIndex(n => n.name == contacto.name);

                if (index != -1)
                {
                    CargandoLasPartidas.ContactosCargados[index] = contacto;
                }
                GeneracionDeContactos.GuardarContactos(Program.InvInt, false);
                
                top.RemoveAll(); // se actualiza la pantalla de inicio
                Program.Inicio(top); // se actualiza la pantalla de inicio
                

                EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPrestanDinero[contacto.idArquetipo], texto, [bt1, bt2, bt3], [op1, op2, op3],
                [bt1], [op1], false, true);
                
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
    static void Consejo(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPidenConsejo[contacto.idArquetipo],
            texto, [bt1, bt2, bt3], [op1, op2, op3],
            [bt1, bt2, bt3], [op1, op2, op3],
            false, true);
        
        SwitchRespuestaConsejo(contacto, op1, op2, op3);

        estado = 2;
    }
    static void FinalConsejo(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
    {
        EscribirBonito(["..........." ], texto,
            [bt1, bt2, bt3], [op1, op2, op3],
            [bt3], [op3],
            false, false );

        
        op3.Text = "Colgar.";
        estado = 4;
    }
    static void InteraccionSector(
      NPC contacto,
      Label texto,
      Button bt1, Button bt2, Button bt3)
    {
        string sector = contacto.sector_dominante;

        // 1. Buscar empresas
        Companias mejor = Indices.ObtenerMejorEmpresa(sector);
        Companias peor = Indices.ObtenerPeorEmpresa(sector);

        // 2. Validación REAL (no confiar en name null)
        bool sectorVacio =
            string.IsNullOrEmpty(mejor.name) &&
            string.IsNullOrEmpty(peor.name);

        if (sectorVacio)
        {
            EscribirBonito(
                [
                $"El sector {sector} parece inactivo...\nNo hay datos suficientes."
                ],
                texto,
                [bt1, bt2, bt3],
                [op1, op2, op3],
                [bt1, bt2],
                [op1, op2],
                false, true
            );

            op1.Text = "Entiendo.";
            bt1.X = Pos.Right(op1);

            op2.Text = "Da igual.";
            bt2.X = Pos.Right(op2);

            estado = 4;
            return;
        }

        // 3. Impacto económico SOLO si hay sector válido
        ModificarPartidas.AplicarImpactoSector(sector, 1.25m);

        // 4. Decisión del NPC
        bool optimista = Random.Shared.NextDouble() > 0.5;

        string mensaje;

        if (optimista)
        {
            if (string.IsNullOrEmpty(mejor.name))
            {
                mensaje = $"El sector {sector} muestra señales de crecimiento.";
                op1.Text = "Interesante.";
                bt1.X = Pos.Right(op1);

            }
            else
            {
                mensaje =
                    $"La empresa {mejor.name} está dominando el sector {sector}.\nEl crecimiento parece inevitable.";

                op1.Text = $"Seguir a {mejor.name}";
                bt1.X = Pos.Right(op1);

            }
        }
        else
        {
            if (string.IsNullOrEmpty(peor.name))
            {
                mensaje = $"El sector {sector} está inestable.";
                op1.Text = "Preocupante.";
                bt1.X = Pos.Right(op1);
            }
            else
            {
                mensaje =
                    $"{peor.name} está muy débil...\nSi esto continúa, el sector podría colapsar.";

                op1.Text = $"Preocuparse por {peor.name}";
                bt1.X = Pos.Right(op1);

            }
        }

        // 5. Diálogo
        EscribirBonito(
            new string[] { mensaje },
            texto,
            [bt1, bt2, bt3],
            [op1, op2, op3],
            [bt1, bt2],
            [op1, op2],
            false, true
        );

        // 6. Opciones
        op2.Text = "No me interesa el mercado.";
        bt2.X = Pos.Right(op2);


        estado = 5;
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
    static void IrseALasEmpresas(NPC contacto, Label texto, Button bt1, Button bt2, Button bt3)
    {
        EscribirBonito(["Me despido."], texto, [bt1, bt2, bt3], [op1, op2, op3], [bt1, bt2], [op1, op2], 
            false, true);
        op1.Text = "Invertiré en el sector, gracias.";
        bt1.X = Pos.Right(op1);

        op2.Text = "Ignoraré el análisis, no me fue útil.";
        bt2.X = Pos.Right(op2);
    }
    static void TerminarLlamada()
    {
        estado = 0;
        Application.RequestStop();
    }
    public static void Llamar(NPC contacto)
    {

        estado = 0;
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
        for (int i = 0; i < CargandoLasPartidas.ContactosCargados.Count; i++)
        {
            var contacto = CargandoLasPartidas.ContactosCargados[i];

            if (!contacto.TienePrestamoActivo)
                continue;

            sbyte presion = Personalidades.Arqueotipos[contacto.idArquetipo].Presion;

            int delay = CalcularDelay(presion);

            if (ManejoDeArchivos.turno >= contacto.UltimoTurnoPrestamo + delay)
            {
                MostrarLlamada(contacto);
            }

            CargandoLasPartidas.ContactosCargados[i] = contacto; 
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
        string mensaje = Dialogos_de_Contacto.CobroDeuda[nivel][Random.Shared.Next(Dialogos_de_Contacto.CobroDeuda[nivel].Length)];
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
                }

                if (index != -1)
                    CargandoLasPartidas.ContactosCargados[index] = contacto;

                ModificarPartidas.Guardarelbalance();
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


