using Terminal.Gui;
using NAudio.Wave;

class Vestia
{
    static Label etiqNombre, etiqPais;
    static WaveOutEvent salidaAudio;
    static AudioFileReader audio;
    static bool reproduciendo = false;
    static bool muteado = false;

    static List<string> Paises = new List<string>() {"Nicaragua", "EE.UU.", "Japón", "China", "Alemania", "España" };
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

        var VentanaPrincipal = new Window("Registro")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
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
            X = 100,
            Y = 25
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
        var botonNuevaPartida = new Button("Nueva Partida")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        VentanaPrincipal.Add(botonNuevaPartida);
        botonNuevaPartida.Clicked += () =>  CreacionPersonaje(top);
  
        Application.Run();//Corre la ventana
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

        var Skill_Txt = new Label("Designar Skills:")
        {
            X = 2,
            Y = 11
        };
        VentanaCreacionPersonaje.Add(Skill_Txt);
        var Skills_in1 = new TextField("")
        {
            X = 2,
            Y = 13,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in1);
        var Skills_in2 = new TextField("")
        {
            X = 2,
            Y = 15,
            Width = 30
        }; 
        VentanaCreacionPersonaje.Add(Skills_in2);
        var Skills_in3 = new TextField("")
        {
            X = 2,
            Y = 17,
            Width = 30
        };
        VentanaCreacionPersonaje.Add(Skills_in3);
        var Skills_in4 = new TextField("")
        {
            X = 2,
            Y = 19,
            Width = 30  
        };
        VentanaCreacionPersonaje.Add(Skills_in4);
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
            int numero = 0;
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
            if  (int.TryParse(Skills_in1.Text.ToString(), out numero)&&int.TryParse(Skills_in2.Text.ToString(), out numero)&& int.TryParse(Skills_in3.Text.ToString(), out numero)&& int.TryParse(Skills_in4.Text.ToString(), out numero) && numero <= 30) {
                Skills = true;
            }
            else
            {
                MessageBox.Query(
                    "ERROR",
                    "Solo puedes ingresar numeros en los stats y una cantidad <= 30",
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

    public void debbuger()
    {
        
    }

}