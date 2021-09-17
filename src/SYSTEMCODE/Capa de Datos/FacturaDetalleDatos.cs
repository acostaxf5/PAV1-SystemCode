using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public class FacturaDetalleDatos
    {
        private static FacturaDetalle DiseniarFacturaDetalle(int posicion, DataTable tabla)
        {
            string numeroFactura = tabla.Rows[posicion]["numero_factura"].ToString();
            int idProyectoAsociado = Convert.ToInt32(tabla.Rows[posicion]["id_proyecto"].ToString());
            int cantidadLicencias = Convert.ToInt32(tabla.Rows[posicion]["cantidad_licencias"].ToString());
            int precio = Convert.ToInt32(tabla.Rows[posicion]["precio"]);
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Proyecto proyectoAsociado = Proyecto.ObtenerProyectoPorID(idProyectoAsociado);

            return new FacturaDetalle(numeroFactura, proyectoAsociado, cantidadLicencias, precio, borrado);
        }

        public static IList<FacturaDetalle> ConsultarListaFacturaDetalle(string numeroFactura)
        {
            string SQL = "SELECT facturasDetalle.* " +
                         "FROM FacturasDetalle facturasDetalle " +
                         "WHERE " +
                            "borrado = 0 AND " +
                            "numero_factura = '" + numeroFactura.ToString() + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            IList<FacturaDetalle> listaFacturaDetalle = new List<FacturaDetalle>();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                listaFacturaDetalle.Add(DiseniarFacturaDetalle(i, tabla));
            }

            return listaFacturaDetalle;
        }

        public static DataTable ConsultarDetalleFactura(string idFactura)
        {
            string SQL = "SELECT " +
                            "fd.cantidad_licencias, " +
                            "p.descripcion, " +
                            "p.version, " +
                            "p.alcance, " +
                            "CAST((fd.precio/fd.cantidad_licencias) AS INT) as unitario, " +
                            "fd.precio " +
                         "FROM " +
                            "FacturasDetalle fd, Proyectos p " +
                         "WHERE " +
                            "fd.id_proyecto = p.id_proyecto AND " +
                            "fd.numero_factura = " + idFactura.ToString() + " AND " +
                            "fd.borrado = 0";

            return GestorBD.Consultar(SQL);
        }
    }
}