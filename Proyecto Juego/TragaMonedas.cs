using System;
using Terminal.Gui;
using Terminal.Gui.Drawing;
using Terminal.Gui.Views;
using System.Globalization;
using Proyecto_Juego;
using Terminal.Gui.ViewBase;
using Terminal.Gui.App;
public static class TragaMonedas
{
    
    public static void Iniciar(IApplication app)
    {
        //Colores 
        var rojo = new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(Color.BrightRed, Color.Gray)
        };

        var verde = new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(Color.BrightGreen, Color.White)
        };

        var amarillo = new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(Color.BrightYellow, Color.Gray)
        };

        var azul = new Scheme()
        {
            Normal = new Terminal.Gui.Drawing.Attribute(Color.BrightBlue, Color.White)
        };
        Scheme[] colores =
        {
            rojo,
            verde,
            amarillo,
            azul
        };

        //Ventana
        var win = new Window()
            {
            Text = "Tragamonedas",
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Scheme = Program.colores[Program.colora]
            };

        
        string[] simbolos = { "9", "3", "6", "1", "7" };
        Random rnd = new Random();

        
        var rodillo1 = new Label()
        {
            Text = "?",
            X = Pos.Center() - 6,
            Y = Pos.Center()
        };
        
        var rodillo2 = new Label()
        {
            Text = "?",
            X = Pos.Center(),
            Y = Pos.Center()
        };

        var rodillo3 = new Label()
        {
            Text = "?",
            X = Pos.Center() + 6,
            Y = Pos.Center()
        };

        var FrameViewer = new FrameView()
        {
            X = Pos.Center(),
            Y = 2,
            Width = 21,
            Height = 3,
        };
        var resultado = new Label()
        {
            X = Pos.Center(),
            Y = 6
        };
        var dinero = new Label()
        {
            Text = "Dinero a apostar:",
            X = Pos.Center(),
            Y = 8
        };
        var cantidadApostar = new TextField()
        {
            X = Pos.Center(),
            Y = 9,
            Width = 11
        };
        cantidadApostar.TextChanging += (e) =>
        {
            if (e.NewText.Length > 7)
            {
                e.Cancel = true;
            }
        };

        var btnGirar = new Button()
        {
            Text = "Girar",
            X = Pos.Center() - 10,
            Y = 12
        };

        var btnCerrar = new Button()
        {
            Text = "Cerrar",
            X = Pos.Center() + 5,
            Y = 12
        };
        var DineroLabel = new Label()
        {
            Text = $"Balance de tu cuenta: {Program.pd.balance.ToString("N2", CultureInfo.InvariantCulture)}",
            X =Pos.Center(),
            Y = 14
        };
        btnGirar.Accepting += (s,e) =>
        {
            decimal cantidad;
            bool IsDecimal = false;
            if (decimal.TryParse(cantidadApostar.Text.ToString(), out cantidad))
            {
                IsDecimal = true;
            }

            if (IsDecimal && cantidad <= Program.pd.balance)
            {
                { 
                    int vueltas = 0;
                    int indice = 0;

                    Application.MainLoop.AddTimeout(
                        TimeSpan.FromMilliseconds(100),
                        (_) =>
                        {
                            btnGirar.Enabled = false;
                            rodillo1.Text = simbolos[rnd.Next(simbolos.Length)];
                            rodillo2.Text = simbolos[rnd.Next(simbolos.Length)];
                            rodillo3.Text = simbolos[rnd.Next(simbolos.Length)];
                            FrameViewer.ColorScheme = colores[indice];
                            indice++;
                            vueltas++;

                            if (indice >= colores.Length)
                            {
                                indice = 0;
                            }
                            if (vueltas >= 20)
                            {
                                string s1 = simbolos[rnd.Next(simbolos.Length)];
                                string s2 = simbolos[rnd.Next(simbolos.Length)];
                                string s3 = simbolos[rnd.Next(simbolos.Length)];

                                rodillo1.Text = s1;
                                rodillo2.Text = s2;
                                rodillo3.Text = s3;

                                if (s1 == s2 && s2 == s3)
                                {
                                    Program.pd.balance += cantidad * 3;
                                    resultado.Text = $" ¡JACKPOT! {cantidad * 4}";
                                    ModificarPartidas.Guardarelbalance();
                                    btnGirar.Enabled = true;
                                    DineroLabel.Text = $"Balance de tu cuenta: {Program.pd.balance.ToString("N2", CultureInfo.InvariantCulture)}";
                                }
                                else if (s1 == s2 || s1 == s3 || s2 == s3)
                                {
                                    Program.pd.balance += cantidad * 1.5m;
                                    resultado.Text = $" Premio pequeño de {cantidad * 1.5m}";
                                    ModificarPartidas.Guardarelbalance();
                                    btnGirar.Enabled = true;
                                    DineroLabel.Text = $"Balance de tu cuenta: {Program.pd.balance.ToString("N2", CultureInfo.InvariantCulture)}";
                                }
                                else
                                {
                                    Program.pd.balance -= cantidad;
                                    resultado.Text = " Inténtalo de nuevo";
                                    ModificarPartidas.Guardarelbalance();
                                    btnGirar.Enabled = true;
                                    DineroLabel.Text = $"Balance de tu cuenta: {Program.pd.balance.ToString("N2", CultureInfo.InvariantCulture)}";
                                }

                                return false;
                            }




                            return true;

                        });
                }
                
            }
            else if (IsDecimal == false)
            {
                MessageBox.Query(app,"Error", "No introduciste un valor valido", "Cerrar");
            }
            else
            {
                MessageBox.Query(app,"Error", "No tienes esa cantidad de dinero", "Cerrar");
            }
            dinero.Text = Program.pd.balance.ToString("N2", CultureInfo.InvariantCulture);
        };

        btnCerrar.Accepting += (s,e) =>
        {
            top.RemoveAll();
            Program.Inicio(top);
        };
        FrameViewer.Add(rodillo1,
            rodillo2,
            rodillo3);
        win.Add(
            resultado,
            btnGirar,
            btnCerrar,
            cantidadApostar,
            dinero,
            DineroLabel,
            FrameViewer
        );


        app.Run(win);
    }
    

}