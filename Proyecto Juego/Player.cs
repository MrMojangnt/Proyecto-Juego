using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_Juego;

public struct Players
{
    public  string name { get; set; }
    public  string pais{ get; set; }
    public  int carisma{ get; set; } //Carisma INT Habilidad para conocer personajes y mejorar relaciones
    public int economia{ get; set; }//Economia INT Habilidad para comprar y negociar acciones asequibles 
    public int fiscalidad{ get; set; }//Fiscalidad INT Habilidad para reducir los impuestos pagados(Empresa o jugador)
    public int corrupcion{ get; set; }//Corrupción INT Habilidad para convencer a las personas de hacer cosas
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
    public decimal marketing { get; set; }
    public decimal investigacion { get; set; }
    public decimal mantenimiento { get; set; }
    public decimal participacion { get; set; }
    public decimal balance { get; set; }

    public override string ToString()
    {
        string ProductosTexto = string.Join(" | ", productos);
        return $"{id}; {name}; {pais}; {rubro}; {capbursatil} M; {accionistas}; {ProductosTexto}; {GananciasTrimestrales} M; {marketing} M; {investigacion}; {mantenimiento}; {participacion}%; {balance} M";
    }
}

public struct Pais
{
    public string name { get; set; }
    public int poblacion { get; set; }
    public decimal PIB { get; set; }
    public float Imp_Mensuales { get; set; }
    public string Gobierno_Tipo { get; set; }// String que guarda los tipos de gobierno de cada pais
    public int Pesadez_Leyes { get; set; }//Un int que va de 0 a 10, siendo 10 normas mas estrictas y 0 el m�nimo
    public int Cap_economica { get; set; } // Un int que va de 0 a 10, siendo 10 la mayor capacidad econ�mica

    }

    public struct Acciones
{
    public int id { get; set; }
    public string name { get; set; }
    public decimal CostoDeCompra  { get; set; }
    public decimal CostoActual{ get; set; }
    public bool TipoDeAccion { get; set; }
    public int cantidad { get; set; }
}