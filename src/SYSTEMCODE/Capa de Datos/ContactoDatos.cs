using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class ContactoDatos
    {
        private static Contacto DiseniarContacto(int posicion, DataTable tabla)
        {
            int id_contacto = Convert.ToInt32(tabla.Rows[posicion]["id_contacto"].ToString());
            string cuit = tabla.Rows[posicion]["cuit"].ToString();
            string nombre = tabla.Rows[posicion]["nombre"].ToString();
            string apellido = tabla.Rows[posicion]["apellido"].ToString();
            string email = tabla.Rows[posicion]["email"].ToString();
            string telefono = tabla.Rows[posicion]["telefono"].ToString();
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            return new Contacto(id_contacto, cuit, nombre, apellido, email, telefono, borrado);
        }

        public static IList<Contacto> ConsultarContacto(string cuit)
        {
            string SQL = "SELECT contactos.* " +
                                 "FROM Contactos contactos " +
                                 "WHERE " +
                                    "cuit = '" + cuit.ToString() + "' AND " +
                                    "borrado = 0";

            IList<Contacto> listaContactos = new List<Contacto>();
            DataTable tabla = GestorBD.Consultar(SQL);
            if (tabla.Rows.Count > 0)
            {
                listaContactos.Add(DiseniarContacto(0, tabla));
            }

            return listaContactos;
        }
        
        public static string InsertarContacto(Contacto contacto)
        {
            string SQL = "INSERT INTO Contactos " +
                         "VALUES " +
                         "(" +
                            "'" + contacto.Cuit.ToString() + "', " +
                            "'" + contacto.Nombre.ToString() + "', " +
                            "'" + contacto.Apellido.ToString() + "', " +
                            "'" + contacto.Email.ToString() + "', " +
                            "'" + contacto.Telefono.ToString() + "', " +
                            "0" +
                         ")";

            return GestorBD.Ejecutar(SQL);
        }

        public static string ModificarContacto(Contacto contacto)
        {
            string SQL = "UPDATE Contactos " +
                         "SET " +
                            "nombre = '" + contacto.Nombre.ToString() + "', " +
                            "apellido = '" + contacto.Apellido.ToString() + "', " +
                            "email = '" + contacto.Email.ToString() + "', " +
                            "telefono = '" + contacto.Telefono.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(contacto.Borrado) + " " +
                         "WHERE cuit = '" + contacto.Cuit.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static string EliminarContacto(Contacto contacto)
        {
            string SQL = "UPDATE Contactos " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE cuit = '" + contacto.Cuit.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }
    }
}