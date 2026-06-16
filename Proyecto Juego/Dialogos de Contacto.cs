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
        static readonly string[] DialogosDeRaul =
        ["¡Qué alegría escucharte! ¿Cómo va todo?", "¡Hola! Contame, ¿qué novedades traés?", "¡Buen momento para llamar! ¿Qué necesitás?", "¡Amigo! ¿Qué se cuenta?", "¡Qué gusto! ¿En qué puedo ayudarte?"];
        static readonly string[] DialogoDelGeneroso =
        [
            "Hola, siempre es bueno saber de vos.",
                    "¿Cómo estás? Espero que todo vaya bien.",
                    "¡Qué bueno que llamaste! Decime.",
                    "Hola, ¿cómo puedo ayudarte hoy?",
                    "Adelante, te escucho."
        ];

        static readonly string[] DialogoDelTacano =
        [
            "Sí... ¿qué necesitás?", "Espero que esto sea importante.", "Decime rápido, estoy ocupado." ,"¿Qué pasa?", "Sí, hablá."
        ];

        static readonly string[] DialogoDelParanoico =
        [
            "¿Estás solo?", "¿Confirmaste que nadie te sigue?", "Habrá que hablar rápido...", "Espero que esta llamada sea segura.", "¿Quién más sabe que estamos hablando?"
        ];

        static readonly string[] DialogoDelCharlatan =
        [
            "¡No sabés todo lo que tengo para contarte!", "¡Justo estaba pensando en vos!", "¡Qué casualidad que llamaras!", "¡Esperá, esperá! Tengo una historia increíble.", "¡Qué bueno escucharte! Contame todo."
        ];

        static readonly string[] DialogoDelFilantropo =
        [
            "Es un placer escucharte.", "Espero que estés teniendo un buen día.", "Siempre hay tiempo para un amigo.", "Me alegra que hayas llamado.", "¿Cómo puedo servirte?"
        ];

        static readonly string[] DialogoDelCorrupto =
        [
            "Depende de lo que quieras...", "¿Es un negocio?", "Espero que valga la pena atender.", "Habrá que ver qué ganamos.", "Te escucho."
        ];

        static readonly string[] DialogoDelIntrovertido =
        [
            "Hola...", "Sí... decime.", "Estoy escuchando.", "Hola, ¿todo bien?", "¿Qué necesitás?"
        ];

        static readonly string[] DialogoDelOptimista =
        [
            "¡Seguro traés buenas noticias!", "¡Qué alegría escuchar tu voz!", "¡Hoy pinta para un gran día!", "¡Excelente! Contame.", "¡Todo va a salir bien! ¿Qué pasa?"
        ];

        static readonly string[] DialogoDelPesimista =
        [
            "¿Qué salió mal ahora?", "Cuando suena el teléfono nunca espero algo bueno...", "Espero equivocarme, pero esto no pinta bien.", "Decime... aunque sospecho lo peor.", "¿Qué problema tenemos ahora?"
        ];

        static readonly string[] DialogoDelCalculador =
        [
            "Te escucho.", "Espero que esta conversación sea útil.", "¿Qué asunto querés tratar?", "Vamos al punto.", "Estoy ocupado, hablá.",
                ];

        static readonly string[] DialogoDelIngenuo =
        [
            "¡Hola! Qué bueno que llamaste.", "Siempre es un gusto hablar con vos.", "¿Todo está bien? Me alegra escucharte.", "¡Qué sorpresa tan agradable!", "Decime, ¿en qué andás?"
        ];

        static readonly string[] DialogoDelGrunon =
        [
            "¿Qué?", "Sí, hablá.", "¿Qué querés ahora?", "¿Y ahora qué pasó?", "Estoy ocupado."
        ];

        static readonly string[] DialogoDelManipulador =
        [
            "¡Qué alegría saber de vos!", "Justamente estaba pensando en llamarte.", "Qué coincidencia tan conveniente.", "Siempre es bueno hablar con amigos.", "Contame, seguro encontramos una solución."
        ];

        static readonly string[] DialogoDelMaquiavelico =
        [
            "Interesante que hayas llamado.", "Te escucho atentamente.", "Todo contacto tiene un motivo.", "Veamos qué podemos sacar de esto.", "Hablá. Estoy escuchando."
        ];



    
    static readonly string[][] Dialogos =
    {
        DialogosDeRaul,
        DialogoDelGeneroso,
        DialogoDelTacano,
        DialogoDelParanoico,
        DialogoDelCharlatan,
        DialogoDelFilantropo,
        DialogoDelCorrupto,
        DialogoDelIntrovertido,
        DialogoDelOptimista,
        DialogoDelPesimista,
        DialogoDelCalculador,
        DialogoDelIngenuo,
        DialogoDelGrunon,
        DialogoDelManipulador,
        DialogoDelMaquiavelico
       
    };
    public static void DialogosAlContestar(NPC contacto)
    {
        Llamar(Dialogos[contacto.idArquetipo], contacto);
    }
    static void PrimerasOpciones(Dialog dial)
    {
        var Prestamo = new Label("¿Cómo va todo?")
        {
            X = 2,
            Y = Pos.AnchorEnd(8)
        };
        var bt1 = new Button()
        {
            X = Pos.Right(Prestamo),
            Y = Pos.AnchorEnd(8)
        };
        var Consejo = new Label("Necesito un consejo.")
        {
            X = 2,
            Y = Pos.AnchorEnd(6)
        };
        var bt2 = new Button()
        {
            X = Pos.Right(Consejo),
            Y = Pos.AnchorEnd(6)
        };
        var Charlar = new Label("Solo quería charlar.")
        {
            X = 2,
            Y = Pos.AnchorEnd(4)
        };
        var bt3 = new Button()
        {
            X = Pos.Right(Charlar),
            Y = Pos.AnchorEnd(4)
        };

        dial.Add(Prestamo, Consejo, Charlar,bt1, bt2, bt3);
    }
    static void Llamar(string[] Dialogoss, NPC contacto)
    {
        //Llamar(Dialogos[contacto.idArquetipo], contacto);
        int indice = Random.Shared.Next(Dialogoss.Length);

        
        var dialog = new Dialog("", 60,20);
        var dialogo = new FrameView("")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(15),
            Width = Dim.Fill() - 2,
            Height = 7
        };

        var nombre = new Label(contacto.name)
        {
            X = 2,
            Y = 0
        };

        var texto = new Label(Dialogoss[indice])
        {
            X = 2,
            Y = 2
        };
        //dialogo respuesta
        var cerrar = new Button("Cortar") 
        { 
            X = 2,
            Y = Pos.AnchorEnd(2)
        };

        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        PrimerasOpciones(dialog);
        dialogo.Add(texto);
        dialog.Add(nombre, dialogo, cerrar);
        Application.Run(dialog);
    }
    
}
