using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class GestorBD
    {
        private static readonly string cadenaConexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=PAV1;Integrated Security=True";
        private static readonly SqlConnection conexion = new SqlConnection(cadenaConexion);
        private static SqlTransaction transaccion = null;

        private static void Conectar()
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
        }

        private static void Desconectar()
        {
            if (conexion.State != ConnectionState.Closed)
            {
                conexion.Close();
            }
        }

        public static DataTable Consultar(string cSQL)
        {
            DataTable tabla = new DataTable();

            Conectar();
            SqlCommand comando = new SqlCommand(cSQL, conexion);
            tabla.Load(comando.ExecuteReader());
            Desconectar();

            return tabla;
        }

        public static DataTable ConsultarTabla(string nombreTabla)
        {
            DataTable tabla = new DataTable();

            Conectar();
            SqlCommand comando = new SqlCommand("SELECT * FROM " + nombreTabla, conexion);
            tabla.Load(comando.ExecuteReader());
            Desconectar();

            return tabla;
        }

        public static string Ejecutar(string SQL)
        {
            try
            {
                Conectar();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand(SQL, conexion, transaccion);
                comando.ExecuteNonQuery();
                transaccion.Commit();
            }
            catch (SqlException)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                return "ERROR DE ESCRITURA EN LA BASE DE DATOS";
            }
            finally
            {
                Desconectar();
            }

            return "";
        }

        public static string MultipleEjecucion(IList<string> listaSQL)
        {
            try
            {
                Conectar();
                transaccion = conexion.BeginTransaction();

                for (int i = 0; i < listaSQL.Count; i++)
                {
                    SqlCommand comando = new SqlCommand(listaSQL[i], conexion, transaccion);
                    comando.ExecuteNonQuery();
                }
                
                transaccion.Commit();
            }
            catch (SqlException)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                return "ERROR DE ESCRITURA EN LA BASE DE DATOS";
            }
            finally
            {
                Desconectar();
            }

            return "";
        }
    }
}