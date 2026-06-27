using Empresas;
using NAudio.Wave;
using Proyecto_Juego;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Terminal.Gui;
using Terminal.Gui.App;
using Terminal.Gui.Drawing;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;
using TextMateSharp.Internal.Rules;
using static System.Runtime.InteropServices.JavaScript.JSType;


class Program
{
    //Jugador
    public static Proyecto_Juego.Players pd = new Players();
    //Jugador
    static FrameView[] Slots = new FrameView[3];
    static Button[] Borration = new Button[3];
    public static int colora = 0;
    //Audio Música
    static WaveOutEvent salidaAudio;
    static AudioFileReader audio;
    static bool muteado = false;
    //Audio Click
    static WaveOutEvent salidaclick;
    static AudioFileReader click;
    //Ventana Principal
    public static Window VentanaPrincipal;
    static string puntosmejorastats = "0";
    //guardado partidas
    public static List<Acciones> AccionesListActuales = new List<Acciones>();
    public static List<string> Paises = new List<string>() { "Nicaragua (predeterminado)", "EE.UU.", "Japón", "China", "Alemania", "España" };
    public static List<Acciones> Accioneshh = new List<Acciones>();


    //define si se mostrará el tutorial
    public static bool MostrarTutorial;

    //Trabajos disponibles
    public static string[] Trabajoslist = { "Desencriptador" , "Programador"};
    public static string TrabajoEscogido = "";
    public static int InvInt = 0;
    static List<FrameView> marcos = new List<FrameView>();
    /*
     Esto se recomendó:

     Colors.Add("Tema_0", colores[0]);
Colors.Add("Tema_1", colores[1]);
Colors.Add("Tema_2", colores[2]);
    */
    public static List<Scheme> colores = new List<Scheme>() {
        new Scheme()
        {
            //Tema predeterminado
            // Controles normales
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.White,
                Color.Blue),

            // Control seleccionado
            Focus = new Terminal.Gui.Drawing.Attribute(
                Color.Black,
                Color.Gray),

            // Hotkeys sin foco
            HotNormal = new Terminal.Gui.Drawing.Attribute(
                Color.BrightYellow,
                Color.Blue),

            // Hotkeys con foco
            HotFocus = new Terminal.Gui.Drawing.Attribute(
                Color.BrightYellow,
                Color.Gray)
        },

        // Tema oscuro
        new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.White,
                Color.Black),

            Focus = new Terminal.Gui.Drawing.Attribute(
                Color.Black,
                Color.BrightGreen),

            HotNormal = new Terminal.Gui.Drawing.Attribute(
                Color.BrightCyan,
                Color.Black),

            HotFocus = new Terminal.Gui.Drawing.Attribute(
                Color.White,
                Color.BrightGreen)
        },

        // Tema blanco
        new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.Black,
                Color.White),

            Focus = new Terminal.Gui.Drawing.Attribute(
                Color.White,
                Color.BrightBlue),

            HotNormal = new Terminal.Gui.Drawing.Attribute(
                Color.Red,
                Color.White),

            HotFocus = new Terminal.Gui.Drawing.Attribute(
                Color.BrightYellow,
                Color.BrightBlue)
        }
    };
    static List<Scheme> ColoreButtonSelected = new List<Scheme>() {
        // Tema azul
        new Scheme()
        {
            // Fondo blanco con texto azul
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.Blue,
                Color.White),
        },

        // Tema oscuro
        new Scheme()
        {
            // Verde brillante estilo terminal retro
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.BrightGreen,
                Color.Black)
        },

        // Tema blanco
        new Scheme()
        {
            // Azul brillante sobre fondo blanco
            Normal = new Terminal.Gui.Drawing.Attribute(
                Color.BrightBlue,
                Color.White)
        }
    };
    static List<string> colorestxt = new List<string>() {"Predeterminado", "Oscuro", "Blanco"};
    //probando las empresas
    
    

    static string PaisSeleccionado = "";
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


        using IApplication app = Application.Create().Init();
        var VentanaPrincipal = new Window()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora],
        };
        if (OperatingSystem.IsWindows())
        {
            Reproducir();
        }
        ContactosLegendariosMenu.CargarUsos();  
      

        
       
        //botón nueva partida
        var label = new Label() { Text = @"╔══════════════════════════════════════════════════════════════════════╗
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
╚══════════════════════════════════════════════════════════════════════╝",
        
            X = Pos.Center(),
            Y = Pos.Center()
        };
        VentanaPrincipal.Add(label);
        var marco = new FrameView()
        {
            X = Pos.Left(label) - 30,
            Y = Pos.Center(),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]
        };
        var botonNuevaPartida = new Button()
        {Text = "_Nueva Partida",
        
            X = Pos.Center(),
            Y = Pos.Center()
        };
        marco.Add(botonNuevaPartida);
        VentanaPrincipal.Add(marco);
        //botón cargar partida
        var marco2 = new FrameView()
        {
            X = Pos.X(marco),
            Y = Pos.Bottom(marco),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]

        };
        var botonCargarPartida = new Button()
        {
            Text = "_Cargar Partida",       
            X = Pos.Center(),
            Y = 1
        };
        VentanaPrincipal.Add(marco2);
        marco2.Add(botonCargarPartida);

        //botón configuración
        var marcoconfig = new FrameView()
        {
            X = Pos.Right(label) + 5,
            Y = Pos.Center(),
            Width = 26,
            Height = 5,
            ColorScheme = colores[colora]
        };
        var botonConfiguracion = new Button()
        {
            Text = "_Configuración",        
            X = Pos.Center(),
            Y = 1,
            
        };
        VentanaPrincipal.Add(marcoconfig);
        marcoconfig.Add(botonConfiguracion);


        //botón salir
        var marcosalir = new FrameView()
        {
            X = Pos.X(marcoconfig),
            Y = Pos.Bottom(marcoconfig),
            Width = 26,
            Height = 5,
        };
        marcosalir.Scheme = colores[colora];

        var botonsalir = new Button()
        {
            Text = "_Salir",        
            X = 8,
            Y = 1,
            
        };
        VentanaPrincipal.Add(marcosalir);
        marcosalir.Add(botonsalir);

       


        //Agregar marcos a la list
        marcos.Add(marco);
        marcos.Add(marco2);
        marcos.Add(marcoconfig);
        marcos.Add(marcosalir);

        
  
        
        //Equivalente a cuando gana y pierde foco
        //Boton Nueva Partida
        botonNuevaPartida.FocusedChanged += (s, e) =>
        {
            if (botonNuevaPartida.HasFocus)
            {
                // equivalente a Enter
                marco.ColorScheme = ColoreButtonSelected[colora];
                if (OperatingSystem.IsWindows())
                {
                    ClickSound();
                }
            }
            else
            {
                // equivalente a Leave
                marco.ColorScheme = colores[colora];
            }
        };

        //Boton Cargar Partida
        botonCargarPartida.FocusedChanged += (s, e) =>
        {
            if (botonCargarPartida.HasFocus)
            {
                // equivalente a Enter
                marco2.ColorScheme = ColoreButtonSelected[colora];
                if (OperatingSystem.IsWindows())
                {
                    ClickSound();
                }
            }
            else
            {
                // equivalente a Leave
                marco2.ColorScheme = colores[colora];
            }
        };

        //Boton Configuracion
        botonConfiguracion.FocusedChanged += (s, e) =>
        {
            if (botonsalir.HasFocus)
            {
                // equivalente a Enter
                marcoconfig.ColorScheme = ColoreButtonSelected[colora];
                if (OperatingSystem.IsWindows())
                {
                    ClickSound();
                }
            }
            else
            {
                // equivalente a Leave
                marcoconfig.ColorScheme = colores[colora];
            }
        };

        //Boton Salir
        botonsalir.FocusedChanged += (s,e) =>
        {
            if (botonsalir.HasFocus)
            {
                // equivalente a Enter
                marcosalir.ColorScheme = ColoreButtonSelected[colora];
                ClickSound();
            }
            else
            {
                // equivalente a Leave
                marcosalir.ColorScheme = colores[colora];
            }
        };

       
        botonNuevaPartida.Accepting += (s,e) =>
        {
            top.Remove(VentanaPrincipal);
            CreacionPersonaje(top);
        };
        botonCargarPartida.Accepting += (s,e) =>
        {

            top.Remove(VentanaPrincipal);
            CargarPartida(top);

        };
        botonConfiguracion.Accepting += (s, e) =>
        {
            top.Remove(VentanaPrincipal);
            Configuracion(top);
        };
        botonsalir.Accepting += (s, e) => Application.RequestStop();

        botonNuevaPartida.SetFocus();
        Application.Run(VentanaPrincipal);//Corre la ventana
    }


    static void CargarPartida(Toplevel top)
    {
        var VentanaCargarPartida = new Window()
        {
            X=0,
            Y=0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };

        var par1 = new Label()
        {
            Text = "Slot 1",
            X =5,
            Y=9
        };
        VentanaCargarPartida.Add(par1);
        Slots[0] = new FrameView()
        {
            X =3,
            Y=10,
            Width = 20,
            Height = 10,
        };
        var bottonslot1 = new Button()
        {
            Text = "Cargar slot 1",
            X = Pos.X(par1),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot1);
        var par2 = new Label()
        {
            Text = "Slot 2",
            X = 30,
            Y = 9
        };
        VentanaCargarPartida.Add(par2);
        Slots[1] = new FrameView()
        {
            X = 28,
            Y = 10,
            Width = 20,
            Height = 10,
        };
        var bottonslot2 = new Button()
        {
            Text = "Cargar slot 2",
            X = Pos.X(par2),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot2);
        var par3 = new Label()
        {
            Text = "Slot 3",
            X = 55,
            Y = 9
        };
        VentanaCargarPartida.Add(par3);
        Slots[2] = new FrameView()
        {
            X = 53,
            Y = 10,
            Width = 20,
            Height = 10,
        };
        var bottonslot3 = new Button()
        {
            Text = "Cargar slot 3",
            X = Pos.X(par3),
            Y = 21
        };
        VentanaCargarPartida.Add(bottonslot3);
        bottonslot1.Accepting += (s, e) =>
        {
            int slot = 0;
            MostrarTutorial = false;
            CargandoLasPartidas.CargarPartida(slot, top, VentanaCargarPartida);
            
        };
        bottonslot2.Accepting += (s, e) =>
        {
            int slot = 1;
            MostrarTutorial = false;
            CargandoLasPartidas.CargarPartida(slot, top, VentanaCargarPartida);
        };
        bottonslot3.Accepting += (s, e) =>
        {
            int slot = 2;
            MostrarTutorial = false;
            CargandoLasPartidas.CargarPartida(slot, top, VentanaCargarPartida);
        };

        var Back = new Button()
        {
            Text = "Volver al Menú",
            X = Pos.Center() + 5,
            Y= 30
        };


        Back.Accepting += (s,e) =>
        {
            top.Remove(VentanaCargarPartida);
            top.Add(VentanaPrincipal);
        };

        VentanaCargarPartida.Add(Slots[0], Slots[1],Slots[2],Back);
        //Agregando el icono para eliminar. 
        CreandoNuevaPartida.VerificarSave();
        for (int i = 0; i < Borration.Length; i++)
        {
            Borration[i] = new Button()
            {
                Text = $"Eliminar Partida {i + 1}",
                X = Pos.X(Slots[i]),
                Y = Pos.Bottom(bottonslot1) + 2,
            };

            if (ManejoDeArchivos.saves[i])//En caso que exista partida el botón de X existirá
            {
                VentanaCargarPartida.Add(Borration[i]);
            }

        }
        Borration[0].Accepting += (s,e) =>
        {
            ModificarPartidas.EliminarPartida(0);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        Borration[1].Accepting += (s,e) =>
        {
            ModificarPartidas.EliminarPartida(1);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        Borration[2].Accepting += (s,e) =>
        {
            ModificarPartidas.EliminarPartida(2);
            ActualizarVentana(
                VentanaCargarPartida,
                () => CargarPartida(top),
                top
            );
        };

        //Verificando que haya partidas guardadas en cada slot
        CargandoLasPartidas.VerificarNoHayDatosGuardadosLabel(Slots);
        top.Add(VentanaCargarPartida);
    } 

    
    static void Configuracion(IApplication app)
    {
        var ventanaconfiguracion = new Window()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };
        var TemasLabel = new Label()
        {
            Text = "Temas",
            X = 2,
            Y = 1
        };
        var ListaTEMAS = new ListView(colorestxt)
        {
            //Aqui se necesita algo como Source = new ListWrapper<string>(colorestxt)
            X = Pos.Right(TemasLabel) + 4,
            Y = 2,
            Width = 30,
            Height = 6
        };
        ListaTEMAS.SelectedItemChanged += (args) =>
        {
            colora = args.Item;
            PaisSeleccionado = colorestxt[args.Item];
        };
        var aceptar = new Button()
        {
            Text = "Aceptar",
            Y = 11,
            X = Pos.Right(ListaTEMAS),
        };
        var labelMusica = new Label()
        {
            Text = "Reproducir / Silenciar Música",
            X = 2,
            Y = 8,
        };
        var botonMusica = new Button()
        {
            Text = " ▶",
            X = Pos.Right(labelMusica) + 2,
            Y = Pos.Y(labelMusica)
        };
        botonMusica.Accepting += (s, e) =>
        {
            muteado = !muteado;
            salidaAudio.Volume = muteado ? 0f : 1f;
        };
        aceptar.Accepting += (s, e) =>
        {
            VentanaPrincipal.ColorScheme = colores[colora];
            foreach (var m in marcos)
            {
                m.ColorScheme = colores[colora];
            }

            MessageBox.Query(app,
                "Guardado",
                "Se guardo la configuracion", //Muestra un aviso, un mensaje
    "Aceptar");//El programa informa que se ha introducido cierto nombre y cierta dirección       
            app.RequestStop();//Cuando se pulsa el botón desaparece la ventana 
            
        };

        ventanaconfiguracion.Add(labelMusica, botonMusica);
        ventanaconfiguracion.Add(aceptar);
        ventanaconfiguracion.Add(ListaTEMAS);
        ventanaconfiguracion.Add(TemasLabel);
        app.Run(ventanaconfiguracion);
    }
    static void CreacionPersonaje(Toplevel top)
    {

        var VentanaCreacionPersonaje = new Window()//Se agrega la ventana
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme =  colores[colora]
        };
        var etiquetaNombre = new Label()//Se agrega texto
        {
            Text = "Nombre",
            X = 2,
            Y = 2,
        };

        var casillaNombre = new TextField()
        {
            X = Pos.Right(etiquetaNombre) + 1,
            Y = etiquetaNombre.Y,
            Width = 30
        };
        //Aquí se valida que solo se puedan ingresar 20 caracteres como máximo
        casillaNombre.TextChanging += (e, s) =>
        {
            if (e.NewText.Length > 20)
            {
                e.Cancel = true;
            }
        };
        var etiquetaPais = new Label()//Se agrega texto
        {
            Text = "País",
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


        PaisSeleccionado = "Nicaragua";
        ListaPaises.SelectedItemChanged += (args) =>
        {
            PaisSeleccionado = Paises[args.Item];
        };


        var botonAceptar = new Button()
        {
            Text = "Aceptar",
            X = Pos.Center(),
            Y = 20
        };
        bool guardado = false;
        bool nombre = false;
        VentanaCreacionPersonaje.Add(botonAceptar);
        botonAceptar.Accepting += (s, e) =>
        {
            CreandoNuevaPartida.VerificarPartidaAntesDeVentanainicio(casillaNombre, PaisSeleccionado, nombre, guardado,
                InvInt, top, VentanaCreacionPersonaje);
            //() son funciones anónimas, todavía no se han creado funciones aparte
        };
        var SalirS = new Button()
        {
            Text = "Salir sin guardar",
            X = Pos.X(botonAceptar)+ 20,
            Y = Pos.Y(botonAceptar),
        };

        SalirS.Accepting += (s, e) =>
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
    public static void ActualizarVentana(Window ventana, Action funcion, Toplevel top)
    {
        top.Remove(ventana);
        funcion();
    }

    

    
    //para controlar una exepcion
    public static void Inicio(Toplevel top)
    {
        var VentanaInicio = new Window()
        {
            X=0,
            Y=0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora]
        };
        top.Add(VentanaInicio);//Inicializador
        VentanaInicio.MouseClick += (args) =>
        {
            if (args.MouseEvent.Flags.HasFlag(MouseFlags.Button1Clicked))
            {
                //Visual
                var AccionRapidaDialogo = new Dialog("Accion rapida", 50, 10)
                {
                    X = Pos.Center(),
                    Y = Pos.Center(),
                };
                var label = new Label("Usa tab para desplazarte mejor")
                {
                    X = Pos.Center(),
                    Y = 0
                };
                var ComprarAcciones = new Button("Comprar Acciones")
                {
                    X = 2,
                    Y = 1
                };
                var Balance = new Button("Balance")
                {
                    X = 30,
                    Y = 1
                };
                var Inventariobt = new Button("Inventario de acciones")
                {
                    X = Pos.Center(),
                    Y = 3
                };
                var CerrarAccionRapida = new Button("Cerrar")
                {
                    X = Pos.Center(),
                    Y = 6
                };
                //Accionar
                ComprarAcciones.Accepting += (s, e) =>
                {
                    top.RemoveAll();
                    Indices.VentanaDeEmpresas(top);

                };
                Balance.Accepting += (s, e) =>
                {
                    top.RemoveAll();
                    Tablasdefrancisco.MostrarReporteBalance(top);
                };
                Inventariobt.Accepting += (s, e) =>
                {
                    top.RemoveAll();
                    Inventario.VentanaInventario(top);
                };
                CerrarAccionRapida.Accepting += (s, e) =>
                {
                    Application.RequestStop(AccionRapidaDialogo);
                };

            AccionRapidaDialogo.Add(CerrarAccionRapida, ComprarAcciones, Balance, label, Inventariobt);
                Application.Run(AccionRapidaDialogo);
            }
        };
        var LabelUsuario = new Label()
        {
            Text = $"Inversor: {pd.name}",
            X = 2,
            Y = 1
        };
        var LabelPais = new Label()
        {
            Text = $"Pais: {pd.pais}",
            X = Pos.X(LabelUsuario),
            Y = 2
        };
        //Noticias
        var FrameNoticias = new FrameView()
        {
            X= Pos.AnchorEnd(62),
            Y = 2,
            Width = 60,
            Height = 8,
        };
        var labelStats = new Label()
        {
            Text = $"Noticias, Turno: {ManejoDeArchivos.turno}",
            X = Pos.Center(),
            Y = 0
            
        };
        var titulo = new Label()
        {
            Text = CambiosDelMercado.Titulo,
            X = Pos.Center(),
            Y = 2
        };
        var descripcion = new Label()
        {
            Text = CambiosDelMercado.Descripcion,
            X = Pos.Center(),
            Y = 4
        };
        //Frame trabajar
        var TrabajoFrame = new FrameView()
        {
            X =  Pos.Center(),
            Y = Pos.Center(),
            Width = 40,
            Height = 10
        };
        var TituloTrabajo = new Label()
        {
            Text = "Buscar un trabajo",
            X = Pos.Center(),
            Y = 0
        };
        var Trabajos = new ListView(Trabajoslist)
        {
            X = Pos.Center(),
            Y = 1,
            Width = 20,
            Height = 3
        };
        Trabajos.SelectedItemChanged += (args) =>
        {
            TrabajoEscogido = Trabajoslist[args.Item];
        };
        var buttonTrabajar = new Button()
        {
            Text = "Trabajar",
            X = Pos.Center(),
            Y = 4
        };
        //Al clickear trabajar
        buttonTrabajar.Accepting += (s, e) =>
        {
            if (TrabajoEscogido == "Desencriptador")
            {
                Proyecto_Juego.Trabajos.Desencriptador(top);
            } else if (TrabajoEscogido == "Programador")
            {
                Proyecto_Juego.Trabajos.Programador(top);
            }
        };
        //botones bajos
        BotonesDeJuegoPredeterminado(top, VentanaInicio);
        
        //Balance
        var Balance = new Label()
        {
            Text = $"Balance: {pd.balance.ToString("N2", CultureInfo.InvariantCulture)}",
            X = Pos.Center(),
            Y = 1
        };
        //Contactos
        GeneracionDeContactos.Contactos(VentanaInicio, CargandoLasPartidas.ContactosCargados);
        
        VentanaInicio.Add(LabelUsuario,Balance, LabelPais, FrameNoticias, TrabajoFrame);
        
        TrabajoFrame.Add(TituloTrabajo, Trabajos, buttonTrabajar);
        FrameNoticias.Add(labelStats, titulo, descripcion);

    }

    public static void BotonesDeJuegoPredeterminado(Toplevel top, Window ventana)
    {
        //botones bajos
        
        var btInicio = new Button()
        {
            Text = "Inicio",
            X = 1,
            Y = Pos.AnchorEnd(2)
        };
        var btBalance = new Button()
        {
            Text = "Balance",
            X = 12,
            Y = Pos.AnchorEnd(2)
        };
        var BtTragamonedas = new Button()
        {
            Text = "Traga Monedas",
            X =23,
            Y=Pos.AnchorEnd(2)
        };
        var btInventario = new Button()
        {
            Text = "Inventario",
            X = Pos.Right(BtTragamonedas),
            Y = Pos.AnchorEnd(2)
        };
        var btVerEmpresa = new Button()
        {
            Text = "Ver Empresas",
            X = 58,
            Y =Pos.AnchorEnd(2)
        };
        var btMenu = new Button()
        {
            Text = "Volver al Menú",
            X = 96,
            Y = Pos.AnchorEnd(2)
        };
        //Botones altos
        var pasarturno = new Button()
        {
            Text = "Pasar turno",
            X = 78,
            Y = Pos.AnchorEnd(2)
        };
        var LabelTurno = new Label()
        {
            Text = $"Turno Actual: {ManejoDeArchivos.turno}",
            X = 116,
            Y = Pos.AnchorEnd(2)
        };
        var creditosButton = new Button()
        {
            Text= "Creditos",
            X = 140,
            Y = Pos.AnchorEnd(2)
        };
        AyudaFinanciera.Aprender(ventana, 155, Pos.AnchorEnd(2));
        //Funciones
        BtTragamonedas.Accepting += (s, e) =>
        {
            top.RemoveAll();
            TragaMonedas.Iniciar(top);
        };
        btInicio.Accepting += (s, e) =>
        {
            top.RemoveAll();
            Inicio(top);
        };
        btVerEmpresa.Accepting += (s, e) =>
        {
            top.RemoveAll();
            Indices.VentanaDeEmpresas(top);
        };
        btInventario.Accepting += (s, e) =>
        {
            top.RemoveAll();
            top.Add(Inventario.VentanaInventario(top));
        };
        btMenu.Accepting += (s, e) =>
        {
            ModificarPartidas.Guardarelbalance();
            top.RemoveAll();
            top.Add(VentanaPrincipal);
        };
        btBalance.Accepting += (s, e) =>
        {
            top.RemoveAll();
            Tablasdefrancisco.MostrarReporteBalance(top);
        };
        creditosButton.Accepting += (s, e) =>
        {
            top.RemoveAll();
            Creditos.MostrarCreditos(top);
        };
        pasarturno.Accepting += (s, e) =>
        {
          
                ModificarPartidas.PasarTurno(top);
                
            
        };
        if (Program.pd.balance < -8000)
        {
            ManejoDeArchivos.PartidaPerdida = true;
            GameOver.VentanaGameOver("Perdiste por falta de dinero", Program.InvInt);
        }

        btInicio.SetFocus();
        ventana.Add(btInicio,BtTragamonedas,btInventario,btVerEmpresa,btMenu, pasarturno, btBalance, LabelTurno, creditosButton);
    }
  
}
