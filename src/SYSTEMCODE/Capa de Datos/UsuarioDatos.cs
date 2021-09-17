using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class UsuarioDatos
    {
        private static Usuario DiseniarUsuario(int posicion, DataTable tabla)
        {
            int id_usuario = Convert.ToInt32(tabla.Rows[posicion]["id_usuario"].ToString());
            string usuario = tabla.Rows[posicion]["usuario"].ToString();
            string dni = tabla.Rows[posicion]["dni"].ToString();
            int id_perfil = Convert.ToInt32(tabla.Rows[posicion]["id_perfil"].ToString());
            string clave = tabla.Rows[posicion]["password"].ToString();
            string email = tabla.Rows[posicion]["email"].ToString();
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Perfil perfil = PerfilDatos.ConsultarPerfil(id_perfil)[0];

            return new Usuario(id_usuario, dni, usuario, perfil, clave, email, borrado);
        }

        public static IList<Usuario> ConsultarTablaUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            DataTable tabla = GestorBD.ConsultarTabla("Usuarios");
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                listaUsuarios.Add(DiseniarUsuario(i, tabla));
            }

            return listaUsuarios;
        }

        public static IList<Usuario> ConsultarTablaUsuariosFiltro(string filtro)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE dni LIKE '" + filtro + "%'";
            
            List<Usuario> listaUsuarios = new List<Usuario>();

            DataTable tabla = GestorBD.Consultar(SQL);
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                listaUsuarios.Add(DiseniarUsuario(i, tabla));
            }

            return listaUsuarios;
        }

        public static DataTable ConsultarTablaUsuariosComboBox()
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE borrado = 0";

            return GestorBD.Consultar(SQL);
        }

        public static Usuario ConsultarUsuarioPorNombreUsuario(string nombreUsuario)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE " +
                            "borrado = 0 AND " +
                            "usuario = '" + nombreUsuario + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarUsuario(0, tabla) : null;
        }

        public static Usuario ConsultarUsuarioPorDNI(string DNI)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE dni = '" + DNI + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarUsuario(0, tabla) : null;
        }

        public static Usuario ConsultarUsuarioPorID(int idUsuario)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE id_usuario = " + idUsuario.ToString();

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarUsuario(0, tabla) : null;
        }

        public static string InsertarUsuario(Usuario usuario)
        {
            string SQL = "INSERT INTO Usuarios " +
                         "VALUES " +
                         "(" +
                            usuario.Perfil.Id_perfil + ", '" +
                            usuario.Dni.ToString() + "', '" +
                            usuario.NombreUsuario.ToString() + "', '" +
                            usuario.Clave.ToString() + "', '" +
                            usuario.Email.ToString() + "', " +
                            "'N', " +
                            "0" +
                         ")";

            return GestorBD.Ejecutar(SQL);
        }

        public static string ModificarUsuario(Usuario usuario)
        {
            string SQL = "UPDATE Usuarios " +
                         "SET " +
                            "id_perfil = " + usuario.Perfil.Id_perfil.ToString() + ", " +
                            "usuario = '" + usuario.NombreUsuario.ToString() + "', " +
                            "password = '" + usuario.Clave.ToString() + "', " +
                            "email = '" + usuario.Email.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(usuario.Borrado) + " " +
                         "WHERE dni = '" + usuario.Dni.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static string EliminarUsuario(Usuario usuario)
        {
            string SQL = "UPDATE Usuarios " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE dni = '" + usuario.Dni.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static DataTable ConsultarListadoUsuariosActivos()
        {
            string SQL = "SELECT u.dni, u.usuario, u.email, p.nombre " +
                         "FROM Usuarios u, Perfiles p " +
                         "WHERE " +
                            "u.id_perfil = p.id_perfil AND " + 
                            "u.borrado = 0";

            return GestorBD.Consultar(SQL);
        }
    }
}