using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;
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
    {//Ahora para los dialog se pone el Title 
        var TutorialIvancito = new Dialog() { Title = $"LLamada de Ivancito",Width = 60, Height = 20 };//No estoy seguro si la altura y el ancho no están al reves

        var cuadro = new FrameView()
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
        var Continue = new Label()
        {
            Text = "Continuar",
            X = 1,
            Y = Pos.Bottom(cuadro)
        };
        var Continuar = new Button()
        {
            X = Pos.Right(Continue),
            Y = Pos.Y(Continue)
        };
        var SaltarTutorial = new Label()
        {
            Text = "Saltar Tutorial",
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

        Saltar.Accepting += (s,e) =>
        {
            Program.MostrarTutorial = false;
            permitirCerrar = true;
            Application.RequestStop();
        };
        Continuar.Accepting += (s,e) =>
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
    static string[] ConceptosFinancieros =
{
    "Acción",
    "Préstamo",
    "Diversificación",
    "Capitalización bursátil",
    "Riesgo financiero",
    "Liquidez",
    "Oferta y demanda",
    "Volatilidad",
    "Ganancia",
    "Deuda"
};

    static string[] ExplicacionesConceptos =
    {
    @"Una acción representa una pequeña parte de una empresa.
Al comprar acciones te conviertes en propietario de una
fracción de la compañía. Su valor puede subir o bajar
dependiendo del desempeño de la empresa y del mercado.",

    @"Un préstamo es dinero recibido con el compromiso
de devolverlo posteriormente. En muchos casos debe
devolverse junto con intereses u otras condiciones.",

    @"La diversificación consiste en distribuir las
inversiones entre varias empresas o sectores para
reducir el riesgo de pérdidas importantes.",

    @"La capitalización bursátil es el valor total de una
empresa en el mercado. Se calcula multiplicando el
precio de una acción por la cantidad de acciones 
existentes.",

    @"El riesgo financiero es la posibilidad de perder
dinero debido a decisiones de inversión, cambios
económicos o eventos inesperados.",

    @"La liquidez es la facilidad con la que un activo
puede convertirse en dinero sin perder valor.
El efectivo es el activo más líquido.",

    @"La oferta y la demanda influyen en el precio de los
activos. Si muchas personas quieren comprar, el precio
tiende a subir. Si muchas quieren vender, suele bajar.",

    @"La volatilidad mide qué tanto cambia el precio de un
activo en poco tiempo. Una alta volatilidad implica
mayor incertidumbre y mayor riesgo.",

    @"La ganancia es el beneficio obtenido después de una
operación o inversión. Se produce cuando los ingresos
superan a los costos.",

    @"La deuda representa una obligación pendiente de pago.
Un nivel de deuda muy alto puede generar problemas
financieros si no se administra correctamente."
};

    public static void Aprender(Window Ventana, int PosX, Pos PosY)
    {
        var Aprender = new Button()
        {
            Text = "Aprender",
            X = PosX,//Para que escogas las coordenadas
            Y = PosY,
        };

        Aprender.Accepting += (s,e) =>
        {
            var dialog = new Dialog() { Title = "Apuesta de Caballos", Width = 95, Height = 30 };
            var buttonAccion = new Button()
            {
                Text = "Sobre Acciones",
                X = 1,
                Y = 2
            };
            var buttonPrestamo = new Button()
            {
                Text = "Sobre Prestamos",
                X = 1,
                Y = 4
            };
            var buttonDiversificacion = new Button()
            {
                Text = "Sobre Diversificacion",
                X = 1,
                Y = 6
            };
            var buttonCap = new Button()
            {
                Text = "Sobre Cap.Bursatil",
                X = 1,
                Y = 8
            };
            var buttonRiesgos = new Button()
            {
                Text = "Sobre Riesgos",
                X = 1,
                Y = 10
            };
            var buttonLiquidez = new Button()
            {
                Text = "Sobre Liquidez",
                X = 1,
                Y = 12
            };
            var buttonOfertaydemanda = new Button()
            {
                Text = "Sobre oferta y demanda",
                X = 1,
                Y = 14
            };
            var buttonVolatilidad = new Button()
            {
                Text = "Sobre volatilidad",
                X = 1,
                Y = 16
            };
            var buttonGanancias = new Button()
            {
                Text = "Sobre ganancias",
                X = 1,
                Y = 18
            };
            var buttonDeudas = new Button()
            {
                Text = "Sobre Deudas",
                X = 1,
                Y = 20
            };
            var buttonSalir = new Button()
            {
                Text = "Salir",
                X = Pos.Center(),
                Y = 27
            };
            string TituloString = "Bienvenido";
            string Descripcion = "Selecciona una opcion de los botones laterales";
          
            
            var titulo = new Label()
            {
                Text = TituloString,
                X = Pos.Center() + 2,
                Y = 2
            };
            var Desc = new Label()
            {
                Text= Descripcion,
                X = 37,
                Y = 4
            };
              //Accion
            buttonAccion.Accepting += (s,e) => { 
                titulo.Text = ConceptosFinancieros[0];
                Desc.Text = ExplicacionesConceptos[0];
                
            };
            //Prestamo
            buttonPrestamo.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[1];
                Desc.Text = ExplicacionesConceptos[1];
            };
            //Diversificacion
            buttonDiversificacion.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[2];
                Desc.Text = ExplicacionesConceptos[2];
            };
            //Capitalizacion
            buttonCap.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[3];
                Desc.Text = ExplicacionesConceptos[3];
            };
            //Riesgos
            buttonRiesgos.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[4];
                Desc.Text = ExplicacionesConceptos[4];

            };
            //Liquidez
            buttonLiquidez.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[5];
                Desc.Text = ExplicacionesConceptos[5];
            };
            //Oferta y demanda
            buttonOfertaydemanda.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[6];
                Desc.Text = ExplicacionesConceptos[6];

            };
            //Volatibilidad
            buttonVolatilidad.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[7];
                Desc.Text = ExplicacionesConceptos[7];

            };
            //Ganancias
            buttonGanancias.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[8];
                Desc.Text = ExplicacionesConceptos[8];

            };
            //Deudas
            buttonDeudas.Accepting += (s, e) => {
                titulo.Text = ConceptosFinancieros[9];
                Desc.Text = ExplicacionesConceptos[9];

            };
            buttonSalir.Accepting += (s, e) => { IApplication.RequestStop(dialog); };
            
            
            dialog.Add(titulo, Desc);
            dialog.Add(buttonAccion,buttonSalir ,buttonPrestamo, buttonDiversificacion, buttonCap, buttonRiesgos, buttonLiquidez, buttonOfertaydemanda, buttonVolatilidad,buttonGanancias,buttonDeudas);
            Application.Run(dialog);
        };

        Ventana.Add(Aprender);
    }
}
