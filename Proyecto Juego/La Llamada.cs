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
        bt1.Clicked += () => OnOpcion(1, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto);
        bt2.Clicked += () => OnOpcion(2, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto);
        bt3.Clicked += () => OnOpcion(3, texto, Prestamo, Consejo, Charlar, bt1, bt2, bt3, contacto);
        dial.Add(Prestamo, Consejo, Charlar, bt1, bt2, bt3);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(texto, Prestamo, bt1, Consejo, bt2, Charlar, bt3, contacto);
            return false;
        });
    }
    //Esto es lo que permite que el texto se escriba letra por letra todo bonito
    static void EscribirBonito(string[] dialogos, Label texto,
                               Button bt1, Button bt2, Button bt3)
    {
        int indice = Random.Shared.Next(dialogos.Length); //escoge un indice aleatorio cuyo máximo es la cantidad de elementos que contiene el array
        string frase = dialogos[indice]; 

        texto.Text = ""; 
        string acumulado = "";//se hace un string porque para el += no acepta caracteres parece, o me daba ese error al menos
        bt1.Visible = false; //se hacen invisibles porque se veria feo que mientras se escribe aparezcan botones sin contexto
        bt2.Visible = false;
        bt3.Visible = false;

        int pos = 0;

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(30), _ =>
        {
            if (pos >= frase.Length) //es decir, si ya sobrepasó la cantidad de caracteres que contiene la frase (el diálogo)
            {
                bt1.Visible = true; //como ya acabó entonces se vuelven visibles
                bt2.Visible = true;
                bt3.Visible = true;
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
    static void PrimerDialogo( Label texto, Label Prestamo, Button bt1, Label Consejo, Button bt2, Label Charlar, Button bt3, NPC contacto)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoContestaLaLlamada[contacto.idArquetipo], texto, bt1, bt2, bt3); // se llama la funcion para escribir letra por letra
        Prestamo.Text = "¿Cómo va todo?"; // El texto que lleva a otro


        Consejo.Text = "Necesito un consejo."; 

        Charlar.Text = "Solo quería charlar.";

    }
    static void OnOpcion(int op, Label texto,
    Label op1, Label op2, Label op3,
    Button bt1, Button bt2, Button bt3,
    NPC contacto)
    {
        switch (op)
        {
            case 1:
                switch (estado)
                {
                    case 0:
                        DespuesDelComoTeVa(op1, bt1, op2, bt2, op3, bt3, contacto, texto); //Llama a una función que tendrá la misma estructura, pero con distintas opciones, que permitirán al usuario pedir préstamos
                        break;

                    case 1:
                        MenuPrestamo(op1, bt1, op2, bt2, op3, bt3, contacto, texto);
                        break;
                }
                break;
        }

    }    

    //Lo que sale cuando el usuario presiona como te va
    static void DespuesDelComoTeVa(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRespondeAComoTeVa[contacto.idArquetipo], texto, bt1, bt2, bt3);
        op1.Text = "Necesito un préstamo.";

        op2.Text = "Estoy pasando un mal momento financiero.";
        
        op3.Text = "¿Podrías ayudarme con algo de dinero?";
        estado = 1;
    }
    static void MenuPrestamo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto)
    {
      
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoPidenPrestamo[contacto.idArquetipo], texto, bt1, bt2, bt3);
        
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
            X = 2,
            Y = 2
        };
        //dialogo respuesta
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
        dialogo.Add(texto);
        Plantilla(dialog, texto, contacto);

        dialog.Add(nombre, dialogo, colgar, cerrar);

        Application.Run(dialog);
    }


}


