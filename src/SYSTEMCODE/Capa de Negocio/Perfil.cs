using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Perfil
    {
        private int id_perfil;
        private string nombre;
        private bool borrado;

        public Perfil()
        {

        }

        public Perfil(string nombre)
        {
            this.Nombre = nombre;
        }

        public Perfil(string nombre, bool borrado)
        {
            this.Nombre = nombre;
            this.Borrado = borrado;
        }

        public Perfil(int id_perfil, string nombre, bool borrado)
        {
            this.Id_perfil = id_perfil;
            this.Nombre = nombre;
            this.Borrado = borrado;
        }

        public int Id_perfil { get => id_perfil; set => id_perfil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static DataTable ObtenerPerfiles()
        {
            return PerfilDatos.ConsultarPerfiles();
        }

        public static DataTable ObtenerPerfilesComboBox()
        {
            return PerfilDatos.ConsultarPerfilesComboBox();
        }

        public static DataTable ObtenerTablaPerfilesFiltro(string filtro)
        {
            return PerfilDatos.ConsultarTablaPerfilesFiltro(filtro);
        }

        public static Perfil ObtenerPerfilPorID(int id_perfil)
        {
            return PerfilDatos.ConsultarPerfil(id_perfil)[0];
        }

        public static Perfil ObtenerPerfilPorNombre(string nombrePerfil)
        {
            return PerfilDatos.ConsultarPerfilPorNombre(nombrePerfil);
        }

        public static string AgregarPerfil(Perfil perfil)
        {
            return PerfilDatos.InsertarPerfil(perfil);
        }

        public static string ModificarPerfil(Perfil perfil)
        {
            return PerfilDatos.ModificarPerfil(perfil);
        }

        public static string EliminarPerfil(Perfil perfil)
        {
            return PerfilDatos.EliminarPerfil(perfil);
        }
    }
}