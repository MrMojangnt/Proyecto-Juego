using System;
using Terminal.Gui;
using System.IO;
using System.Globalization;
namespace Proyecto_Juego;

public class Events
{
       // Verificar si hay carreras
       public static bool carreras = false;
       //Kavayos
       public static int kavayo;
       public static decimal cantidad;
       //Noticias
    public static string[] titulosBuenos =
    {
        "Aumento del consumo interno",
        "Reducción de impuestos empresariales",
        "Boom tecnológico impulsa inversiones",
        "Exportaciones alcanzan récord histórico",
        "Descenso de la inflación",
        "Nuevos acuerdos comerciales internacionales",
        "Crecimiento económico superior al esperado",
        "Mayor confianza de los consumidores",
        "Descubrimiento de recursos estratégicos",
        "Bancos anuncian créditos más accesibles"
    };

    public static string[] descripcionesBuenas =
    {
        @"El aumento del gasto de los consumidores impulsa las
ventas y mejora las expectativas de las empresas.",
        @"El gobierno reduce la carga fiscal sobre
las compañías, aumentando sus beneficios netos.",
        @"Las empresas tecnológicas lideran una
ola de inversiones que fortalece al mercado.",
        @"Las exportaciones nacionales baten récords
y mejoran los ingresos de numerosas industrias.",
        @"La inflación cae a niveles bajos, favoreciendo
la estabilidad económica y el consumo.",
        @"Nuevos tratados comerciales abren mercados
adicionales para las empresas locales.",
        @"La economía crece más de lo previsto por los
analistas, impulsando la confianza inversora.",
        @"Los consumidores muestran optimismo sobre
el futuro económico y aumentan sus compras.",
        @"El hallazgo de recursos valiosos atrae
inversión nacional e internacional.",
        @"Las entidades financieras facilitan el
acceso al crédito para empresas y ciudadanos."
    };

    public static string[] titulosMalos =
    {
        "Caída del consumo interno",
        "Aumento de impuestos empresariales",
        "Crisis en el sector tecnológico",
        "Fuerte descenso de las exportaciones",
        "Repunte de la inflación",
        "Fracaso de negociaciones comerciales",
        "Desaceleración económica inesperada",
        "Pérdida de confianza de los consumidores",
        "Escasez de materias primas",
        "Restricción del crédito bancario"
    };

    public static string[] descripcionesMalas =
    {
        @"Los consumidores reducen sus gastos, afectando
negativamente las ventas de las empresas.",
        @"Las compañías enfrentan mayores costos
fiscales que reducen su rentabilidad.",
        @"Varias empresas tecnológicas reportan pérdidas
y provocan incertidumbre en el mercado.",
        @"Las exportaciones disminuyen significativamente,
afectando los ingresos de múltiples sectores.",
        @"La inflación aumenta y reduce
el poder adquisitivo de los ciudadanos.",
        @"Las negociaciones comerciales fracasan y
limitan las oportunidades de expansión empresarial.",
        @"La economía muestra señales de enfriamiento
que preocupan a los inversionistas.",
        @"Los consumidores reducen sus expectativas
económicas y retrasan compras importantes.",
        @"La falta de materias primas provoca
retrasos y mayores costos de producción.",
        @"Los bancos endurecen los requisitos para 
otorgar préstamos, frenando nuevas inversiones."
    };




    public static void Periodico(Toplevel top)
    {
        var VentanaPeriodico = new Window()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        top.Add(VentanaPeriodico);
    }

    public static void PasarTurnoPeriodico(ref string titulo , ref string descripcion, int NumeroDeArchivo, ref decimal change)
    {
        bool BuenaNoticia = false;
        Random rnd = new Random();
        BuenaNoticia = rnd.Next(0, 2) == 1;
        //Cuando es una buena noticia
        if (BuenaNoticia)
        {
            int numNoticia = rnd.Next(descripcionesBuenas.Length);
            Periodicos nuevanoticia = new Periodicos();
            titulo = nuevanoticia.titulo = titulosBuenos[numNoticia];
            descripcion = nuevanoticia.descripcion = descripcionesBuenas[numNoticia];
            change = nuevanoticia.change = (decimal)(rnd.NextDouble() * 0.20 + 0.05);
            
        }
        else
        { 
            int numNoticia = rnd.Next(descripcionesMalas.Length);
            Periodicos nuevanoticia = new Periodicos();
            titulo = nuevanoticia.titulo = titulosMalos[numNoticia];
            descripcion = nuevanoticia.descripcion = descripcionesMalas[numNoticia];
            change = nuevanoticia.change = -(decimal)(rnd.NextDouble() * 0.20 + 0.05);
        }
    }

    public static void PasarTurnoEventoPerderDinero(ref decimal balance)
    {
           decimal monto = 0;
           Random rnd = new Random();
           monto = rnd.Next(0, 10001);
           //Eventos perdida de dinero
           //Titulos malos
           string[] titulosFinancierosMalos = new string[]
           {
                  $"Pérdida de {monto.ToString("N0", CultureInfo.InvariantCulture)} por mantenimiento de cuenta",
                  $"Comisión de {monto.ToString("N0", CultureInfo.InvariantCulture)} en cajero automático externo",
                  $"Deducción fiscal de {monto.ToString("N0", CultureInfo.InvariantCulture)} sobre intereses ganados",
                  $"Intereses moratorios de {monto.ToString("N0", CultureInfo.InvariantCulture)} en tarjeta de crédito",
                  $"Impuesto estatal de {monto.ToString("N0", CultureInfo.InvariantCulture)} a las transacciones",
                  $"Gasto de {monto.ToString("N0", CultureInfo.InvariantCulture)} por transferencia internacional",
                  $"Penalización de {monto.ToString("N0", CultureInfo.InvariantCulture)} por cuenta bancaria inactiva",
                  $"Multa de {monto.ToString("N0", CultureInfo.InvariantCulture)} por retiro anticipado de fondos",
                  $"Cargo por sobregiro de {monto.ToString("N0", CultureInfo.InvariantCulture)} en cuenta corriente",
                  $"Impuesto digital de {monto.ToString("N0", CultureInfo.InvariantCulture)} aplicado por el gobierno",
                  $"Descuento de {monto.ToString("N0", CultureInfo.InvariantCulture)} por seguro obligatorio no solicitado",
                  $"Costo de {monto.ToString("N0", CultureInfo.InvariantCulture)} por reposición de tarjeta física",
                  $"Recargo de {monto.ToString("N0", CultureInfo.InvariantCulture)} por pago tardío de préstamo",
                  $"Comisión de {monto.ToString("N0", CultureInfo.InvariantCulture)} por intento de cobro de cheque devuelto",
                  $"Cobro de {monto.ToString("N0", CultureInfo.InvariantCulture)} por emisión de estado de cuenta impreso",
                  $"Retención gubernamental de {monto.ToString("N0", CultureInfo.InvariantCulture)} por adeudo fiscal",
                  $"Pérdida cambiaria de {monto.ToString("N0", CultureInfo.InvariantCulture)} en compra de divisas",
                  $"Tarifa de {monto.ToString("N0", CultureInfo.InvariantCulture)} por emisión de cheque certificado",
                  $"Anualidad de {monto.ToString("N0", CultureInfo.InvariantCulture)} por renovación de tarjeta",
                  $"Retención de {monto.ToString("N0", CultureInfo.InvariantCulture)} de ISR sobre rendimientos financieros"
           };

// Arreglo de Descripciones
           string[] descripcionesFinancierasMalos = new string[]
           {
                  $"El banco debitó automáticamente {monto.ToString("N0", CultureInfo.InvariantCulture)} de tu saldo por no mantener el depósito mínimo mensual requerido.",
                  $"Retiraste efectivo en una red bancaria ajena a la tuya y te aplicaron una comisión de {monto.ToString("N0", CultureInfo.InvariantCulture)} por el uso del ATM.",
                  $"El gobierno aplicó una retención automática de {monto.ToString("N0", CultureInfo.InvariantCulture)} directamente sobre los intereses devengados este mes.",
                  $"Por olvidar la fecha límite de pago, tu tarjeta de crédito generó {monto.ToString("N0", CultureInfo.InvariantCulture)} adicionales en intereses de financiación.",
                  $"El estado te descontó un total de {monto.ToString("N0", CultureInfo.InvariantCulture)} bajo el concepto de impuesto obligatorio a los movimientos financieros.",
                  $"Enviar dinero al extranjero te costó un cargo extra de {monto.ToString("N0", CultureInfo.InvariantCulture)} debido a las tarifas operativas de la banca corresponsal.",
                  $"Como no realizaste movimientos en los últimos seis meses, el banco te penalizó cobrando {monto.ToString("N0", CultureInfo.InvariantCulture)} por inactividad.",
                  $"Decidiste retirar tu dinero antes del vencimiento del plazo fijo y la institución te quitó {monto.ToString("N0", CultureInfo.InvariantCulture)} en comisiones.",
                  $"Tu cuenta se quedó momentáneamente en rojo y el banco te aplicó una tarifa fija de {monto.ToString("N0", CultureInfo.InvariantCulture)} por protección de sobregiro.",
                  $"El fisco recaudó {monto.ToString("N0", CultureInfo.InvariantCulture)} correspondientes al impuesto al valor agregado aplicado sobre tus suscripciones y servicios financieros.",
                  $"Te cargaron {monto.ToString("N0", CultureInfo.InvariantCulture)} por un seguro de vida asociado a tu tarjeta que se activó automáticamente y sin tu consentimiento.",
                  $"Solicitar un nuevo plástico físico debido a un aparente desgaste o extravío redujo tu saldo disponible en {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Te retrasaste en la fecha de vencimiento de tu crédito y la financiera te cobró {monto.ToString("N0", CultureInfo.InvariantCulture)} de penalización inmediata.",
                  $"Intentaste depositar un documento que terminó siendo rebotado, lo que te generó un cobro administrativo de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"El banco te cobró una tarifa de {monto.ToString("N0", CultureInfo.InvariantCulture)} por el simple hecho de enviarte el resumen financiero en papel a tu domicilio.",
                  $"La entidad tributaria ejecutó una orden de retención directa en tu cuenta de ahorros por {monto.ToString("N0", CultureInfo.InvariantCulture)} debido a una multa vencida.",
                  $"Al realizar una compra internacional, el banco aplicó un tipo de cambio desfavorable que te costó {monto.ToString("N0", CultureInfo.InvariantCulture)} extras.",
                  $"Solicitar un cheque de caja garantizado para realizar un trámite legal te costó {monto.ToString("N0", CultureInfo.InvariantCulture)} por la gestión en ventanilla.",
                  $"Llegó el mes de aniversario de tu tarjeta de crédito y el sistema te cobró automáticamente {monto.ToString("N0", CultureInfo.InvariantCulture)} por la membresía anual.",
                  $"Tus inversiones generaron ganancias, pero el gobierno te aplicó una retención en la fuente del ISR por un valor de {monto.ToString("N0", CultureInfo.InvariantCulture)}."
           };


           int numEvento = rnd.Next(titulosFinancierosMalos.Length);
           string titulo = titulosFinancierosMalos[numEvento];
           string descripcion = descripcionesFinancierasMalos[numEvento];
           int Message = MessageBox.Query(titulo, descripcion, "Aceptar");
           if (Message == 0)
           {
                  balance -= monto;
                  ModificarPartidas.Guardarelbalance();
           }
    }

    public static void PasarTurnoGanarDinero(ref decimal balance)
    {
           decimal monto = 0;
           Random rnd = new Random();
           monto = rnd.Next(0, 10001);
           //Ganancias financieras
           //Gana dinero
           string[] titulosGanancias = new string[]
           {
                  $"Pago de dividendos depositado por {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Intereses ganados de {monto.ToString("N0", CultureInfo.InvariantCulture)} en cuenta de ahorro",
                  $"Rendimiento de {monto.ToString("N0", CultureInfo.InvariantCulture)} por inversión a plazo fijo",
                  $"Bonificación de {monto.ToString("N0", CultureInfo.InvariantCulture)} por cashback de tarjeta",
                  $"Devolución fiscal de {monto.ToString("N0", CultureInfo.InvariantCulture)} por parte del gobierno",
                  $"Bono de bienvenida de {monto.ToString("N0", CultureInfo.InvariantCulture)} por apertura de cuenta",
                  $"Ganancia de {monto.ToString("N0", CultureInfo.InvariantCulture)} por venta de acciones",
                  $"Abono de {monto.ToString("N0", CultureInfo.InvariantCulture)} por concepto de regalías",
                  $"Recompensa de {monto.ToString("N0", CultureInfo.InvariantCulture)} por referir nuevos clientes",
                  $"Subsidio gubernamental recibido por {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Ingreso de {monto.ToString("N0", CultureInfo.InvariantCulture)} por rendimiento de fondos indexados",
                  $"Reembolso de {monto.ToString("N0", CultureInfo.InvariantCulture)} por cobro bancario indebido",
                  $"Ganancia cambiaria de {monto.ToString("N0", CultureInfo.InvariantCulture)} al vender divisas",
                  $"Pago recibido de {monto.ToString("N0", CultureInfo.InvariantCulture)} por concepto de pasivos",
                  $"Premio de {monto.ToString("N0", CultureInfo.InvariantCulture)} por sorteo de lealtad bancaria",
                  $"Abono de {monto.ToString("N0", CultureInfo.InvariantCulture)} por intereses de bonos soberanos",
                  $"Comisión financiera ganada de {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Plusvalía liquidada de {monto.ToString("N0", CultureInfo.InvariantCulture)} en bienes raíces",
                  $"Recompensa por staking de criptoactivos de {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Liquidación a favor de {monto.ToString("N0", CultureInfo.InvariantCulture)} de seguro automotriz"
           };

// Arreglo de Descripciones (Ganancias Financieras)
           string[] descripcionesGanancias = new string[]
           {
                  $"Tu portafolio de acciones generó un excelente rendimiento trimestral, depositando {monto.ToString("N0", CultureInfo.InvariantCulture)} directamente en tu cuenta de corretaje.",
                  $"Gracias a la tasa de interés preferencial de tu cuenta, este mes acumulaste {monto.ToString("N0", CultureInfo.InvariantCulture)} adicionales a favor de tu saldo.",
                  $"Finalizó el período de tu pagaré bancario y la institución te pagó {monto.ToString("N0", CultureInfo.InvariantCulture)} netos correspondientes a los intereses pactados.",
                  $"Por realizar tus compras mensuales con la tarjeta de crédito, el banco te recompensó con un reembolso de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"El fisco procesó de manera positiva tu declaración anual de impuestos y te transfirió un saldo a favor de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Cumpliste con los requisitos de depósito inicial y el banco te otorgó un incentivo de {monto.ToString("N0", CultureInfo.InvariantCulture)} en tu nueva cuenta corriente.",
                  $"Aprovechaste el alza del mercado para liquidar una posición financiera, lo que te dejó una utilidad neta de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"La plataforma de distribución financiera procesó los derechos de autor y te abonó {monto.ToString("N0", CultureInfo.InvariantCulture)} por los activos digitales.",
                  $"El programa de referidos se activó con éxito tras la inscripción de tus amigos, sumando {monto.ToString("N0", CultureInfo.InvariantCulture)} a tu saldo disponible.",
                  $"El gobierno acreditó en tu cuenta el apoyo financiero correspondiente a este mes por un valor de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Tu estrategia de inversión pasiva automatizada superó las expectativas, sumando {monto.ToString("N0", CultureInfo.InvariantCulture)} al valor total de tu fondo.",
                  $"Tras proceder con el reclamo, la auditoría del banco determinó que la tarifa fue errónea y te devolvió {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"El mercado de monedas extranjeras jugó a tu favor y obtuviste un beneficio de {monto.ToString("N0", CultureInfo.InvariantCulture)} al cambiar tus dólares a moneda local.",
                  $"Se registró un flujo de efectivo positivo de {monto.ToString("N0", CultureInfo.InvariantCulture)} derivado de los ingresos recurrentes de tus inversiones automatizadas.",
                  $"Fuiste uno de los usuarios seleccionados en la tómbola digital del banco, resultando ganador de un premio en efectivo de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"El tesoro nacional realizó el pago de los cupones semestrales, acreditando {monto.ToString("N0", CultureInfo.InvariantCulture)} por los títulos de deuda que posees.",
                  $"La intermediación financiera que realizaste fue exitosa, por lo que recibiste un pago de comisión por {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Se concretó la venta del fideicomiso inmobiliario en el que participabas, entregándote una ganancia de capital de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"La red validó tus bloques de participación en el protocolo financiero, generando {monto.ToString("N0", CultureInfo.InvariantCulture)} en recompensas pasivas de red.",
                  $"La aseguradora resolvió el dictamen de manera satisfactoria y te reembolsó {monto.ToString("N0", CultureInfo.InvariantCulture)} tras los ajustes de la póliza."
           };
           int numEvento = rnd.Next(titulosGanancias.Length);
           string titulo = titulosGanancias[numEvento];
           string descripcion = descripcionesGanancias[numEvento];
           int Message = MessageBox.Query(titulo, descripcion, "Aceptar");
           if (Message == 0)
           {
                  balance += monto;
                  ModificarPartidas.Guardarelbalance();

        }
    }

    public static void DecisionesInversion(ref decimal balance)
    {
           decimal monto = 0;
           Random rds = new Random();
           monto = rds.Next(10000, 10000001);
           // Arreglo de Títulos (Oportunidades de Inversión y Apuestas)
           string[] titulosOportunidades = new string[]
           {

                  $"Oportunidad de inversión ángel en Startup tecnológica desde {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Invierte {monto.ToString("N0", CultureInfo.InvariantCulture)} en el nuevo desarrollo inmobiliario residencial",
                  $"Apuesta recomendada de {monto.ToString("N0", CultureInfo.InvariantCulture)} para el torneo de fútbol internacional",
                  $"Participa en la preventa exclusiva del nuevo token DeFi por {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Conviértete en socio de una franquicia de café invirtiendo {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Copia la estrategia del trader líder de Forex iniciando con {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Adquiere bonos de energía renovable con un valor de {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Oportunidad de refugio financiero en contratos de oro por {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Invitación al fondo de capital de riesgo con un ticket de {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Invierte {monto.ToString("N0", CultureInfo.InvariantCulture)} en pagarés financieros de alto rendimiento garantizado",
                  $"Oportunidad de negocio en la compra de inventario mayorista por {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Compra una fracción de una obra de arte moderna valorada en {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Participa en la oferta pública inicial (IPO) de la Fintech del año con {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Invierte {monto.ToString("N0", CultureInfo.InvariantCulture)} en el proyecto de cultivo tecnológico sostenible",
                  $"Configura tu portafolio automatizado inyectando {monto.ToString("N0", CultureInfo.InvariantCulture)} en fondos indexados",
                  $"Únete al pool de validación y staking cripto aportando {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Diversifica prestando {monto.ToString("N0", CultureInfo.InvariantCulture)} a solicitantes pre-aprobados mediante P2P",
                  $"Invitación exclusiva al torneo de póker VIP con una entrada de {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Adquiere potencia de procesamiento de datos en la nube invirtiendo {monto.ToString("N0", CultureInfo.InvariantCulture)}",
                  $"Compra pagarés corporativos de la multinacional líder por {monto.ToString("N0", CultureInfo.InvariantCulture)}"
           };

           // Arreglo de Descripciones (Oportunidades de Inversión y Apuestas)
           string[] descripcionesOportunidades = new string[]
           {
                  $"Te invitamos a formar parte de la próxima gran empresa de inteligencia artificial; asegura tu participación con una inversión inicial de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Súmate al fondeo colectivo para la construcción de una torre de departamentos de lujo aportando un monto mínimo de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Nuestros analistas sugieren colocar un stake de {monto.ToString("N0", CultureInfo.InvariantCulture)} a la victoria del equipo local debido a su racha invicta esta temporada.",
                  $"Consigue acceso temprano antes del listado oficial en exchanges públicos adquiriendo un paquete de tokens valorado en {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Abre una nueva sucursal bajo un modelo llave en mano participando en el pool de inversionistas con una cuota fija de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Automatiza tus operaciones en el mercado de divisas replicando los movimientos del portafolio institucional con un capital base de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Apoya la transición hacia paneles solares a gran escala e invierte {monto.ToString("N0", CultureInfo.InvariantCulture)} en renta fija con un impacto ambiental positivo.",
                  $"Protege tu patrimonio contra la inflación abriendo una posición de futuros respaldada en lingotes de oro físicos desde {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Accede a un portafolio diversificado de empresas de biotecnología en etapa temprana aportando un capital semilla de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Asegura una tasa de interés fija bastante atractiva colocando {monto.ToString("N0", CultureInfo.InvariantCulture)} a un plazo de 365 días con total respaldo institucional.",
                  $"Únete a la campaña de importación de productos de alta demanda para la temporada comercial financiando un lote por {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"El mercado del arte ya no es solo para millonarios; adquiere tu token de copropiedad sobre un cuadro de colección por {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Te invitamos a colocar una orden de compra preferencial por {monto.ToString("N0", CultureInfo.InvariantCulture)} antes de que las acciones coticen de manera abierta en la bolsa.",
                  $"Financia la tecnificación de campos de cultivo hidropónico y recibe utilidades anuales invirtiendo un monto base de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Aprovechaste la corrección del mercado de valores para programar una inyección de capital de {monto.ToString("N0", CultureInfo.InvariantCulture)} en las empresas del S&P 500.",
                  $"Incrementa tus activos digitales congelando tus fondos en el nodo de gobernanza, con una participación mínima requerida de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Conviértete en el banco de alguien más y fondea microcréditos comerciales distribuyendo un capital total de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Reserva tu asiento en la mesa principal del campeonato internacional de Texas Hold'em cubriendo el buy-in obligatorio de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Participa en la infraestructura de procesamiento para redes blockchain arrendando servidores dedicados por un costo de {monto.ToString("N0", CultureInfo.InvariantCulture)}.",
                  $"Financia la expansión logística de la empresa líder del sector adquiriendo títulos de deuda privada con un valor nominal de {monto.ToString("N0", CultureInfo.InvariantCulture)}."
           };
           int numEvento = rds.Next(titulosOportunidades.Length);
           string titulo = titulosOportunidades[numEvento];
           string descripcion = descripcionesOportunidades[numEvento];
           int Message = MessageBox.Query(titulo, descripcion, "Invertir", "No, Gracias");
           if (Message == 0 && balance >= monto)
           {
                  balance -= monto;
                  int exito = rds.Next(0, 101);
                  if (exito < 40)
                  {
                         MessageBox.Query("Suerte para la proxima", "Haz perdido tu capital", "Cerrar");
                         ModificarPartidas.Guardarelbalance();
                  }
                  else if (exito <= 60)
                  {
                         MessageBox.Query("Ganaste, nada...", "Al menos recuperaste tu dinero", "Cerrar");
                         balance += monto;
                         ModificarPartidas.Guardarelbalance();
                  }
                  else if (exito <= 90)
                  {
                         MessageBox.Query("Ganaste dinero", "Te llevas a tu casa un 50% de lo invertido", "Cerrar");
                         balance += monto + (monto *0.5m);
                         ModificarPartidas.Guardarelbalance();
                  }
                  else if (exito <= 100)
                  {
                         MessageBox.Query("Exito total", "Ganaste un 300% tu dinero", "Cerrar");
                         balance += monto + (monto * 3);
                         ModificarPartidas.Guardarelbalance();
                  }
           }
           else if (Message == 1 || balance < monto)
           {
                  MessageBox.Query("Para la proxima sera...", "Otras ofertas vendran...", "Cerrar");
           }
    }

    public static void ApuestasOportunidades( Toplevel top)
    {
           string titulo = "Oportunidad de apostar";
           string descripcion = "Proximas carreras de caballos";
           int apuesta = MessageBox.Query(titulo, descripcion, "Apostar", "No, Gracias");
           if (apuesta == 0)
           {
                  //variable para ver si es entero
                  bool IsDecimal = false;
                  var dialog = new Dialog("Apuesta de Caballos", 60, 15);

                  var lblCaballo = new Label("Seleccione un caballo:")
                  {
                         X = 2,
                         Y = 1
                  };

                  var listaCaballos = new ListView(new string[]
                  {
                         "Kavayo rojo", "Kavayo Amarillo", "Kavayo Verde", "Kavayo Negro" 
                  })
                  {
                         X = 2,
                         Y = 2,
                         Width = 25,
                         Height = 5
                  };

                  var lblDinero = new Label("Monto a apostar:")
                  {
                         X = 30,
                         Y = 2
                  };

                  var txtDinero = new TextField("")
                  {
                         X = 30,
                         Y = 3,
                         Width = 20
                  };

                  txtDinero.TextChanging += (e) =>
                  {
                      if (e.NewText.Length >= 6)
                      {
                          e.Cancel = true;
                      }
                  };

                  var btnAceptar = new Button("Aceptar")
                  {
                         X = Pos.Center() - 10,
                         Y = 8
                  };

                  var btnCancelar = new Button("Cancelar")
                  {
                         X = Pos.Center() + 2,
                         Y = 8
                  };

            btnAceptar.Clicked += () =>
            {
                kavayo = listaCaballos.SelectedItem;
                if (decimal.TryParse(txtDinero.Text.ToString(), out cantidad))
                {
                    IsDecimal = true;
                }
                if (IsDecimal && cantidad <= Program.pd.balance && cantidad <= Program.pd.balance-1)
                {
                    Application.RequestStop();
                    top.RemoveAll();
                    Apuesta.Iniciar(top);
                    carreras = false;
                }

                else
                {
                    MessageBox.Query("ERROR", "No tienes esa cantidad de dinero", "Cerrar");
                }
            };

                  btnCancelar.Clicked += () =>
                  {
                         
                         top.RemoveAll();
                         Program.Inicio(top);
                         Application.RequestStop();
                         carreras = false;
                         
                  };

                  dialog.Add(
                         lblCaballo,
                         listaCaballos,
                         lblDinero,
                         txtDinero,
                         btnAceptar,
                         btnCancelar
                  );
                  Application.Run(dialog);

                  
           } else if (apuesta == 1)
           {
                  MessageBox.Query("BAH", "Tú te lo pierdes", "Cerrar");
                  carreras = false;
           }
           
    }

    public static void GestorDeEventos(ref decimal balances)
    {
           Random rnd = new Random();
           int Evento = rnd.Next(0, 10);
           if (Evento == 0)
           {
                  PasarTurnoEventoPerderDinero(ref balances);
                  ModificarPartidas.Guardarelbalance();
           }
        else if (Evento == 1)
           {
                  PasarTurnoGanarDinero(ref balances);
                  ModificarPartidas.Guardarelbalance();
           }
        else if (Evento == 2)
           {
                  DecisionesInversion(ref balances);
                  ModificarPartidas.Guardarelbalance();
           }
        else if (Evento == 3)
           {
                  carreras = true;
           }
    }

    public static void Apuestas(Toplevel top)
    {
           if (carreras)
           {
                  ApuestasOportunidades(top);
           }
    }
}
