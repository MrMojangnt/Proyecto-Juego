namespace Proyecto_Juego;
using Terminal.Gui;

public class Trabajos
{
    //Trabajo de desencriptador
    // Arreglo 1: Dirección y magnitud del movimiento en el abecedario
    public static string[] desplazamientos = new string[]
    {
        "3 posiciones hacia la derecha (+3)",
        "1 posición hacia la izquierda (-1)",
        "5 posiciones hacia la derecha (+5)",
        "2 posiciones hacia la izquierda (-2)",
        "4 posiciones hacia la derecha (+4)",
        "2 posiciones hacia la derecha (+2)",
        "3 posiciones hacia la izquierda (-3)",
        "1 posición hacia la derecha (+1)",
        "6 posiciones hacia la derecha (+6)",
        "4 posiciones hacia la izquierda (-4)",
        "7 posiciones hacia la derecha (+7)",
        "5 posiciones hacia la izquierda (-5)",
        "8 posiciones hacia la derecha (+8)",
        "3 posiciones hacia la derecha (+3)",
        "2 posiciones hacia la izquierda (-2)"
    };

// Arreglo 2: Texto cifrado (El mensaje oculto)
    public static string[] palabrasCifradas = new string[]
    {
        "FHVDU",
        "AZMMDQ",
        "QQFAJ",
        "QCACPCRM",
        "LSPE",
        "OWPFQ",
        "QBUQL",
        "APSSP",
        "JGZUY",
        "IWPNET",
        "LUPNTH",
        "VGAV",
        "BWSMV",
        "ELWFXLQ",
        "AMFCRC"
    };

// Arreglo 3: Texto descifrado (El mensaje original)
    public static string[] palabrasDescifradas = new string[]
    {
        "CESAR",
        "BANNER",
        "LLAVE",
        "SECRETO",
        "HOLA",
        "MUNDO",
        "TEXTO",
        "ZORRO",
        "DATOS",
        "MATRIX",
        "ENIGMA",
        "ALFA",
        "TOKEN",
        "BITCOIN",
        "COHETE"
    };

// Arreglo 1: Codigo a digitar (sin acentos y sin barras)
    public static string[] codigoADigitar = new string[]
    {
        """Console.WriteLine("Bienvenido al sistema.");""",
        """Console.WriteLine("Cargando datos, por favor espere...");""",
        """Console.WriteLine("Tu contraseña ha sido actualizada con exito.");""",
        """Console.WriteLine("Error: Conexion perdida con el servidor.");""",
        """Console.WriteLine("Sesion iniciada correctamente.");""",
        """Console.WriteLine("El archivo se descargo de manera exitosa.");""",
        """Console.WriteLine("¿Estas seguro de que deseas salir?");""",
        """Console.WriteLine("Operacion cancelada por el usuario.");""",
        """Console.WriteLine("Tienes 3 notificaciones nuevas sin leer.");""",
        """Console.WriteLine("Tu saldo actual es de $0.00.");""",
        """Console.WriteLine("Gracias por su compra, vuelva pronto.");""",
        """Console.WriteLine("Nueva actualizacion de software disponible.");""",
        """Console.WriteLine("El correo electronico ingresado no es valido.");""",
        """Console.WriteLine("Guardando los cambios de forma automatica...");""",
        """Console.WriteLine("¡Felicidades! Has completado el registro.");""",
        """Console.WriteLine("Codigo de verificacion enviado a tu telefono.");""",
        """Console.WriteLine("Buscando actualizaciones disponibles...");""",
        """Console.WriteLine("No se encontraron resultados para tu busqueda.");""",
        """Console.WriteLine("La configuracion se guardo correctamente.");""",
        """Console.WriteLine("Cerrando sesion de forma segura... Adios.");"""
    };

// Arreglo 2: El texto literal que se imprimira en la consola
    public static string[] resultadosEsperados = new string[]
    {
        "Bienvenido al sistema.",
        "Cargando datos, por favor espere...",
        "Tu contraseña ha sido actualizada con exito.",
        "Error: Conexion perdida con el servidor.",
        "Sesion iniciada correctamente.",
        "El archivo se descargo de manera exitosa.",
        "¿Estas seguro de que deseas salir?",
        "Operacion cancelada por el usuario.",
        "Tienes 3 notificaciones nuevas sin leer.",
        "Tu saldo actual es de $0.00.",
        "Gracias por su compra, vuelva pronto.",
        "Nueva actualizacion de software disponible.",
        "El correo electronico ingresado no es valido.",
        "Guardando los cambios de forma automatica...",
        "¡Felicidades! Has completado el registro.",
        "Codigo de verificacion enviado a tu telefono.",
        "Buscando actualizaciones disponibles...",
        "No se encontraron resultados para tu busqueda.",
        "La configuracion se guardo correctamente.",
        "Cerrando sesion de forma segura... Adios."
    };

    public static void Desencriptador(Toplevel top)
    {

        var dialog = new Dialog("Trabajo de desencriptador", 60, 15);
        Random rnd = new Random();
        int numEvento = rnd.Next(desplazamientos.Length);
        string desplazamientotitle = desplazamientos[numEvento];
        string PalabraCifrada = palabrasCifradas[numEvento];

        var titulo = new Label(desplazamientotitle)
        {
            X = Pos.Center(),
            Y = 1
        };
        var descripcion = new Label(PalabraCifrada)
        {
            X = Pos.Center(),
            Y = 3
        };
        var Inputs = new TextField()
        {
            X = Pos.Center(),
            Y = 4,
            Width = 30
        };
        var buttonEnviar = new Button("Enviar")
        {
            X = Pos.Center(),
            Y = 6
        };
        var buttonSalir = new Button("Salir")
        {
            X = Pos.Center(),
            Y = 8
        };
        buttonSalir.Clicked += () =>
        {
            Application.RequestStop();
            top.RemoveAll();
            Program.Inicio(top);
        };
        buttonEnviar.Clicked += () =>
        {
            if (Inputs.Text == palabrasDescifradas[numEvento])
            {
                decimal dineroganar = rnd.Next(0, 2001);
                Program.pd.balance += dineroganar;
                MessageBox.Query("Completado!", "Completaste el trabajo", "Cerrar");
                Application.RequestStop();
                top.RemoveAll();
                Program.Inicio(top);

            }
            else
            {
                MessageBox.Query("Error", "Error esa no es la palabra", "Cerrar");
            }
        };

        dialog.Add(titulo, descripcion, Inputs, buttonEnviar, buttonSalir);
        Application.Run(dialog);
    }

    public static void Programador(Toplevel top)
    {
        var dialog = new Dialog("Trabajo de Programador", 60, 15);
        Random rnd = new Random();
        int numEvento = rnd.Next(codigoADigitar.Length);
        string resultadosEsperadoTitle = resultadosEsperados[numEvento];
        string InputEsperado = codigoADigitar[numEvento];

        var titulo = new Label("Imprime la siguiente palabra con C#:")
        {
            X = Pos.Center(),
            Y = 1
        };
        var descripcion = new Label(resultadosEsperadoTitle)
        {
            X = Pos.Center(),
            Y = 3
        };
        var Inputs = new TextField()
        {
            X = Pos.Center(),
            Y = 4,
            Width = 30
        };
        var buttonEnviar = new Button("Enviar")
        {
            X = Pos.Center(),
            Y = 6
        };
        var buttonSalir = new Button("Salir")
        {
            X = Pos.Center(),
            Y = 8
        };
        buttonEnviar.Clicked += () =>
        {
            if (Inputs.Text == InputEsperado)
            {
                decimal dineroganar = rnd.Next(0, 2001);
                Program.pd.balance += dineroganar;
                MessageBox.Query("Completado!", "Completaste el trabajo", "Cerrar");
                Application.RequestStop();
                top.RemoveAll();
                Program.Inicio(top);
            }
            else
            {
                MessageBox.Query("Error", "Error ese no es el codigo", "Cerrar");
            }
        };
        buttonSalir.Clicked += () =>
        {
            Application.RequestStop(dialog);
            top.RemoveAll();
            Program.Inicio(top);
        };
        dialog.Add(titulo, descripcion, Inputs, buttonEnviar, buttonSalir);
        Application.Run(dialog);

    }
}