using Empresas;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using Terminal.Gui;
namespace Proyecto_Juego;

public class Dialogos_de_Contacto
{
    public static void ConsejosDeContacto()
    {

    }

    //Raul aqui pone cosas que decis cuando contestas una llamada
    public static readonly string[][] DialogosCuandoContestaLaLlamada =
        {
            // 0 - DialogosDeRaul
                [ "¡Qué alegría escucharte! ¿Cómo va todo?", "¡Hola! Contame, ¿qué novedades traés?", "¡Buen momento para llamar! ¿Qué necesitás?", "¡Amigo! ¿Qué se cuenta?", "¡Qué gusto! ¿En qué puedo ayudarte?"],
            // 1 - DialogoDelGeneroso =
                [
              "Hola, siempre es bueno saber de vos.",
                    "¿Cómo estás? Espero que todo vaya bien.",
                    "¡Qué bueno que llamaste! Decime.",
                    "Hola, ¿cómo puedo ayudarte hoy?",
                    "Adelante, te escucho."
                ],

            // 2 - DialogoDelTacano =
                [
              "Sí... ¿qué necesitás?", "Espero que esto sea importante.", "Decime rápido, estoy ocupado." ,"¿Qué pasa?", "Sí, hablá."
                ],

            // 3 - DialogoDelParanoico =
                [
              "¿Estás solo?", "¿Confirmaste que nadie te sigue?", "Habrá que hablar rápido...", "Espero que esta llamada sea segura.", "¿Quién más sabe que estamos hablando?"
                ],

            // 4 - DialogoDelCharlatan =
                [
              "¡No sabés todo lo que tengo para contarte!", "¡Justo estaba pensando en vos!", "¡Qué casualidad que llamaras!", "¡Esperá, esperá! Tengo una historia increíble.", "¡Qué bueno escucharte! Contame todo."
                ],

            // 5 - DialogoDelCorrupto =
                [
              "Depende de lo que quieras...", "¿Es un negocio?", "Espero que valga la pena atender.", "Habrá que ver qué ganamos.", "Te escucho."
                ],

            // 6 - DialogoDelIntrovertido =
                [
              "Hola...", "Sí... decime.", "Estoy escuchando.", "Hola, ¿todo bien?", "¿Qué necesitás?"
                ],

            // 7 - DialogoDelGrunon =
                [
              "¿Qué?", "Sí, hablá.", "¿Qué querés ahora?", "¿Y ahora qué pasó?", "Estoy ocupado."
                ],

            // 8 - DialogoDelManipulador =
          [
              "¡Qué alegría saber de vos!", "Justamente estaba pensando en llamarte.", "Qué coincidencia tan conveniente.", "Siempre es bueno hablar con amigos.", "Contame, seguro encontramos una solución."
          ]
        };
    public static readonly string[][] DialogosCuandoRespondeAComoTeVa =
{
    // 0 - Raul
    [
        "Bien, todo tranquilo. ¿Y vos?",
        "Ahí vamos, sin mucho que contar. ¿Cómo vas vos?",
        "Todo en orden por aquí, gracias por preguntar.",
        "Bien, otro día agradecido de no ser vibecoder."
    ],

    // 1 - Generoso (amable pero natural)
    [
        "Muy bien, gracias a Dios. ¿Y tú cómo has estado?",
        "Bastante bien, todo fluyendo con calma. ¿Qué me cuentas?",
        "Bien, de verdad. Me alegra que preguntes.",
        "Todo en paz por aquí, espero que tú también estés bien."
    ],

    // 2 - Tacaño (mínimo esfuerzo emocional)
    [
        "Bien.",
        "Normal.",
        "Ahí.",
        "Sí, bien. ¿Y tú?",
        "Todo bien."
    ],

    // 3 - Paranoico
    [
        "Bien… creo. ¿Por qué preguntas?",
        "Normal, pero dime… ¿quién más está escuchando?",
        "Estoy bien, pero esto es seguro, ¿verdad?",
        "Bien… aunque no me gusta hablar mucho por teléfono."
    ],

    // 4 - Charlatán (responde “cómo te va” y se desborda igual)
    [
        "Bien, pero eso es lo aburrido, porque hoy me pasaron mil cosas: primero pensé que iba a ser un día normal, luego todo se complicó por cosas mínimas que se fueron acumulando sin sentido y ahora estoy aquí procesando todo eso.",
        "Más o menos bien, aunque honestamente hoy fue un caos: empecé tranquilo, luego pasó una cosa, luego otra, y cuando me di cuenta ya estaba metido en un problema que ni siquiera entiendo del todo.",
        "Bien, pero te cuento rápido porque esto no es normal: todo empezó con algo pequeño y terminó siendo una serie de eventos que no sé ni cómo explicar sin parecer exagerado.",
        "Estoy bien, pero mi día no fue normal en absoluto, de hecho fue tan raro que si te lo cuento completo no terminamos hoy."
    ],

    // 5 - Corrupto
    [
        "Bien… depende de lo que entiendas por ‘bien’.",
        "Se podría decir que bien. Todo tiene su precio, ya sabes.",
        "Bien por ahora. Nada es gratis en este mundo.",
        "Estoy bien, siempre que las cosas sigan como deben seguir."
    ],

    // 6 - Introvertido
    [
        "Bien… gracias por preguntar.",
        "Sí… estoy bien.",
        "Todo tranquilo.",
        "Estoy bien, supongo."
    ],

    // 7 - Gruñón
    [
        "Bien. ¿Y eso qué importa?",
        "Estoy bien, no molestes.",
        "Sí, bien. Rápido con lo tuyo.",
        "Bien… supongo."
    ],

    // 8 - Manipulador
    [
        "Muy bien, sorprendentemente bien… aunque siempre es interesante ver quién pregunta eso y con qué intención.",
        "Estoy bien, gracias por preocuparte… es curioso, siempre apareces en momentos oportunos.",
        "Bien, todo bajo control… pero me alegra que llames, las coincidencias dicen mucho.",
        "Estoy bien, y tú seguramente también, aunque podríamos estar mejor si hablamos un poco más."
    ]
};
}

    