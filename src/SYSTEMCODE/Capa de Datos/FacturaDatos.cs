using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class FacturaDatos
    {
        private static Factura DiseniarFactura(int posicion, DataTable tabla)
        {
            string numeroFactura = tabla.Rows[posicion]["numero_factura"].ToString();
            int idCliente = Convert.ToInt32(tabla.Rows[posicion]["id_cliente"].ToString());
            DateTime fechaGenerada = Convert.ToDateTime(tabla.Rows[posicion]["fecha"]);
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Cliente clienteAsociado = Cliente.ObtenerCliente(idCliente);

            return new Factura(numeroFactura, clienteAsociado, fechaGenerada, borrado);
        }

        public static Factura ConsultarFactura(string numeroFactura)
        {
            string SQL = "SELECT facturas.* " +
                         "FROM Facturas facturas " +
                         "WHERE " +
                            "numero_factura = '" + numeroFactura.ToString() + "' AND " +
                            "borrado = 0";

            DataTable tabla = GestorBD.Consultar(SQL);
            
            return (tabla.Rows.Count > 0) ? DiseniarFactura(0, tabla) : null;
        }

        public static string ConsultarNumeroUltimaFactura()
        {
            DataTable tabla = GestorBD.ConsultarTabla("Facturas");

            return (tabla.Rows.Count > 0) ? tabla.Rows[tabla.Rows.Count - 1]["numero_factura"].ToString() : "0";
        }

        public static DataTable ConsultarTablaFacturas()
        {
            return GestorBD.Consultar("SELECT facturas.* FROM Facturas facturas");
        }

        public static string AgregarFactura(Factura factura, IList<FacturaDetalle> listaFacturaDetalle)
        {
            IList<string> listaSQL = new List<string>
            {
                "INSERT INTO Facturas (numero_factura, id_cliente, fecha, id_usuario_creador, borrado) " +
                "VALUES (" +
                    "'" + factura.NumeroFactura + "', " +
                    factura.Cliente.Id_cliente.ToString() + ", " +
                    "CAST('" + factura.FechaCreacion.ToString("yyyy-MM-dd") + "' AS Date), " +
                    "3, " +
                    "'0')"
            };

            for (int i = 0; i < listaFacturaDetalle.Count; i++)
            {
                listaSQL.Add
                (
                    "INSERT INTO FacturasDetalle (numero_factura, id_proyecto, cantidad_licencias, precio, borrado)" +
                    "VALUES (" +
                        listaFacturaDetalle[i].NumeroFactura + ", " +
                        Proyecto.ObtenerProyectoPorDescripcion(listaFacturaDetalle[i].ProyectoAsociado.Descripcion).IdProyecto.ToString() + ", " +
                        listaFacturaDetalle[i].CantidadLicencias.ToString() + ", " +
                        listaFacturaDetalle[i].Precio.ToString() + ", " +
                        "'0')"
                );
            }

            return GestorBD.MultipleEjecucion(listaSQL);
        }

        public static string AnularFactura(Factura factura)
        {
            IList<string> listaSQL = new List<string>
            {
                "UPDATE Facturas " +
                "SET " +
                    "borrado = 1 " +
                "WHERE numero_factura = " + factura.NumeroFactura.ToString(),

                "UPDATE FacturasDetalle " +
                "SET " +
                    "borrado = 1 " +
                "WHERE numero_factura = " + factura.NumeroFactura.ToString()
            };

            return GestorBD.MultipleEjecucion(listaSQL);
        }

        public static DataTable ConsultarListadoFacturasPorFecha(string fechaDesde, string fechaHasta)
        {
            string SQL = "SELECT f.numero_factura, c.razon_social, FORMAT(f.fecha, 'dd/MM/yyyy') as fecha, u.usuario " +
                         "FROM Facturas f, Clientes c, Usuarios u " +
                         "WHERE " +
                            "f.borrado = 0 AND " +
                            "f.id_cliente = c.id_cliente AND " +
                            "f.id_usuario_creador = u.id_usuario AND " +
                            "f.fecha BETWEEN '" + fechaDesde + "' AND '" + fechaHasta + "'";

            return GestorBD.Consultar(SQL);
        }
    }
}