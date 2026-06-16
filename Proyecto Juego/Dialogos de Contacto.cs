using System;
using Terminal.Gui;
using System.IO;
namespace Proyecto_Juego;

public class Dialogos_de_Contacto
{
    public static void ConsejosDeContacto()
    {

    }

    public static void DialogosAlContestar()
    {
        NPC pj = new NPC();
        switch (pj.idArquetipo)
        {
            case 0://Raul aqui pone cosas que decis cuando contestas una llamada
                string[] DialogosDeRaul = new string[]
                {"¡Qué alegría escucharte! ¿Cómo va todo?", "¡Hola! Contame, ¿qué novedades traés?", "¡Buen momento para llamar! ¿Qué necesitás?", "¡Amigo! ¿Qué se cuenta?", "¡Qué gusto! ¿En qué puedo ayudarte?"};
                break;
            case 1:
                string[] DialogoDelGeneroso = new string[]
                {
                    "Hola, siempre es bueno saber de vos.",
                    "¿Cómo estás? Espero que todo vaya bien.",
                    "¡Qué bueno que llamaste! Decime.",
                    "Hola, ¿cómo puedo ayudarte hoy?",
                    "Adelante, te escucho."
                };
                break;
            case 2:
                string[] DialgoDelTacaño = new string[]
                {
                    "Sí... ¿qué necesitás?", "Espero que esto sea importante.", "Decime rápido, estoy ocupado." ,"¿Qué pasa?", "Sí, hablá."
                };
                break;
            case 3:
                string[] DialogoDelParanoico = new string[]
                {
                    "¿Estás solo?", "¿Confirmaste que nadie te sigue?", "Habrá que hablar rápido...", "Espero que esta llamada sea segura.", "¿Quién más sabe que estamos hablando?"
                };
                break;
                case 4:
                string[] DialogoDelCharlatan = new string[]
                {
                    "¡No sabés todo lo que tengo para contarte!", "¡Justo estaba pensando en vos!", "¡Qué casualidad que llamaras!", "¡Esperá, esperá! Tengo una historia increíble.", "¡Qué bueno escucharte! Contame todo."
                };
                break;
                case 5:
                string[] DialogoDelFilantropo = new string[]
                {
                    "Es un placer escucharte.", "Espero que estés teniendo un buen día.", "Siempre hay tiempo para un amigo.", "Me alegra que hayas llamado.", "¿Cómo puedo servirte?"
                };
                break;
            case 6:
                string[] DialogoDelCorrupto = new string[]
                {
                    "Depende de lo que quieras...", "¿Es un negocio?", "Espero que valga la pena atender.", "Habrá que ver qué ganamos.", "Te escucho."
                };
                break;
            case 7:
                string[] DialogoDelIntrovertido = new string[]
                {
                    "Hola...", "Sí... decime.", "Estoy escuchando.", "Hola, ¿todo bien?", "¿Qué necesitás?"
                };
                break;
            case 8:
                string[] DialogoDelOptimista = new string[]
                {
                    "¡Seguro traés buenas noticias!", "¡Qué alegría escuchar tu voz!", "¡Hoy pinta para un gran día!", "¡Excelente! Contame.", "¡Todo va a salir bien! ¿Qué pasa?"
                };
                break;
            case 9:
                string[] DialogoDelPesimista = new string[]
                {
                    "¿Qué salió mal ahora?", "Cuando suena el teléfono nunca espero algo bueno...", "Espero equivocarme, pero esto no pinta bien.", "Decime... aunque sospecho lo peor.", "¿Qué problema tenemos ahora?"
                };
                break;
            case 10:
                string[] DialogoDelCalculador = new string[]
                {
                    "Te escucho.", "Espero que esta conversación sea útil.", "¿Qué asunto querés tratar?", "Vamos al punto.", "Estoy ocupado, hablá.",
                };
                break;
            case 11:
                string[] DialogoDelIngenuo = new string[]
                {
                    "¡Hola! Qué bueno que llamaste.", "Siempre es un gusto hablar con vos.", "¿Todo está bien? Me alegra escucharte.", "¡Qué sorpresa tan agradable!", "Decime, ¿en qué andás?"
                };
                break;
            case 12:
                string[] DialogoDelGrunon = new string[]
                {
                    "¿Qué?", "Sí, hablá.", "¿Qué querés ahora?", "¿Y ahora qué pasó?", "Estoy ocupado."
                };
                break;
            case 13:
                string[] DialogoDelManipulador = new string[]
                {
                    "¡Qué alegría saber de vos!", "Justamente estaba pensando en llamarte.", "Qué coincidencia tan conveniente.", "Siempre es bueno hablar con amigos.", "Contame, seguro encontramos una solución."
                };
                break;
            case 14:
                string[] DialogoDelMaquiavelico = new string[]
                {
                    "Interesante que hayas llamado.", "Te escucho atentamente.", "Todo contacto tiene un motivo.", "Veamos qué podemos sacar de esto.", "Hablá. Estoy escuchando."
                };
                break;

        }
    }


}
