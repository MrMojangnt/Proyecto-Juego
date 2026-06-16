using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Juego;
/*A tomar en cuenta
 * -10 a -7: extremo negativo
-6 a -3: negativo
-2 a 2: neutral
3 a 6: positivo
7 a 10: extremo positivo*/
public static class Personalidades
{
    public static Dictionary<string, int> PersonalidadesFijas =new()
    {
        { "Raul Castillo", 1 },
    };
    public static Personalidad[] Arqueotipos =
    {
        //Raul
        new Personalidad
        {
            Dadivoso = 10,
            Presion = 0,
            Extraversion = 10,
            Amabilidad = 10,
            Neuroticismo = -5,
            Honestidad = 10,
            Ambicion = 10,
            Confianza = 10
        },

        //Generoso
        new Personalidad
        {
            Dadivoso = 8,
            Presion = -8,
            Extraversion = 7,
            Amabilidad = 8,
            Neuroticismo = -2,
            Honestidad = 8,
            Ambicion = 2,
            Confianza = 9
        },

        //Tacaño
        new Personalidad
        {
            Dadivoso = -7,
            Presion = 10,
            Extraversion = -3,
            Amabilidad = -4,
            Neuroticismo = -3,
            Honestidad = 7,
            Ambicion = 8,
            Confianza = -4
        },

        //Paranoico
        new Personalidad
        {
            Dadivoso = 3,
            Presion = 8,
            Extraversion = -5,
            Amabilidad = 5,
            Neuroticismo = 9,
            Honestidad = 8,
            Ambicion = -3,
            Confianza = -5
        },

        //Charlatán
        new Personalidad
        {
            Dadivoso = 0,
            Presion = 5,
            Extraversion = 9,
            Amabilidad = 7,
            Neuroticismo = -5,
            Honestidad = -3,
            Ambicion = 5,
            Confianza = 7
        },

        // Filántropo
        new Personalidad
        {
            Dadivoso = 10,
            Presion = 0,
            Extraversion = 4,
            Amabilidad = 10,
            Neuroticismo = -4,
            Honestidad = 8,
            Ambicion = -5,
            Confianza = 7
        },

        // Corrupto
        new Personalidad
        {
            Dadivoso = -2,
            Presion = 5,
            Extraversion = 4,
            Amabilidad = 1,
            Neuroticismo = -3,
            Honestidad = -10,
            Ambicion = 8,
            Confianza = 2
        },

        // Introvertido
        new Personalidad
        {
            Dadivoso = 1,
            Presion = -3,
            Extraversion = -10,
            Amabilidad = 2,
            Neuroticismo = 1,
            Honestidad = 5,
            Ambicion = -1,
            Confianza = -2
        },

        // Optimista
        new Personalidad
        {
            Dadivoso = 5,
            Presion = -2,
            Extraversion = 6,
            Amabilidad = 8,
            Neuroticismo = -8,
            Honestidad = 5,
            Ambicion = 3,
            Confianza = 6
        },

        // Pesimista
        new Personalidad
        {
            Dadivoso = -1,
            Presion = -7,
            Extraversion = -2,
            Amabilidad = 2,
            Neuroticismo = 8,
            Honestidad = 5,
            Ambicion = -2,
            Confianza = -5
        },

        // Calculador
        new Personalidad
        {
            Dadivoso = -6,
            Presion = 4,
            Extraversion = -3,
            Amabilidad = -1,
            Neuroticismo = -5,
            Honestidad = 4,
            Ambicion = 9,
            Confianza = -6
        },

        // Ingenuo
        new Personalidad
        {
            Dadivoso = 8,
            Presion = -10,
            Extraversion = 5,
            Amabilidad = 8,
            Neuroticismo = 1,
            Honestidad = 9,
            Ambicion = -4,
            Confianza = 10
        },

        // Gruñón
        new Personalidad
        {
            Dadivoso = -4,
            Presion = 8,
            Extraversion = -5,
            Amabilidad = -8,
            Neuroticismo = 5,
            Honestidad = 7,
            Ambicion = 1,
            Confianza = -5
        },

        // Manipulador
        new Personalidad
        {
            Dadivoso = 1,
            Presion = 1, //pero le va a ofrecer más préstamos y no sé, va a intentar endeudarlo
            Extraversion = 8,
            Amabilidad = 6,
            Neuroticismo = -3,
            Honestidad = -9,
            Ambicion = 9,
            Confianza = 3
        }, 
        // Maquiavélico
        new Personalidad
        {
            Dadivoso = -8,
            Presion = 1,
            Extraversion = 6,
            Amabilidad = 3,
            Neuroticismo = -7,
            Honestidad = -10,
            Ambicion = 10,
            Confianza = -9
        },
    };
}
