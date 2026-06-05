using System;

class avance
{
    struct npc
    {
        public String name;
        public int edad;
        public double corup;
        public double happy;
        public double money;
    }

    public static string[] listaNombres = {
        "Alejandro García (H) [MEX]", "Sofía García (M) [ESP]", "Mateo Martínez (H) [ARG]", "Valentina Martínez (M) [COL]",
        "Lucas López (H) [CHL]", "Camila López (M) [PER]", "Santiago González (H) [VEN]", "Isabella González (M) [ECU]",
        "Daniel Pérez (H) [GUA]", "Valeria Pérez (M) [CUB]", "Sebastián Rodríguez (H) [MEX]", "Luciana Rodríguez (M) [ESP]",
        "Matías Fernández (H) [ARG]", "Mariana Fernández (M) [COL]", "Leonardo Gómez (H) [CHL]", "Victoria Gómez (M) [PER]",
        "Diego Ruiz (H) [VEN]", "Martina Ruiz (M) [ECU]", "Joaquín Sánchez (H) [GUA]", "Emma Sánchez (M) [CUB]",
        "Tomás Díaz (H) [MEX]", "Daniela Díaz (M) [ESP]", "Gabriel Torres (H) [ARG]", "Gabriela Torres (M) [COL]",
        "Nicolás Ramírez (H) [CHL]", "Natalia Ramírez (M) [PER]", "Martín Flores (H) [VEN]", "Elena Flores (M) [ECU]",
        "Samuel Benítez (H) [GUA]", "Paula Benítez (M) [CUB]", "Andrés Herrera (H) [MEX]", "Andrea Herrera (M) [ESP]",
        "Felipe Medina (H) [ARG]", "Renata Medina (M) [COL]", "Ignacio Castro (H) [CHL]", "Julieta Castro (M) [PER]",
        "Emilio Rojas (H) [VEN]", "Emilia Rojas (M) [ECU]", "Julián Vargas (H) [GUA]", "Samantha Vargas (M) [CUB]",
        "Maximiliano Cruz (H) [MEX]", "Mía Cruz (M) [ESP]", "Benjamín Ortiz (H) [ARG]", "Antonia Ortiz (M) [COL]",
        "Carlos Silva (H) [CHL]", "María Silva (M) [PER]", "Juan Morales (H) [VEN]", "Ana Morales (M) [ECU]",
        "Luis Romero (H) [GUA]", "Laura Romero (M) [CUB]", "Pedro Ríos (H) [MEX]", "Carmen Ríos (M) [ESP]",
        "Pablo Méndez (H) [ARG]", "Patricia Méndez (M) [COL]", "Fernando Acosta (H) [CHL]", "Clara Acosta (M) [PER]",
        "Ricardo Mendoza (H) [VEN]", "Rosa Mendoza (M) [ECU]", "Javier Peña (H) [GUA]", "Josefina Peña (M) [CUB]",
        "Francisco Cabrera (H) [MEX]", "Lucía Cabrera (M) [ESP]", "Hugo Fuentes (H) [ARG]", "Sara Fuentes (M) [COL]",
        "Víctor Aguilar (H) [CHL]", "Alicia Aguilar (M) [PER]", "Mario Vega (H) [VEN]", "Beatriz Vega (M) [ECU]",
        "Manuel Paredes (H) [GUA]", "Teresa Paredes (M) [CUB]", "Roberto Guzmán (H) [MEX]", "Julia Guzmán (M) [ESP]",
        "Eduardo Salazar (H) [ARG]", "Blanca Salazar (M) [COL]", "Jorge Carmona (H) [CHL]", "Gloria Carmona (M) [PER]",
        "Héctor Soto (H) [VEN]", "Diana Soto (M) [ECU]", "Raúl Navarro (H) [GUA]", "Mónica Navarro (M) [CUB]",
        "Óscar Pacheco (H) [MEX]", "Verónica Pacheco (M) [ESP]", "Arturo Castillo (H) [ARG]", "Carolina Castillo (M) [COL]",
        "César Delgado (H) [CHL]", "Silvia Delgado (M) [PER]", "Iván Reyes (H) [VEN]", "Alejandra Reyes (M) [ECU]",
        "Marcos Varela (H) [GUA]", "Inés Varela (M) [CUB]", "Gonzalo Villanueva (H) [MEX]", "Ángela Villanueva (M) [ESP]",
        "Simón Iglesias (H) [ARG]", "Miranda Iglesias (M) [COL]", "Rodrigo Blanco (H) [CHL]", "Catalina Blanco (M) [PER]",
        "Esteban Molina (H) [VEN]", "Luna Molina (M) [ECU]", "David Cortés (H) [GUA]", "Alma Cortés (M) [CUB]"
    };

    static void six()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        const int MAX = 100;
        npc[] pj = new npc[MAX];

        for (int i = 0; i < MAX; i++)
        {
            pj[i].name = listaNombres[i];
            pj[i].edad = Random.Shared.Next(28, 86);
            pj[i].corup = Random.Shared.Next(1, 100);
            pj[i].happy = Random.Shared.Next(1, 100);
            pj[i].money = Random.Shared.Next(0, 99999999);
        }

        Console.WriteLine("=== REGISTRO DE EMPRESARIOS ===\n");
        for (int i = 0; i < MAX; i++)
        {
            Console.WriteLine($"Empresari@: {pj[i].name}");
            Console.WriteLine($"Edad:       {pj[i].edad} años");
            Console.WriteLine($"Corrupción: {pj[i].corup}");
            Console.WriteLine($"Integridad: {pj[i].happy}");
            Console.WriteLine($"Dinero:     ${pj[i].money}");
            Console.WriteLine("-------------------------");
        }

        Console.ReadKey();
    }
}