using Empresas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using Terminal.Gui;

namespace Proyecto_Juego;

public class Tablasdefrancisco
{
    //MOSTRAR REPORTE DE BALANCE
    public static void MostrarReporteBalance(Toplevel top)
    {
        AsegurarHistorialBalance(Program.InvInt);

        decimal valorCartera;
        decimal costoBase;
        int totalAcciones;
        DataTable tablaPosiciones = CrearTablaPosicionesJugador(out valorCartera, out costoBase, out totalAcciones);
        DataTable tablaMovimientos = CrearTablaMovimientosBalance();
        decimal patrimonioEstimado = Program.pd.balance + valorCartera - ManejoDeArchivos.DeudaEmergencia;
        decimal gananciaFlotante = valorCartera - costoBase;

        var VentanaBalance = new Window("Reporte de Balance")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Program.colores[Program.colora]
        };

        var labelResumen = new Label(
            $@"Inversor: {Program.pd.name} | País: {Program.pd.pais} | Turno: {ManejoDeArchivos.turno}
Efectivo: ${Program.pd.balance.ToString("N2")} | Cartera: ${valorCartera.ToString("N2")} | Deuda: ${ManejoDeArchivos.DeudaEmergencia:F2} | Patrimonio estimado: ${patrimonioEstimado.ToString("N2")}
Costo base: ${costoBase.ToString("N2")} | Ganancia/Pérdida flotante: ${gananciaFlotante:+0.00;-0.00;0.00} | Acciones: {totalAcciones}")
        {
            X = 1,
            Y = 1
        };

        var marcoPosiciones = new FrameView("Posiciones actuales")
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaPosicionesView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaPosicionesView.Table = tablaPosiciones;
        marcoPosiciones.Add(tablaPosicionesView);

        var marcoMovimientos = new FrameView("Compras y ventas")
        {
            X = 1,
            Y = 15,
            Width = Dim.Fill() - 2,
            Height = 9
        };

        var tablaMovimientosView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        tablaMovimientosView.Table = tablaMovimientos;
        marcoMovimientos.Add(tablaMovimientosView);

        var botonVolver = new Button("Volver al Inicio")
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd(2)
        };

        botonVolver.Clicked += () =>
        {
            top.RemoveAll();
            Program.Inicio(top);
        };

        VentanaBalance.Add(labelResumen, marcoPosiciones, marcoMovimientos, botonVolver);
        top.Add(VentanaBalance);
    }

    public static DataTable CrearTablaPosicionesJugador(out decimal valorCartera, out decimal costoBase, out int totalAcciones)
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Costo compra");
        tabla.Columns.Add("Precio actual");
        tabla.Columns.Add("Valor actual");
        tabla.Columns.Add("Ganancia/Pérdida");

        valorCartera = 0m;
        costoBase = 0m;
        totalAcciones = 0;

        if (!File.Exists(ManejoDeArchivos.rutaInventario(Program.InvInt)))
        {
            return tabla;
        }

        List<Acciones> acciones = Inventario.CargarInventario(Program.InvInt);
        foreach (Acciones accion in acciones)
        {
            Companias empresa;
            decimal precioActual = accion.CostoActual;
            string sector = "Desconocido";
            string nombreEmpresa = accion.name;

            if (TryObtenerEmpresa(accion.id, out empresa))
            {
                precioActual = ObtenerPrecioAccion(empresa);
                sector = empresa.rubro;
                nombreEmpresa = empresa.name;
            }

            decimal valorActual = precioActual * accion.cantidad;
            decimal costoRegistrado = accion.CostoDeCompra * accion.cantidad;
            decimal diferencia = valorActual - costoRegistrado;

            valorCartera += valorActual;
            costoBase += costoRegistrado;
            totalAcciones += accion.cantidad;

            tabla.Rows.Add(
                nombreEmpresa,
                sector,
                accion.cantidad,
                $"{accion.CostoDeCompra:F2}",
                $"{precioActual:F2}",
                $"{valorActual:F2}",
                $"{diferencia:+0.00;-0.00;0.00}");
        }

        return tabla;
    }
    public static DataTable CrearTablaMovimientosBalance()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Tipo");
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cant.");
        tabla.Columns.Add("Precio");
        tabla.Columns.Add("Total");
        tabla.Columns.Add("Balance");
        tabla.Columns.Add("Turno");

        if (!File.Exists(ManejoDeArchivos.rutaBalance(Program.InvInt)))
        {
            return tabla;
        }

        string[] lineas = File.ReadAllLines(ManejoDeArchivos.rutaBalance(Program.InvInt), Encoding.UTF8);
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(';');
            if (datos.Length < 9)
            {
                continue;
            }

            tabla.Rows.Add(
                datos[0],
                datos[2],
                datos[3],
                datos[4],
                datos[5],
                datos[6],
                datos[7],
                datos[8]);
        }

        return tabla;
    }

    static decimal ObtenerPrecioAccion(Companias empresa)
    {
        return (empresa.capbursatil * 1000000m) / 50000000m;
    }

    static bool TryObtenerEmpresa(int id, out Companias empresa)
    {
        foreach (Companias actual in CargandoLasPartidas.Companiass)
        {
            if (actual.id == id)
            {
                empresa = actual;
                return true;
            }
        }

        empresa = default;
        return false;
    }
    public static void AsegurarHistorialBalance(int indice)
    {
        if (!File.Exists(ManejoDeArchivos.rutaBalance(indice)))
        {
            CreandoNuevaPartida.InicializarHistorialBalance(indice);
        }
    }
    public static DataTable ObtenerPronosticoMercado()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add("Empresa");
        tabla.Columns.Add("Sector");
        tabla.Columns.Add("Cambio esperado");
        tabla.Columns.Add("Precio estimado");

        foreach (Companias empresa in CargandoLasPartidas.Companiass)
        {
            decimal cambio = 0m;
            CambiosDelMercado.PronosticoMercado.TryGetValue(empresa.id, out cambio);
            decimal precioEstimado = Math.Round(empresa.capbursatil + (empresa.capbursatil * cambio), 2);

            tabla.Rows.Add(
                empresa.name,
                empresa.rubro,
                $"{cambio:+0.00%;-0.00%;0.00%}",
                $"{precioEstimado:F2}M");
        }

        return tabla;
    }


}

public class Tablasdejocksand
{
    public static void MostrarDetalleEmpresa(Toplevel top, Companias empresa)
    {
        var DetalleEmpresa = new Window("Detalle de Empresa")
        {
            X = 0,
            Y = 0,
            ColorScheme = Program.colores[Program.colora],
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        var MasInfo = new Label(
@$" ID: {empresa.id}                  
 Empresa: {empresa.name}           
 País: {empresa.pais}              
 Sector: {empresa.rubro}           
 Capital: {empresa.capbursatil:F2}M   
 Accionistas: {empresa.accionistas}
 balance: {empresa.balance}M       ")
        {
            X = 1,
            Y = 0,

        };
        var chunchito1 = new FrameView("")
        {
            X = 1,
            Y = 0,
            Width = 40,
            Height = 10
        };
        DetalleEmpresa.Add(chunchito1);
        chunchito1.Add(MasInfo);

        var Gastos = new Label(
@$" Gastos en Marketing: {empresa.marketing}M
 Gastos en Investigación: {empresa.investigacion}M
 Gastos en Mantenimiento: {empresa.mantenimiento}M
 Participación: {empresa.participacion:f2}%")
        {
            X = Pos.Center(),
            Y = 0,
        };
        var chunchito2 = new FrameView("")
        {
            X = 5,
            Y = 15,
            Width = 40,
            Height = 7,
        };
        DetalleEmpresa.Add(chunchito2);
        chunchito2.Add(Gastos);


        var Productitos = new Label(
$@"         PRODUCTOS             
                                   
 Productos: {empresa.productos[0]} 
 Productos: {empresa.productos[1]} 
 Productos: {empresa.productos[2]} 
 Productos: {empresa.productos[3]} 
 Productos: {empresa.productos[4]} 
 Productos: {empresa.productos[5]} 
 Productos: {empresa.productos[6]} 
 Productos: {empresa.productos[7]} 
 Productos: {empresa.productos[8]} 
 Productos: {empresa.productos[9]}")
        {
            X = Pos.Center(),
            Y = 0
        };
        var chunchito3 = new FrameView("")
        {
            X = 50,
            Y = 10,
            Width = 50,
            Height = 15,
        };
        DetalleEmpresa.Add(chunchito3);
        chunchito3.Add(Productitos);


        var btVolver = new Button("Volver")
        {
            X = Pos.Center(),
            Y = 30
        };
        var InputCantidad = new TextField()
        {
            X = Pos.X(btVolver) + 4,
            Y = Pos.Y(btVolver) - 5,
            Width = 15
        };
        InputCantidad.TextChanging += (e) =>
        {
            if (e.NewText.Length >= 7)
            {
                e.Cancel = true;
            }
        };
        DetalleEmpresa.Add((InputCantidad));
        var LabelCantidad = new Label("Cantidad:")
        {
            X = Pos.X(InputCantidad),
            Y = Pos.Y(InputCantidad) - 1,

        };
        DetalleEmpresa.Add((LabelCantidad));
        var btcomprar_acciones = new Button("Comprar Accion")
        {
            X = Pos.X(btVolver) + 4,
            Y = Pos.Y(btVolver) - 2,
        };
        var btvender_acciones = new Button("Vender Accion")
        {
            X = Pos.X(btcomprar_acciones),
            Y = Pos.Y(btcomprar_acciones) + 1,
        };
        DetalleEmpresa.Add(btvender_acciones);

        var LabelPrecioAccion = new Label($"Precio: {((empresa.capbursatil * 1000000m) / 50000000m):F2}")
        {
            X = Pos.X(btcomprar_acciones) + 4,
            Y = Pos.Y(btcomprar_acciones) - 1,
        };
        DetalleEmpresa.Add(btcomprar_acciones, LabelPrecioAccion);
        btVolver.Clicked += () =>
        {
            top.RemoveAll();
            Indices.VentanaDeEmpresas(top);
        };
        btcomprar_acciones.Clicked += () =>
        {
            int cantidty = 0;
            bool IsInt = false;
            List<string> lineas = File.ReadAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt)).ToList();
            decimal precioAccional = (empresa.capbursatil * 1000000) / 50000000;
            if (int.TryParse(InputCantidad.Text.ToString(), out cantidty))
            {
                IsInt = true;
            }
            if (Program.pd.balance >= (precioAccional * cantidty) && IsInt == true && cantidty > 0)
            {
                Acciones NuevaAccion = new Acciones();
                NuevaAccion.id = empresa.id;
                NuevaAccion.name = empresa.name;
                NuevaAccion.CostoActual = precioAccional;
                NuevaAccion.CostoDeCompra = precioAccional;
                NuevaAccion.TipoDeAccion = true;
                NuevaAccion.cantidad += cantidty;
                bool pader = false; // verificando si existe la acción creo, maldito raul que es pader
                for (int i = 2; i < lineas.Count; i++)
                {
                    string[] datos = lineas[i].Split(',');

                    if (datos[0] == empresa.id.ToString())
                    {
                        int cantity = int.Parse(datos[5]);
                        cantity += cantidty;
                        datos[5] = cantity.ToString();
                        lineas[i] = string.Join(",", datos);
                        pader = true;
                        break;
                    }
                }

                if (pader == false)
                {
                    using (StreamWriter str = new StreamWriter(ManejoDeArchivos.rutaInventario(Program.InvInt), true, Encoding.UTF8))
                    {
                        str.WriteLine(
                            $"{NuevaAccion.id},{NuevaAccion.name},{NuevaAccion.CostoActual}, {NuevaAccion.CostoDeCompra},{NuevaAccion.TipoDeAccion}, {NuevaAccion.cantidad}");
                    }
                }
                else if (pader == true)
                {
                    File.WriteAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt), lineas);
                }

                Program.pd.balance -= precioAccional * cantidty;
                ModificarPartidas.Guardarelbalance();
                ModificarPartidas.RegistrarMovimientoBalance("COMPRA", empresa, cantidty, precioAccional, precioAccional * cantidty);
                MessageBox.Query(
                    "Acciones compradas con exito!",
                    $@"Lograste comprar {cantidty} acciones a un precio unitario de {precioAccional:F2}
para un precio total de {precioAccional * cantidty:F2}",
                    "Listo");
            }
            else
            {
                MessageBox.Query(
                    "Error",
                    "Ocurrio un error",
                    "Aceptar");
            }


        };

        btvender_acciones.Clicked += () =>
        {
            int cantidty = 0;
            bool IsInt = false;
            List<string> lineas = File.ReadAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt)).ToList();
            decimal precioAccional = (empresa.capbursatil * 1000000) / 50000000;

            bool encontrada = false;
            if (int.TryParse(InputCantidad.Text.ToString(), out cantidty))
            {
                IsInt = true;
            }
            for (int i = 2; i < lineas.Count; i++)
            {
                string[] datos = lineas[i].Split(',');

                if (datos[0] == empresa.id.ToString() && IsInt == true)
                {
                    int cantidadActual = int.Parse(datos[5]);

                    if (cantidadActual > 0 && cantidty > 0 && cantidty <= cantidadActual)
                    {
                        cantidadActual -= cantidty;

                        if (cantidadActual == 0)
                        {
                            // Eliminar la línea si ya no quedan acciones
                            lineas.RemoveAt(i);
                        }
                        else
                        {
                            datos[5] = cantidadActual.ToString();
                            // Actualizamos el costo actual con el precio de venta vigente
                            datos[2] = precioAccional.ToString();
                            lineas[i] = string.Join(",", datos);
                        }

                        File.WriteAllLines(ManejoDeArchivos.rutaInventario(Program.InvInt), lineas);
                        Program.pd.balance += precioAccional * cantidty;
                        ModificarPartidas.Guardarelbalance();
                        using (StreamWriter save = new StreamWriter(ManejoDeArchivos.rutaPartidas(Program.InvInt), false, Encoding.UTF8))
                            ModificarPartidas.RegistrarMovimientoBalance("VENTA", empresa, cantidty, precioAccional, precioAccional * cantidty);
                        encontrada = true;
                        MessageBox.Query(
                            "Acciones vendidas con exito!",
                            $@"Haz vendido {cantidty} acciones a un precio de {precioAccional:F2}
para un total de {precioAccional * cantidty:F2}",
                            "Aceptar");
                    }
                    else
                    {
                        MessageBox.Query(
                            "Error",
                            "No tienes acciones de esta empresa para vender",
                            "Aceptar");
                        encontrada = true; // evita el mensaje de "no tienes acciones" duplicado
                    }

                    break;
                }
            }

            if (!encontrada)
            {
                MessageBox.Query(
                    "Error",
                    "No posees acciones de esta empresa",
                    "Aceptar");
            }
        };

        DetalleEmpresa.Add(btVolver);
        top.Add(DetalleEmpresa);
    }




    public static bool PagarDeuda(decimal monto)
    {
        if (monto <= 0m)
            return false;

        if (Program.pd.balance < monto)
            return false;

        Program.pd.balance -= monto;
        ManejoDeArchivos.DeudaEmergencia = Math.Max(0m, ManejoDeArchivos.DeudaEmergencia - monto);
        ModificarPartidas.Guardarelbalance();
        return true;
    }
}
