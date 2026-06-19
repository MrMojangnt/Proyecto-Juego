using Terminal.Gui;
public static class Apuesta
{
    public static void Iniciar(Toplevel top)
    {
        var win = new Window("Carrera de Caballos")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
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
                            $"¡Ganó el Kavayo #{i + 1}!",
                            "Aceptar"
                        );

                        return false; // Detiene el temporizador
                    }
                }

                win.SetNeedsDisplay();

                return true; // Continúa la carrera
            });
    }
}