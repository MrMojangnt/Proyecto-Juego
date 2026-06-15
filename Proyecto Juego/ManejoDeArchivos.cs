namespace Proyecto_Juego;
public class ManejoDeArchivos
{
    public static string[] inventario = {"Inventario1.csv", "Inventario2.csv", "Inventario3.csv"};
    static string[] save_compania = { "empresas1.csv", "empresas2.csv", "empresas3.csv" };
    public static string[] contactos = { "contactos1.csv", "contactos2.csv", "contactos3.csv" };
    static string[] partidas = { "save1.txt", "save2.txt", "save3.txt" };

    public static string LeerParteDeUnArchivo(string archivo, int numarchivo, string Busqueda)
    {
        if (archivo == "partidas")
        {
            using (StreamReader guardado = new StreamReader(partidas[numarchivo]))
            {
                switch (Busqueda)
                {
                    case "nombre":
                        string nombre = guardado.ReadLine();
                        nombre = nombre.Replace("Nombre: ", ""); //reemplaza "Nombre" por ""
                        return nombre;
                        
                    case "pais":    
                        string pais = guardado.ReadLine();
                        pais = pais.Replace("Pais: ", "");
                        return pais;
                    case "carisma":
                        string carisma = guardado.ReadLine();
                        carisma = carisma.Replace("Carisma: ", "");
                        return carisma;
                    case "economia":
                        string economia = guardado.ReadLine();
                        economia = economia.Replace("Economia: ", "");
                        return economia;
                    case "fiscalidad":    
                        string fiscalidad = guardado.ReadLine();
                        fiscalidad = fiscalidad.Replace("Fiscalidad: ", "");
                        return fiscalidad;
                    case "corrupcion":
                        string corrupcion = guardado.ReadLine();
                        corrupcion = corrupcion.Replace("Corrupcion: ", "");
                        return corrupcion;
                    case "balance":
                        string balance = guardado.ReadLine();
                        balance = balance.Replace("Balance: ", "");
                        return balance;
                }
            } 
        }

        if (archivo == "inventario")
        {
            
        }

        return null;
    }

    public void CambiarDatos(string archivo, int numarchivo, string Busqueda, int change)
    {
        if (archivo == "partidas")
        {
            using (StreamReader guardado = new StreamReader(partidas[numarchivo]))
            using (StreamWriter carga = new StreamWriter(partidas[numarchivo]))
            {

            }
        }
    }
}