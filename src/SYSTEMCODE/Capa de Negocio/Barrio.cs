using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Barrio
    {
        private int id_barrio;
        private string nombre;
        private bool borrado;

        public Barrio()
        {

        }

        public Barrio(string nombre)
        {
            this.Nombre = nombre;
        }

        public Barrio(int id_barrio, string nombre, bool borrado)
        {
            this.Id_barrio = id_barrio;
            this.Nombre = nombre;
            this.Borrado = borrado;
        }

        public int Id_barrio { get => id_barrio; set => id_barrio = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static DataTable ObtenerBarrios()
        {
            return BarrioDatos.ConsultarBarrios();
        }

        public static DataTable ObtenerBarriosComboBox()
        {
            return BarrioDatos.ConsultarBarriosComboBox();
        }

        public static DataTable ObtenerTablaBarriosFiltro(string filtro)
        {
            return BarrioDatos.ConsultarTablaBarriosFiltro(filtro);
        }

        public static Barrio ObtenerBarrioPorNombre(string nombreBarrio)
        {
            return BarrioDatos.ConsultarBarrioPorNombre(nombreBarrio);
        }

        public static string AgregarBarrio(Barrio barrio)
        {
            return BarrioDatos.InsertarBarrio(barrio);
        }

        public static string ModificarBarrio(Barrio barrio)
        {
            return BarrioDatos.ModificarBarrio(barrio);
        }

        public static string EliminarBarrio(Barrio barrio)
        {
            return BarrioDatos.EliminarBarrio(barrio);
        }

        public static DataTable ObtenerBarriosConMasVentas(string fechaDesde, string fechaHasta)
        {
            return BarrioDatos.ConsultarBarriosConMasVentas(fechaDesde, fechaHasta);
        }
    }
}