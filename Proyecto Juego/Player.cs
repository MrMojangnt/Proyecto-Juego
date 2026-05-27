using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_Juego;

public struct Players
{
    public  string name { get; set; }
    public  string pais{ get; set; }
    public  int carisma{ get; set; }
    public  int economia{ get; set; }
    public  int fiscalidad{ get; set; }
    public  int corrupcion{ get; set; }

    public override string ToString()
    {
        return $"Nombre: {name} \nPaís: {pais} \nCarisma: {carisma} \nEconomía: {economia} \nFiscalidad: {fiscalidad} \nCorrupción: {corrupcion}" ;
    }
}
