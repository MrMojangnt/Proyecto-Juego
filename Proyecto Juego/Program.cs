using NAudio.Wave;
using System.IO;
using Terminal.Gui;

class Program
{
    static bool save = false;
    static bool save2 = false;
    static bool save3 = false;
    static int colora = 0;
    static WaveOutEvent salidaAudio;
    static AudioFileReader audio;
    static bool reproduciendo = false;
    static bool muteado = false;
    static Window VentanaPrincipal;
    static List<string> Paises = new List<string>() { "Nicaragua (predeterminado)", "EE.UU.", "Japón", "China", "Alemania", "España" };
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
        

        VentanaPrincipal = new Window("Menu")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = colores[colora],
        };
        top.Add(VentanaPrincipal);


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
        //Agregar marcos a la list
        marcos.Add(marco);
        marcos.Add(marco2);
        marcos.Add(marcoconfig);
        marcos.Add(marcosalir);
        
        botonNuevaPartida.Enter += (_) =>
        {
            marco.ColorScheme = ColoreButtonSelected[colora];
        };
        botonCargarPartida.Enter += (_) =>
        {
            marco2.ColorScheme = ColoreButtonSelected[colora];
        };
        botonConfiguracion.Enter += (_) => 
        {
            marcoconfig.ColorScheme = ColoreButtonSelected[colora];
        };
        botonsalir.Enter += (_) =>
        {
            marcosalir.ColorScheme = ColoreButtonSelected[colora];
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
        botonNuevaPartida.Clicked += () =>
        {
            top.Remove(VentanaPrincipal);
            CreacionPersonaje(top);
        };
        botonCargarPartida.Clicked += () =>
        {
            top.Remove(VentanaPrincipal);
            CargarPartida(top);
        };
        botonConfiguracion.Clicked += () =>
        {
            top.Remove(VentanaPrincipal);
            Configuracion(top);
        };
        botonsalir.Clicked += () => Application.RequestStop();
        Application.Run();//Corre la ventana
    }

    static void VerificarSave()
    {
        if (File.Exists("save1.txt"))
        {
            save = true;
        }
        if (File.Exists("save2.txt"))
        {
            save2 = true;
        }
        if (File.Exists("save3.txt"))
        {
            save3 = true;
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

        var Back = new Button("Volver al Menú")
        {
            X = Pos.Center(),
            Y= 30
        };

        Back.Clicked += () =>
        {
            top.Remove(VentanaCargarPartida);
            top.Add(VentanaPrincipal);
        };

        VentanaCargarPartida.Add(Slot1, Slot2, Slot3, Back);

        //verificando que haya partida guardada en el slot 1
        if (save)
        {
            StreamReader save1 = new StreamReader("save1.txt");
            string linea = save1.ReadLine();
            linea = linea.Replace("Nombre: ", "");//reemplaza "Nombre" por ""
            save1.Close();

            Slot1.Add(new Label("Nombre:\n" +linea)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
        }
        else
        {

            Slot1.Add(new Label("No hay \ndatos guardados")
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
        }
        VerificarSave();
        //verificando que haya partida guardada en el slot 2
        if (save2)
        {
            StreamReader save_2 = new StreamReader("save2.txt");
            string linea = save_2.ReadLine();
            linea = linea.Replace("Nombre: ", "");
            save_2.Close();
            Slot2.Add(new Label("Nombre:\n" + linea)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
        }
        else
        {
            Slot2.Add(new Label("No hay \ndatos guardados")
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
        }

        //verificando que haya partida guardada en el slot 3
        if (save3)
        {
            StreamReader save_3 = new StreamReader("save3.txt");
            string linea = save_3.ReadLine();
            linea = linea.Replace("Nombre: ", "");
            save_3.Close();
            Slot3.Add(new Label("Nombre:\n" + linea)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
        }
        else
        {
            Slot3.Add(new Label("No hay \ndatos guardados")
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            });
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
                    "Solo puedes ingresar numeros enteros en las skills",
                    
                    
                    "Introduce nuevos datos");
            }
            //Comprobar pais
            //Comprobacion final
            if (nombre && Skills)
            {

                MessageBox.Query(
                    "Añadido",
                    "Introducido: " + casillaNombre.Text.ToString() + //Muestra un aviso, un mensaje
                    " - " + PaisSeleciconado + 
                    "\nCarisma: " + numero1 +
                "\nEconomía: " + numero2+
                "\nFiscalidades: " + numero3 +
                "\nCorrupción: " + numero4
                    , "Aceptar");//El programa informa que se ha introducido cierto nombre y cierta dirección       

                GuardarPartida(casillaNombre.Text.ToString(), PaisSeleciconado, numero1, numero2, numero3, numero4);
                top.Remove(VentanaCreacionPersonaje);//Cuando se pulsa el botón desaparece la ventana  
                top.Add(VentanaPrincipal);
            }
        };
        //() son funciones anónimas, todavía no se han creado funciones aparte

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
        top.Add(VentanaCreacionPersonaje);//Se agrega la ventana a la raíz
    }
    
    //intentando hacer un sistema de guardado de partidas
    static void CargarPartida()
    {
        StreamReader save = new StreamReader("save.txt");

    }
    //función para guardar partida
    static void GuardarPartida(string Nombre, string Pais, int n1, int n2, int n3, int n4)
    {
        VerificarSave();
        if (save == false)
        {
            StreamWriter save = new StreamWriter("save1.txt");
            save.WriteLine($"Nombre: {Nombre}");
            save.WriteLine($"País: {Pais}");
            save.WriteLine("    SKILLS      ");
            save.WriteLine($"Carisma: {n1}");
            save.WriteLine($"Economía: {n2}");
            save.WriteLine($"Fiscalidades: {n3}");
            save.WriteLine($"Corrupción: {n4}");

            save.Close();
        } else if(save2 == false)
        {
            StreamWriter save2 = new StreamWriter("save2.txt");
            save2.WriteLine($"Nombre: {Nombre}");
            save2.WriteLine($"País: {Pais}");
            save2.WriteLine("    SKILLS      ");
            save2.WriteLine($"Carisma: {n1}");
            save2.WriteLine($"Economía: {n2}");
            save2.WriteLine($"Fiscalidades: {n3}");
            save2.WriteLine($"Corrupción: {n4}");

            save2.Close();
        }else if(save3 == false)
        {
            StreamWriter save3 = new StreamWriter("save3.txt");
            save3.WriteLine($"Nombre: {Nombre}");
            save3.WriteLine($"País: {Pais}");
            save3.WriteLine("    SKILLS      ");
            save3.WriteLine($"Carisma: {n1}");
            save3.WriteLine($"Economía: {n2}");
            save3.WriteLine($"Fiscalidades: {n3}");
            save3.WriteLine($"Corrupción: {n4}");

            save3.Close();
        }
    }

    public void debbuger()
    {

    }

}