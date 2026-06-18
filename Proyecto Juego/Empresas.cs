using Proyecto_Juego;
using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Terminal.Gui;
namespace Empresas;

//creando las empresas?
public struct EmpresasNombres //Los nombres de las empresas por sector
{
    public static string[] TecnologiaHardware = { "PineApple", "Sansung", "Votorola", "WhiteBerry", "Wuawei", "Siaomi",
        "TELL", "Ovistar", "Celtel", "Oscuro", "HP", "Nvidia", "SteelSeries", "Whoow", "IBM", "Western Digital", 
        "Vizio", "Red 7", "BUE Systems, Inc.", "Anazon", "Vast", "Sisco", "Samsara", "Apptronik", "HTC Global Services", "Micron Technology",
        "LGC Electronics", "Shark", "Mony", "Goal", "Mason", "Biowater", "Canon", "Roku", "AMP", "Gogo", "True Anomaly", "Ambiq", "Turion Space",
        "Caliola Engineering", "Machina Labs, Inc", "Graphcore", "Astus", "Altern Space", "Runwide", "ElectronQ", "Doodle Labs", "Protonauta", 
        "Dandy", "Sierra Space", "Cenovo", "Halter", "Motive", "Logitech", "Sevasa", "Carbon Robotics", "Piaggio Fast Forward", 
        "Inspiren", "AOAS"};

    public static string[] TecnologiaSoftware = { "Popstar Games","Gugul", "Nantendo", "CloseAI", "AntiMojang", "BoredGames", "LogicMax", "Equipo Cereza", 
        "IEI Sports", "Ventanilla", "Macrosoft", "OCloud", "Spoorifly", "Disquettecord", "Walve", "Quantum Logic", "Unreel Ungine", "GitLub", "Dripbox",
        "Virtual Forge", "AlphaWare", "CyberLeaf", "Code Republic", "Infinity Labs", "Nova Network", "Hyperlink Solutions", "Core Dynamics", 
        "BlueScreen Labs", "Binary Horizon","Vertex Cloud", "PixelForge", "BrightCode", "Synapse Software", "Lunar Logic", "Stellar Systems", "EchoSoft",
        "Zenith Digital", "Proton Software", "Atlas Technologies","CloudMatrix", "ByteWorks", "MetaNova", "Fusion Logic", "Aether Software", 
        "Hexagon Studios", "Omega Interactive", "Astral Computing", "Nebula Technologies", "Quantum Engine","CodeStorm", "Skyline Software", "Unities", 
        "Pixel Dynamics", "Vertex Systems", "Aurora Systems", "Prime Software", "Titan Systems", "DeepLogic", "MindMesh", "RoboticMind"};

    public static string[] Agronomia = {"Nexus CropScience", "Grupo Magenta", "Trinity Agriscience", " BASD", "Lara International",
        "SQM - Sociedad Química y Minera", "Bunge", "Cargil", "Archer Daniels Midland - ADM", "Louis Dreyfus Company",
        "CNH Industrial", "John Deere", "AGCO", "FMC Corporation", "UPL Limited","Sumitomo Chemical", "ADAMA", "Nufarm", "Limagrain", "KWS Saat",
        "Rijk Zwaan", "DFL Seeds", "The Moisac Company", "OCP Gruop", "CF Industries", "EuroChem Group", "Trimble Agriculture", "Topcon Agriculture",
        "Ceres Imaging", "Sakata Seed Corporation", "Takii & Co.", "East-West Seed", " Yuan Longping High-Tech Agriculture", "Enza Zaden", 
        "Shandong Weifang Rainbow Chemical", "Sipcam Oxon Group", "Albaugh LLC", "PI Industries", "Certis Biologicals", "Helm AG", "ICL Group", 
        "K+S Group", "Haifa Group", "Verdesian Life Sciense", "COFCO Group", "Wilmar International", "Olam International", "Netafim", 
        "Valmont Industries", "Valley Irrigation", "Lindsay Corporation", "Zimmatic", "Spacegronomy", "Monte de Oro", "Magic Beans", "CampRespect", 
        "Generic Green Gatsby", "ILV Coliflor", "Disagro", "Foragro"};

    public static string[] Comercio = {"Global Trade Corp", "Mercurio Logistics", "TradeSphere", "Comex International", "Blue Harbor Trading","Atlas Commerce",
        "Golden Route Exports", "Nova Imports", "Pacific Exchange", "Prime Distribution","Urban Markets", "Capital Traders", "Titan Commerce", 
        "Silver Bridge Trading", "Omni Retail Group","Vertex Commerce", "Metro Supply", "Infinity Traders", "Central Exchange Co.", "Summit Retail",
        "InterWorld Logistics", "Mercantile Dynamics", "Horizon Trade Network", "Pioneer Imports", "Nexus Commerce","Bright Market Holdings", 
        "Continental Trade Corp", "CrossOcean Exports", "United Distribution", "CommerceLink","Evergreen Retail", "Starlight Trading", "BluePeak Markets",
        "Fusion Commerce", "SkyBridge Imports","Grand Bazaar Group", "TradePoint International", "Open Market Systems", "Velocity Distribution", "Urban Exchange",
        "Golden Cartel", "Global Goods Network", "Prime Merchants", "Red Harbor Logistics", "Mercado Universal","Silver Road Commerce", "Frontier Traders", 
        "Alpha Retail Group", "Metro Merchandising", "OceanGate Imports","Summit Trading Co.", "NorthStar Commerce", "BrightPath Logistics", "WorldConnect Trade", 
        "Capital Market Group", "Liberty Commerce", "TradeCore International", "Unified Retail Systems", "Grand Exchange Holdings", "MercaNova" };

    public static string[] Alimenticio = {"Café Devuelvo", "Jewstlé", "Bambo", "Bamboo Enterprises", "El Maisán", "Little Lety", "Zazas", "Lalala",
        "Golden Harvest Foods", "Fresh Valley", "NutriFoods International", "Green Table", "Sunrise Nutrition", "Prime Foods Group", "Blue Farm Products",
        "Nature's Choice", "Vital Harvest", "Pure Grain Company", "Silver Spoon Foods", "Happy Cow Dairy", "Mountain Fresh", "Crystal Water Co.", "Ocean Catch Foods",
        "Golden Wheat Industries", "Healthy Roots", "Fresh Planet", "Evergreen Foods", "Harvest Moon Products","Red Apple Nutrition", "Sunny Fields", "Pure Taste Industries", 
        "Daily Bread Company", "Farm Select","Natural Bliss Foods", "Royal Harvest", "True Organic", "FreshWay", "Valley Farms International","Golden Corn Group", 
        "Blue River Foods", "Green Basket", "NutriLife", "Premium Harvest","Healthy Choice Foods", "Sun Valley Produce", "Earthly Delights", "Bright Farm Co.", 
        "Pure Source Nutrition","Grand Dairy Industries", "Ocean Pearl Seafood", "Fresh Orchard", "Golden Chicken Foods", "Vital Foods Corp", "Nature First", "Harvest King",
        "Green Leaf Nutrition", "Farmhouse Products", "Morning Star Foods","Silver Grain Company", "Prime Nutrition Group", "Healthy Harvest", "Los Pollos Hermanos"};

    public static string[] ManufacturaTextil = {"Telaraña S.A.", "Hilo y Destino", "Moda Express", "Costurín", "Tejidos El Cóndor", "TextiMax", "Algodones Unidos", "La Aguja Feliz",
        "Moda Capital", "Vestimenta Nacional", "Adibás", "Nikea", "Pumba", "Lacostra", "Levais", "Calvin Klein't", "Tommy Hilfinger", "Versaché", "Dolce & Banana",
        "FibraRica", "Confecciones Aurora", "Tejidos del Pacífico", "Hilandería Central", "Moda Horizonte", "Punto y Costura", "Textiles del Norte", "Fashion Factory", "Urban Stitch",
        "Elite Threads","Blue Cotton", "Royal Fabric", "Golden Needle", "Silk Avenue", "Velvet Dreams", "Cotton Empire", "Nova Fashion", "Metro Textile", "Premium Weave", "Infinity Apparel",
        "Costura Suprema", "La Camisa Dorada", "Moda Dinámica", "Puntada Perfecta", "Diseños Sol", "Tejidos Modernos", "Fashion Republic", "TelaNova", "Estilo Urbano", "Luxury Loom","Fibra Global",
        "Hilo Real", "Confecciones Atlas", "Moda Nexus", "TextiCore", "FashionWare", "Loom Industries", "TelaSoft", "Vestex", "ModaSphere", "Ricotextil"};

    public static string[] Recursos = {"Petrobas", "Chevrolento", "Exxo", "Shelln't", "BPoco", "Valecito", "Rio Tinto de Verano", "Gazpromedio", "Barrick Gol", "Glencoreta","Minerales del Norte", 
        "Excavaciones El Topo", "Oro Blanco S.A.", "Hierros Unidos", "GeoRica", "Profundidad Infinita", "La Roca Feliz", "ExcavaMás", "Tierras Raras S.A.", "Cantera Nacional", "Minería Aurora", 
        "Recursos del Pacífico", "Piedra Azul", "Mina Dorada", "GeoCapital", "Excavaciones Atlas", "Metal Tierra", "Cobre Real", "Plata Viva", "Recursos Horizonte","DeepRock Mining", "Iron Mountain",
        "Golden Quarry", "Blue Ore Corp", "Prime Resources", "Titan Minerals", "EarthCore", "Nova Mining", "Frontier Resources", "Global Extraction", "Mountain Gold", "BlackStone Mining", 
        "Crystal Minerals", "Silver Horizon", "Continental Resources", "Terra Holdings", "Geo Dynamics", "Core Mining", "Iron Peak", "Aurum Corp","Minera Libertad", "Grupo Cantera", "GeoSphere", 
        "ResourceMax", "TerraNova", "Metal Nexus", "Minería Central", "Cumbre Resources", "ExcavaCorp", "GeoMatrix" };

    public static string[] ManufacturaDeRecursos = { "Boing", "Aerobús", "LockNeed Martin", "General Dinamicsn't", "Caterpillarcito", "Komatsí", "John Ciervo", "Volvó", "Mitsubichi", "Hitachiwi", 
        "Aceros Scarface", "Fundiciones Libertad", "Metalúrgica Central", "Acero y Punto", "Hierro Puro", "Fundidora El Martillo", "ConstruAcero", "Procesadora Atlas", "Industrias Forja", "Metarista",
        "Acero Industrial", "Fundiciones Aurora", "Metal del Pacífico", "Procesadora Nacional", "Forja Suprema", "Hierro Moderno", "Metal Capital", "Fundición Horizonte", "Aceros del Norte", "MegaForja",
        "SteelWorks", "Prime Alloy", "Titan Manufacturing", "Iron Forge", "BlueSteel Industries", "Grand Foundry", "MetalCore", "Industrial Dynamics", "Vertex Manufacturing", "Nova Alloy", "Fusion Metals", 
        "Alloy Systems", "Steel Horizon", "Continental Manufacturing", "Resource Processing Group", "Metro Steel", "Apex Manufacturing", "Golden Forge", "Industrial Nexus", "Core Industries", 
        "Procesos Integrados", "MetalSphere", "Acero Global", "Fundidora Delta", "Forja Uno", "Manufacturas Atlas", "SteelNova", "IronWorks", "Metal Dynamics", "ForgeTech" };
}

public struct ProductosPorSector //Los nombres de los productos por sector
{
    public static string[] ProductosTecnologiaSoftware = { "Sistema Operativo", "Motor Grafico", "Videojuego", "Aplicacion Movil", "Antivirus", "CRM", "ERP", "Base de Datos", "Editor de Codigo", 
        "Navegador Web", "IA Generativa", "Chatbot", "API Empresarial", "Sistema Bancario", "App de Streaming", "Plataforma Educativa", "Software de Diseño", "Sistema Contable", "App de Mensajeria", 
        "Buscador Web", "Red Social", "Sistema de RRHH", "Servicio Cloud", "Gestor de Contraseñas", "Software Medico", "Software Juridico", "Marketplace Digital", "Motor de IA", "Sistema Electoral", 
        "Software Militar" };

    public static string[] ProductosTecnologiaHardware = { "Smartphone", "Laptop", "Tablet", "Monitor", "Teclado", "Mouse", "Tarjeta Grafica", "Procesador", "Memoria RAM", "SSD", "Disco Duro", 
        "Placa Base", "Fuente de Poder", "Gabinete", "Router", "Switch de Red", "Servidor", "Camara Web", "Microfono USB", "Auriculares", "Altavoces", "Impresora", "Escaner", "Proyector", "Smartwatch", 
        "Pulsera Inteligente", "Drone", "Consola de Videojuegos", "Control Inalambrico", "Visor VR" };

    public static string[] ProductosAgronomia = { "Maiz", "Frijoles", "Trigo", "Arroz", "Cafe", "Cacao", "Algodon", "Soya", "Papa", "Tomate", "Lechuga", "Banano", "Piña", "Mango", "Semillas Hibridas", 
        "Fertilizante", "Herbicida", "Pesticida", "Fungicida", "Sistema de Riego", "Tractor", "Cosechadora", "Dron Agricola", "Invernadero", "Alimento Animal", "Forraje", "Compost", "Biofertilizante", 
        "Sensores de Cultivo", "Plantas Ornamentales" };

    public static string[] ProductosComercio = { "Electrodomesticos", "Ropa", "Calzado", "Juguetes", "Libros", "Muebles", "Herramientas", "Medicamentos OTC", "Cosmeticos", "Perfumes", "Articulos Deportivos",
        "Productos Escolares","Alimentos Importados", "Bebidas", "Repuestos", "Equipos Industriales", "Joyeria", "Relojes", "Articulos de Oficina", "Decoracion", "Material de Construccion", 
        "Productos de Limpieza", "Equipos Medicos", "Mascotas","Accesorios para Autos", "Articulos de Cocina", "Equipos Electronicos", "Articulos de Camping", "Instrumentos Musicales", "Regalos" };

    public static string[] ProductosAlimenticios = { "Pan", "Leche", "Queso", "Yogur", "Mantequilla", "Chocolate", "Cafe Molido", "Galletas", "Refrescos", "Jugos", "Agua Embotellada", "Helados", "Cereales", 
        "Pasta", "Salsa de Tomate", "Aceite Vegetal", "Azucar", "Harina", "Embutidos", "Pollo Procesado", "Carne Empacada", "Atun Enlatado", "Mermelada", "Miel", "Snacks", "Papas Fritas", "Comida Congelada",
        "Bebidas Energeticas", "Sopas Instantaneas", "Alimento Infantil" };

    public static string[] ProductosTextiles = { "Camisetas", "Pantalones", "Chaquetas", "Vestidos", "Uniformes", "Zapatos", "Calcetines", "Gorras", "Bufandas", "Guantes", "Trajes", "Corbatas", "Ropa Deportiva",
        "Ropa Interior", "Bolsos", "Mochilas", "Tela de Algodon", "Tela Sintetica", "Tela de Seda", "Tela de Lana", "Jeans", "Sudaderas", "Abrigos", "Pijamas", "Ropa Infantil", "Ropa Ejecutiva", "Botas", 
        "Sandalias", "Cinturones", "Sombreros" };

    public static string[] ProductosRecursos = { "Petroleo Crudo", "Gas Natural", "Carbon", "Hierro", "Cobre", "Oro", "Plata", "Aluminio", "Litio", "Uranio", "Niquel", "Zinc", "Plomo", "Titanio", "Arena Silice", 
        "Caliza", "Granito", "Marmol", "Agua Industrial", "Sal", "Fosfato", "Bauxita", "Diamantes", "Esmeraldas", "Cobalto", "Manganeso", "Tierras Raras", "Azufre", "Grafito", "Arcilla" };

    public static string[] ProductosManufacturaRecursos = { "Acero", "Vigas Metalicas", "Tubos Industriales", "Maquinaria Pesada", "Excavadoras", "Bulldozers", "Motores Industriales", "Turbinas", "Locomotoras", 
        "Vagones", "Barcos de Carga", "Aviones Comerciales", "Helicopteros", "Paneles Metalicos", "Transformadores", "Generadores", "Grúas", "Equipos Mineros", "Contenedores", "Herramientas Industriales", 
        "Aleaciones", "Rieles", "Cables de Alta Tension", "Compresores", "Bombas Hidraulicas", "Calderas", "Robots Industriales", "Plataformas Petroleras", "Equipos de Construccion", "Sistemas de Transporte Industrial" };
}



//Existirá un menu de reglas. Pequeño: 20 empresas, pocas personas. Medio: 40 empresas, 60 personas. Grande: 120 empresas, 150 personas
public class Indices
{
    public static string[][] Sectores = { EmpresasNombres.TecnologiaSoftware, EmpresasNombres.TecnologiaHardware, EmpresasNombres.Agronomia,//un array para usar los indices
    EmpresasNombres.Comercio, EmpresasNombres.Alimenticio, EmpresasNombres.ManufacturaTextil, EmpresasNombres.Recursos, EmpresasNombres.ManufacturaDeRecursos};

    static string[][] Productos = {ProductosPorSector.ProductosTecnologiaSoftware, ProductosPorSector.ProductosTecnologiaHardware, ProductosPorSector.ProductosAgronomia,
    ProductosPorSector.ProductosComercio, ProductosPorSector.ProductosAlimenticios, ProductosPorSector.ProductosTextiles, ProductosPorSector.ProductosRecursos,
    ProductosPorSector.ProductosManufacturaRecursos};
    public static Dictionary<string, string[]> Nombre_Sectores_Variables = new()
    {
        {"Tecnología Software", EmpresasNombres.TecnologiaSoftware },
        {"Tecnología Hardware", EmpresasNombres.TecnologiaHardware },
        {"Agronomía", EmpresasNombres.Agronomia },
        {"Comercio", EmpresasNombres.Comercio },
        {"Alimenticio", EmpresasNombres.Alimenticio },
        {"Manufactura Textil", EmpresasNombres.ManufacturaTextil },
        {"Recursos", EmpresasNombres.Recursos },
        {"Manufactura de Recursos", EmpresasNombres.ManufacturaDeRecursos }
    };

    public static List<Companias> GenerarIndicesEmpresas()
    {
        List<Companias> Empresas = new List<Companias>();
        List<string> Sect = new List<string>();
        //Digamos que es Pequeño: 20 empresas, personas no hay todavía
        int IndiceSector, IndiceEmpresa;
        int IndicePais, IndiceAccionistas;
        int IndiceProductos;
        int Indiceatraccion; //skill de empresas que multiplica ganancias por un millon
        decimal IndiceCapitalBursatil, IndiceGananciasTrimestrales, IndiceBalance, indiceMarketing, IndiceMantenimiento, IndiceInvestigacion, IndiceGastos;

        for (int i = 0; i < 20; i++)
        {
            Proyecto_Juego.Companias empresitas = new Companias();
            empresitas.productos = new string[10];
            
                        

            IndiceSector = Random.Shared.Next(0, Sectores.Length);
            IndiceEmpresa  = Random.Shared.Next(Sectores[IndiceSector].Length); //Pues el indice de empresas, entre 0 y 60 porque acaba en 59 :v
            IndicePais = Random.Shared.Next(0, 6);
            IndiceCapitalBursatil = Math.Round(0.1m + (decimal)Random.Shared.NextDouble() * 999.9m,  2);//como nextdouble solo genera entre 0.0 y 1 se multiplica
            IndiceAccionistas = Random.Shared.Next(0, 1000);
            Indiceatraccion = Random.Shared.Next(1, 101);
            IndiceGananciasTrimestrales = Math.Round((IndiceCapitalBursatil / 10) * Indiceatraccion, 2);

            //para los 10 productos por empresa
            for (int j = 0; j< 10; j++)
            {
                IndiceProductos= Random.Shared.Next(0, 30);
                empresitas.productos[j] = Productos[IndiceSector][IndiceProductos];//se usa indice sector para que agarre solo los del sector de la empresa

            }
            //para los 4 aspectos de presupuesto: marketing, investigación y mantenimiento
                IndiceGastos = 0;
            indiceMarketing = Math.Round(IndiceGananciasTrimestrales * (0.10m + (decimal)Random.Shared.NextDouble() * 0.20m),2);
            IndiceInvestigacion = Math.Round(IndiceGananciasTrimestrales * (0.05m + (decimal)Random.Shared.NextDouble() * 0.15m), 2);
            Indiceatraccion += (int)(IndiceInvestigacion / 10);
            IndiceMantenimiento = Math.Round(IndiceGananciasTrimestrales * (0.10m + (decimal)Random.Shared.NextDouble() * 0.15m),2);
            Indiceatraccion += (int)(IndiceInvestigacion / 10);
            IndiceGastos += indiceMarketing + IndiceMantenimiento + IndiceInvestigacion;

            IndiceBalance = Math.Round(IndiceGananciasTrimestrales - IndiceGastos, 2);

            //llenando la struct
            empresitas.id = i;
            empresitas.name = Sectores[IndiceSector][IndiceEmpresa];
            empresitas.pais = Program.Paises[IndicePais];
            empresitas.pais = empresitas.pais.Replace("(predeterminado)", "");
            empresitas.rubro = Nombre_Sectores_Variables.ElementAt(IndiceSector).Key;
            empresitas.capbursatil = IndiceCapitalBursatil; 
            empresitas.accionistas = IndiceAccionistas;
            empresitas.GananciasTrimestrales = IndiceGananciasTrimestrales;
            empresitas.balance = IndiceBalance;
            empresitas.marketing = indiceMarketing;
            empresitas.mantenimiento = IndiceMantenimiento;
            empresitas.investigacion = IndiceInvestigacion;
            Empresas.Add(empresitas);
        }
        //intentando la participacion
        Dictionary<string, decimal> totalPorSector = new Dictionary<string, decimal>();
        for(int i = 0; i < Empresas.Count; i++)
        {
            var emp = Empresas[i];

            if (!totalPorSector.ContainsKey(emp.rubro))
                totalPorSector[emp.rubro] = 0;

            totalPorSector[emp.rubro] += emp.balance;
        }
        for (int i = 0; i < Empresas.Count; i++)
        {
            var emp = Empresas[i];

            decimal total = totalPorSector[emp.rubro];

            if (total > 0)
            {
                emp.participacion = emp.balance / total;
                emp.participacion *= 100;

            }
            else
            {
                emp.participacion = 0;
            }
            Empresas[i] = emp;
        }

        return Empresas;
    }

    public static List<Companias> CargarEmpresa(int indice)
    {
        List<Companias> Comp = new List<Companias>();
        char[] delimitadores = { ';', '\n', '|', '\r' };
        using (StreamReader savecompani = new StreamReader(Program.save_compania[indice], Encoding.UTF8))
        {
            string[] encabezados = (savecompani.ReadLine() ?? "").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            while (!savecompani.EndOfStream)
            {
                Proyecto_Juego.Companias compitas = new Companias();
                compitas.productos = new string[10];

                string[] lineas = (savecompani.ReadLine() ?? "").Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
                compitas.id = int.Parse(lineas[0]);
                compitas.name = lineas[1];
                lineas[2] = lineas[2].Replace("(predeterminado)", ""); //reemplaza "M" por ""
                compitas.pais = lineas[2];
                compitas.rubro = lineas[3];
                lineas[4] = lineas[4].Replace("M", ""); //reemplaza "M" por ""
                compitas.capbursatil = decimal.Parse(lineas[4]);
                compitas.accionistas = int.Parse(lineas[5]);
                int p = 6;
                for (int i = 0; i < compitas.productos.Length; i++)
                {
                    compitas.productos[i] = lineas[p];
                    p++;
                }
                lineas[16] = lineas[16].Replace("M", ""); //reemplaza "M" por ""
                compitas.GananciasTrimestrales = decimal.Parse(lineas[16]);
                lineas[17] = lineas[17].Replace("M", ""); //reemplaza "M" por ""
                compitas.marketing = decimal.Parse(lineas[17]);
                lineas[18] = lineas[18].Replace("M", ""); //reemplaza "M" por ""
                compitas.investigacion = decimal.Parse(lineas[18]);
                lineas[19] = lineas[19].Replace("M", ""); //reemplaza "M" por ""
                compitas.mantenimiento = decimal.Parse(lineas[19]);
                lineas[20] = lineas[20].Replace("%", ""); //reemplaza "%" por ""
                compitas.participacion = decimal.Parse(lineas[20]);
                lineas[21] = lineas[21].Replace("M", ""); //reemplaza "M" por ""
                compitas.balance = decimal.Parse(lineas[21]);

                Comp.Add(compitas);
            }

        }
        return Comp;
    }

    public static void VentanaDeEmpresas(Toplevel top, List<ColorScheme> colores, int colora)
    {
        var VentanaDeEmpresas = new Window()
        {
            X = 0,
            Y = 0,
            ColorScheme = colores[colora],
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        DataTable tabla = new DataTable();

        tabla.Columns.Add("ID");
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Pais");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Capital Bursátil");


        foreach (Companias i in Program.Companiass)
        {
            tabla.Rows.Add(
                i.id,
                i.name,
                i.pais,
                i.rubro,
                $"{i.capbursatil:F2}" + "M"
            );

        }

        var tableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = 120,
            Height = 30
        };
        tableView.CellActivated += (e) =>
        {
            int row = e.Row;

            var empresa = Program.Companiass[row];
            top.Remove(VentanaDeEmpresas);
            Program.MostrarDetalleEmpresa(top, empresa);

        };
        tableView.Table = tabla;
        Program.BotonesDeJuegoPredeterminado(top, VentanaDeEmpresas);


        VentanaDeEmpresas.Add(tableView);
        top.Add(VentanaDeEmpresas);

    }
}


