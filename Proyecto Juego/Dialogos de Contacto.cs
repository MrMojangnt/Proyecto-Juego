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

    static readonly string[] DialogoDelCorrupto =
    [
        "Depende de lo que quieras...", "¿Es un negocio?", "Espero que valga la pena atender.", "Habrá que ver qué ganamos.", "Te escucho."
    ];

    static readonly string[] DialogoDelIntrovertido =
    [
        "Hola...", "Sí... decime.", "Estoy escuchando.", "Hola, ¿todo bien?", "¿Qué necesitás?"
    ];

    static readonly string[] DialogoDelGrunon =
    [
        "¿Qué?", "Sí, hablá.", "¿Qué querés ahora?", "¿Y ahora qué pasó?", "Estoy ocupado."
    ];

    static readonly string[] DialogoDelManipulador =
    [
        "¡Qué alegría saber de vos!", "Justamente estaba pensando en llamarte.", "Qué coincidencia tan conveniente.", "Siempre es bueno hablar con amigos.", "Contame, seguro encontramos una solución."
    ];





    static readonly string[][] Dialogos =
    {
        DialogosDeRaul,
        DialogoDelGeneroso,
        DialogoDelTacano,
        DialogoDelParanoico,
        DialogoDelCharlatan,
        DialogoDelCorrupto,
        DialogoDelIntrovertido,
        DialogoDelGrunon,
        DialogoDelManipulador,

    };
    public static void DialogosAlContestar(NPC contacto)
    {
        Llamar(contacto);
    }
    static void Plantilla(string[] Dialogoss, Dialog dial, Label texto)
    {
        //Llamar(Dialogos[contacto.idArquetipo], contacto);
        
        var Prestamo = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(8)
        };
        var bt1 = new Button()
        {
            X = Pos.Right(Prestamo),
            Y = Pos.AnchorEnd(8)
        };

        var Consejo = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(6)
        };
        var bt2 = new Button()
        {
            X = Pos.Right(Consejo),
            Y = Pos.AnchorEnd(6)
        };
        var Charlar = new Label()
        {
            X = 2,
            Y = Pos.AnchorEnd(4)
        };
        var bt3 = new Button()
        {
            X = Pos.Right(Charlar),
            Y = Pos.AnchorEnd(4)
        };
        dial.Add(Prestamo, Consejo, Charlar, bt1, bt2, bt3);
        Application.MainLoop.AddIdle(() =>
        {
            PrimerDialogo(Dialogoss, texto, Prestamo, bt1, Consejo, bt2, Charlar, bt3);
            return false;
        });
    }
    static void PrimerDialogo(string[] Dialogoss, Label texto,Label Prestamo, Button bt1, Label Consejo, Button bt2, Label Charlar, Button bt3)
    {
        int indice = Random.Shared.Next(Dialogoss.Length);
        string frase = Dialogoss[indice];

        texto.Text = "";
        string acumulado = "";
        bt1.Visible = false;
        bt2.Visible = false;
        bt3.Visible = false;
        foreach (char c in frase)
        {
            acumulado += c;
            texto.Text = acumulado;
            Application.Refresh();
            Thread.Sleep(30);
        }
        bt1.Visible = true;
        bt2.Visible = true;
        bt3.Visible = true;
        Prestamo.Text = "¿Cómo va todo?"; // Prestamo

        bt1.Clicked += () =>
        {
            MenuPrestamo(Prestamo,  bt1,  Consejo,  bt2,  Charlar,  bt3);
        };
        Consejo.Text = "Necesito un consejo.";

        Charlar.Text = "Solo quería charlar.";
    }
    static void MenuPrestamo(Label op1, Button bt1, Label op2, Button bt2, Label op3, Button bt3)
    {
        op1.Text = "Necesito un préstamo.";
        op2.Text = "Estoy pasando un mal momento financiero.";
        op3.Text = "¿Podrías ayudarme con algo de dinero?";
    }

    static void Llamar( NPC contacto)
    {


        var dialog = new Dialog("", 60, 20);
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
        var texto = new Label()
        {
            X = 2,
            Y = 2
        };
        //dialogo respuesta
        var cerrar = new Button("Colgar")
        {
            X = 2,
            Y = Pos.AnchorEnd(2)
        };

        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        dialogo.Add(texto);
        Plantilla(Dialogos[contacto.idArquetipo], dialog, texto);

        dialog.Add(nombre, dialogo, cerrar);

        Application.Run(dialog);
    }


}
