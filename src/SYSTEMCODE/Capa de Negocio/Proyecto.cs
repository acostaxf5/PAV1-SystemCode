using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Proyecto
    {
        private int idProyecto;
        private string descripcion;
        private string version;
        private string alcance;
        private Usuario responsable;
        private bool borrado;

        public Proyecto()
        {

        }

        public Proyecto(string descripcion, string version, string alcance, Usuario responsable, bool borrado)
        {
            this.Descripcion = descripcion;
            this.Version = version;
            this.Alcance = alcance;
            this.Responsable = responsable;
            this.Borrado = borrado;
        }

        public Proyecto(int idProyecto, string descripcion, string version, string alcance, Usuario responsable, bool borrado)
        {
            this.idProyecto = idProyecto;
            this.Descripcion = descripcion;
            this.Version = version;
            this.Alcance = alcance;
            this.Responsable = responsable;
            this.Borrado = borrado;
        }

        public int IdProyecto { get => idProyecto; set => idProyecto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Version { get => version; set => version = value; }
        public string Alcance { get => alcance; set => alcance = value; }
        public Usuario Responsable { get => responsable; set => responsable = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static Proyecto ObtenerProyectoPorDescripcion(string descripcionProyecto)
        {
            return ProyectoDatos.ConsultarProyectoPorDescripcion(descripcionProyecto);
        }

        public static Proyecto ObtenerProyectoPorID(int idProyecto)
        {
            return ProyectoDatos.ConsultarProyectoPorID(idProyecto);
        }

        public static IList<Proyecto> ObtenerTablaProyectos()
        {
            return ProyectoDatos.ConsultarTablaProyectos();
        }

        public static DataTable ObtenerTablaProyectosComboBox()
        {
            return ProyectoDatos.ConsultarTablaProyectosComboBox();
        }

        public static IList<Proyecto> ObtenerTablaProyectosFiltro(string filtro)
        {
            return ProyectoDatos.ConsultarTablaProyectosFiltro(filtro);
        }

        public static string AgregarProyecto(Proyecto proyecto)
        {
            return ProyectoDatos.InsertarProyecto(proyecto);
        }

        public static string ModificarProyecto(Proyecto proyecto)
        {
            return ProyectoDatos.ModificarProyecto(proyecto);
        }

        public static string EliminarProyecto(Proyecto proyecto)
        {
            return ProyectoDatos.EliminarProyecto(proyecto);
        }

        public static DataTable ObtenerListadoProyectosActivos()
        {
            return ProyectoDatos.ConsultarListadoProyectosActivos();
        }

        public static DataTable ObtenerProyectosConMasVentas(string fechaDesde, string fechaHasta)
        {
            return ProyectoDatos.ConsultarProyectosConMasVentas(fechaDesde, fechaHasta);
        }
    }
}