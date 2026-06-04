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
        return $"Nombre: {name} \nPais: {pais} \nCarisma: {carisma} \nEconomia: {economia} \nFiscalidad: {fiscalidad} \nCorrupcion: {corrupcion}" ;
    }
}

public struct Companias
{
    public int id { get; set; }
    public  string name { get; set; }
    public  string pais{ get; set; }
    public decimal capbursatil { get; set; }
    public  int accionistas{ get; set; }
    public decimal GananciasMensuales { get; set; }
    public decimal participacion { get; set; }
    public decimal balance  { get; set; }

    public string Companies()
    {
        return $"{id}, {name}, {pais}, {capbursatil}, {accionistas}, {GananciasMensuales}, {participacion}, {balance}";
    }
} 

public struct Pais
{
    
}