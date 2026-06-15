using Empresas;
using Proyecto_Juego;
using System;
using System.Drawing;
using System.Text;
using Terminal.Gui;
public struct NPC
{
    public String name;
    public bool masculino;
    public int edad;
    public int carisma;
    public int economia;
    public int fiscalidad;
    public int corrupcion;
    public string sector_dominante; //pues te va a ayudar mas si invertis en empresas de tal sector
    public decimal balance;

    public override string ToString()
    {
        return $"{name}; {masculino}; {edad}; {carisma}; {economia}; {fiscalidad}; {corrupcion}; {sector_dominante}; {balance}";
    }
}
public class GeneracionDeContactos
{
    public static string[] Hombres =
{
    "Alejandro García", "Mateo Martínez", "Lucas López", "Santiago González",
    "Daniel Pérez", "Sebastián Rodríguez", "Matías Fernández", "Leonardo Gómez",
    "Diego Ruiz", "Joaquín Sánchez", "Tomás Díaz", "Gabriel Torres",
    "Nicolás Ramírez", "Martín Flores", "Samuel Benítez", "Andrés Herrera",
    "Felipe Medina", "Ignacio Castro", "Emilio Rojas", "Julián Vargas",
    "Esteban Molina", "David Cortés"
};

    public static string[] Mujeres =
    {
    "Sofía García", "Valentina Martínez", "Camila López", "Isabella González",
    "Valeria Pérez", "Luciana Rodríguez", "Mariana Fernández", "Victoria Gómez",
    "Martina Ruiz", "Emma Sánchez", "Daniela Díaz", "Gabriela Torres",
    "Natalia Ramírez", "Elena Flores", "Paula Benítez", "Andrea Herrera",
    "Renata Medina", "Julieta Castro", "Emilia Rojas", "Samantha Vargas",
    "Mía Cruz", "Antonia Ortiz", "María Silva", "Ana Morales"
};

    static List<NPC> GenerarPersonas()
    {
        NPC pj = new NPC();
        const int MAX = 4;
        List<NPC> ContactoshStruct = new List<NPC>();
        List<string> HombresTemp = new(Hombres.ToList());
        List<string> MujeresTemp = new(Mujeres.ToList());
        int index;
        int IndiceSector;
        bool sexo; //0 si es mujer, 1 si es hombre

        for (int i = 0; i < MAX; i++)
        {
            sexo = Random.Shared.Next(2) == 1;
            if (!sexo)
            {
                index = Random.Shared.Next(0, MujeresTemp.Count);
                pj.name = MujeresTemp[index];
                MujeresTemp.RemoveAt(index);
            }
            else
            {
                index = Random.Shared.Next(0, HombresTemp.Count);
                pj.name = HombresTemp[index];
                HombresTemp.RemoveAt(index);
            }

            IndiceSector = Random.Shared.Next(0,Indices.Sectores.Length);
            pj.masculino = sexo;
            pj.edad = Random.Shared.Next(28, 86);
            pj.carisma = Random.Shared.Next(1, 100);
            pj.economia = Random.Shared.Next(1, 100);
            pj.fiscalidad = Random.Shared.Next(1, 100);
            pj.corrupcion = Random.Shared.Next(1, 100);

            pj.sector_dominante = Indices.Nombre_Sectores_Variables.ElementAt(IndiceSector).Key;

            pj.balance = Random.Shared.Next(0, 100000);

            ContactoshStruct.Add(pj);
   
        }

        return ContactoshStruct;
    }
    public static void GuardarContactos(int i, bool zzz)
    {
        List<NPC> ContactosDelJugador = new(GenerarPersonas());

        using (StreamWriter Contac = new StreamWriter(ManejoDeArchivos.contactos[i], zzz, Encoding.UTF8))
        {
            Contac.WriteLine("Nombre; Sexo; Edad; Carisma; Economía; Fiscalidad; Corrupción; Sector; Balance");
            for (int p = 0; p < ContactosDelJugador.Count; p++)
            {
                Contac.WriteLine(ContactosDelJugador[p]);

            }


        }
    }

    public static List<NPC> CargarContactos(int indice)
    {
        List<NPC> ConNPC = new List<NPC>();
        NPC ContactosCargados = new NPC(); //structttttttttttt
        char[] delimitadores = { ';', '\n','\r' };
        using (StreamReader savecontactos = new StreamReader(ManejoDeArchivos.contactos[indice], Encoding.UTF8))
        {
            string[] encabezados = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            while (!savecontactos.EndOfStream)
            {
                string[] lineas = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
                ContactosCargados.name = lineas[0];
                ContactosCargados.masculino = bool.Parse(lineas[1]);
                ContactosCargados.edad = int.Parse(lineas[2]);
                ContactosCargados.carisma = int.Parse(lineas[3]);
                ContactosCargados.economia = int.Parse(lineas[4]);
                ContactosCargados.fiscalidad = int.Parse(lineas[5]);              
                ContactosCargados.corrupcion = int.Parse(lineas[6]);
                ContactosCargados.sector_dominante = lineas[7];
                ContactosCargados.balance = decimal.Parse(lineas[8]);

                ConNPC.Add(ContactosCargados);
            }

        }
        return ConNPC;
    }

    public void Contactos(ColorScheme[] colores, int colora, Window VentanaInicio)
    {
        var FrameContactos = new FrameView()
        {
            X = 1,
            Y = Pos.Center(),
            Width = 20,
            Height = 18,
            ColorScheme = colores[colora]
        };
        var ContactosLabel = new Label("Contactos")
        {
            X = Pos.Center(),
            Y = 0
        };
        var Contactositos = new Label()//Los nombres de los contactos
        {

        };

        VentanaInicio.Add(FrameContactos, ContactosLabel);
    }
}

