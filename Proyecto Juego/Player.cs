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
    public decimal balance { get; set; }

    public override string ToString()
    {
        return $"Nombre: {name} \nPais: {pais} \nCarisma: {carisma} \nEconomia: {economia} \nFiscalidad: {fiscalidad} \nCorrupcion: {corrupcion} \nBalance: {balance}";
    }
}

public struct Companias
{
    public int id { get; set; }
    public  string name { get; set; }
    public  string pais{ get; set; }
    public string rubro { get; set; }
    public decimal capbursatil { get; set; }
    public  int accionistas{ get; set; }
    public string[] productos { get; set; }
    public decimal GananciasTrimestrales { get; set; }
    public decimal[] presupuesto { get; set; }
    public decimal participacion { get; set; }
    public decimal balance { get; set; }

    public override string ToString()
    {
        string ProductosTexto = string.Join(" | ", productos);
        string PresupuestoTexto = string.Join(" | ", presupuesto);

        return $"{id}; {name}; {pais}; {rubro}; {capbursatil} M; {accionistas}; {ProductosTexto}; {GananciasTrimestrales} M; {PresupuestoTexto} M; {participacion}%; {balance} M";
    }
} 

public struct Pais
{
    
}

public struct Acciones
{
    public int id { get; set; }
    public string name { get; set; }
    public decimal CostoDeCompra  { get; set; }
    public decimal CostoActual{ get; set; }
    public bool TipoDeAccion { get; set; }
    public int cantidad { get; set; }
    public decimal Porcentaje { get; set; }
}