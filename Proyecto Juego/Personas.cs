using System;
using Terminal.Gui;
struct npc
{
    public String name;
    public int edad;
    public int carisma;
    public int economia;
    public int fiscalidad;
    public int corrupcion;
    public decimal balance;
}
public class avance
{
    public static string[] listaNombres = {
    "Alejandro García (H) [NIC]", "Sofía García (M) [USA]", "Mateo Martínez (H) [JPN]", "Valentina Martínez (M) [CHN]",
    "Lucas López (H) [DEU]", "Camila López (M) [ESP]", "Santiago González (H) [NIC]", "Isabella González (M) [USA]",
    "Daniel Pérez (H) [JPN]", "Valeria Pérez (M) [CHN]", "Sebastián Rodríguez (H) [DEU]", "Luciana Rodríguez (M) [ESP]",
    "Matías Fernández (H) [NIC]", "Mariana Fernández (M) [USA]", "Leonardo Gómez (H) [JPN]", "Victoria Gómez (M) [CHN]",
    "Diego Ruiz (H) [DEU]", "Martina Ruiz (M) [ESP]", "Joaquín Sánchez (H) [NIC]", "Emma Sánchez (M) [USA]",
    "Tomás Díaz (H) [JPN]", "Daniela Díaz (M) [CHN]", "Gabriel Torres (H) [DEU]", "Gabriela Torres (M) [ESP]",
    "Nicolás Ramírez (H) [NIC]", "Natalia Ramírez (M) [USA]", "Martín Flores (H) [JPN]", "Elena Flores (M) [CHN]",
    "Samuel Benítez (H) [DEU]", "Paula Benítez (M) [ESP]", "Andrés Herrera (H) [NIC]", "Andrea Herrera (M) [USA]",
    "Felipe Medina (H) [JPN]", "Renata Medina (M) [CHN]", "Ignacio Castro (H) [DEU]", "Julieta Castro (M) [ESP]",
    "Emilio Rojas (H) [NIC]", "Emilia Rojas (M) [USA]", "Julián Vargas (H) [JPN]", "Samantha Vargas (M) [CHN]",
    "Maximiliano Cruz (H) [DEU]", "Mía Cruz (M) [ESP]", "Benjamín Ortiz (H) [NIC]", "Antonia Ortiz (M) [USA]",
    "Carlos Silva (H) [JPN]", "María Silva (M) [CHN]", "Juan Morales (H) [DEU]", "Ana Morales (M) [ESP]",
    "Luis Romero (H) [NIC]", "Laura Romero (M) [USA]", "Pedro Ríos (H) [JPN]", "Carmen Ríos (M) [CHN]",
    "Pablo Méndez (H) [DEU]", "Patricia Méndez (M) [ESP]", "Fernando Acosta (H) [NIC]", "Clara Acosta (M) [USA]",
    "Ricardo Mendoza (H) [JPN]", "Rosa Mendoza (M) [CHN]", "Javier Peña (H) [DEU]", "Josefina Peña (M) [ESP]",
    "Francisco Cabrera (H) [NIC]", "Lucía Cabrera (M) [USA]", "Hugo Fuentes (H) [JPN]", "Sara Fuentes (M) [CHN]",
    "Víctor Aguilar (H) [DEU]", "Alicia Aguilar (M) [ESP]", "Mario Vega (H) [NIC]", "Beatriz Vega (M) [USA]",
    "Manuel Paredes (H) [JPN]", "Teresa Paredes (M) [CHN]", "Roberto Guzmán (H) [DEU]", "Julia Guzmán (M) [ESP]",
    "Eduardo Salazar (H) [NIC]", "Blanca Salazar (M) [USA]", "Jorge Carmona (H) [JPN]", "Gloria Carmona (M) [CHN]",
    "Héctor Soto (H) [DEU]", "Diana Soto (M) [ESP]", "Raúl Navarro (H) [NIC]", "Mónica Navarro (M) [USA]",
    "Óscar Pacheco (H) [JPN]", "Verónica Pacheco (M) [CHN]", "Arturo Castillo (H) [DEU]", "Carolina Castillo (M) [ESP]",
    "César Delgado (H) [NIC]", "Silvia Delgado (M) [USA]", "Iván Reyes (H) [JPN]", "Alejandra Reyes (M) [CHN]",
    "Marcos Varela (H) [DEU]", "Inés Varela (M) [ESP]", "Gonzalo Villanueva (H) [NIC]", "Ángela Villanueva (M) [USA]",
    "Simón Iglesias (H) [JPN]", "Miranda Iglesias (M) [CHN]", "Rodrigo Blanco (H) [DEU]", "Catalina Blanco (M) [ESP]",
    "Esteban Molina (H) [NIC]", "Luna Molina (M) [USA]", "David Cortés (H) [JPN]", "Alma Cortés (M) [CHN]"
    };

 
    public static void GenerarPersonas()
    {
        const int MAX = 100;
        npc[] pj = new npc[MAX];

        for (int i = 0; i < MAX; i++)
        {
            pj[i].name = listaNombres[i];
            pj[i].edad = Random.Shared.Next(28, 86);
            pj[i].carisma = Random.Shared.Next(1, 100);
            pj[i].economia = Random.Shared.Next(1, 100);
            pj[i].fiscalidad = Random.Shared.Next(1, 100);
            pj[i].corrupcion = Random.Shared.Next(1, 100);
            pj[i].balance = Random.Shared.Next(0, 99999999);
        }

        
    }
}

public class Tutorial
{
    public static void LLamadaIvancito(Window VentanaCualquiera)
    {
        npc Ivancito = new npc();
        Ivancito.name = "Ivancito";
        Ivancito.edad = 76;
        Ivancito.carisma = 50;
        Ivancito.economia = 50;
        Ivancito.fiscalidad = 50;
        Ivancito.corrupcion = 50;
        Ivancito.balance = 10000;

        Label[] llamando = new Label[3];

        var MarcoLlamada = new FrameView()
        {
            X = 100,
            Y = 15,
            Width = 50,
            Height = 80,
        };

        var telefono1 = new Label(@"                          .--.
                          |  |
                          |  |
                          |  |
                          |  |
         _.-----------._  |  |
      .-'      __       `-.  |
    .'       .'  `.        `.|
   ;         :    :          ;
   |         `.__.'          |
   |   ___                   |
   |  (_V_) V O T O R A L A  |
   | .---------------------. |
   | |                     | |
   | |   LLAMANDO          | |
   | |                     | |
   | |      Ivancito       | |
   | |                     | |")
        {
            X = 1,
            Y = 1
        };
        llamando[0] = new Label("| |      Llamando.      | |")
        {
            X = Pos.X(telefono1) + 1,
            Y = Pos.Bottom(telefono1)
        };
        llamando[1] = new Label("| |      Llamando..     | |")
        {
            X = Pos.X(telefono1) + 1,
            Y = Pos.Bottom(telefono1)
        };
        llamando[2] = new Label("| |      Llamando...    | |")
        {
            X = Pos.X(telefono1) + 1,
            Y = Pos.Bottom(telefono1)
        };
       
        int estado = 0;
        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(500), (_) =>
        {
            switch (estado)
            {
                case 0:
                    llamando[0].Visible = true;
                    llamando[1].Visible = false;
                    llamando[2].Visible = false;
                    break;
                case 1:
                    llamando[0].Visible = false;
                    llamando[1].Visible = true;
                    llamando[2].Visible = false;
                    break;
                case 2:
                    llamando[0].Visible = false;
                    llamando[1].Visible = false;
                    llamando[2].Visible = true;
                    break;
            }
            // cambia estado de label
            estado = (estado + 1) % 3;
            return true; // sigue ejecutándose
        });
        VentanaCualquiera.Add(MarcoLlamada);
        MarcoLlamada.Add(telefono1, llamando[0], llamando[1], llamando[2]);
    }


}