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

    public static void DialogosAlContestar(NPC contactos)
    {
        switch (contactos.idArquetipo)
        {
            case 0://Raul aqui pone cosas que decis cuando contestas una llamada
                string[] DialogosDeRaul = 
                ["¡Qué alegría escucharte! ¿Cómo va todo?", "¡Hola! Contame, ¿qué novedades traés?", "¡Buen momento para llamar! ¿Qué necesitás?", "¡Amigo! ¿Qué se cuenta?", "¡Qué gusto! ¿En qué puedo ayudarte?"];
                Llamar(DialogosDeRaul, contactos);
                break;
            case 1:
                string[] DialogoDelGeneroso =
                [
                    "Hola, siempre es bueno saber de vos.",
                    "¿Cómo estás? Espero que todo vaya bien.",
                    "¡Qué bueno que llamaste! Decime.",
                    "Hola, ¿cómo puedo ayudarte hoy?",
                    "Adelante, te escucho."
                ];
                Llamar(DialogoDelGeneroso, contactos);
                break;
            case 2:
                string[] DialgoDelTacaño = 
                [
                    "Sí... ¿qué necesitás?", "Espero que esto sea importante.", "Decime rápido, estoy ocupado." ,"¿Qué pasa?", "Sí, hablá."
                ];
                Llamar(DialgoDelTacaño, contactos);
                break;
            case 3:
                string[] DialogoDelParanoico =
                [
                    "¿Estás solo?", "¿Confirmaste que nadie te sigue?", "Habrá que hablar rápido...", "Espero que esta llamada sea segura.", "¿Quién más sabe que estamos hablando?"
                ];
                Llamar(DialogoDelParanoico, contactos);
                break;
            case 4:
                string[] DialogoDelCharlatan = 
                [
                    "¡No sabés todo lo que tengo para contarte!", "¡Justo estaba pensando en vos!", "¡Qué casualidad que llamaras!", "¡Esperá, esperá! Tengo una historia increíble.", "¡Qué bueno escucharte! Contame todo."
                ];
                Llamar(DialogoDelCharlatan, contactos);
                break;
            case 5:
                string[] DialogoDelFilantropo = 
                [
                    "Es un placer escucharte.", "Espero que estés teniendo un buen día.", "Siempre hay tiempo para un amigo.", "Me alegra que hayas llamado.", "¿Cómo puedo servirte?"
                ];
                Llamar(DialogoDelFilantropo, contactos);
                break;
            case 6:
                string[] DialogoDelCorrupto = 
                [
                    "Depende de lo que quieras...", "¿Es un negocio?", "Espero que valga la pena atender.", "Habrá que ver qué ganamos.", "Te escucho."
                ];
                Llamar(DialogoDelCorrupto, contactos);
                break;
            case 7:
                string[] DialogoDelIntrovertido = 
                [
                    "Hola...", "Sí... decime.", "Estoy escuchando.", "Hola, ¿todo bien?", "¿Qué necesitás?"
                ];
                Llamar(DialogoDelIntrovertido, contactos);
                break;
            case 8:
                string[] DialogoDelOptimista = 
                [
                    "¡Seguro traés buenas noticias!", "¡Qué alegría escuchar tu voz!", "¡Hoy pinta para un gran día!", "¡Excelente! Contame.", "¡Todo va a salir bien! ¿Qué pasa?"
                ];
                Llamar(DialogoDelOptimista, contactos);
                break;
            case 9:
                string[] DialogoDelPesimista = 
                [
                    "¿Qué salió mal ahora?", "Cuando suena el teléfono nunca espero algo bueno...", "Espero equivocarme, pero esto no pinta bien.", "Decime... aunque sospecho lo peor.", "¿Qué problema tenemos ahora?"
                ];
                Llamar(DialogoDelPesimista, contactos);
                break;
            case 10:
                string[] DialogoDelCalculador =
                [
                    "Te escucho.", "Espero que esta conversación sea útil.", "¿Qué asunto querés tratar?", "Vamos al punto.", "Estoy ocupado, hablá.",
                ];
                Llamar(DialogoDelCalculador, contactos);
                break;
            case 11:
                string[] DialogoDelIngenuo = 
                [
                    "¡Hola! Qué bueno que llamaste.", "Siempre es un gusto hablar con vos.", "¿Todo está bien? Me alegra escucharte.", "¡Qué sorpresa tan agradable!", "Decime, ¿en qué andás?"
                ];
                Llamar(DialogoDelIngenuo, contactos);
                break;
            case 12:
                string[] DialogoDelGrunon = 
                [
                    "¿Qué?", "Sí, hablá.", "¿Qué querés ahora?", "¿Y ahora qué pasó?", "Estoy ocupado."
                ];
                Llamar(DialogoDelGrunon, contactos);
                break;
            case 13:
                string[] DialogoDelManipulador = 
                [
                    "¡Qué alegría saber de vos!", "Justamente estaba pensando en llamarte.", "Qué coincidencia tan conveniente.", "Siempre es bueno hablar con amigos.", "Contame, seguro encontramos una solución."
                ];
                Llamar(DialogoDelManipulador, contactos);
                break;
            case 14:
                string[] DialogoDelMaquiavelico = 
                [
                    "Interesante que hayas llamado.", "Te escucho atentamente.", "Todo contacto tiene un motivo.", "Veamos qué podemos sacar de esto.", "Hablá. Estoy escuchando."
                ];
                Llamar(DialogoDelMaquiavelico, contactos);
                break;

        }
    }
    static void Llamar(string[] Dialogoss, NPC contacto)
    {
        int Indice = Random.Shared.Next(0, Dialogoss.Length);
        var dialog = new Dialog("", 60,20);
        var dialogo = new FrameView("")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(8),
            Width = Dim.Fill() - 2,
            Height = 7
        };

        var nombre = new Label(contacto.name)
        {
            X = 2,
            Y = 0
        };

        var texto = new Label(Dialogoss[Indice])
        {
            X = 2,
            Y = 2
        };

        var cerrar = new Button("Cerrar") 
        { 
        X = 5,
        Y = 6
        };

        cerrar.Clicked += () =>
        {
            Application.RequestStop();
        };
        dialogo.Add(texto);
        dialog.Add(dialogo, cerrar);
        Application.Run(dialog);
    }

}
