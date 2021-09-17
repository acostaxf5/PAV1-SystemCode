using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Cliente
    {
        private int id_cliente;
        private string cuit;
        private string razon_social;
        private bool borrado;
        private string calle;
        private int numero;
        private DateTime fecha_alta;
        private Barrio barrioAsociado;
        private Contacto contactoAsociado;

        public Cliente()
        {

        }

        public Cliente(string cuit, string razon_social, bool borrado, string calle, int numero, DateTime fecha_alta, Barrio barrioAsociado, Contacto contactoAsociado)
        {
            this.Cuit = cuit;
            this.Razon_social = razon_social;
            this.Borrado = borrado;
            this.Calle = calle;
            this.Numero = numero;
            this.Fecha_alta = fecha_alta;
            this.barrioAsociado = barrioAsociado;
            this.contactoAsociado = contactoAsociado;
        }

        public Cliente(int id_cliente, string cuit, string razon_social, bool borrado, string calle, int numero, DateTime fecha_alta, Barrio barrioAsociado, Contacto contactoAsociado)
        {
            this.Id_cliente = id_cliente;
            this.Cuit = cuit;
            this.Razon_social = razon_social;
            this.Borrado = borrado;
            this.Calle = calle;
            this.Numero = numero;
            this.Fecha_alta = fecha_alta;
            this.barrioAsociado = barrioAsociado;
            this.contactoAsociado = contactoAsociado;
        }

        public int Id_cliente { get => id_cliente; set => id_cliente = value; }
        public string Cuit { get => cuit; set => cuit = value; }
        public string Razon_social { get => razon_social; set => razon_social = value; }
        public bool Borrado { get => borrado; set => borrado = value; }
        public string Calle { get => calle; set => calle = value; }
        public int Numero { get => numero; set => numero = value; }
        public DateTime Fecha_alta { get => fecha_alta; set => fecha_alta = value; }
        public Barrio BarrioAsociado { get => barrioAsociado; set => barrioAsociado = value; }
        public Contacto ContactoAsociado { get => contactoAsociado; set => contactoAsociado = value; }

        public static Cliente ObtenerCliente(string numeroCUIT)
        {
            return ClienteDatos.ConsultarClientePorCUIT(numeroCUIT);
        }

        public static Cliente ObtenerClienteNoBorrado(int idCliente)
        {
            return ClienteDatos.ConsultarClientePorIDNoBorrado(idCliente);
        }

        public static Cliente ObtenerCliente(int idCliente)
        {
            return ClienteDatos.ConsultarClientePorID(idCliente);
        }

        public static IList<Cliente> ObtenerTablaClientes()
        {
            return ClienteDatos.ConsultarTablaClientes();
        }

        public static DataTable ObtenerTablaClientesComboBox()
        {
            return ClienteDatos.ConsultarTablaClientesComboBox();
        }

        public static IList<Cliente> ObtenerTablaClientesFiltro(string filtro)
        {
            return ClienteDatos.ConsultarTablaClientesFiltro(filtro);
        }

        public static string AgregarCliente(Cliente cliente)
        {
            return ClienteDatos.InsertarCliente(cliente);
        }

        public static string ModificarCliente(Cliente cliente)
        {
            return ClienteDatos.ModificarCliente(cliente);
        }

        public static string EliminarCliente(Cliente cliente)
        {
            return ClienteDatos.EliminarCliente(cliente);
        }

        public static DataTable ObtenerListadoClientesActivos()
        {
            return ClienteDatos.ConsultarListadoClientesActivos();
        }

        public static DataTable ObtenerProyectosPorCliente(string numeroCliente, string fechaDesde, string fechaHasta)
        {
            return ClienteDatos.ConsultarProyectosPorCliente(ObtenerCliente(numeroCliente).Id_cliente, fechaDesde, fechaHasta);
        }
    }
}