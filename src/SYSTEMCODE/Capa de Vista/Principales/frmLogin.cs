using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class FrmLogin : Form
    {
        private bool cerrado = false;
        private bool btnIngresarPresionado = false;
        private static Usuario usuarioActual;
        private int temporizador = 3;

        public FrmLogin()
        {
            InitializeComponent();
        }

        public bool Cerrado { get => cerrado; set => cerrado = value; }
        public static Usuario UsuarioActual { get => usuarioActual; set => usuarioActual = value; }

        private void LabelEstadoLogin(string mensaje, bool estado)
        {
            if (!estado)
            {
                lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                lblEstadoLogin.Text = mensaje;
            }
            else
            {
                lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                lblEstadoLogin.Text = mensaje;
            }
        }

        private void BtnLock_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = false;
        }

        private void BtnLock_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = true;
        }


        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            btnIngresarPresionado = true;

            if (txtUsuario.Text.Length == 0)
            {
                LabelEstadoLogin("DATO OBLIGATORIO: USUARIO", false);
                txtUsuario.Focus();

                return;
            }

            if (txtClave.Text.Length == 0)
            {
                LabelEstadoLogin("DATO OBLIGATORIO: CLAVE", false);
                txtClave.Focus();

                return;
            }

            try
            {
                string NombrePerfil;

                (UsuarioActual, NombrePerfil) = Usuario.ValidarUsuario(txtUsuario.Text, txtClave.Text);
                if (UsuarioActual != null)
                {
                    if (NombrePerfil.Equals("Encargado General") || NombrePerfil.Equals("Encargado de Administración") || NombrePerfil.Equals("Encargado de Ventas") || NombrePerfil.Equals("Testing"))
                    {
                        btnIngresar.Enabled = false;
                        temporizadorAcceso.Enabled = true;
                        txtUsuario.Enabled = false;
                        txtClave.Enabled = false;
                    }
                    else
                    {
                        LabelEstadoLogin("ACCESO DENEGADO\nPERFIL NO CORRESPONDIENTE", false);

                        txtClave.Text = "";
                        txtUsuario.Text = "";
                        txtUsuario.Focus();
                    }
                }
                else
                {
                    LabelEstadoLogin("ACCESO DENEGADO\nDATOS INCORRECTOS", false);

                    txtClave.Text = "";
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                }
            }
            catch (SqlException)
            {
                LabelEstadoLogin("ERROR DE CONEXIÓN\nBASE DE DATOS", false);

                txtUsuario.Enabled = false;
                txtClave.Enabled = false;
                btnIngresar.Enabled = false;
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            cerrado = true;
            Close();
        }

        private void TemporizadorAcceso_Tick(object sender, EventArgs e)
        {
            LabelEstadoLogin("ACCESO CORRECTO [" + temporizador.ToString() + "]", true);
            temporizador--;

            if (temporizador == -1)
            {
                temporizadorAcceso.Enabled = false;
                Close();
            }
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!btnIngresarPresionado)
            {
                cerrado = true;
            }
        }
    }
}