using System.Collections.Generic;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Contacto
    {
        private int id_contacto;
        private string cuit;
        private string nombre;
        private string apellido;
        private string email;
        private string telefono;
        private bool borrado;

        public Contacto()
        {

        }

        public Contacto(string cuit, string nombre, string apellido, string email, string telefono)
        {
            this.Cuit = cuit;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Telefono = telefono;
        }

        public Contacto(int id_contacto, string cuit, string nombre, string apellido, string email, string telefono)
        {
            this.Id_contacto = id_contacto;
            this.Cuit = cuit;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Telefono = telefono;
        }

        public Contacto(int id_contacto, string cuit, string nombre, string apellido, string email, string telefono, bool borrado)
        {
            this.Id_contacto = id_contacto;
            this.Cuit = cuit;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Telefono = telefono;
            this.Borrado = borrado;
        }

        public int Id_contacto { get => id_contacto; set => id_contacto = value; }
        public string Cuit { get => cuit; set => cuit = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static Contacto ObtenerContacto(string cuit)
        {
            IList<Contacto> listaContactos = ContactoDatos.ConsultarContacto(cuit);

            if (listaContactos.Count > 0)
            {
                return listaContactos[0];
            }

            return null;
        }

        public static string AgregarContacto(Contacto contacto)
        {
            return ContactoDatos.InsertarContacto(contacto);
        }

        public static string ModificarContacto(Contacto contacto)
        {
            return ContactoDatos.ModificarContacto(contacto);
        }
    }
}