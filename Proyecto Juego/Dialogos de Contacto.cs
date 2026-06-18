using Empresas;
using NAudio.Codecs;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using Terminal.Gui;
using Unix.Terminal;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Proyecto_Juego;

public class Dialogos_de_Contacto
{
    public static readonly string[][] DialogosCuandoPidenConsejo =
    {
        // 0 - Raúl (equilibrado, directo)
    [
        "Decime qué está pasando exactamente,\nsin adornos.",
        "Si no podés cambiar la situación, \ncambia cómo la enfrentás.",
        "No todo consejo es útil. \nElegí bien a quién escuchás.",
        "A veces el problema no es el problema,\n es cómo lo estás viendo.",
        "Organizá tus ideas antes de buscar respuestas."
    ],

    // 1 - Generoso (empático)
    [
        "No tenés que cargar con todo solo.",
        "Es válido no tener claridad ahora mismo.",
        "Tomate tu tiempo, no hay prisa real.",
        "A veces hablarlo ya empieza a ordenarlo.",
        "No te exijas resolverlo todo hoy."
    ],

    // 2 - Tacaño (frío, mínimo esfuerzo)
    [
        "No sé qué decirte.",
        "Arreglate como puedas.",
        "Eso ya es problema tuyo.",
        "No tengo tiempo para esto.",
        "Pensalo vos."
    ],

    // 3 - Paranoico
    [
        "¿Y si esto es una trampa para confundirte más?",
        "No confíes en soluciones demasiado simples.",
        "Primero asegurate de que no te están manipulando.",
        "Nada es tan casual como parece.",
        "Incluso este consejo podría estar equivocado."
    ],

    // 4 - Charlatán
    [
        @"Esto me recuerda a una vez que alguien intentó
resolver su vida con una decisión mínima y terminó 
cuestionando todo su pasado.",
        @"El consejo es interesante, pero más interesante
es por qué lo necesitás ahora mismo.",
        @"A veces la respuesta no importa tanto
como la pregunta.",
        @"Todo problema es también una historia 
esperando ser contada correctamente.",
        @"Podría darte una solución, pero prefiero
observar cómo evoluciona esto."
    ],

    // 5 - Corrupto
    [
        @"Los consejos también tienen precio, aunque no 
siempre en dinero.",
        "Depende de qué estés dispuesto a sacrificar.",
        "Puedo orientarte… si el resultado me \nbeneficia.",
        "No hay consejo gratis en este mundo.",
        "Decidir bien también es una forma de \nnegociación."
    ],

    // 6 - Introvertido
    [
        "No estoy seguro de ser el indicado para esto…",
        "Quizá podrías intentar… no apresurarte.",
        "A veces el silencio ayuda más que las palabras.",
        "No sé si tengo una respuesta útil.",
        "Lo siento, no tengo mucho que aportar."
    ],

    // 7 - Gruñón
    [
        "No compliques las cosas.",
        "Haz lo que tengas que hacer y ya.",
        "Esto no es tan profundo como creés.",
        "Deja de pensar tanto.",
        "Siguiente problema."
    ],

    // 8 - Manipulador
    [
        "Podría ayudarte a ver algo que todavía \nno estás viendo.",
        "Interesante momento para buscar guía.",
        "A veces el mejor consejo es el que te hace \ndepender menos de otros… o más, dependiendo del objetivo.",
        "Todo consejo tiene una intención detrás,\n incluso este.",
        "Te conviene pensar quién gana con la \ndecisión que tomes."
    ],

    // 9 - DialogosSofia
    [
        @"¿Un consejo? Talvez no te ayude en tus negocios,
pero: aprovecha el tiempo, no lo desperdicies haciendo
algo que odias. Se te acabará antes de que te des cuenta.",
"No te dejes llevar por las apariencias,\nla gente nunca es lo que tu crees...\n Claro, excluyéndome a mi.",
@"Mi abogado me dijo una vez que nunca firme un 
contrato sin asesorarme primero. No cometas ese error.",
@"Miente tanto como puedas. Los buenos mentirosos 
nunca enfrentan las consecuencias de sus actos.",
@"El dinero puede comprarlo todo, incluso la 
dignidad de las personas. Recuérdalo cuando 
estés en problemas."
    ],
    // 10 - DialogosLeah
    [
@"Trabaja, la gente pobre realmente es pobre porque quiere. 
El que diga lo contrario, basta ver su cuenta de 
banco para saber porque se miente a si mismo.",
@"Piensa un poco más antes de actuar. 
Aunque creo que ya llego tarde para eso.",
@"No todos nacieron para las finanzas. Y eso está bien.",
@"No todos los problemas se resuelven con dinero.
Aunque por suerte para mí, muchos intentan hacerlo.",
@"Nunca me pidas prestado nada si valoras tus estadísticas."
    ]
    };

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
                ],
            // 9 - DialogosSofia
                [
                "¿Qué es lo que quieres? \n¿No tienes cosas más importantes por hacer?",
                "¿Aló...? Ah, eres tú. ¿Para qué me llamas?",
                "Hey. Realmente estoy ocupada, ¿Qué necesitas?",
                "No estoy de buen humor. Sea lo que sea que quieras, habla rápido.",
                "Estaba teniendo un buen día hasta que recibí tu llamada."
                ],
                    // 10 - DialogosLeah
                [
                @"Qué sorpresa. Pensé que era alguien que me 
debía dinero.",
                "Qué bueno que llamaste, justo estaba \npensando en que obra de caridad hacer este año.",
                @"Espero que sea algo importante. Bueno, importante 
para ti al menos.",
                @"Que bueno escucharte. Hasta pensé que ya habías
aprendido a resolver tus problemas por tu cuenta.",
                @"Adelante, siempre es interesante saber que 
necesita la gente como vos."
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
        @"Bien, pero eso es lo aburrido, porque hoy me pasaron mil cosas:
primero pensé que iba a ser un día normal, luego todo se complicó 
por cosas mínimas que se fueron acumulando sin sentido y 
ahora estoy aquí procesando todo eso.",
        @"Más o menos bien, aunque honestamente hoy fue un caos: 
empecé tranquilo, luego pasó una cosa, luego otra, y
cuando me di cuenta ya estaba metido en un problema 
que ni siquiera entiendo del todo.",
        @"Bien, pero te cuento rápido porque esto no es normal:
todo empezó con algo pequeño y terminó siendo 
una serie de eventos que no sé ni cómo explicar 
sin parecer exagerado.",
        @"Estoy bien, pero mi día no fue normal
en absoluto, de hecho fue tan raro que si
te lo cuento completo no terminamos hoy."
    ],

    // 5 - Corrupto
    [
        "Bien… depende de lo que entiendas por ‘bien’.",
        "Se podría decir que bien. Todo tiene su precio, ya sabes.",
        "Bien por ahora. Nada es gratis en este mundo.",
        @"Estoy bien, siempre que las cosas 
sigan como deben seguir."
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
    ],
    // 9 - DialogosSofia
    [
    "¿Querías preguntarme eso? \nPodrías haberme enviado un mensaje.",
    "Supongo que estoy bien, al menos el negocio va bien.",
    "No quiero hablar de eso.",
    "¿Te preocupas por mi? No necesito que lo hagas.\n Mejor hablemos de negocios.",
    "Mal. ¿De qué te sirve saberlo?"
    ],
    // 10 - DialogosLeah
    [@"No me quejo. Cada día descubro alguna nueva forma...
legal de hacer dinero.",
        "Mejor que ayer y peor que mañana, claramente",
        "Excelente, gracias por preguntar, \ncasi nadie se preocupa por nosotros los ricos.",
        "Es bueno saber que aún hay... \ngente como vos por las calles.",
        "Bastante bien. Quien diría que tomar buenas\ndecisiones ayuda muchísimo.",
        "Me va mejor que a mis deudores, eso está claro."
        ]
};

    public static readonly string[][] DialogosCuandoPidenPrestamo =
    {
        // 0 - Raul
        [
        "Si es para algo importante, decime. No me gusta dar vueltas cuando hay necesidad real.",
        "Te escucho, pero quiero claridad. No me pidas lo que no puedas sostener después.",
        "Podría ayudarte, pero necesito entender bien el motivo.",
        "Esto no es solo dinero, es confianza. Hablame directo.",
        "Veré qué puedo hacer, pero no prometo milagros."
        ],
        // 1. Generoso
        [
        "Claro, decime cuánto necesitás, seguro encontramos una forma de ayudarte.",
        "No te preocupes, para eso están los amigos.",
        "Si está en mis manos, contá con eso.",
        "Podemos ver cómo solucionarlo juntos, no estás solo en esto.",
        "Está bien, pero quiero saber que lo vas a poder recuperar después."
        ],
        // 2. Tacaño
        [
        "¿Préstamo?… eso suena como una palabra peligrosa.",
        "No estoy muy bien de liquidez ahora mismo… casualmente siempre.",
        "Preferiría no mezclar dinero con amistad.",
        "¿Y cómo planeas devolverlo exactamente?",
        "Estoy un poco corto… desde hace años."
        ],
        // 3 - Paranoico
        [
        "¿Por qué justo ahora me pedís eso?",
        "¿Esto es seguro? ¿No hay alguien más escuchando?",
        "Necesito más detalles antes de cualquier cosa.",
        "El dinero deja rastros… no sé si me conviene.",
        "¿Esto es una prueba o algo así?"
        ],
        // 4 - Charlatán
        [
        "Mirá, justo ayer pensaba en cómo el dinero cambia las relaciones humanas, y ahora venís con esto… curioso.",
        "Podría ayudarte, pero dejame contarte algo antes, porque esto me recuerda una historia increíble.",
        "Te juro que una vez presté dinero y terminó siendo una experiencia filosófica casi absurda… pero bueno, decime.",
        "Esto es interesante, porque el concepto de deuda es más psicológico que económico, ¿sabías?",
        "Ok, sí, pero antes de eso tengo que explicarte algo que nadie me pidió pero igual voy a decir."
        ],
        // 5 - Corrupto
        [
        "Todo tiene un precio, incluso la ayuda.",
        "Podemos arreglarlo… dependiendo de qué obtengo a cambio.",
        "No es un problema de dinero, es de acuerdos.",
        "Esto se puede resolver, si hay incentivo correcto.",
        "Decime cuánto vale tu urgencia."
        ],
        // 6 - Introvertido
        [
        "...sí, decime.",
        "No sé si puedo ayudar mucho, pero te escucho.",
        "Es complicado para mí, pero entiendo.",
        "Podría intentarlo… no prometo nada.",
        "Prefiero cosas simples, esto no lo es tanto."
        ],
        // 7 - Gruñón
        [
        "No empieces con eso.",
        "Siempre lo mismo… dinero.",
        "¿No podés arreglártelas solo?",
        "Esto nunca es rápido, ¿verdad?",
        "Estoy ocupado, pero decime de una vez."
        ],
        // 8 - Manipulador
        [
        "Claro… aunque todo en esta vida tiene consecuencias.",
        "Podría ayudarte, pero quiero que recuerdes esto después.",
        "No hay problema, confío en vos… por ahora.",
        "Interesante momento para pedir algo así.",
        "Veremos cómo se alinea esto con lo que necesito más adelante."
        ],
        // 9 - DialogosSofia
        [
        "Sí,  pero... ¿me pagarás, verdad? \nNo sé si puedo confiar en ti.",
        "Claro que no. No digas tonterías.",
        "Está bien. Pero si no me pagas te prometo \nque no te volveré a hablar en tu vida.",
        "Quisiera ayudarte, pero no puedo hacerlo en este momento.",
        "Solo por esta vez, ¿entiendes?\n Solo porque eres tú."
        ],
        // 10 - DialogosLeah
        [@"Claro que estoy dispuesta a prestarte, 
la verdadera pregunta es cuánto estás dispuesto a devolver.",
        @"Claro, después de todo confío en ti. 
No lo suficiente para prestarte sin garantía, pero igual.",
        @"Depende, inversión o futura decepción?",
        @"No veo por qué no. Cometer errores de vez en cuando también es divertido.",
        @"5. No hay problema. He financiado ideas 
peores y personas menos convincentes."
        ]
    };
}

    