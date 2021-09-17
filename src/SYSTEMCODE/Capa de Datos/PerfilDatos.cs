using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class PerfilDatos
    {
        private static Perfil DiseniarPerfil(int posicion, DataTable tabla)
        {
            int id_perfil = Convert.ToInt32(tabla.Rows[posicion]["id_perfil"].ToString());
            string nombre = tabla.Rows[posicion]["nombre"].ToString();
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            return new Perfil(id_perfil, nombre, borrado);
        }

        public static IList<Perfil> ConsultarPerfil(int id_perfil)
        {
            string SQL = "SELECT perfiles.* " +
                                 "FROM Perfiles perfiles " +
                                 "WHERE " +
                                    "id_perfil = '" + id_perfil.ToString() + "' AND " +
                                    "borrado = 0";

            IList<Perfil> listaPerfiles = new List<Perfil>();
            DataTable tabla = GestorBD.Consultar(SQL);
            if (tabla.Rows.Count > 0)
            {
                listaPerfiles.Add(DiseniarPerfil(0, tabla));
            }

            return listaPerfiles;
        }

        public static DataTable ConsultarTablaPerfilesFiltro(string filtro)
        {
            string SQL = "SELECT perfiles.* " +
                         "FROM Perfiles perfiles " +
                         "WHERE nombre LIKE '" + filtro + "%'";

            return GestorBD.Consultar(SQL);
        }

        public static Perfil ConsultarPerfilPorNombre(string nombrePerfil)
        {
            string SQL = "SELECT * " +
                         "FROM Perfiles " +
                         "WHERE " +
                            "nombre = '" + nombrePerfil.ToString() + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarPerfil(0, tabla) : null;
        }

        public static DataTable ConsultarPerfiles()
        {
            return GestorBD.ConsultarTabla("Perfiles");
        }

        public static DataTable ConsultarPerfilesComboBox()
        {
            string SQL = "SELECT * " +
                         "FROM Perfiles " +
                         "WHERE " +
                             "borrado = 0";

            return GestorBD.Consultar(SQL);
        }

        public static string InsertarPerfil(Perfil perfil)
        {
            string SQL = "INSERT INTO Perfiles " +
                         "VALUES " +
                         "(" +
                            "'" + perfil.Nombre.ToString() + "', " +
                            "0" +
                         ")";

            return GestorBD.Ejecutar(SQL);
        }

        public static string ModificarPerfil(Perfil perfil)
        {
            string SQL = "UPDATE Perfiles " +
                         "SET " +
                            "nombre = '" + perfil.Nombre.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(perfil.Borrado) + " " +
                         "WHERE id_perfil = '" + perfil.Id_perfil.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static string EliminarPerfil(Perfil perfil)
        {
            string SQL = "UPDATE Perfiles " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE nombre = '" + perfil.Nombre.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }
    }
}