using Empresas;
using Proyecto_Juego;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using Terminal.Gui;
using static Terminal.Gui.TabView;

public class GeneracionDeContactos
{
    public static string[] Hombres =
{
    "Dorian Martínez", "Jocksand Valladares", "Raúl Castillo", "Roberto Sobalvarro",
    "Daniel Pérez", "Carlos Cerda", "José René Bonilla", "Leonardo Gómez",
    "Diego Ruiz", "Joaquín Sánchez", "Daniel Marquez", "Gabriel Torres",
    "Nicolás Ramírez", "Martín Flores", "Samuel Benítez", "Jorge Ulloa", "David Ulloa",
    "Kevin Osejo", "Hugo López", "Francisco Álvarez", "Francisco Silva",
    "Mauricio Lacayo", "Guillermo Ayerdis"
};

    public static string[] Mujeres =
    {
    "Sofía Martínez", "Alyssa Rodríguez", "Camila Navarro", "Isabella González",
    "Valeria Pérez", "Leah Dávila", "Mariana Fernández", "Victoria Gómez", "Andrea Vasco",
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

            pj.sector_dominante = Indices.Nombre_Sectores_Variables.ElementAt(IndiceSector).Key;

            pj.balance = Random.Shared.Next(0, 100000);

            if (Personalidades.PersonalidadesFijas.TryGetValue(pj.name, out int id))
            {
                pj.rasgospersonalidad = Personalidades.Arqueotipos[id];
                pj.idArquetipo = id;
            }
            else
            {
                id = Random.Shared.Next(Personalidades.Arqueotipos.Length);
                pj.rasgospersonalidad =
                    Personalidades.Arqueotipos[Random.Shared.Next(Personalidades.Arqueotipos.Length)];
            }
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
                ContactosCargados.sector_dominante = lineas[3];
                ContactosCargados.balance = decimal.Parse(lineas[4]);

                ConNPC.Add(ContactosCargados);
            }

        }
        return ConNPC;
    }

    public static void Contactos(List<ColorScheme> colores, int colora, Window VentanaInicio, List<NPC> Lista)
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Nombre");
        tabla.Columns.Add("Sector");

        //                ContactosCargados = GeneracionDeContactos.CargarContactos(index);

        TableView TablaContactos = new TableView()
        {
            X = 1,
            Y = Pos.Center(),
            Width = 27,
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
        TablaContactos.CellActivated += (e) =>
        {
            int row = e.Row;

            var contactorancio = Program.ContactosCargados[row];
            ContactarAUnContacto(contactorancio);

        };
        TablaContactos.Table = tabla;
        var ContactosLabel = new Label("Contactos")
        {
            X = 4,
            Y = Pos.Top(TablaContactos)-1
        };
        
        

        VentanaInicio.Add(TablaContactos, ContactosLabel);
    }
 /*   static string LeerNombre(string dato, int i)
    {
        using (StreamReader save = new StreamReader(ManejoDeArchivos.contactos[i], Encoding.UTF8))
        {
            string linea = (save.ReadLine() ?? "");
            linea = linea.Replace("Nombre:", "");//reemplaza "Nombre" por ""
            return linea;
        }

    }*/
   static void ContactarAUnContacto(NPC contactos)
    {
        var Llamar = new Dialog($"{contactos.name}",
   60,
   20
);
        string sexo;
        if (contactos.masculino)
        {
            sexo = "Masculino";
        }
        else
        {
            sexo = "Femenino";
        }
        var DatosContacto = new Label(@$"Nombre: {contactos.name}
Edad: {contactos.edad}
Sexo: {sexo}
Sector Principal: {contactos.sector_dominante}
Balance: {contactos.balance}")
        {
            X = 1,
            Y = 1
        };

        var btPedirPrestamo = new Button("Pedir Préstamo")
        {
            X = 1,
            Y = 12
        };

        var btPedirConsejo = new Button("Pedir Consejo")
        {
            X = Pos.Right(btPedirPrestamo),
            Y = 12
        };
        var btLlamar = new Button("Charlar")
        {
            X = Pos.Right(btPedirConsejo),
            Y = 12
        };

        var btcancelar = new Button("Cancelar")
        {
            X = Pos.Center(),
            Y = 16
        };
        btPedirConsejo.Clicked += () =>
        {
            Dialogos_de_Contacto.ConsejosDeContacto();
        };
        btLlamar.Clicked += () =>
        {
            Dialogos_de_Contacto.DialogosCasuales();
        };
        btcancelar.Clicked += () =>
        {
            Application.RequestStop();
            
        };
        Llamar.Add(DatosContacto, btPedirPrestamo, btPedirConsejo, btLlamar, btcancelar);
        Application.Run(Llamar);


        
    }
}



