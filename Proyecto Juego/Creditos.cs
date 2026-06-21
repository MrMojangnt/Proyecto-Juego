namespace Proyecto_Juego;

using Terminal.Gui;

public class Creditos
{
    public static void MostrarCreditos(Toplevel top)
    {
        var ventana = new Window("Créditos")
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        var titulo = new Label("EQUIPO DE DESARROLLO")
        {
            X = Pos.Center(),
            Y = 1,
            TextAlignment = TextAlignment.Centered
        };

        var creditos = new Label("")
        {
            X = Pos.Center(),
            Y = 5,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        var btnCerrar = new Button("Cerrar")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        btnCerrar.Clicked += () =>
        {
            top.Remove(ventana);
            Program.Inicio(top);
        };

        ventana.Add(titulo, creditos, btnCerrar);

        string[] nombres =
        {
            "Raúl Antonio Castillo Zamora",
            "Jocksand Mateo Valladares Ramírez",
            "Dorian Zamir Martínez Lopez",
            "Kennely Maisha Ordóñez Blandón",
            "",
            "Mencion honorífica:",
            "Big Data Cluster",
            "",
            "Versión:",
            "1.0 Beta 2026"
        };

        int nombreActual = 0;
        int letraActual = 0;
        string textoMostrado = "";

        Application.MainLoop.AddTimeout(
            TimeSpan.FromMilliseconds(50),
            _ =>
            {
                if (nombreActual >= nombres.Length)
                {
                    titulo.Text = "¡GRACIAS POR JUGAR!";
                    ventana.SetNeedsDisplay();
                    return false;
                }

                string nombre = nombres[nombreActual];

                if (letraActual < nombre.Length)
                {
                    textoMostrado += nombre[letraActual];
                    letraActual++;
                }
                else
                {
                    textoMostrado += "\n\n";
                    nombreActual++;
                    letraActual = 0;
                }

                creditos.Text = textoMostrado;
                ventana.SetNeedsDisplay();

                return true;
            });

        top.Add(ventana);
    }
}