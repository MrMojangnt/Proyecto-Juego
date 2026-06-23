using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_Juego;

public class Tutorial
{
    static int estado = 0;
    static bool permitirCerrar = false;
    public static void EvaluarTutorial() // Verifica si tiene que invocar el tutorial o no
    {
        if (Program.MostrarTutorial)
        {
            EmpezarTutorial();
        }
    }

    static void EmpezarTutorial()
    {
        var TutorialIvancito = new Dialog($"LLamada de Ivancito", 60, 20);

        var cuadro = new FrameView("")
        {
            X = 1,
            Y = 2,
            Width = Dim.Fill() - 2,
            Height = 15
        };
        var texto = new Label()
        {
            X = 1,
            Y = 1
        };
        var Continue = new Label("Continuar")
        {
            X = 1,
            Y = Pos.Bottom(cuadro)
        };
        var Continuar = new Button()
        {
            X = Pos.Right(Continue),
            Y = Pos.Y(Continue)
        };
        var SaltarTutorial = new Label( "Saltar Tutorial")
        {
            X = Pos.AnchorEnd(20),
            Y = Pos.Bottom(cuadro)
        };
        var Saltar = new Button()
        {
            X = Pos.Right(SaltarTutorial),
            Y = Pos.Y(SaltarTutorial)
        };
        //Para que no se salga del dialog con Esc
        TutorialIvancito.Closing += (e) =>
        {
            if (!permitirCerrar)
                e.Cancel = true;
        };

        Saltar.Clicked += () =>
        {
            Program.MostrarTutorial = false;
            permitirCerrar = true;
            Application.RequestStop();
        };
        Continuar.Clicked += () =>
        {
            SwitchTutorial(texto, [Saltar, Continuar],
                [SaltarTutorial, Continue], [Saltar, Continuar], [SaltarTutorial, Continue]);
        };

        //Se escribe el primer párrafo del tutorial
            EscribirBonito(@$"Bienvenido al mundo de las inversiones.

Comienzas con un capital inicial y una red de 
contactos aún por construir. Tus decisiones 
influirán en el mercado, en tus relaciones 
y en el crecimiento de tu fortuna.

Antes de empezar, aprenderás los conceptos básicos 
necesarios para sobrevivir en este entorno 
competitivo.", texto,
[Continuar, Saltar], [Continue, SaltarTutorial], [Continuar, Saltar], [Continue, SaltarTutorial]);

        

        cuadro.Add(texto);
        TutorialIvancito.Add(cuadro, Continue, Continuar,Saltar, SaltarTutorial);

        Application.Run(TutorialIvancito);
    }//NO SABEN DONDE COMPRAR ACCIONES
    //NO SABEN COMO TRABAJAR

    static void SwitchTutorial(Label texto, Button[] TodosLosBotones, Label[] TodosLosLabels,
                          Button[] botones, Label[] labels)
    {
        switch (estado)
        {

            case 0:
                EscribirBonito(@"           Capital Inicial

Dispones de un capital inicial de $50,000.

Este dinero será utilizado para comprar 
acciones, realizar inversiones y aprovechar
oportunidades que aparecerán durante 
la partida.

Administra tus recursos con cuidado. 
Quedarte sin liquidez puede limitar 
tus opciones.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 1:
                EscribirBonito(@"           Empresas

Las empresas son el centro del mercado.

Cada una pertenece a un sector económico
distinto y posee características propias que
afectan el valor de sus acciones.

Analizar una empresa antes de invertir puede
marcar la diferencia entre una ganancia 
y una pérdida.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 2:
                EscribirBonito(@"           Acciones

Al comprar acciones te conviertes en propietario
de una pequeña parte de una empresa.

Si el valor de la empresa aumenta, tus acciones 
también aumentarán de valor.

Si la empresa atraviesa dificultades, el precio 
de sus acciones podría disminuir.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 3:
                EscribirBonito(@"           Contactos

A lo largo de la partida conocerás distintos 
contactos.

Algunos podrán ofrecer información útil, 
mientras que otros podrían brindarte 
oportunidades especiales o apoyo financiero.

Mantener buenas relaciones puede abrir puertas 
que el dinero por sí solo no puede abrir.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 4:
                EscribirBonito(@"           Eventos y noticias

El mercado cambia constantemente.

Noticias, acontecimientos económicos y eventos 
inesperados pueden afectar el valor de las 
empresas y alterar las condiciones del mercado.

Mantente atento a la información disponible antes de
tomar decisiones importantes.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 5:
                EscribirBonito(@"       Cierre

Ya conoces los conceptos básicos.

A partir de ahora, el éxito dependerá de tus 
decisiones.

Buena suerte en tu camino como inversor.", texto,
TodosLosBotones, TodosLosLabels, botones, labels);
                estado++;
                break;
            case 6:
                Program.MostrarTutorial = false;
                permitirCerrar = true;
                Application.RequestStop();
                break;
        }
        
    }
    static void EscribirBonito(string frase, Label texto, Button[] TodosLosBotones, Label[] TodosLosLabels,
                          Button[] botones, Label[] labels)
    {
        

        texto.Text = "";
        string acumulado = "";//se hace un string porque para el += no acepta caracteres parece, o me daba ese error al menos
        //se hacen invisibles porque se veria feo que mientras se escribe aparezcan botones sin contexto

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

                return false; // termina el temporizador
            }

            acumulado += frase[pos];
            texto.Text = acumulado;
            texto.SetNeedsDisplay();
            pos++;

            return true; // sigue ejecutándose
        });
    }
}

public class AyudaFinanciera
{

}
