using Empresas;
using Proyecto_Juego;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using Terminal.Gui;
using static Terminal.Gui.TabView;
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
    "Dorian Martínez", "Jocksand Valladares", "Raúl Castillo", "Roberto Sobalvarro",
    "Daniel Pérez", "Carlos Cerda", "José René Bonilla", "Leonardo Gómez",
    "Diego Ruiz", "Joaquín Sánchez", "Daniel Marquez", "Gabriel Torres",
    "Nicolás Ramírez", "Martín Flores", "Samuel Benítez", "Andrés Herrera",
    "Felipe Medina", "Ignacio Castro", "Francisco Álvarez", "Francisco Silva",
    "Mauricio Lacayo", "Guillermo Ayerdis"
};

    public static string[] Mujeres =
    {
    "Sofía Martínez", "Alyssa Rodríguez", "Camila Navarro", "Isabella González",
    "Valeria Pérez", "Leah Dávila", "Mariana Fernández", "Victoria Gómez",
    "Marisa D'Trinidad", "Emma Sánchez", "Litzy Mendoza", "Gabriela Torres",
    "Natalia Ramírez", "Elena Flores", "Paula Benítez", "Andrea Herrera",
    "Renata Medina", "Julieta Castro", "Emilia Rojas", "Samantha Vargas",
    "Kenely Ordoñez", "Antonia Ortiz", "María Silva", "Ana Morales"
};

    static List<NPC> GenerarPersonas()//GENERA LOS NOMBRES, SUS HABILIDADES Y SU BALANCE
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
    public static void GuardarContactos(int i, bool zzz)//FUNCION QUE GUARDA LOS CONTACTOS
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

    public static List<NPC> CargarContactos(int indice)//FUNCION QUE CARGA LOS CONTACTOS
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

    public static void Contactos(List<ColorScheme> colores, int colora, Window VentanaInicio, List<NPC> Lista)
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("ID");
        tabla.Columns.Add("Nombre");

        //                ContactosCargados = GeneracionDeContactos.CargarContactos(index);

        TableView TablaContactos = new TableView()
        {
            X = 1,
            Y = Pos.Center(),
            Width = 25,
            Height = 8,
            ColorScheme = colores[colora]
        };

        foreach (NPC i in Program.ContactosCargados)
        {
            tabla.Rows.Add(
                i.name,
                i.sector_dominante
            );

        }

        TablaContactos.Table = tabla;
        var ContactosLabel = new Label("Contactos")
        {
            X = 4,
            Y = Pos.Top(TablaContactos)-1
        };
        
        var btContactar = new Button("Llamar a un contacto")
        {
            X = Pos.X(TablaContactos) + 2,
            Y = Pos.Bottom(TablaContactos)
        };
       //btContactar.Clicked += () => ContactarAUnContacto();

        VentanaInicio.Add(TablaContactos, ContactosLabel, btContactar);
    }
    static string LeerNombre(string dato, int i)
    {
        using (StreamReader save = new StreamReader(ManejoDeArchivos.contactos[i], Encoding.UTF8))
        {
            string linea = (save.ReadLine() ?? "");
            linea = linea.Replace("Nombre:", "");//reemplaza "Nombre" por ""
            return linea;
        }

    }
   /* static void ContactarAUnContacto()
    {
        var Llamar = new Dialog(
   "Escoge un contacto",
   60,
   20
);
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            string linea = LeerNombre("Nombre", index);
            SobreSlot[index] = new Button($"Nombre: {linea}")
            {
                X = 2,
                Y = i + 2
            };

            SobreSlot[index].Clicked += () =>
            {
                using (StreamWriter save = new StreamWriter(partidas[index]))
                {
                    save.WriteLine(pd.ToString());
                }
                using (StreamWriter save = new StreamWriter(inventario[index]))
                {
                    save.WriteLine(pd.name);
                }
                Guardarempresa(index, false);
                GeneracionDeContactos.GuardarContactos(index, false);
                Companiass = CargarEmpresa(index);
                ContactosCargados = GeneracionDeContactos.CargarContactos(index);

                Application.RequestStop();
                top.RemoveAll();
                Inicio(top);
            };

            Sobreescribir.Add(SobreSlot[index]);

        }

        var cancelar = new Button("Cancelar")
        {
            X = 20,
            Y = 6
        };
        cancelar.Clicked += () =>
        {
            Application.RequestStop();
            top.RemoveAll();
            top.Add(VentanaPrincipal);
        };
        Sobreescribir.Add(cancelar);
    }*/
}



