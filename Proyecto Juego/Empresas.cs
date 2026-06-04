using System;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using Proyecto_Juego;
namespace Empresas;

//creando las empresas?
public struct EmpresasNombres
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
    public static string[] Alimenticio = {"Café Devuelvo", "Mestlé", "Bambo", "Bamboo Enterprises", "El Maisán", "Little Lety", "Zazas", "Lalala",
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
//Existirá un menu de reglas. Pequeño: 20 empresas, pocas personas. Medio: 40 empresas, 60 personas. Grande: 120 empresas, 150 personas
public struct Indices
{
    static string[][] Sectores = { EmpresasNombres.TecnologiaSoftware, EmpresasNombres.TecnologiaHardware, EmpresasNombres.Agronomia,//un array para usar los indices
    EmpresasNombres.Comercio, EmpresasNombres.Alimenticio, EmpresasNombres.ManufacturaTextil, EmpresasNombres.Recursos, EmpresasNombres.ManufacturaDeRecursos};
    static Dictionary<string, string[]> Nombre_Sectores_Variables = new()
    {
        {"Tecnología Software", EmpresasNombres.TecnologiaSoftware },
        {"Tecnología Hardware", EmpresasNombres.TecnologiaHardware },
        {"Agronomía", EmpresasNombres.Agronomia },
        {"Comercio", EmpresasNombres.Comercio },
        {"Alimenticio", EmpresasNombres.ManufacturaTextil },
        {"Manufactura Textil", EmpresasNombres.ManufacturaTextil },
        {"Recursos", EmpresasNombres.Recursos },
        {"Manufactura de Recursos", EmpresasNombres.ManufacturaDeRecursos }
    };
    public static List<Companias> EmpresasGuardadas = new List<Companias>(GenerarIndicesEmpresas());

    public static List<Companias> GenerarIndicesEmpresas()
    {
        List<Companias> Empresas = new List<Companias>();
        List<string> Sect = new List<string>();
        //Digamos que es Pequeño: 20 empresas, personas no hay todavía
        int IndiceSector, IndiceEmpresa;
        int IndicePais;
        decimal IndiceCapitalBursatil = 0;
        for (int i = 0; i < 20; i++)
        {
            Proyecto_Juego.Companias empresitas = new Companias();
            IndiceEmpresa = 0;
            IndiceSector = 0;
            IndicePais = 0;
            IndiceEmpresa = Random.Shared.Next(0, 60); //Pues el indice de empresas, entre 0 y 60 porque acaba en 59 :v
            IndiceSector = Random.Shared.Next(0, 8);
            IndicePais = Random.Shared.Next(0, 6);
            IndiceCapitalBursatil = Math.Round(0.1m + (decimal)Random.Shared.NextDouble() * 999.9m,  2);//como nextdouble solo genera entre 0.0 y 1 se multiplica

            //llenando la struct
            empresitas.id = i;
            empresitas.name = Sectores[IndiceSector][IndiceEmpresa];
            empresitas.pais = Program.Paises[IndicePais];
            empresitas.sector = Nombre_Sectores_Variables.ElementAt(IndiceSector).Key;
            empresitas.capbursatil = IndiceCapitalBursatil; 

            Empresas.Add(empresitas);
        }
        //paises hay 6, por lo tanto 5

        return Empresas;
    }


}

public struct GuardarStruct
{
    public static void Guardarempresa()
    {
        using (StreamWriter save_empresas = new StreamWriter("save_empresa.csv"))
        {
            save_empresas.WriteLine("ID, Empresa, Pais, Sector");
            for (int p = 0; p <Indices.EmpresasGuardadas.Count; p++)
            {
                save_empresas.WriteLine(Indices.EmpresasGuardadas[p]);

            }


        }
    }

}
