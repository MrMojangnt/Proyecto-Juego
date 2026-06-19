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
    public static Proyecto_Juego.Players pd = new Players();
    //Jugador
    static FrameView[] Slots = new FrameView[3];
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
    public static string[] save_compania = { "empresas1.csv", "empresas2.csv", "empresas3.csv" };
    static string[] partidas = { "save1.txt", "save2.txt", "save3.txt" };
    public static List<NPC> ContactosCargados = new List<NPC>();
    public static List<Companias> Companiass = new List<Companias>();
    public static List<Acciones> AccionesListActuales = new List<Acciones>();
    public static List<string> Paises = new List<string>() { "Nicaragua (predeterminado)", "EE.UU.", "Japón", "China", "Alemania", "España" };
    public static List<Acciones> Accioneshh = new List<Acciones>();
    public static string[] inventario = {"Inventario1.csv", "Inventario2.csv", "Inventario3.csv"};
    
    //Relacionado a noticias y periodico
    public static List<Periodicos> noticiash = new List<Periodicos>();
    public static string[] PeriodicoCSV = {"Periodico1.csv", "Periodico2.csv", "Periodico3.csv" };
    public static string[] historialBalance = { "balance1.csv", "balance2.csv", "balance3.csv" };
    public static Dictionary<int, decimal> PronosticoMercado = new Dictionary<int, decimal>();
    public static decimal DeudaEmergencia = 0m;
    public static string Titulo;
    public static string Descripcion;
    
    public static int turno = 0;
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
        ContactosLegendariosMenu.CargarUsos();  
      

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
            X = Pos.Left(label) - 30,
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
            X = Pos.Right(label) + 5,
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
                int slot = 0;
                if (!ArchivosDisponibles(
                    partidas[slot],
                    inventario[slot],
                    save_compania[slot],
                    ManejoDeArchivos.contactos[slot],
                    historialBalance[slot],
                    PeriodicoCSV[slot]))
                {
                    MessageBox.Query(
                        "Error",
                        "Uno o más archivos de la partida están abiertos en otro programa.",
                        "Aceptar");

                    return;
                }

                using (StreamReader save = new StreamReader(partidas[slot]))
                {
                    string nombre = (save.ReadLine() ?? "");
                    nombre = nombre.Replace("Nombre: ", "");
                    string pais = (save.ReadLine() ?? "");
                    pais = pais.Replace("Pais: ", "");
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Carisma: ", ""), out int carismas);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Economia: ", ""), out int economia);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Fiscalidad: ", ""), out int fiscalidad);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Corrupcion: ", ""), out int corrupcion);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("Balance: ", ""), out decimal balance);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("DeudaEmergencia: ", ""), out decimal deuda);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Turno: ", ""), out int turnos);
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    pd.balance = balance;
                    DeudaEmergencia = deuda;
                    turno = turnos;
                    InvInt = slot;
                }

                Companiass = Indices.CargarEmpresa(slot);
                PrepararPronosticoMercado();
                ContactosCargados = GeneracionDeContactos.CargarContactos(slot);
                RecalcularDeudaEmergencia();

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
                int slot = 1;
                if (!ArchivosDisponibles(
                    partidas[slot],
                    inventario[slot],
                    save_compania[slot],
                    ManejoDeArchivos.contactos[slot],
                    historialBalance[slot],
                    PeriodicoCSV[slot]))
                {
                    MessageBox.Query(
                        "Error",
                        "Uno o más archivos de la partida están abiertos en otro programa.",
                        "Aceptar");

                    return;
                }

                using (StreamReader save = new StreamReader(partidas[slot]))
                {
                    string nombre = (save.ReadLine() ?? "");
                    nombre = nombre.Replace("Nombre: ", "");
                    string pais = (save.ReadLine() ?? "");
                    pais = pais.Replace("Pais: ", "");
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Carisma: ", ""), out int carismas);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Economia: ", ""), out int economia);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Fiscalidad: ", ""), out int fiscalidad);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Corrupcion: ", ""), out int corrupcion);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("Balance: ", ""), out decimal balance);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("DeudaEmergencia: ", ""), out decimal deuda);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Turno: ", ""), out int turnos);
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    pd.balance = balance;
                    DeudaEmergencia = deuda;
                    turno = turnos;
                    InvInt = slot;
                }

                Companiass = Indices.CargarEmpresa(slot);
                PrepararPronosticoMercado();
                ContactosCargados = GeneracionDeContactos.CargarContactos(slot);
                RecalcularDeudaEmergencia();

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
                int slot = 2;
                if (!ArchivosDisponibles(
                    partidas[slot],
                    inventario[slot],
                    save_compania[slot],
                    ManejoDeArchivos.contactos[slot],
                    historialBalance[slot],
                    PeriodicoCSV[slot]))
                {
                    MessageBox.Query(
                        "Error",
                        "Uno o más archivos de la partida están abiertos en otro programa.",
                        "Aceptar");

                    return;
                }

                using (StreamReader save = new StreamReader(partidas[slot]))
                {
                    string nombre = (save.ReadLine() ?? "");
                    nombre = nombre.Replace("Nombre: ", "");
                    string pais = (save.ReadLine() ?? "");
                    pais = pais.Replace("Pais: ", "");
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Carisma: ", ""), out int carismas);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Economia: ", ""), out int economia);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Fiscalidad: ", ""), out int fiscalidad);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Corrupcion: ", ""), out int corrupcion);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("Balance: ", ""), out decimal balance);
                    _ = decimal.TryParse((save.ReadLine() ?? "").Replace("DeudaEmergencia: ", ""), out decimal deuda);
                    _ = int.TryParse((save.ReadLine() ?? "").Replace("Turno: ", ""), out int turnos);
                    pd.name = nombre;
                    pd.pais = pais;
                    pd.carisma = carismas;
                    pd.economia = economia;
                    pd.fiscalidad = fiscalidad;
                    pd.corrupcion = corrupcion;
                    pd.balance = balance;
                    DeudaEmergencia = deuda;
                    turno = turnos;
                    InvInt = slot;
                }

                Companiass = Indices.CargarEmpresa(slot);
                PrepararPronosticoMercado();
                ContactosCargados = GeneracionDeContactos.CargarContactos(slot);
                RecalcularDeudaEmergencia();

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
        for (int i = 0; i < Borration.Length; i++)
        {
            Borration[i] = new Button($"Eliminar Partida {i+1}")
            {
                X = Pos.X(Slots[i]),
                Y = Pos.Bottom(bottonslot1) + 2,
            };

            if (saves[i])//En caso que exista partida el botón de X existirá
            {
                VentanaCargarPartida.Add(Borration[i]);
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
                string nombre = (save.ReadLine() ?? "");
                nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                string pais = (save.ReadLine() ?? "");
                pais = pais.Replace("Pais: ", "");
                _ = int.TryParse((save.ReadLine() ?? "").Replace("Carisma: ", ""), out int carismas);
                _ = int.TryParse((save.ReadLine() ?? "").Replace("Economia: ", ""), out int economia);
                _ = int.TryParse((save.ReadLine() ?? "").Replace("Fiscalidad: ", ""), out int fiscalidad);
                _ = int.TryParse((save.ReadLine() ?? "").Replace("Corrupcion: ", ""), out int corrupcion);
                _ = decimal.TryParse((save.ReadLine() ?? "").Replace("Balance: ", ""), out decimal balance);
                save.Close();

                Slots[i].Add(new Label("Nombre:\n" + nombre)
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                });
                InvInt = i;
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
        var InfoFiscalidades = new Dialog(
    "Habilidad para reducir los impuestos pagados(Empresa o jugador)",
    60,
    20
);
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
                pd.name = casillaNombre.Text.ToString()!;
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
        turno = 0;

        
        for (int i = 0; i < saves.Length; i++)
        {
            if (!saves[i])
            {
                using (StreamWriter save = new StreamWriter(partidas[i], false, Encoding.UTF8))
                {
                    save.WriteLine($"Nombre: {pd.name} \nPais: {pd.pais} \nCarisma: {pd.carisma} " +
                        $"\nEconomia: {pd.economia} \nFiscalidad: {pd.fiscalidad} \nCorrupcion: {pd.corrupcion} \nBalance: {pd.balance}");
                    // nueva línea que persiste la deuda total
                    save.WriteLine($"DeudaEmergencia: {DeudaEmergencia}");
    
                }

                using (StreamWriter save = new StreamWriter(inventario[i]))
                {
                    save.WriteLine(pd.name);
                    save.WriteLine($"ID,Nombre,Costo_Compra,CostoActual,TipoAccion,Cantidad");
                }
                
                InicializarHistorialBalance(i);
                Guardarempresa(i, true);

                // preparar estado y generar contactos para la nueva partida
                Companiass = Indices.CargarEmpresa(i);
                PrepararPronosticoMercado();
                ContactosCargados = GeneracionDeContactos.GenerarPersonas();
                RecalcularDeudaEmergencia();
                GeneracionDeContactos.GuardarContactos(i, true);

                InvInt = i;
                guardado = true;
                break;
            }
        }

        using (StreamWriter save = new StreamWriter(partidas[InvInt], true))
        {
            save.WriteLine($"Turno: {turno}");
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
    public static void ActualizarVentana(Window ventana, Action funcion, Toplevel top)
    {
        top.Remove(ventana);
        funcion();
    }
    static string LeerNombre(int i)
    {
        if (!ArchivosDisponibles(
    partidas[InvInt],
    inventario[InvInt],
    save_compania[InvInt],
    ManejoDeArchivos.contactos[InvInt],
    historialBalance[InvInt],
    PeriodicoCSV[InvInt]))
        {
            MessageBox.Query(
                "Error",
                "Uno o más archivos de la partida están abiertos en otro programa.",
                "Aceptar");

            return "";
        }
        else
        {

            using (StreamReader save = new StreamReader(partidas[i], Encoding.UTF8))
            {
                string linea = (save.ReadLine() ?? "");
                linea = linea.Replace("Nombre:", "");//reemplaza "Nombre" por ""
                return linea;
            }
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
            string linea = LeerNombre(index);
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
                    save.WriteLine($"Turno: {turno}");
                }
                using (StreamWriter save = new StreamWriter(inventario[index]))
                {
                    save.WriteLine(pd.name);
                }
                InicializarHistorialBalance(index);
                Guardarempresa(index, false);

                if (Program.ContactosCargados == null || Program.ContactosCargados.Count == 0)
                {
                    MessageBox.Query("Error", "No hay contactos en memoria para guardar. Cancela y carga/crea contactos antes de sobrescribir.", "Aceptar");
                    return;
                }

                GeneracionDeContactos.GuardarContactos(index, false);
                Companiass = Indices.CargarEmpresa(index);
                PrepararPronosticoMercado();
                ContactosCargados = GeneracionDeContactos.CargarContactos(index);

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
            Companiass = Indices.GenerarIndicesEmpresas();
            for (int p = 0; p < Companiass.Count; p++)
            {
                save_empresas.WriteLine(Companiass[p]);

            }


        }
    }
    

    static void EliminarPartida(int i)
    {
        VerificarSave();
       
            int Eliminar = MessageBox.Query("Eliminar",
            "¿Está seguro que desea eliminar esta partida?",
            "Sí", "No");

            if(Eliminar== 0)
            {
            if (!ArchivosDisponibles(
    partidas[InvInt],
    inventario[InvInt],
    save_compania[InvInt],
    ManejoDeArchivos.contactos[InvInt],
    historialBalance[InvInt],
    PeriodicoCSV[InvInt]))
            {
                MessageBox.Query(
                    "Error",
                    "Uno o más archivos de la partida están abiertos en otro programa.",
                    "Aceptar");

                return;
            }
            else
            {

                MessageBox.Query("Eliminar",
                    "Partida eliminada con éxito",
                    "Aceptar");

                File.Delete(partidas[i]);
                File.Delete(inventario[i]);
                File.Delete(save_compania[i]);
                File.Delete(historialBalance[InvInt]);
                File.Delete(PeriodicoCSV[InvInt]);
                File.Delete(ManejoDeArchivos.contactos[i]);
            }
            }

    }
    //para controlar una exepcion
    static bool ArchivosDisponibles(params string[] archivos)
    {
        foreach (string archivo in archivos)
        {
            try
            {
                using FileStream fs = File.Open(
                    archivo,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.None);
            }
            catch (IOException)
            {
                return false;
            }
        }

        return true;
    }
    public static void Inicio(Toplevel top)
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
        var FrameNoticias = new FrameView()
        {
            X= Pos.AnchorEnd(62),
            Y = 2,
            Width = 60,
            Height = 8,
        };
        var labelStats = new Label($"Noticias, Turno: {turno}")
        {
            X = Pos.Center(),
            Y = 0
            
        };
        var titulo = new Label(Titulo)
        {
            X = Pos.Center(),
            Y = 2
        };
        var descripcion = new Label(Descripcion)
        {
            X = Pos.Center(),
            Y = 4
        };
        //botones bajos
        BotonesDeJuegoPredeterminado(top, VentanaInicio);
        
        //Balance
        var Balance = new Label($"Balance: {pd.balance:F2}")
        {
            X = Pos.Center(),
            Y = 1
        };
        //Contactos
        GeneracionDeContactos.Contactos(colores, colora, VentanaInicio, ContactosCargados);

        VentanaInicio.Add(LabelUsuario,Balance, LabelPais, FrameNoticias);
        //Esto es lo que se activa si se quiere ver el celular
        //Tutorial.LLamadaIvancito(VentanaInicio);

        FrameNoticias.Add(labelStats, titulo, descripcion);
        FrameNoticias.Add(labelStats, titulo, descripcion);

    }

    public static void BotonesDeJuegoPredeterminado(Toplevel top, Window ventana)
    {
        //botones bajos
        
        var btInicio = new Button("Inicio")
        {
            X = 1,
            Y = Pos.AnchorEnd(2)
        };
        var btBalance = new Button("Balance")
        {
            X = 12,
            Y = Pos.AnchorEnd(2)
        };
        var btPortafolio = new Button("Periodico")
        {
            X=24,
            Y=Pos.AnchorEnd(2)
        };
        var btInventario = new Button("Inventario")
        {
            X = 40,
            Y = Pos.AnchorEnd(2)
        };
        var btVerEmpresa = new Button("Ver Empresas")
        {
            X = 58,
            Y =Pos.AnchorEnd(2)
        };
        var btMenu = new Button("Volver al Menu")
        {
            X = 96,
            Y = Pos.AnchorEnd(2)
        };
        //Botones altos
        var pasarturno = new Button("Pasar turno")
        {
            X = 78,
            Y = Pos.AnchorEnd(2)
        };
        var LabelTurno = new Label($"Turno Actual: {turno}")
        {
            X = 116,
            Y = Pos.AnchorEnd(2)
        };
        var creditosButton = new Button("Creditos")
        {
            X = 140,
            Y = Pos.AnchorEnd(2)
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
            Indices.VentanaDeEmpresas(top, colores, colora);
        };
        btInventario.Clicked += () =>
        {
            top.RemoveAll();
            top.Add(Inventario.VentanaInventario(top, InvInt, colores, colora));
        };
        btMenu.Clicked += () =>
        {
            top.RemoveAll();
            top.Add(VentanaPrincipal);
        };
        btBalance.Clicked += () =>
        {
            top.RemoveAll();
            MostrarReporteBalance(top);
        };
        creditosButton.Clicked += () =>
        {
            top.RemoveAll();
            Creditos.MostrarCreditos(top);
        };
        pasarturno.Clicked += () =>
        {
            turno++;
            TeLlamanPapuContesta.EvaluarLlamadas();
            if (PronosticoMercado.Count != Companiass.Count)
            {
                PrepararPronosticoMercado();
            }

            for (int i = 0; i < Companiass.Count; i++)
            {
                Companias empresa = Companiass[i];
                decimal cambio = 0m;
                PronosticoMercado.TryGetValue(empresa.id, out cambio);
                empresa.capbursatil += empresa.capbursatil * cambio;

                Companiass[i] = empresa;
            }

            GuardarEmpresasActualizadas();
            ActualizarPreciosInventario();
            PrepararPronosticoMercado();
            string[] lineas = File.ReadAllLines(partidas[InvInt]);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].StartsWith("Turno:"))
                {
                    lineas[i] = $"Turno: {turno}";
                    break;
                }
            }

            File.WriteAllLines(partidas[InvInt], lineas);
            
            
            MessageBox.Query(
                "Turno",
                "Se actualizaron los capitales bursátiles",
                "Aceptar");
            decimal balancepormientras = pd.balance;
            Events.GestorDeEventos(ref balancepormientras);
            pd.balance = balancepormientras;
            top.RemoveAll();
            Inicio(top);
            Events.Apuestas(top);
        };
        btInicio.SetFocus();
        ventana.Add(btInicio,btPortafolio,btInventario,btVerEmpresa,btMenu, pasarturno, btBalance, LabelTurno, creditosButton);
    }
    static void GuardarEmpresasActualizadas()
    {
        using (StreamWriter save = new StreamWriter(save_compania[InvInt], false, Encoding.UTF8))
        {
            save.WriteLine("IdEmpresa; Empresa; Pais; Sector; Capital Bursátil; Accionistas; Productos; Ganancias; Gastos Marketing;Gastos Investigación; Gastos Mantenimiento; Participacion; Balance");

            foreach (Companias empresa in Companiass)
            {
                save.WriteLine(empresa.ToString());
            }
        }
    }
    static void ActualizarPreciosInventario()
    {
        if (!File.Exists(inventario[InvInt]))
            return;

        List<string> lineas = File.ReadAllLines(inventario[InvInt]).ToList();

        for (int i = 2; i < lineas.Count; i++)
        {
            string[] datos = lineas[i].Split(",");

            int idEmpresa = int.Parse(datos[0]);

            int indiceEmpresa = Companiass.FindIndex(e => e.id == idEmpresa);

            if (indiceEmpresa != -1)
            {
                decimal nuevoPrecio =
                    (Companiass[indiceEmpresa].capbursatil * 1000000m)
                    / 50000000m;

                // Columna CostoActual
                datos[3] = nuevoPrecio.ToString();

                lineas[i] = string.Join(",", datos);
            }
        }

        File.WriteAllLines(inventario[InvInt], lineas);
    }

    public static void AplicarImpactoSector(string sector, decimal multiplicador)
    {
        for (int i = 0; i < Companiass.Count; i++)
        {
            Companias empresa = Companiass[i];

            if (empresa.rubro == sector)
            {
                empresa.capbursatil = Math.Round(empresa.capbursatil * multiplicador, 2);
                empresa.balance = Math.Round(empresa.balance * multiplicador, 2);
                Companiass[i] = empresa;
            }
        }

        GuardarEmpresasActualizadas();
        ActualizarPreciosInventario();
    }

    public static void AplicarPrestamoEmergencia(decimal monto)
    {
        pd.balance += monto;
        DeudaEmergencia += monto;
    }

    public static void PrepararPronosticoMercado()
    {
        PronosticoMercado.Clear();

        foreach (Companias empresa in Companiass)
        {
            decimal cambioBase = (decimal)(Random.Shared.NextDouble() * 0.20 - 0.10);
            decimal cambioNoticias = 0m;
            Events.PasarTurnoPeriodico(ref Titulo, ref Descripcion, InvInt, ref cambioNoticias);
            PronosticoMercado[empresa.id] = Math.Round(cambioBase + cambioNoticias, 4);
        }
    }

    public static DataTable ObtenerPronosticoMercado()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cambio esperado");
        tabla.Columns.Add("Precio estimado");

        foreach (Companias empresa in Companiass)
        {
            decimal cambio = 0m;
            PronosticoMercado.TryGetValue(empresa.id, out cambio);
            decimal precioEstimado = Math.Round(empresa.capbursatil + (empresa.capbursatil * cambio), 2);

            tabla.Rows.Add(
                empresa.name,
                empresa.rubro,
                $"{cambio:+0.00%;-0.00%;0.00%}",
                $"{precioEstimado:F2}M");
        }

        return tabla;
    }

    //creando la ventana de empresas
   	

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

    static decimal ObtenerPrecioAccion(Companias empresa)
    {
        return (empresa.capbursatil * 1000000m) / 50000000m;
    }

    static bool TryObtenerEmpresa(int id, out Companias empresa)
    {
        foreach (Companias actual in Companiass)
        {
            if (actual.id == id)
            {
                empresa = actual;
                return true;
            }
        }

        empresa = default;
        return false;
    }

    static void InicializarHistorialBalance(int indice)
    {
        using (StreamWriter historial = new StreamWriter(historialBalance[indice], false, Encoding.UTF8))
        {
            historial.WriteLine("Tipo;EmpresaId;Empresa;Sector;Cantidad;PrecioUnitario;Total;BalanceDespues;Turno");
        }
    }

    static void AsegurarHistorialBalance(int indice)
    {
        if (!File.Exists(historialBalance[indice]))
        {
            InicializarHistorialBalance(indice);
        }
    }

    static void RegistrarMovimientoBalance(string tipo, Companias empresa, int cantidad, decimal precioUnitario, decimal total)
    {
        AsegurarHistorialBalance(InvInt);

        using (StreamWriter historial = new StreamWriter(historialBalance[InvInt], true, Encoding.UTF8))
        {
            historial.WriteLine($"{tipo};{empresa.id};{empresa.name};{empresa.rubro};{cantidad};{precioUnitario:F2};{total:F2};{pd.balance:F2};{turno}");
        }
    }

    static DataTable CrearTablaPosicionesJugador(out decimal valorCartera, out decimal costoBase, out int totalAcciones)
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Costo compra");
        tabla.Columns.Add("Precio actual");
        tabla.Columns.Add("Valor actual");
        tabla.Columns.Add("Ganancia/Pérdida");

        valorCartera = 0m;
        costoBase = 0m;
        totalAcciones = 0;

        if (!File.Exists(inventario[InvInt]))
        {
            return tabla;
        }

        List<Acciones> acciones = Inventario.CargarInventario(InvInt);
        foreach (Acciones accion in acciones)
        {
            Companias empresa;
            decimal precioActual = accion.CostoActual;
            string sector = "Desconocido";
            string nombreEmpresa = accion.name;

            if (TryObtenerEmpresa(accion.id, out empresa))
            {
                precioActual = ObtenerPrecioAccion(empresa);
                sector = empresa.rubro;
                nombreEmpresa = empresa.name;
            }

            decimal valorActual = precioActual * accion.cantidad;
            decimal costoRegistrado = accion.CostoDeCompra * accion.cantidad;
            decimal diferencia = valorActual - costoRegistrado;

            valorCartera += valorActual;
            costoBase += costoRegistrado;
            totalAcciones += accion.cantidad;

            tabla.Rows.Add(
                nombreEmpresa,
                sector,
                accion.cantidad,
                $"{accion.CostoDeCompra:F2}",
                $"{precioActual:F2}",
                $"{valorActual:F2}",
                $"{diferencia:+0.00;-0.00;0.00}");
        }

        return tabla;
    }

    static DataTable CrearTablaMovimientosBalance()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Tipo");
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Precio");
        tabla.Columns.Add("Total");
        tabla.Columns.Add("Balance");
        tabla.Columns.Add("Turno");

        if (!File.Exists(historialBalance[InvInt]))
        {
            return tabla;
        }

        string[] lineas = File.ReadAllLines(historialBalance[InvInt], Encoding.UTF8);
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(';');
            if (datos.Length < 9)
            {
                continue;
            }

            tabla.Rows.Add(
                datos[0],
                datos[2],
                datos[3],
                datos[4],
                datos[5],
                datos[6],
                datos[7],
                datos[8]);
        }

        return tabla;
    }

    static void MostrarReporteBalance(Toplevel top)
    {
        AsegurarHistorialBalance(InvInt);

        decimal valorCartera;
        decimal costoBase;
        int totalAcciones;
        DataTable tablaPosiciones = CrearTablaPosicionesJugador(out valorCartera, out costoBase, out totalAcciones);
        DataTable tablaMovimientos = CrearTablaMovimientosBalance();
        decimal patrimonioEstimado = pd.balance + valorCartera - DeudaEmergencia;
        decimal gananciaFlotante = valorCartera - costoBase;

        var VentanaBalance = new Window("Reporte de Balance")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };

        var labelResumen = new Label(
            $@"Jugador: {pd.name} | País: {pd.pais} | Turno: {turno}
Efectivo: ${pd.balance:F2} | Cartera: ${valorCartera:F2} | Deuda: ${DeudaEmergencia:F2} | Patrimonio estimado: ${patrimonioEstimado:F2}
Costo base: ${costoBase:F2} | Ganancia/Pérdida flotante: ${gananciaFlotante:+0.00;-0.00;0.00} | Acciones: {totalAcciones}")
        {
            X = 1,
            Y = 1
        };

        var marcoPosiciones = new FrameView("Posiciones actuales")
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaPosicionesView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaPosicionesView.Table = tablaPosiciones;
        marcoPosiciones.Add(tablaPosicionesView);

        var marcoMovimientos = new FrameView("Compras y ventas")
        {
            X = 1,
            Y = 15,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaMovimientosView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaMovimientosView.Table = tablaMovimientos;
        marcoMovimientos.Add(tablaMovimientosView);

        var botonVolver = new Button("Volver al Inicio")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        botonVolver.Clicked += () =>
        {
            top.RemoveAll();
            Inicio(top);
        };

        VentanaBalance.Add(labelResumen, marcoPosiciones, marcoMovimientos, botonVolver);
        top.Add(VentanaBalance);
    }
    
    public static void MostrarDetalleEmpresa(Toplevel top, Companias empresa)
    {
        var DetalleEmpresa = new Window("Detalle de Empresa")
        {
            X = 0,
            Y = 0,
            ColorScheme = colores[colora],
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        var MasInfo = new Label(
@$" ID: {empresa.id}                  
 Empresa: {empresa.name}           
 País: {empresa.pais}              
 Sector: {empresa.rubro}           
 Capital: {empresa.capbursatil:F2}M   
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
        var InputCantidad = new TextField()
        {
            X = Pos.X(btVolver) + 4,
            Y = Pos.Y(btVolver) - 5,
            Width = 15
        };
        DetalleEmpresa.Add((InputCantidad));
        var LabelCantidad = new Label("Cantidad:")
        {
            X = Pos.X(InputCantidad),
            Y = Pos.Y(InputCantidad) -1,
                
        };
        DetalleEmpresa.Add((LabelCantidad));
        var btcomprar_acciones = new Button("Comprar Accion")
        {
            X = Pos.X(btVolver) +4,
            Y = Pos.Y(btVolver)- 2,
        };
        var btvender_acciones = new Button("Vender Accion")
        {
            X = Pos.X(btcomprar_acciones),
            Y = Pos.Y(btcomprar_acciones) + 1,
        };
        DetalleEmpresa.Add(btvender_acciones);
        
        var LabelPrecioAccion = new Label($"Precio: {((empresa.capbursatil * 1000000m) / 50000000m):F2}")
        {
            X = Pos.X(btcomprar_acciones) +4,
            Y = Pos.Y(btcomprar_acciones)- 1,
        };
        DetalleEmpresa.Add(btcomprar_acciones, LabelPrecioAccion);
        btVolver.Clicked += () =>
        {
            top.RemoveAll();
            Indices.VentanaDeEmpresas(top, colores, colora);
        };
        btcomprar_acciones.Clicked += () =>
        {
            int cantidty = 0;
            bool IsInt = false;
            List<string> lineas = File.ReadAllLines(inventario[InvInt]).ToList();
            decimal precioAccional = (empresa.capbursatil * 1000000) / 50000000;
            if (int.TryParse(InputCantidad.Text.ToString(), out cantidty))
            {
                IsInt = true;
            }
            if (pd.balance >= (precioAccional*cantidty) && IsInt == true && cantidty > 0)
            {
                Acciones NuevaAccion = new Acciones();
                NuevaAccion.id = empresa.id;
                NuevaAccion.name = empresa.name;
                NuevaAccion.CostoActual = precioAccional;
                NuevaAccion.CostoDeCompra = precioAccional;
                NuevaAccion.TipoDeAccion = true;
                NuevaAccion.cantidad += cantidty;
                bool pader = false; // verificando si existe la acción creo, maldito raul que es pader
                for (int i = 2; i < lineas.Count; i++)
                {
                    string[] datos = lineas[i].Split(',');

                    if (datos[0] == empresa.id.ToString())
                    {
                        int cantity = int.Parse(datos[5]);
                        cantity += cantidty;
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
                        str.WriteLine(
                            $"{NuevaAccion.id},{NuevaAccion.name},{NuevaAccion.CostoActual}, {NuevaAccion.CostoDeCompra},{NuevaAccion.TipoDeAccion}, {NuevaAccion.cantidad }");
                    }
                }
                else if (pader == true)
                {
                    File.WriteAllLines(inventario[InvInt], lineas);
                }

                pd.balance -= precioAccional * cantidty;
                Guardarelbalance();
                RegistrarMovimientoBalance("COMPRA", empresa, cantidty, precioAccional, precioAccional * cantidty);
                MessageBox.Query(
                    "Acciones compradas con exito!",
                    $@"Lograste comprar {cantidty} acciones a un precio unitario de {precioAccional:F2}
para un precio total de {precioAccional*cantidty:F2}",
                    "Listo");
            }
            else
            {
                MessageBox.Query(
                    "Error",
                    "Ocurrio un error",
                    "Aceptar");
            }

        
    };
        
        btvender_acciones.Clicked += () =>
        {
            int cantidty = 0;
            bool IsInt = false;
            List<string> lineas = File.ReadAllLines(inventario[InvInt]).ToList();
            decimal precioAccional = (empresa.capbursatil * 1000000) / 50000000;

            bool encontrada = false;
            if (int.TryParse(InputCantidad.Text.ToString(), out cantidty))
            {
                IsInt = true;
            }
            for (int i = 2; i < lineas.Count; i++)
            {
                string[] datos = lineas[i].Split(',');

                if (datos[0] == empresa.id.ToString() && IsInt == true )
                {
                    int cantidadActual = int.Parse(datos[5]);

                    if (cantidadActual > 0 && cantidty > 0 && cantidty <= cantidadActual)
                    {
                        cantidadActual -= cantidty;

                        if (cantidadActual == 0)
                        {
                            // Eliminar la línea si ya no quedan acciones
                            lineas.RemoveAt(i);
                        }
                        else
                        {
                            datos[5] = cantidadActual.ToString();
                            // Actualizamos el costo actual con el precio de venta vigente
                            datos[2] = precioAccional.ToString();
                            lineas[i] = string.Join(",", datos);
                        }

                        File.WriteAllLines(inventario[InvInt], lineas);
                        pd.balance += precioAccional * cantidty;
                        Guardarelbalance();
                        using (StreamWriter save = new StreamWriter(partidas[InvInt], false, Encoding.UTF8))
                        RegistrarMovimientoBalance("VENTA", empresa, cantidty, precioAccional, precioAccional * cantidty);
                        encontrada = true;
                        MessageBox.Query(
                            "Acciones vendidas con exito!",
                            $@"Haz vendido {cantidty} acciones a un precio de {precioAccional:F2}
para un total de {precioAccional*cantidty:F2}", 
                            "Aceptar");
                    }
                    else
                    {
                        MessageBox.Query(
                            "Error",
                            "No tienes acciones de esta empresa para vender",
                            "Aceptar");
                        encontrada = true; // evita el mensaje de "no tienes acciones" duplicado
                    }

                    break;
                }
            }

            if (!encontrada)
            {
                MessageBox.Query(
                    "Error",
                    "No posees acciones de esta empresa",
                    "Aceptar");
            }
        };

        DetalleEmpresa.Add(btVolver);
        top.Add(DetalleEmpresa);
    }
    public static void Guardarelbalance()
    {
        using (StreamWriter save = new StreamWriter(partidas[InvInt], false, Encoding.UTF8))
        {
            save.WriteLine($"Nombre: {pd.name} \nPais: {pd.pais} \nCarisma: {pd.carisma} " +
                $"\nEconomia: {pd.economia} \nFiscalidad: {pd.fiscalidad} \nCorrupcion: {pd.corrupcion} \nBalance: {pd.balance}");
            save.WriteLine($"DeudaEmergencia: {DeudaEmergencia}");
            save.WriteLine($"Turno: {turno}");

        }
    }

    // Utilidad: recalcular deuda total desde los contactos (llamar después de CargarContactos)
    public static void RecalcularDeudaEmergencia()
    {
        decimal total = 0m;
        foreach (var c in ContactosCargados)
            total += c.montoprestado;
        DeudaEmergencia = total;
    }

    public static bool PagarDeuda(decimal monto)
    {
        if (monto <= 0m)
            return false;

        if (pd.balance < monto)
            return false;

        pd.balance -= monto;
        DeudaEmergencia = Math.Max(0m, DeudaEmergencia - monto);
        Guardarelbalance();
        return true;
    }
}
