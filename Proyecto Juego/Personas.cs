using Empresas;
using Proyecto_Juego;
using System.Linq;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;
using Terminal.Gui;
using static Terminal.Gui.TabView;

public class GeneracionDeContactos
{
    public static string[] Hombres =
{
    "Dorian Martínez", "Roberto Sobalvarro",
    "Daniel Pérez", "Carlos Cerda", "José René Bonilla", "Leonardo Gómez",
    "Diego Ruiz", "Joaquín Sánchez", "Elon Mush", "Gabriel Torres",
    "Steve Unemployment", "Martín Flores", "Samuel Benítez", "Jorge Ulloa", "David Ulloa",
    "Kevin Osejo", "Hugo López", "Francisco Silva",
    "Mauricio Lacayo", "Guillermo Ayerdis"
};

    public static string[] Mujeres =
    {
    "Sofía Martínez", "Alyssa Rodríguez", "Melanie Perkins", "Isabella González",
    "Valeria Pérez", "Leah Dávila", "Mariana Fernández", "Victoria Gómez", "Andrea Vasco",
    "Marisa D'Trinidad", "Emma Sánchez", "Elaine Miranda", "Gabriela Torres",
    "Natalia Ramírez", "Elena Flores", "Paula Benítez", "Andrea Herrera",
    "Renata Medina", "Julieta Castro", "Emilia Rojas", "Samantha Vargas",
    "Kenely Ordoñez", "Antonia Ortiz", "María Silva", "Ana Morales"
};

    // Hacer público y asegurar que se cree una instancia nueva por cada contacto
    public static List<NPC> GenerarPersonas()//GENERA LOS NOMBRES, SUS HABILIDADES Y SU BALANCE
    {
        const int MAX = 4;
        List<NPC> ContactoshStruct = new List<NPC>();
        List<string> HombresTemp = Hombres.ToList();// aqui habia new, por si acaso llegan a repetirse contactos eso borré
        List<string> MujeresTemp = Mujeres.ToList();
        int index;
        int IndiceSector;
        bool sexo; //0 si es mujer, 1 si es hombre

        for (int i = 0; i < MAX; i++)
        {
            NPC pj = new NPC(); // <-- nueva instancia cada iteración

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

            IndiceSector = Random.Shared.Next(0, Indices.Sectores.Length);
            pj.masculino = sexo;
            pj.edad = Random.Shared.Next(28, 86);
            pj.sector_dominante = Indices.Nombre_Sectores_Variables.ElementAt(IndiceSector).Key;
            pj.balance = Random.Shared.Next(0, 100000);

            if (Personalidades.PersonalidadesFijas.TryGetValue(pj.name, out int id))
            {
                pj.idArquetipo = id;
            }
            else
            {
              
                id = Random.Shared.Next(Personalidades.Arqueotipos.Length - 2);
                pj.idArquetipo = id;
            }
            pj.Amistad = RandomizadorControladoDeAmistad(pj);
            pj.UltimoTurnoLlamado = -1;
            ContactoshStruct.Add(pj);
   
        }

        return ContactoshStruct;
    }
    static sbyte RandomizadorControladoDeAmistad(NPC pj)
    {

        switch (pj.idArquetipo)
        {
            case 0:
                pj.Amistad = (sbyte)Random.Shared.Next(5, 11);
                break;

            case >= 1 and <= 5:
                pj.Amistad = (sbyte)Random.Shared.Next(-4, 4);
                break;

            case 6:
                pj.Amistad = (sbyte)Random.Shared.Next(-10, 1);
                break;

            case 7:
                pj.Amistad = (sbyte)Random.Shared.Next(-5, 10);
                break;

            case >= 8 and <= 9:
                pj.Amistad = (sbyte)Random.Shared.Next(0, 6);
                break;

            default:
                pj.Amistad = 0;
                break;
        }
        return pj.Amistad;
    }
    public static void GuardarContactos(int i, bool zzz)//FUNCION QUE GUARDA LOS CONTACTOS
    {
        // Guardar el estado actual de los contactos, no generar nuevos cada vez.
        List<NPC> ContactosDelJugador = Program.ContactosCargados;

        using (StreamWriter Contac = new StreamWriter(ManejoDeArchivos.contactos[i], zzz, Encoding.UTF8))
        {
            Contac.WriteLine("Nombre;Sexo;Edad;Sector;Balance;idArquetipo;Amistad;UltimoTurnoLlamado;TienePrestamoActivo;UltimoTurnoPrestamo;PresionActual;montoprestado");
            for (int p = 0; p < ContactosDelJugador.Count; p++)
            {
                Contac.WriteLine(ContactosDelJugador[p]);
            }
        }
    }

    public static List<NPC> CargarContactos(int indice)//FUNCION QUE CARGA LOS CONTACTOS
    {
        List<NPC> ConNPC = new List<NPC>();
        NPC ContactosCargados = new NPC();
        char[] delimitadores = { ';', '\n','\r' };

        using (StreamReader savecontactos = new StreamReader(ManejoDeArchivos.contactos[indice], Encoding.UTF8))
        {
            string[] encabezados = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            while (!savecontactos.EndOfStream)
            {
                string[] lineas = savecontactos.ReadLine()!.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

                int len = lineas.Length;
                ContactosCargados.name = len > 0 ? lineas[0] : string.Empty;
                ContactosCargados.masculino = len > 1 && bool.TryParse(lineas[1], out bool m) ? m : false;
                ContactosCargados.edad = len > 2 && int.TryParse(lineas[2], out int e) ? e : 0;
                ContactosCargados.sector_dominante = len > 3 ? lineas[3] : string.Empty;
                ContactosCargados.balance = len > 4 && decimal.TryParse(lineas[4], out decimal b) ? b : 0m;
                ContactosCargados.idArquetipo = len > 5 && int.TryParse(lineas[5], out int id) ? id : 0;
                ContactosCargados.Amistad = len > 6 && sbyte.TryParse(lineas[6], out sbyte a) ? a : (sbyte)0;
                ContactosCargados.UltimoTurnoLlamado = len > 7 && int.TryParse(lineas[7], out int utl) ? utl : -1;
                ContactosCargados.TienePrestamoActivo = len > 8 && bool.TryParse(lineas[8], out bool tpa) ? tpa : false;
                ContactosCargados.UltimoTurnoPrestamo = len > 9 && int.TryParse(lineas[9], out int utp) ? utp : -1;
                ContactosCargados.PresionActual = len > 10 && int.TryParse(lineas[10], out int pa) ? pa : 0;
                ContactosCargados.montoprestado = len > 11 && decimal.TryParse(lineas[11], out decimal mp) ? mp : 0m;

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

        TableView TablaContactos = new TableView()
        {
            X = 1,
            Y = Pos.Center(),
            Width = 27,
            Height = 8,
            ColorScheme = colores[colora]
        };

        // Lista que vincula cada FILA de la tabla con: o un NPC normal, o un legendario.
        // null en LegendarioEnFila[i] significa que esa fila es un NPC normal de Program.ContactosCargados[i]
        var legendarioEnFila = new List<ContactoLegendarioBase>();

        foreach (NPC i in Program.ContactosCargados)
        {
            tabla.Rows.Add(i.name, i.sector_dominante);
            legendarioEnFila.Add(null);
        }

        // ── Probabilidad baja de que aparezca UN legendario esta vez ──
        int probabilidad = 5;
        if (Random.Shared.Next(1, 101) <= probabilidad)
        {
            var legendariosDisponibles = ContactosLegendariosMenu.Contactos
                .Where(l => l.PuedeUsarse)
                .ToArray();

            if (legendariosDisponibles.Length > 0)
            {
                var elegido = legendariosDisponibles[Random.Shared.Next(legendariosDisponibles.Length)];
                tabla.Rows.Add(elegido.Nombre, elegido.Rol);
                legendarioEnFila.Add(elegido);
            }
        }

        TablaContactos.CellActivated += (e) =>
        {
            int row = e.Row;
            if (row < 0 || row >= legendarioEnFila.Count) return;

            var legendario = legendarioEnFila[row];
            if (legendario != null)
            {
                if (legendario.PuedeUsarse)
                    legendario.Ejecutar();
                else
                    MessageBox.Query("Aviso", $"{legendario.Nombre} ya no tiene más ayudas disponibles.", "Aceptar");
            }
            else
            {
                ContactarAUnContacto(row);   
            }
        };

        TablaContactos.Table = tabla;
        var ContactosLabel = new Label("Contactos")
        {
            X = 4,
            Y = Pos.Top(TablaContactos) - 1
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
   static void ContactarAUnContacto(int indice)
    {
        NPC contactos = Program.ContactosCargados[indice];
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


        var btcancelar = new Button("X")
        {
            X = Pos.AnchorEnd(6),
            Y = 1
        };

        var btLlamar = new Button("Llamar")
        {
            X = Pos.Center(),
            Y = 16
        };

        bool iniciarLlamada = false;

        btLlamar.Clicked += () =>
        {
            iniciarLlamada = true;
            Application.RequestStop();
        };

        btcancelar.Clicked += () =>
        {
            Application.RequestStop();
        };
        Llamar.Add(DatosContacto, btLlamar, btcancelar);
        Application.Run(Llamar);

        if (iniciarLlamada)
        {
            if (contactos.UltimoTurnoLlamado == Program.turno)
            {
                MessageBox.Query(
                    "Llamada",
                    "Ya hablaste con esta persona este turno.",
                    "Aceptar");
                return;
            }

            LaLlamada.Llamar(contactos);

           
            contactos.UltimoTurnoLlamado = Program.turno;
            Program.ContactosCargados[indice] = contactos;
        }



    }
}

