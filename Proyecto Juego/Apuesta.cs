using Proyecto_Juego;
using Terminal.Gui;
public static class Apuesta
{
    public static string[] kavayosColores = { "Rojo", "Amarillo", "Verde", "Negro" };
    public static void Iniciar(Toplevel top)
    {
        //Colores de los caballos
        var rojo = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.BrightRed)
        };

        var amarillo = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.BrightYellow)
        };

        var verde = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.BrightGreen)
        };

        var negro = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Black)
        };
        var win = new Window("Carrera de Caballos")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Program.colores[Program.colora]
        };

        Label[] caballos = new Label[4];
        int[] posiciones = new int[4];

        for (int i = 0; i < 4; i++)
        {
            caballos[i] = new Label($"Kavayo {i + 1}")
            {
                X = 1,
                Y = i + 2
            };
            //Asignar 
            switch (i)
            {
                case 0:
                    caballos[i].ColorScheme = rojo;
                    break;

                case 1:
                    caballos[i].ColorScheme = amarillo;
                    break;

                case 2:
                    caballos[i].ColorScheme = verde;
                    break;

                case 3:
                    caballos[i].ColorScheme = negro;
                    break;
            }
            win.Add(caballos[i]);
        }

        var meta = new Label("META")
        {
            X = 60,
            Y = 0
        };

        win.Add(meta);

        Application.Top.Add(win);

        Random rnd = new Random();

        Application.MainLoop.AddTimeout(
            TimeSpan.FromMilliseconds(100),
            (_) =>
            {
                for (int i = 0; i < caballos.Length; i++)
                {
                    // Avanza entre 0 y 3 posiciones
                    posiciones[i] += rnd.Next(0, 4);

                    caballos[i].X = posiciones[i];

                    if (posiciones[i] >= 55)
                    {
                        MessageBox.Query(
                            "Carrera terminada",
                            $"¡Ganó el Kavayo {kavayosColores[i]}!",
                            "Aceptar"
                        );
                        if (Events.kavayo == i)
                        {
                            Program.pd.balance += Events.cantidad * 4;
                        }
                        else
                        {
                            Program.pd.balance -= Events.cantidad;
                        }

                        return false; // Detiene el temporizador
                    }
                }

                win.SetNeedsDisplay();


                return true; // Continúa la carrera
            });
        var cerrarbutton = new Button("Cerrar")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        cerrarbutton.Clicked += () =>
        {
            top.RemoveAll();
            Program.Inicio(top);
        };
        win.Add(cerrarbutton);
    }
}