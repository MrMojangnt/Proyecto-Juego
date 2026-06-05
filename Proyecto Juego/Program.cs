using Empresas;
using NAudio.Wave;
using Proyecto_Juego;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using Terminal.Gui;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Terminal.Gui.Graphs.PathAnnotation;


class Program
{
    //Jugador
    static Proyecto_Juego.Players pd = new Players();
    //Jugador
    static FrameView[] Slots = new FrameView[3];
    static Label[] Deletezzz = new Label[3];
    static Button[] Borration = new Button[3];
    static bool[] saves = new bool[3];
    static int colora = 0;
    //Audio Música
    static WaveOutEvent salidaAudio;
    static AudioFileReader audio;
    static bool reproduciendo = false;
    static bool muteado = false;
    //Audio Click
    static WaveOutEvent salidaclick;
    static AudioFileReader click;
    //Ventana Principal
    static Window VentanaPrincipal;
    static string puntosmejorastats = "0";
    //guardado partidas
    static string[] save_compania = { "empresas1.csv", "empresas2.csv", "empresas3.csv" };
    static string[] partidas = { "save1.txt", "save2.txt", "save3.txt" };
    static List<Companias> Companiass = new List<Companias>();
    public static List<Acciones> AccionesListActuales = new List<Acciones>();
    public static List<string> Paises = new List<string>() { "Nicaragua (predeterminado)", "EE.UU.", "Japón", "China", "Alemania", "España" };
    public static List<Acciones> Accioneshh = new List<Acciones>();
    public static string[] inventario = {"Inventario1.csv", "Inventario2.csv", "Inventario3.csv"};
    public static int InvInt = 0;
    static List<FrameView> marcos = new List<FrameView>();
    static List<ColorScheme> colores = new List<ColorScheme>() {
        new ColorScheme()
        {
            //Tema predeterminado
            // Controles normales
            Normal = Application.Driver.MakeAttribute(
                Color.White,
                Color.Blue),

            // Control seleccionado
            Focus = Application.Driver.MakeAttribute(
                Color.Black,
                Color.Gray),

            // Hotkeys sin foco
            HotNormal = Application.Driver.MakeAttribute(
                Color.BrightYellow,
                Color.Blue),

            // Hotkeys con foco
            HotFocus = Application.Driver.MakeAttribute(
                Color.BrightYellow,
                Color.Gray)
        },

        // Tema oscuro
        new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(
                Color.White,
                Color.Black),

            Focus = Application.Driver.MakeAttribute(
                Color.Black,
                Color.BrightGreen),

            HotNormal = Application.Driver.MakeAttribute(
                Color.BrightCyan,
                Color.Black),

            HotFocus = Application.Driver.MakeAttribute(
                Color.White,
                Color.BrightGreen)
        },

        // Tema blanco
        new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(
                Color.Black,
                Color.White),

            Focus = Application.Driver.MakeAttribute(
                Color.White,
                Color.BrightBlue),

            HotNormal = Application.Driver.MakeAttribute(
                Color.Red,
                Color.White),

            HotFocus = Application.Driver.MakeAttribute(
                Color.BrightYellow,
                Color.BrightBlue)
        }
    };
    static List<ColorScheme> ColoreButtonSelected = new List<ColorScheme>() {
        // Tema azul
        new ColorScheme()
        {
            // Fondo blanco con texto azul
            Normal = Application.Driver.MakeAttribute(
                Color.Blue,
                Color.White),
        },

        // Tema oscuro
        new ColorScheme()
        {
            // Verde brillante estilo terminal retro
            Normal = Application.Driver.MakeAttribute(
                Color.BrightGreen,
                Color.Black)
        },

        // Tema blanco
        new ColorScheme()
        {
            // Azul brillante sobre fondo blanco
            Normal = Application.Driver.MakeAttribute(
                Color.BrightBlue,
                Color.White)
        }
    };
    static List<string> colorestxt = new List<string>() {"Predeterminado", "Oscuro", "Blanco"};
    //probando las empresas
    
    

    static string PaisSeleciconado = "";
    static void ClickSound()
    {
        salidaclick = new WaveOutEvent();
        click = new AudioFileReader("click.mp3");
        salidaclick.Init(click);
        salidaclick.Play();
    }
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
        

      
        VentanaPrincipal = new Window("Menu")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora],
        };
        top.Add(VentanaPrincipal);
        /*
        
        var fondo = new Label(@"░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
")
        {
            X =  0,
            Y = 26,
            ColorScheme = new ColorScheme()
            {
                Normal = Application.Driver.MakeAttribute(
            Color.BrightGreen,
            Color.Black)
            }
        };
        /*        ████
        ████                        ██████
        ████                        ██████
        ████         ███████        ██████
        ████         ███████        ██████
        ████         ███████        ██████
        ████         ███████        ██████

    ██████████   █████████████   ██████████
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ██████████   █████████████   ██████████*/
       /* VentanaPrincipal.Add(fondo);
        var fondo1 = new Label(@"                                                                    ☼

               ████            ████████            ████
               █  █            █      █            █  █
      ██████   ████    ████    ████████    ████    ████
      █    █   █  █    █  █    █      █    █  █    █  █
███████████████████████████████████████████████████████████████████
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        {
            X = 0,
            Y = 22
        };
        VentanaPrincipal.Add(fondo1);
        var fondo2 = new Label(@"        ████
        ████                        ██████
        ████                        ██████
        ████         ███████        ██████
        ████         ███████        ██████
        ████         ███████        ██████
        ████         ███████        ██████

    ██████████   █████████████   ██████████
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ███ □ □ ██   ██ □ □ □ □ ██   ██ □ □ ██
    ██████████   █████████████   ██████████")
        {
            X = 100,
            Y = 15,
        };
        VentanaPrincipal.Add(fondo2);*/
        //botón nueva partida
        var label = new Label(@"╔══════════════════════════════════════════════════════════════════════╗
║                                                                      ║
║   ███████╗ ██████╗ ██████╗ ███╗   ██╗ ██████╗ ███╗   ███╗██╗ ██████╗ ║
║   ██╔════╝██╔════╝██╔═══██╗████╗  ██║██╔═══██╗████╗ ████║██║██╔════╝ ║
║   █████╗  ██║     ██║   ██║██╔██╗ ██║██║   ██║██╔████╔██║██║██║      ║
║   ██╔══╝  ██║     ██║   ██║██║╚██╗██║██║   ██║██║╚██╔╝██║██║██║      ║
║   ███████╗╚██████╗╚██████╔╝██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██║╚██████╗ ║
║   ╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═╝ ╚═════╝ ║
║                                                                      ║
║          &   L I B E R T Y                                           ║
║                                                                      ║
║      ╭──────────────────────────────────────────────╮                ║
║      │   ▲ NASDAQ +4.2%     GOLD ▲     OIL ▼        │                ║
║      │                                              │                ║
║      │      ╱╲       ╱╲                             │                ║
║      │  ╱╲ ╱  ╲  ╱╲ ╱  ╲      $$$$$$$$              │                ║
║      │ ╱  V    ╲╱  V    ╲        $$$                │                ║
║      │───────────────────────────────► TIME         │                ║
║      ╰──────────────────────────────────────────────╯                ║
║                                                                      ║
║         “Control the Market. Influence the Nation.”                  ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        VentanaPrincipal.Add(label);
        var marco = new FrameView("")
        {
            X = 10,
            Y = Pos.Center(),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]
        };
        var botonNuevaPartida = new Button("_Nueva Partida")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        marco.Add(botonNuevaPartida);
        VentanaPrincipal.Add(marco);
        //botón cargar partida
        var marco2 = new FrameView("")
        {
            X = Pos.X(marco),
            Y = Pos.Bottom(marco),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]

        };
        var botonCargarPartida = new Button("_Cargar Partida")
        {
            X = Pos.Center(),
            Y = 1
        };
        VentanaPrincipal.Add(marco2);
        marco2.Add(botonCargarPartida);

        //botón configuración
        var marcoconfig = new FrameView("")
        {
            X = 120,
            Y = Pos.Center(),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]
        };
        var botonConfiguracion = new Button("_Configuración")
        {
            X = Pos.Center(),
            Y = 1,
            
        };
        VentanaPrincipal.Add(marcoconfig);
        marcoconfig.Add(botonConfiguracion);


        //botón salir
        var marcosalir = new FrameView("")
        {
            X = Pos.X(marcoconfig),
            Y = Pos.Bottom(marcoconfig),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]
        };
        var botonsalir = new Button("_Salir")
        {
            X = 8,
            Y = 1,
            
        };
        VentanaPrincipal.Add(marcosalir);
        marcosalir.Add(botonsalir);

        var marcomusica = new FrameView("")
        {
            X = 130,
            Y = 33,
            Width = 10,
            Height = 4,
        };
        var botonMusica = new Button("▶ MUSICA")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        VentanaPrincipal.Add(marcomusica);
        marcomusica.Add(botonMusica);
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


        //Agregar marcos a la list
        marcos.Add(marco);
        marcos.Add(marco2);
        marcos.Add(marcoconfig);
        marcos.Add(marcosalir);

        botonNuevaPartida.Enter += (_) =>
        {
            marco.ColorScheme = ColoreButtonSelected[colora];
            if (OperatingSystem.IsWindows())
            {
                ClickSound();
            }
        };
        botonCargarPartida.Enter += (_) =>
        {
            marco2.ColorScheme = ColoreButtonSelected[colora];
            if (OperatingSystem.IsWindows())
            {
                ClickSound();
            }
        };
        botonConfiguracion.Enter += (_) => 
        {
            marcoconfig.ColorScheme = ColoreButtonSelected[colora];
            if (OperatingSystem.IsWindows())
            {
                ClickSound();
            }
        };
        botonsalir.Enter += (_) =>
        {
            marcosalir.ColorScheme = ColoreButtonSelected[colora];
            ClickSound();
        };
        botonMusica.Enter += (_) =>
        {
            marcomusica.ColorScheme = ColoreButtonSelected[colora];
            if (OperatingSystem.IsWindows())
            {
                ClickSound();
            }
        };

        // Cuando pierde foco
        botonCargarPartida.Leave += (_) =>
        {
            marco2.ColorScheme = colores[colora];
        };
        botonNuevaPartida.Leave += (_) =>
        {
            marco.ColorScheme = colores[colora];
        };
        botonConfiguracion.Leave += (_) =>
        {
            marcoconfig.ColorScheme = colores[colora];
        };
        botonsalir.Leave += (_) =>
        {
            marcosalir.ColorScheme = colores[colora];
        };
        botonMusica.Leave += (_) =>
        {
            marcomusica.ColorScheme = colores[colora];
        };
        botonNuevaPartida.Clicked += () =>
        {
            top.Remove(VentanaPrincipal);
            marcos.Clear();
            CreacionPersonaje(top);
        };
        botonCargarPartida.Clicked += () =>
        {

            top.Remove(VentanaPrincipal);
            marcos.Clear();
            CargarPartida(top);

        };
        botonConfiguracion.Clicked += () =>
        {
            top.Remove(VentanaPrincipal);
            marcos.Clear();
            Configuracion(top);
        };
        botonsalir.Clicked += () => Application.RequestStop();

        botonNuevaPartida.SetFocus();
        Application.Run();//Corre la ventana
    }

    static void VerificarSave()
    {
        for(int i = 0; i < saves.Length; i++)
        {
            saves[i] = File.Exists(partidas[i]);
        }

    }

    static void CargarPartida(Toplevel top)
    {
        var VentanaCargarPartida = new Window("")
        {
            X=0,
            Y=0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };

        var par1 = new Label("Slot 1")
        {
            X =5,
            Y=9
        };
        VentanaCargarPartida.Add(par1);
        Slots[0] = new FrameView("")
        {
            X =3,
            Y=10,
            Width = 20,
            Height = 10,
        };
        var bottonslot1 = new Button("Cargar slot 1")
        {
            X = Pos.X(par1),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot1);
        var par2 = new Label("Slot 2")
        {
            X = 30,
            Y = 9
        };
        VentanaCargarPartida.Add(par2);
        Slots[1] = new FrameView("")
        {
            X = 28,
            Y = 10,
            Width = 20,
            Height = 10,
        };
        var bottonslot2 = new Button("Cargar slot 2")
        {
            X = Pos.X(par2),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot2);
        var par3 = new Label("Slot 3")
        {
            X = 55,
            Y = 9
        };
        VentanaCargarPartida.Add(par3);
        Slots[2] = new FrameView("")
        {
            X = 53,
            Y = 10,
            Width = 20,
            Height = 10,
        };
        var bottonslot3 = new Button("Cargar slot 3")
        {
            X = Pos.X(par3),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot3);
        if (File.Exists(partidas[0]) && File.Exists(save_compania[0]))
        {
            bottonslot1.Clicked += () =>
            {
                using (StreamReader save = new StreamReader(partidas[0]))
                {
                    string nombre = save.ReadLine();
                    nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                    string pais = save.ReadLine();
                    pais = pais.Replace("Pais: ", "");
                    int carismas = int.Parse(save.ReadLine().Replace("Carisma: ", ""));
                    int economia = int.Parse(save.ReadLine().Replace("Economia: ", ""));
                    int fiscalidad = int.Parse(save.ReadLine().Replace("Fiscalidad: ", ""));
                    int corrupcion = int.Parse(save.ReadLine().Replace("Corrupcion: ", ""));
                    decimal balance = decimal.Parse(save.ReadLine().Replace("Balance: ", ""));
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    pd.balance = balance;
                    InvInt = 0;

                }

                Companiass = CargarEmpresa(0);

                top.Remove(VentanaCargarPartida);
                Inicio(top);
            };
        }    
        else
        {
            bottonslot1.Clicked += () =>
            {
                MessageBox.Query(
                    "Error",
                    "No tienes partida guardada",
                    "Aceptar");
            };
        }

        if (File.Exists(partidas[1]) && File.Exists(save_compania[1]))
        {
            bottonslot2.Clicked += () =>
            {
                using (StreamReader save = new StreamReader(partidas[1]))
                {
                    string nombre = save.ReadLine();
                    nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                    string pais = save.ReadLine();
                    pais = pais.Replace("Pais: ", "");
                    int carismas = int.Parse(save.ReadLine().Replace("Carisma: ", ""));
                    int economia = int.Parse(save.ReadLine().Replace("Economia: ", ""));
                    int fiscalidad = int.Parse(save.ReadLine().Replace("Fiscalidad: ", ""));
                    int corrupcion = int.Parse(save.ReadLine().Replace("Corrupcion: ", ""));
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    InvInt = 1;

                }
                Companiass = CargarEmpresa(1);

                top.Remove(VentanaCargarPartida);
                Inicio(top);
            };
        }      
        else
        {
            bottonslot2.Clicked += () =>
            {
                MessageBox.Query(
                    "Error",
                    "No tienes partida guardada",
                    "Aceptar");
            };
        }

        if (File.Exists(partidas[2]) && File.Exists(save_compania[2]))
        {
            bottonslot3.Clicked += () =>
            {
                using (StreamReader save = new StreamReader(partidas[2]))
                {
                    string nombre = save.ReadLine();
                    nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                    string pais = save.ReadLine();
                    pais = pais.Replace("Pais: ", "");
                    int carismas = int.Parse(save.ReadLine().Replace("Carisma: ", ""));
                    int economia = int.Parse(save.ReadLine().Replace("Economia: ", ""));
                    int fiscalidad = int.Parse(save.ReadLine().Replace("Fiscalidad: ", ""));
                    int corrupcion = int.Parse(save.ReadLine().Replace("Corrupcion: ", ""));
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    InvInt = 2;
                }
                Companiass =CargarEmpresa(2);

                top.Remove(VentanaCargarPartida);
                Inicio(top);
            };
        }
        else
        {
            bottonslot3.Clicked += () =>
            {
                MessageBox.Query(
                    "Error",
                    "No tienes partida guardada",
                    "Aceptar");
            };
        }

        var Back = new Button("Volver al Menú")
        {
            X = Pos.Center() + 5,
            Y= 30
        };


        Back.Clicked += () =>
        {
            top.Remove(VentanaCargarPartida);
            top.Add(VentanaPrincipal);
        };

        VentanaCargarPartida.Add(Slots[0], Slots[1],Slots[2],Back);
        //Agregando el icono para eliminar. 
        VerificarSave();
        for (int i = 0; i < Deletezzz.Length; i++)
        {
            Borration[i] = new Button("")
            {
                X = 2,
                Y = 3,
                Width = 1,
                Height = 1,
            };

            Deletezzz[i] = new Label(@"██    ██
 ██  ██
  ████
   ██
  ████
 ██  ██
██    ██")
            {
                X = Pos.X(Slots[i]) + 5,
                Y = Pos.Bottom(bottonslot1) + 2,
            };

            if (saves[i])//En caso que exista partida el botón de X existirá
            {
                VentanaCargarPartida.Add(Deletezzz[i]);
                Deletezzz[i].Add(Borration[i]);
            }

        }
        Borration[0].Clicked += () =>
        {
            EliminarPartida(0);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        Borration[1].Clicked += () =>
        {
            EliminarPartida(1);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        Borration[2].Clicked += () =>
        {
            EliminarPartida(2);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        //Verificando que haya partidas guardadas en cada slot
        for (int i = 0; i < saves.Length; i++)
        {
            if (saves[i])
            {
                StreamReader save = new StreamReader(partidas[i], Encoding.UTF8);
                string nombre = save.ReadLine();
                nombre = nombre.Replace("Nombre: ", "");//reemplaza "Nombre" por ""
                string pais = save.ReadLine();
                pais = pais.Replace("Pais: ", "");
                int carismas = int.Parse(save.ReadLine().Replace("Carisma: ", ""));
                int economia = int.Parse(save.ReadLine().Replace("Economia: ", ""));
                int fiscalidad = int.Parse(save.ReadLine().Replace("Fiscalidad: ", ""));
                int corrupcion = int.Parse(save.ReadLine().Replace("Corrupcion: ", ""));
                save.Close();

                Slots[i].Add(new Label("Nombre:\n" + nombre)
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                });
            }
            else
            {

                Slots[i].Add(new Label("No hay \ndatos guardados")
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                });
            }
        }


        top.Add(VentanaCargarPartida);
    } 

    
    static void Configuracion(Toplevel top)
    {
        var ventanaconfiguracion = new Window("Configuracion")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };
        var TemasLabel = new Label("Temas")
        {
            X = 2,
            Y = 1
        };
        var ListaTEMAS = new ListView(colorestxt)
        {
            X = Pos.Right(TemasLabel) + 4,
            Y = 2,
            Width = 30,
            Height = 6
        };
        ListaTEMAS.SelectedItemChanged += (args) =>
        {
            colora = args.Item;
            PaisSeleciconado = colorestxt[args.Item];
        };
        var aceptar = new Button("Aceptar")
        {
            Y= 9,
            X = Pos.Right(ListaTEMAS),
        };
        aceptar.Clicked += () =>
        {
            VentanaPrincipal.ColorScheme = colores[colora];
            foreach (var m in marcos)
            {
                m.ColorScheme = colores[colora];
            }
            MessageBox.Query(
                "Guardado",
                "Se guardo la configuracion", //Muestra un aviso, un mensaje
    "Aceptar");//El programa informa que se ha introducido cierto nombre y cierta dirección       
            top.Remove(ventanaconfiguracion);//Cuando se pulsa el botón desaparece la ventana 
            top.Add(VentanaPrincipal);
        };

        ventanaconfiguracion.Add(aceptar);
        ventanaconfiguracion.Add(ListaTEMAS);
        ventanaconfiguracion.Add(TemasLabel);
        top.Add(ventanaconfiguracion);
    }
    static void CreacionPersonaje(Toplevel top)
    {

        var VentanaCreacionPersonaje = new Window("Añadir")//Se agrega la ventana
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme =  colores[colora]
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
        bool guardado = false;
        bool Skills = false, nombre = false;
        VentanaCreacionPersonaje.Add(botonAceptar);
        botonAceptar.Clicked += () =>
        {
            int numero1 = 0, numero2 = 0, numero3 = 0, numero4 = 0;
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
                    "Solo puedes ingresar numeros enteros en las skills",


                    "Introduce nuevos datos");
            }
            //Comprobar pais
            //Comprobacion final
            if (nombre && Skills)
            {
                pd.name = casillaNombre.Text.ToString();
                pd.pais = PaisSeleciconado;
                pd.carisma = numero1;
                pd.economia = numero2;
                pd.fiscalidad = numero3;
                pd.corrupcion = numero4;
                pd.balance = 50000;
                guardado = GuardarPartida();

                if (guardado)
                {    
                    top.Remove(VentanaCreacionPersonaje);//Cuando se pulsa el botón desaparece la ventana  
                    Inicio(top);
                }
            }
            ;
            //() son funciones anónimas, todavía no se han creado funciones aparte
        };
        var SalirS = new Button("Salir sin guardar")
        {
            X = Pos.X(botonAceptar)+ 20,
            Y = Pos.Y(botonAceptar),
        };

        SalirS.Clicked += () =>
        {
            top.Remove(VentanaCreacionPersonaje);
            top.Add(VentanaPrincipal);
        };
        VentanaCreacionPersonaje.Add(SalirS);
        VentanaCreacionPersonaje.Add(etiquetaNombre, casillaNombre, etiquetaPais, ListaPaises);
        casillaNombre.SetFocus();
        top.Add(VentanaCreacionPersonaje);//Se agrega la ventana a la raíz
    }
    
    //intentando hacer un sistema de guardado de partidas
    //función para guardar partida
    static bool GuardarPartida()
    {
        bool guardado = false;
        var top = Application.Top;
        VerificarSave();

        
        for (int i = 0; i < saves.Length; i++)
        {
            if (!saves[i])
            {
                using (StreamWriter save = new StreamWriter(partidas[i], false, Encoding.UTF8))
                {
                    save.WriteLine(pd.ToString());
                }

                using (StreamWriter save = new StreamWriter(inventario[i]))
                {
                    save.WriteLine(pd.name);
                    save.WriteLine($"ID,Nombre,Costo_Compra,CostoActual,TipoAccion,Cantidad");
                }
                
                Guardarempresa(i, true);
                Companiass = CargarEmpresa(i);

                guardado = true;
                break;
            }
        }

        if (!guardado)
        {
            int fullSlots = MessageBox.Query("Slots llenos",
                "Ha superado el límite de partidas guardadas, ¿quiere sobreescribir alguna?",
                "Sí", "No");
            
            if (fullSlots == 0)
            {
                SobreescribirPartida(top);
            }
        }
        return guardado;
    }
    static void ActualizarVentana(Window ventana, Action funcion, Toplevel top)
    {
        top.Remove(ventana);
        funcion();
    }
    static string LeerNombre(string dato, int i)
    {
        using (StreamReader save = new StreamReader(partidas[i], Encoding.UTF8))
        {
            string linea = save.ReadLine();
            linea = linea.Replace("Nombre:", "");//reemplaza "Nombre" por ""
            return linea;
        }

    }

    static void SobreescribirPartida(Toplevel top)
    {
        
        Button[] SobreSlot = new Button[3];
        var Sobreescribir = new Dialog(
    "Sobreescribir partida",
    60,
    20
);
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            string linea = LeerNombre("Nombre", index);
            SobreSlot[index] = new Button($"Nombre: {linea}")
            {
                X = 2,
                Y = i + 2
            };

            SobreSlot[index].Clicked += () =>
            {
                using (StreamWriter save = new StreamWriter(partidas[index]))
                {
                    save.WriteLine(pd.ToString());
                }
                using (StreamWriter save = new StreamWriter(inventario[index]))
                {
                    save.WriteLine(pd.name);
                }
                Guardarempresa(index, false);
                Companiass = CargarEmpresa(index);

                Application.RequestStop();
                top.RemoveAll();
                Inicio(top);
            };

            Sobreescribir.Add(SobreSlot[index]);

        }

        var cancelar = new Button("Cancelar")
        {
            X = 20,
            Y = 6
        };
        cancelar.Clicked += () =>
        {
            Application.RequestStop();
            top.RemoveAll();
            top.Add(VentanaPrincipal);
        };
        Sobreescribir.Add(cancelar);
        Application.Run(Sobreescribir);
    }
    static void Guardarempresa(int i, bool zzz)
    {

        using (StreamWriter save_empresas = new StreamWriter(save_compania[i], zzz, Encoding.UTF8))
        {
            save_empresas.WriteLine("IdEmpresa; Empresa; Pais; Sector; Capital Bursátil; Accionistas; Productos; Ganancias; Gastos Marketing;Gastos Investigación; Gastos Mantenimiento; Participacion; Balance");
            for (int p = 0; p < Indices.EmpresasGuardadas.Count; p++)
            {
                save_empresas.WriteLine(Indices.EmpresasGuardadas[p]);

            }


        }
    }
    static List<Companias> CargarEmpresa(int indice)
    {
        List<Companias> Comp = new List<Companias>();
        Proyecto_Juego.Companias compitas = new Companias(); //structttttttttttt
        char[] delimitadores = { ';', '\n', '|', '\r' };
        using (StreamReader savecompani = new StreamReader(save_compania[indice], Encoding.UTF8))
        {
            string[] encabezados = savecompani.ReadLine().Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            compitas.productos = new string[10];
            while (!savecompani.EndOfStream)
            {
                string[] lineas = savecompani.ReadLine().Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
                compitas.id = int.Parse(lineas[0]);
                compitas.name = lineas[1];
                lineas[2] = lineas[2].Replace("(predeterminado)", ""); //reemplaza "M" por ""
                compitas.pais = lineas[2];
                compitas.rubro = lineas[3];
                lineas[4] = lineas[4].Replace("M", ""); //reemplaza "M" por ""
                compitas.capbursatil = decimal.Parse(lineas[4]);
                compitas.accionistas = int.Parse(lineas[5]);
                int p = 6;
                for (int i = 0; i < compitas.productos.Length; i++)
                {
                    compitas.productos[i] = lineas[p];
                    p++;
                }
                lineas[16] = lineas[16].Replace("M", ""); //reemplaza "M" por ""
                compitas.GananciasTrimestrales = decimal.Parse(lineas[16]);
                lineas[17] = lineas[17].Replace("M", ""); //reemplaza "M" por ""
                compitas.marketing = decimal.Parse(lineas[17]);
                lineas[18] = lineas[18].Replace("M", ""); //reemplaza "M" por ""
                compitas.investigacion = decimal.Parse(lineas[18]);
                lineas[19] = lineas[19].Replace("M", ""); //reemplaza "M" por ""
                compitas.mantenimiento = decimal.Parse(lineas[19]);
                lineas[20] = lineas[20].Replace("%", ""); //reemplaza "%" por ""
                compitas.participacion = decimal.Parse(lineas[20]);
                lineas[21] = lineas[21].Replace("M", ""); //reemplaza "M" por ""
                compitas.balance = decimal.Parse(lineas[21]);

                Comp.Add(compitas);
            }

        }
        return Comp;
    }

    static void EliminarPartida(int i)
    {
        VerificarSave();
       
            int Eliminar = MessageBox.Query("Eliminar",
            "¿Está seguro que desea eliminar esta partida?",
            "Sí", "No");
            if(Eliminar== 0)
            {
            MessageBox.Query("Eliminar",
                "Partida eliminada con éxito",
                "Aceptar");

                File.Delete(partidas[i]);
                File.Delete(inventario[i]);
                File.Delete(save_compania[i]);
            }

    }

    static void Inicio(Toplevel top)
    {
        var VentanaInicio = new Window("Inicio")
        {
            X=0,
            Y=0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };
        top.Add(VentanaInicio);//Inicializador
        var LabelUsuario = new Label($"Usuario: {pd.name}")
        {
            X = 2,
            Y = 1
        };
        var LabelPais = new Label($"Pais: {pd.pais}")
        {
            X = Pos.X(LabelUsuario),
            Y = 2
        };
        //Stats del jugador
        var FrameStats = new FrameView()
        {
            X= 130,
            Y = 2,
            Width = 28,
            Height = 8,
        };
        var labelStats = new Label($"Stats, puntos: {puntosmejorastats}")
        {
            X = Pos.Center(),
            Y = 0
            
        };
        var labelCarisma = new Label($"Carisma: {pd.carisma}")
        {
            X = Pos.Center(),
            Y = 2
        };
        var btCarismatic = new Button("+")
        {
            X = Pos.Right(labelCarisma)+1,
            Y = Pos.Y(labelCarisma),
        };
        var labeleconomia = new Label($"Economia: {pd.economia}")
        {
            X = Pos.Center(),
            Y = 3
        };
        var btEconomia = new Button("+")
        {
            X = Pos.Right(labeleconomia)+1,
            Y = Pos.Y(labeleconomia),
        };
        var labelfiscalidad = new Label($"Fiscalidad: {pd.fiscalidad}")
        {
            X = Pos.Center(),
            Y = 4
        };
        var btfiscalidad = new Button("+")
        {
            X = Pos.Right(labelfiscalidad)+1,
            Y = Pos.Y(labelfiscalidad),
        };
        var labelcorrupcion = new Label($"Corrupcion: {pd.corrupcion}")
        {
            X = Pos.Center(),
            Y = 5
        };
        var btCorrupcion = new Button("+")
        {
            X = Pos.Right(labelcorrupcion)+1,
            Y = Pos.Y(labelcorrupcion),
        };
        //botones bajos
        BotonesDeJuegoPredeterminado(top, VentanaInicio);
        //Contactos
        var FrameContactos = new FrameView()
        {
            X = 1,
            Y = Pos.Center(),
            Width = 20,
            Height = 18,
            ColorScheme = colores[colora]
        };
        var ContactosLabel = new Label("Contactos")
        {
            X = Pos.Center(),
            Y = 0
        };
        //Balance
        var Balance = new Label($"Balance: {pd.balance}")
        {
            X = Pos.Center(),
            Y = 1
        };
        VentanaInicio.Add(LabelUsuario,Balance,FrameContactos,ContactosLabel, LabelPais, FrameStats);
        FrameContactos.Add(ContactosLabel);
        FrameStats.Add(labelCarisma, labelStats, labeleconomia, labelfiscalidad, labelcorrupcion, btCarismatic, btEconomia, btfiscalidad, btCorrupcion);

    }

    public static void BotonesDeJuegoPredeterminado(Toplevel top, Window ventana)
    {
        //botones bajos
        var btMercado = new Button("Mercado")
        {
            X = 12,
            Y = 38
        };
        var btInicio = new Button("Inicio")
        {
            X = 1,
            Y = 38
        };
        var btPortafolio = new Button("Portafolio")
        {
            X=24,
            Y=38
        };
        var btInventario = new Button("Inventario")
        {
            X = 40,
            Y = 38
        };
        var btVerEmpresa = new Button("Ver Empresas")
        {
            X = 58,
            Y = 38
        };
        var btMenu = new Button("Volver al Menu")
        {
            X = 78,
            Y = 38,
        };
        //Funciones
        btInicio.Clicked += () =>
        {
            top.RemoveAll();
            Inicio(top);
        };
        btVerEmpresa.Clicked += () =>
        {
            top.RemoveAll();
            VentanaDeEmpresas(top);
        };
        btInventario.Clicked += () =>
        {
            top.RemoveAll();
            top.Add(Inventario.VentanaInventario(top, InvInt));
        };
        btMenu.Clicked += () =>
        {
            top.RemoveAll();
            top.Add(VentanaPrincipal);
        };
        btInicio.SetFocus();
        ventana.Add(btMercado,btInicio,btPortafolio,btInventario,btVerEmpresa,btMenu);
    }
    
    //creando la ventana de empresas
    static void VentanaDeEmpresas(Toplevel top)
    {
        var VentanaDeEmpresas = new Window()
        {
            X = 0,
            Y= 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        DataTable tabla = new DataTable();

        tabla.Columns.Add("ID");
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Pais");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Capital Bursátil");


        foreach(Companias i in Companiass)
        {
            tabla.Rows.Add(
                i.id,
                i.name,
                i.pais,
                i.rubro,
                i.capbursatil + "M"
            );

        }

        var tableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = 120,
            Height = 30
        };
        tableView.CellActivated += (e) =>
        {
            int row = e.Row;

            var empresa = Companiass[row];
            top.Remove(VentanaDeEmpresas);
            MostrarDetalleEmpresa(top, empresa);
        };
        tableView.Table = tabla;
        Program.BotonesDeJuegoPredeterminado(top, VentanaDeEmpresas);


        VentanaDeEmpresas.Add(tableView);
        top.Add(VentanaDeEmpresas);
      
    }

    static void ComprarAcciones(Toplevel top)
    {
        var Mercado = new Window("Mercado")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        
        BotonesDeJuegoPredeterminado(top, Mercado);
        top.Add(Mercado);
    }
    static void MostrarDetalleEmpresa(Toplevel top, Companias empresa)
    {
        var DetalleEmpresa = new Window("Detalle de Empresa")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        var MasInfo = new Label(
@$" ID: {empresa.id}                  
 Empresa: {empresa.name}           
 País: {empresa.pais}              
 Sector: {empresa.rubro}           
 Capital: {empresa.capbursatil}M   
 Accionistas: {empresa.accionistas}
 balance: {empresa.balance}M       ")
        {
            X = 1,
            Y = 0,

        };
        var chunchito1 = new FrameView("") {
            X =1,
            Y =0,
            Width = 40,
            Height = 10
        };
        DetalleEmpresa.Add(chunchito1);
        chunchito1.Add(MasInfo);

        var Gastos = new Label(
@$" Gastos en Marketing: {empresa.marketing}M
 Gastos en Investigación: {empresa.investigacion}M
 Gastos en Mantenimiento: {empresa.mantenimiento}M
 Participación: {empresa.participacion:f2}%")
        {
            X = Pos.Center(),
            Y = 0,
        };
        var chunchito2 = new FrameView("")
        {
            X = 5,
            Y = 15,
            Width = 40,
            Height = 7,
        };
        DetalleEmpresa.Add(chunchito2);
        chunchito2.Add(Gastos);


        var Productitos = new Label(
$@"         PRODUCTOS             
                                   
 Productos: {empresa.productos[0]} 
 Productos: {empresa.productos[1]} 
 Productos: {empresa.productos[2]} 
 Productos: {empresa.productos[3]} 
 Productos: {empresa.productos[4]} 
 Productos: {empresa.productos[5]} 
 Productos: {empresa.productos[6]} 
 Productos: {empresa.productos[7]} 
 Productos: {empresa.productos[8]} 
 Productos: {empresa.productos[9]}")
        {
            X = Pos.Center(),
            Y=0
        };
        var chunchito3 = new FrameView("")
        {
            X = 50,
            Y = Pos.Center(),
            Width = 50,
            Height = 15,
        };
        DetalleEmpresa.Add(chunchito3);
        chunchito3.Add(Productitos);


        var btVolver = new Button("Volver")
        {
            X = Pos.Center(),
            Y = 30
        };
        var btcomprar_acciones = new Button("Comprar Accion")
        {
            X = Pos.X(btVolver) +4,
            Y = Pos.Y(btVolver)- 2,
        };
        var LabelPrecioAccion = new Label($"Precio: {(empresa.capbursatil*1000000) / 50000000}")
        {
            X = Pos.X(btcomprar_acciones) +4,
            Y = Pos.Y(btcomprar_acciones)- 1,
        };
        DetalleEmpresa.Add(btcomprar_acciones, LabelPrecioAccion);
        btVolver.Clicked += () =>
        {
            top.RemoveAll();
            VentanaDeEmpresas(top);
        };
        btcomprar_acciones.Clicked += () =>
        {
            List<string> lineas = File.ReadAllLines(inventario[InvInt]).ToList();
            decimal precioAccional = (empresa.capbursatil * 1000000) / 50000000;
            if (pd.balance >= precioAccional)
            {
                Acciones NuevaAccion = new Acciones();
                NuevaAccion.id = empresa.id;
                NuevaAccion.name = empresa.name;
                NuevaAccion.CostoActual = precioAccional;
                NuevaAccion.CostoDeCompra = precioAccional;
                NuevaAccion.TipoDeAccion = true;
                NuevaAccion.cantidad += 1;
                bool pader = false; // verificando si existe la acción creo, maldito raul que es pader

                    for (int i = 2; i < lineas.Count; i++)
                    {
                        string[] datos = lineas[i].Split(',');

                        if (datos[0] == empresa.id.ToString())
                        {
                            int cantity = int.Parse(datos[5]);
                            cantity++;
                            datos[5] = cantity.ToString();
                            lineas[i] = string.Join(",", datos);
                            pader = true;
                            break;
                        }
                    }
                
                    if (pader == false)
                    {
                        using (StreamWriter str = new StreamWriter(inventario[InvInt], true, Encoding.UTF8))
                        {
                            str.WriteLine($"{NuevaAccion.id},{NuevaAccion.name},{NuevaAccion.CostoActual}, {NuevaAccion.CostoDeCompra},{NuevaAccion.TipoDeAccion}, {NuevaAccion.cantidad}");
                        }
                    } else if (pader == true)
                    {
                        File.WriteAllLines(inventario[InvInt], lineas);
                    }
                
                pd.balance -= precioAccional;
            }
            else
            {
                MessageBox.Query(
                    "Error",
                    "No tienes Suficiente dinero",
                    "Aceptar");
            }
            
        
    };

        DetalleEmpresa.Add(btVolver);
        top.Add(DetalleEmpresa);
    }
    
    
}