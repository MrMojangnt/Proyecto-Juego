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
        { "Raúl Castillo", 0 },
    };
    public static readonly Personalidad[] Arqueotipos =
    {
        //Raul
        new ()
        {
            Dadivoso = 10,
            Presion = 0,
        },

        //Generoso
        new ()
        {
            Dadivoso = 8,
            Presion = -8,
        },

        //Tacaño
        new ()
        {
            Dadivoso = -7,
            Presion = 10,
        },

        //Paranoico
        new ()
        {
            Dadivoso = 3,
            Presion = 8,
        },

        //Charlatán
        new ()
        {
            Dadivoso = 0,
            Presion = 5,
        },

        // Corrupto
        new ()
        {
            Dadivoso = -2,
            Presion = 5,
        },

        // Introvertido
        new ()
        {
            Dadivoso = 1,
            Presion = -3,
        },

        // Gruñón
        new ()
        {
            Dadivoso = -4,
            Presion = 8,
        },

        // Manipulador
        new ()
        {
            Dadivoso = 1,
            Presion = 1, //pero le va a ofrecer más préstamos y no sé, va a intentar endeudarlo
        }, 

    };
}
