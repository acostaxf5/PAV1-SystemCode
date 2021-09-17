using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class BarrioDatos
    {
        private static Barrio DiseniarBarrio(int posicion, DataTable tabla)
        {
            int id_barrio = Convert.ToInt32(tabla.Rows[posicion]["id_barrio"].ToString());
            string nombre = tabla.Rows[posicion]["nombre"].ToString();
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            return new Barrio(id_barrio, nombre, borrado);
        }

        public static IList<Barrio> ConsultarBarrio(int id_barrio)
        {
            string SQL = "SELECT barrios.* " +
                         "FROM Barrios barrios " +
                         "WHERE " +
                             "id_barrio = '" + id_barrio.ToString() + "' AND " +
                             "borrado = 0";

            IList<Barrio> listaBarrios = new List<Barrio>();
            DataTable tabla = GestorBD.Consultar(SQL);
            if (tabla.Rows.Count > 0)
            {
                listaBarrios.Add(DiseniarBarrio(0, tabla));
            }

            return listaBarrios;
        }

        public static DataTable ConsultarBarrios()
        {
            return GestorBD.ConsultarTabla("Barrios");
        }

        public static DataTable ConsultarBarriosComboBox()
        {
            string SQL = "SELECT * " +
                         "FROM Barrios " +
                         "WHERE " +
                             "borrado = 0";

            return GestorBD.Consultar(SQL);
        }

        public static DataTable ConsultarTablaBarriosFiltro(string filtro)
        {
            string SQL = "SELECT barrios.* " +
                         "FROM Barrios barrios " +
                         "WHERE nombre LIKE '" + filtro + "%'";

            return GestorBD.Consultar(SQL);
        }

        public static Barrio ConsultarBarrioPorNombre(string nombreBarrio)
        {
            string SQL = "SELECT * " +
                         "FROM Barrios " +
                         "WHERE " +
                            "nombre = '" + nombreBarrio.ToString() + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarBarrio(0, tabla) : null;
        }

        public static string InsertarBarrio(Barrio barrio)
        {
            string SQL = "INSERT INTO Barrios " +
                         "VALUES " +
                         "(" +
                            "'" + barrio.Nombre.ToString() + "', " +
                            "0" +
                         ")";

            return GestorBD.Ejecutar(SQL);
        }

        public static string ModificarBarrio(Barrio barrio)
        {
            string SQL = "UPDATE Barrios " +
                         "SET " +
                            "nombre = '" + barrio.Nombre.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(barrio.Borrado) + " " +
                         "WHERE id_barrio = '" + barrio.Id_barrio.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static string EliminarBarrio(Barrio barrio)
        {
            string SQL = "UPDATE Barrios " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE nombre = '" + barrio.Nombre.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static DataTable ConsultarBarriosConMasVentas(string fechaDesde, string fechaHasta)
        {
            string SQL = "SELECT TOP(15) b.nombre, COUNT(*) as cantidad_ventas " +
                         "FROM Barrios b, Facturas f, Clientes c " +
                         "WHERE " +
                            "f.id_cliente = c.id_cliente AND " +
                            "c.id_barrio = b.id_barrio AND " +
                            "f.fecha BETWEEN '" + fechaDesde + "' AND '" + fechaHasta + "'" +
                         "GROUP BY b.nombre";

            return GestorBD.Consultar(SQL);
        }
    }
}