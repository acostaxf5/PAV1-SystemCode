using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class FacturaDetalle
    {
        private string numeroFactura;
        private Proyecto proyectoAsociado;
        private int cantidadLicencias;
        private int precio;
        private bool borrado;

        public FacturaDetalle()
        {

        }

        public FacturaDetalle(string numeroFactura, Proyecto proyectoAsociado, int cantidadLicencia, int precio, bool borrado)
        {
            this.NumeroFactura = numeroFactura;
            this.ProyectoAsociado = proyectoAsociado;
            this.CantidadLicencias = cantidadLicencia;
            this.Precio = precio;
            this.Borrado = borrado;
        }

        public string NumeroFactura { get => numeroFactura; set => numeroFactura = value; }
        public Proyecto ProyectoAsociado { get => proyectoAsociado; set => proyectoAsociado = value; }
        public int CantidadLicencias { get => cantidadLicencias; set => cantidadLicencias = value; } 
        public int Precio { get => precio; set => precio = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static IList<FacturaDetalle> ObtenerListaFacturaDetalle(string numeroFactura)
        {
            return FacturaDetalleDatos.ConsultarListaFacturaDetalle(numeroFactura);
        }

        public static DataTable ObtenerDetalleFactura(string idFactura)
        {
            return FacturaDetalleDatos.ConsultarDetalleFactura(idFactura);
        }
    }
}