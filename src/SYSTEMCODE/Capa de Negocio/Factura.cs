using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Factura
    {
        private string numeroFactura;
        private Cliente clienteAsociado;
        private DateTime fechaCreacion;
        private bool borrado;

        public Factura()
        {

        }

        public Factura(string numeroFactura, Cliente cliente, DateTime fechaCreacion, bool borrado)
        {
            this.NumeroFactura = numeroFactura;
            this.Cliente = cliente;
            this.FechaCreacion = fechaCreacion;
            this.Borrado = borrado;
        }

        public string NumeroFactura { get => numeroFactura; set => numeroFactura = value; }
        public Cliente Cliente { get => clienteAsociado; set => clienteAsociado = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static Factura ObtenerFactura(string numeroFactura)
        {
            return FacturaDatos.ConsultarFactura(numeroFactura);
        }

        public static string ObtenerNumeroUltimaFactura()
        {
            return FacturaDatos.ConsultarNumeroUltimaFactura();
        }

        public static DataTable ObtenerTablaFacturas()
        {
            return FacturaDatos.ConsultarTablaFacturas();
        }

        public static string AgregarFactura(Factura factura, IList<FacturaDetalle> listaFacturaDetalle)
        {
            return FacturaDatos.AgregarFactura(factura, listaFacturaDetalle);
        }

        public static string AnularFactura(Factura factura)
        {
            return FacturaDatos.AnularFactura(factura);
        }

        public static DataTable ObtenerListadoFacturasPorFecha(string fechaDesde, string fechaHasta)
        {
            return FacturaDatos.ConsultarListadoFacturasPorFecha(fechaDesde, fechaHasta);
        }
    }
}