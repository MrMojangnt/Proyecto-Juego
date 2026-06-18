using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Terminal.Gui;
namespace Proyecto_Juego;

public static class LaLlamada
{
    static void Plantilla(Dialog dial, Label texto, NPC contacto)
    {
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
        dial.Add(Prestamo, Consejo, Charlar, bt1, bt2, bt3);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(texto, Prestamo, bt1, Consejo, bt2, Charlar, bt3, contacto);
            return false;
        });
    }
    static void EscribirBonito(string[] dialogos, Label texto,
                               Button bt1, Button bt2, Button bt3)
    {
        int indice = Random.Shared.Next(dialogos.Length);
        string frase = dialogos[indice];

        texto.Text = "";
        string acumulado = "";
        bt1.Visible = false;
        bt2.Visible = false;
        bt3.Visible = false;

        int pos = 0;

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(30), _ =>
        {
            if (pos >= frase.Length)
            {
                bt1.Visible = true;
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
    static void PrimerDialogo( Label texto, Label Prestamo, Button bt1, Label Consejo, Button bt2, Label Charlar, Button bt3, NPC contacto)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoContestaLaLlamada[contacto.idArquetipo], texto, bt1, bt2, bt3);
        Prestamo.Text = "¿Cómo va todo?"; // Prestamo

        bt1.Clicked += () =>
        {
            MenuPrestamo(Prestamo, bt1, Consejo, bt2, Charlar, bt3, contacto, texto);
        };
        Consejo.Text = "Necesito un consejo.";

        Charlar.Text = "Solo quería charlar.";
    }
    static void MenuPrestamo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3, NPC contacto, Label texto)
    {
        EscribirBonito(Dialogos_de_Contacto.DialogosCuandoRespondeAComoTeVa[contacto.idArquetipo], texto, bt1, bt2, bt3);
        op1.Text = "Necesito un préstamo.";
        op2.Text = "Estoy pasando un mal momento financiero.";
        op3.Text = "¿Podrías ayudarme con algo de dinero?";
    }

    public static void Llamar(NPC contacto)
    {


        var dialog = new Dialog("", 60, 20);
        var dialogo = new FrameView("")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(15),
            Width = Dim.Fill() - 2,
            Height = 7
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
        var cerrar = new Button("Colgar")
        {
            X = 2,
            Y = Pos.AnchorEnd(2)
        };

        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        dialogo.Add(texto);
        Plantilla(dialog, texto, contacto);

        dialog.Add(nombre, dialogo, cerrar);

        Application.Run(dialog);
    }


}


