using NAudio.Wave;
using Terminal.Gui;
using System.IO;

class Vestia
{
    static Label etiqNombre, etiqPais;
    static WaveOutEvent salidaAudio;
    static AudioFileReader audio;
    static bool reproduciendo = false;
    static bool muteado = false;

    static List<string> Paises = new List<string>() { "Nicaragua (predeterminado)", "EE.UU.", "Japón", "China", "Alemania", "España" };
    static string PaisSeleciconado = "";

    static void Reproducir()
    {
        audio = new AudioFileReader("ded.mp3");
        salidaAudio = new WaveOutEvent();

        salidaAudio.Init(audio);

        salidaAudio.PlaybackStopped += (s, e) =>
        {
            audio.Position = 0;
            salidaAudio.Play();
        };

        salidaAudio.Play();
    }
    static void Main()
    {
        Application.Init();
        var top = Application.Top;

        var colore = new ColorScheme()
        {
            Normal =  Application.Driver.MakeAttribute(Color.White, Color.Black),
        };

        var VentanaPrincipal = new Window("Registro")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme =  colore,
        };
        top.Add(VentanaPrincipal);


        etiqNombre = new Label("Nombre: ")//Se agrega texto
        {
            X = 2,//Pos.Center() lo deja centrado :OOOOOO
            Y = 2,
        };
        etiqPais = new Label("País: ")//Se agrega texto
        {
            X = 2,//Pos.Center() lo deja centrado :OOOOOO
            Y = 4,
        };
        var botonMusica = new Button("▶ MUSICA")
        {
            X = 140,
            Y = 35
        };
        VentanaPrincipal.Add(botonMusica);
        botonMusica.Clicked += () =>
        {
            if (!reproduciendo)
            {
                Reproducir();
                reproduciendo = true;
                muteado = false;
            }
            else
            {
                muteado = !muteado;
                salidaAudio.Volume = muteado ? 0f : 1f;
            }
        };
        var SelectedButton = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.Black, Color.White),
        };

        var NormaleButton = new ColorScheme()
        {
            Normal =  Application.Driver.MakeAttribute(Color.White, Color.Black),
        };

        //botón nueva partida
        var marco = new FrameView("")
        {
            X = Pos.Center() - 14,
            Y = Pos.Center(),
            Width = 26,
            Height = 5,
            ColorScheme =  NormaleButton,      
        };
        var botonNuevaPartida = new Button("Nueva Partida")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        VentanaPrincipal.Add(marco);
        VentanaPrincipal.Add(botonNuevaPartida);
        //botón cargar partida
        var marco2 = new FrameView("")
        {
            X = Pos.X(marco),
            Y = Pos.Bottom(marco),
            Width = 26,
            Height = 5,
            ColorScheme = NormaleButton,

        };
        var botonCargarPartida = new Button("Cargar Partida")
        {
            X = Pos.Center(),
            Y = 1
        };
        VentanaPrincipal.Add(marco2);
        marco2.Add(botonCargarPartida);

        //botón configuración
        var marcoconfig = new FrameView("")
        {
            X = Pos.X(marco2),
            Y = Pos.Bottom(marco2),
            Width = 26,
            Height = 5
        };
        var botonConfiguracion = new Button("Configuración")
        {
            X = Pos.Center(),
            Y = 1
        };
        VentanaPrincipal.Add(marcoconfig);
        marcoconfig.Add(botonConfiguracion);

        //botón salir
        var marcosalir = new FrameView("")
        {
            X = Pos.X(marcoconfig),
            Y = Pos.Bottom(marcoconfig),
            Width = 26,
            Height = 5
        };
        var botonsalir = new Button("Salir")
        {
            X = 8,
            Y = 1
        };
        VentanaPrincipal.Add(marcosalir);
        marcosalir.Add(botonsalir);

        botonNuevaPartida.Enter += (_) =>
        {
            marco.ColorScheme = SelectedButton;
        };
        botonCargarPartida.Enter += (_) =>
        {
            marco.ColorScheme = SelectedButton;
        };


        // Cuando pierde foco
        botonCargarPartida.Leave += (_) =>
        {
            marco.ColorScheme = NormaleButton;
        };
        botonNuevaPartida.Leave += (_) =>
        {
            marco.ColorScheme = NormaleButton;
        };
        botonNuevaPartida.Clicked += () => CreacionPersonaje(top);
        botonCargarPartida.Clicked += () => CargarPartida(top);
        botonsalir.Clicked += () => Application.RequestStop();
        Application.Run();//Corre la ventana
    }


    static void CargarPartida(Toplevel top)
    {
        var VentanaCargarPartida = new Window("")
        {
            X=0,
            Y=0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        var par1 = new Label("Slot 1")
        {
            X =5,
            Y=9
        };
        VentanaCargarPartida.Add(par1);
        var Slot1 = new FrameView("")
        {
            X=3,
            Y=10,
            Width = 20,
            Height = 10,
        };

        var par2 = new Label("Slot 2")
        {
            X = 30,
            Y = 9
        };
        VentanaCargarPartida.Add(par2);
        var Slot2 = new FrameView("")
        {
            X = 28,
            Y = 10,
            Width = 20,
            Height = 10,
        };

        var par3 = new Label("Slot 3")
        {
            X = 55,
            Y = 9
        };
        VentanaCargarPartida.Add(par3);
        var Slot3 = new FrameView("")
        {
            X = 53,
            Y = 10,
            Width = 20,
            Height = 10,
        };

        VentanaCargarPartida.Add(Slot1, Slot2, Slot3);
        top.Add(VentanaCargarPartida);
    }
    static void CreacionPersonaje(Toplevel top)
    {

        var VentanaCreacionPersonaje = new Window("Añadir")//Se agrega la ventana
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        var etiquetaNombre = new Label("Nombre")//Se agrega texto
        {
            X = 2,
            Y = 2,
        };

        var casillaNombre = new TextField("")
        {
            X = Pos.Right(etiquetaNombre) + 1,
            Y = etiquetaNombre.Y,
            Width = 30
        };
        var etiquetaPais = new Label("País")//Se agrega texto
        {
            X = 2,
            Y = 4,
        };

        var ListaPaises = new ListView(Paises)
        {
            X = Pos.Right(etiquetaPais) + 4,
            Y = 4,
            Width = 30,
            Height = 6
        };

        var Skill_Txt = new Label("Designar Skills (Máximo 30):")
        {
            X = 2,
            Y = 11
        };
        VentanaCreacionPersonaje.Add(Skill_Txt);

        var skillCarisma = new Label("Carisma")
        {
            X = 2,
            Y = 12
        };
        var Skills_in1 = new TextField("")
        {
            X = 2,
            Y = 13,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in1, skillCarisma);

        var skillEconomia = new Label("Economia")
        {
            X = 2,
            Y = 14
        };
        var Skills_in2 = new TextField("")
        {
            X = 2,
            Y = 15,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in2, skillEconomia);

        var skillFiscalidades = new Label("Fiscalidades")
        {
            X = 2,
            Y = 16,
        };
        var Skills_in3 = new TextField("")
        {
            X = 2,
            Y = 17,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in3, skillFiscalidades);

        var skillCorrupcion = new Label("Corrupción")
        {
            X= 2,  
            Y = 18,
        };
        var Skills_in4 = new TextField("")
        {
            X = 2,
            Y = 19,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in4, skillCorrupcion);
        PaisSeleciconado = "Nicaragua";
        ListaPaises.SelectedItemChanged += (args) =>
        {
            PaisSeleciconado = Paises[args.Item];
        };


        var botonAceptar = new Button("Aceptar")
        {
            X = Pos.Center(),
            Y = 20
        };
        bool Skills = false, nombre = false, pais = false;
        VentanaCreacionPersonaje.Add(botonAceptar);
        botonAceptar.Clicked += () =>
        {
            int numero1 = 0, numero2= 0, numero3 = 0, numero4 = 0;
            int suma;
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
            //Comprobar skills
            if (int.TryParse(Skills_in1.Text.ToString(), out numero1) && int.TryParse(Skills_in2.Text.ToString(), out numero2)
            && int.TryParse(Skills_in3.Text.ToString(), out numero3) && int.TryParse(Skills_in4.Text.ToString(), out numero4))
            {
                suma = numero1 + numero2 + numero3 + numero4;
                if (suma <= 30)
                {
                    Skills = true;
                }
                else
                {
                    MessageBox.Query(
                        "ERROR",
                        "Solo tienes 30 puntos para distribuir entre todas las skills",
                        "Introduce nuevos datos");
                }
            }
            else
            {
                MessageBox.Query(
                    "ERROR",
                    "Solo puedes ingresar numeros enteros en los stats",
                    
                    
                    "Introduce nuevos datos");
            }
            //Comprobar pais
            //Comprobacion final
            if (nombre && Skills)
            {

                MessageBox.Query(
                    "Añadido",
                    "Introducido: " + casillaNombre.Text + //Muestra un aviso, un mensaje
                    " - " + PaisSeleciconado, "Aceptar");//El programa informa que se ha introducido cierto nombre y cierta dirección       
                etiqNombre.Text = "Nombre: " + casillaNombre.Text;
                etiqPais.Text = "País: " + PaisSeleciconado;
                top.Remove(VentanaCreacionPersonaje);//Cuando se pulsa el botón desaparece la ventana  
            }
        };
        //() son funciones anónimas, todavía no se han creado funciones aparte



        VentanaCreacionPersonaje.Add(etiquetaNombre, casillaNombre, etiquetaPais, ListaPaises);
        top.Add(VentanaCreacionPersonaje);//Se agrega la ventana a la raíz
    }

    static void GuardarPartida()
    {
        string rutaArchivo = "partida.txt";
    }

    public void debbuger()
    {

    }

}