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
        
        dialog.Add(titulo, descripcion, Inputs, buttonEnviar);
        Application.Run(dialog);
    }

    public static void Programador(Toplevel top)
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

    }
}