using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;
using SYSTEMCODE.Capa_de_Vista;
using SYSTEMCODE.Capa_de_Vista.ABMC;

namespace SYSTEMCODE
{
    public partial class FrmSystemCode : Form
    {
        public FrmSystemCode()
        {
            InitializeComponent();
        }

        public static Usuario UsuarioActual { get; set; } = null;

        private void FrmSystemCode_Load(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.ShowDialog();

            if (login.Cerrado)
            {
                Close();
                return;
            }

            UsuarioActual = FrmLogin.UsuarioActual;

            lblBienvenida.Text = "¡Bienvenido, " + UsuarioActual.NombreUsuario + "!";

            switch (Usuario.ObtenerPerfil(UsuarioActual))
            {
                case "Encargado General":
                    menuUsuarios.Enabled = true;
                    menuPerfiles.Enabled = true;
                    menuClientes.Enabled = false;
                    menuBarrios.Enabled = false;
                    menuProyectos.Enabled = false;
                    menuVentas.Enabled = false;
                    menuInformes.Enabled = false;
                    break;

                case "Encargado de Administración":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = false;
                    menuInformes.Enabled = true;
                    break;

                case "Encargado de Ventas":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = false;
                    menuBarrios.Enabled = false;
                    menuProyectos.Enabled = false;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = false;
                    break;

                /*case "Testing":
                    menuUsuarios.Enabled = true;
                    menuPerfiles.Enabled = true;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = true;
                    break;
                    */
                default:
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = false;
                    menuBarrios.Enabled = false;
                    menuProyectos.Enabled = false;
                    menuVentas.Enabled = false;
                    menuInformes.Enabled = false;
                    break;

            }
        }

        private void MenuSalir_Click(object sender, EventArgs e)
        {
            FrmConfirmacion confirmacion = new FrmConfirmacion();
            confirmacion.ShowDialog();

            if (confirmacion.BtnSiPresionado)
            {
                Close();
                return;
            }
        }

        private void MenuUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios usuarios = new FrmUsuarios
            {
                Text = "Usuarios [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            usuarios.ShowDialog();
        }

        private void MenuPerfiles_Click(object sender, EventArgs e)
        {
            FrmPerfiles perfiles = new FrmPerfiles
            {
                Text = "Perfiles [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            perfiles.ShowDialog();
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes
            {
                Text = "Clientes [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            clientes.ShowDialog();
        }

        private void MenuBarrios_Click(object sender, EventArgs e)
        {
            FrmBarrios barrios = new FrmBarrios
            {
                Text = "Barrios [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            barrios.ShowDialog();
        }

        private void MenuProyectos_Click(object sender, EventArgs e)
        {
            FrmProyectos proyectos = new FrmProyectos
            {
                Text = "Proyectos [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            proyectos.ShowDialog();
        }

        private void MenuVentas_Click(object sender, EventArgs e)
        {
            FrmGestionarVentas ventas = new FrmGestionarVentas(UsuarioActual.NombreUsuario)
            {
                Text = "Venta [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            ventas.ShowDialog();
        }
    }
}