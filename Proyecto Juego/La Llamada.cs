using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Terminal.Gui;
namespace Proyecto_Juego;

public static class LaLlamada
{
    static int estado = 0;
    //Aquí se crean los labels y botones que se utilizarán en las otras funciones, cuando se toque una opcion, cuando cambie el dialogo y asi
    static void Plantilla(Dialog dial, Label texto, NPC contacto)
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
        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        bt1.Clicked += () => OnOpcion(1, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar);
        bt2.Clicked += () => OnOpcion(2, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar);
        bt3.Clicked += () => OnOpcion(3, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto, colgar, cerrar);
        dial.Add(Prestamo, Consejo, Charlar, bt1, bt2, bt3, colgar, cerrar);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(texto, Prestamo, bt1, Consejo, bt2, Charlar, bt3, contacto, Prestamo, Consejo, Charlar, colgar, cerrar);
            return false;
        });
    }
    //Esto es lo que permite que el texto se escriba letra por letra todo bonito
    static void EscribirBonito(string[] dialogos, Label texto,
                               Button bt1, Button bt2, Button bt3,
                               Label op1, Label op2, Label op3,
                               Label colgar, Button cerrar)
    {
        int indice = Random.Shared.Next(dialogos.Length); //escoge un indice aleatorio cuyo máximo es la cantidad de elementos que contiene el array
        string frase = dialogos[indice]; 

        texto.Text = ""; 
        string acumulado = "";//se hace un string porque para el += no acepta caracteres parece, o me daba ese error al menos
        bt1.Visible = false; //se hacen invisibles porque se veria feo que mientras se escribe aparezcan botones sin contexto
        bt2.Visible = false;
        bt3.Visible = false;
        op1.Visible = false;
        op2.Visible = false;
        op3.Visible = false;
        colgar.Visible = false;
        cerrar.Visible = false;

        int pos = 0;

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(30), _ =>
        {
            if (pos >= frase.Length) //es decir, si ya sobrepasó la cantidad de caracteres que contiene la frase (el diálogo)
            {
                bt1.Visible = true; //como ya acabó entonces se vuelven visibles
                bt2.Visible = true;
                bt3.Visible = true;
                op1.Visible = true;
                op2.Visible = true;
                op3.Visible = true;
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
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoContestaLaLlamada[contacto.idArquetipo], texto, bt1, bt2, bt3, op1, op2, op3, colgar, cerrar); // se llama la funcion para escribir letra por letra
        Prestamo.Text = "¿Cómo va todo?"; // El texto que lleva a otro


        Consejo.Text = "Necesito un consejo."; 

        Charlar.Text = "Solo quería charlar.";

    }
    static void OnOpcion(int op, Label texto,
    Label op1, Label op2, Label op3,
    Button bt1, Button bt2, Button bt3,
    NPC contacto,
    Label colgar, Button cerrar)
    {
        switch (op)
        {
            case 1:
                switch (estado)
                {
                    case 0:
                        DespuesDelComoTeVa(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar);
                        break;

                    case 1:
                        MenuPrestamo(op1, bt1, op2, bt2, op3, bt3, contacto, texto, colgar, cerrar);
                        estado = 2;
                        break;
                    case 2:
                        FinalConsejo(op1, op2, op3, contacto, texto, colgar, cerrar);
                        break;
                    case 3:
                        TerminarLlamada();
                        break;
                }
                break;

            case 2:
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
                        TerminarLlamada();
                        break;
                }
                break;

            case 3:
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
                        TerminarLlamada();
                        break;
                }
                break;
        }
    }
    

    //Lo que sale cuando el usuario presiona como te va
    static void DespuesDelComoTeVa(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRespondeAComoTeVa[contacto.idArquetipo], texto, bt1, bt2, bt3, op1, op2, op3, colgar, cerrar);
        op1.Text = "Necesito un préstamo.";

        op2.Text = "Estoy pasando un mal momento financiero.";
        
        op3.Text = "¿Podrías ayudarme con algo de dinero?";
        estado = 1;
    }
    static void MenuPrestamo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
      
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPidenPrestamo[contacto.idArquetipo], texto, bt1, bt2, bt3, op1, op2, op3, colgar, cerrar);
        op1.Visible = false;

        
    }
    static void Consejo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto, Label colgar, Button cerrar)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPidenConsejo[contacto.idArquetipo],
            texto, bt1, bt2, bt3, op1, op2, op3,
            colgar, cerrar);

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
        estado = 3;
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
                op3.Text = "Esto se puede usar a mi favor.";
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


