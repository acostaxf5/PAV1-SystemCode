using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC
{
    public partial class FrmUsuarios : Form
    {
        Usuario usuario;

        private string botonPresionado = "";
        private bool btnMostrarBorradosPresionado = false;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void BtnVisualizar_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = false;
        }

        private void BtnVisualizar_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = true;
        }

        private void Numero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void CargarTablaUsuariosNoBorrados(DataGridView dgv, IList<Usuario> listaUsuarios)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                if (!listaUsuarios[i].Borrado)
                {
                    dgv.Rows.Add
                    (
                        listaUsuarios[i].Dni,
                        listaUsuarios[i].NombreUsuario,
                        listaUsuarios[i].Email
                    );
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaUsuariosBorrados(DataGridView dgv, IList<Usuario> listaUsuarios)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                dgv.Rows.Add
                (
                    listaUsuarios[i].Dni,
                    listaUsuarios[i].NombreUsuario,
                    listaUsuarios[i].Email
                );

                if (listaUsuarios[i].Borrado)
                {
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                }
            }

            dgv.ClearSelection();
        }

        private void CargarComboBox(ComboBox cbo, DataTable tabla)
        {
            cbo.DataSource = tabla;
            cbo.DisplayMember = tabla.Columns[1].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private void CargarCampos()
        {
            usuario = Usuario.ObtenerUsuario(dgvUsuarios.CurrentRow.Cells[0].Value.ToString());

            numDNI.Text = usuario.Dni.ToString();
            DataTable tablaPerfiles = Perfil.ObtenerPerfiles();
            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                if (tablaPerfiles.Rows[i]["nombre"].ToString() == usuario.Perfil.Nombre.ToString())
                {
                    cboPerfiles.SelectedIndex = i;
                    break;
                }
            }
            txtNombreUsuario.Text = usuario.NombreUsuario.ToString();
            txtClave.Text = usuario.Clave.ToString();
            txtEmail.Text = usuario.Email.ToString();
        }

        private void CargarInforme(string mensaje, bool estado, bool defecto)
        {
            if (defecto)
            {
                lblInformes.BackColor = System.Drawing.Color.LightGray;
                lblInformes.ForeColor = System.Drawing.Color.Black;
                lblInformes.Text = mensaje;
            }
            else
            {
                if (!estado)
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = mensaje;
                }
                else
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = mensaje;
                }
            }
        }

        private bool ValidarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return (Regex.Replace(email, expresion, String.Empty).Length == 0);
            }

            return false;
        }

        private bool ValidarCampos()
        {
            if (cboPerfiles.SelectedIndex == -1)
            {
                CargarInforme("DATO OBLIGATORIO: PERFIL DE USUARIO", false, false);
                cboPerfiles.Focus();

                return false;
            }

            if (txtNombreUsuario.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: NOMBRE DE USUARIO", false, false);
                txtNombreUsuario.Focus();

                return false;
            }

            if (txtClave.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: CLAVE", false, false);
                txtClave.Focus();

                return false;
            }

            if (txtEmail.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: CORREO ELECTRÓNICO", false, false);
                txtEmail.Focus();

                return false;
            }

            if (!ValidarEmail(txtEmail.Text))
            {
                CargarInforme("FORMATO DE EMAIL INCORRECTO\nFORMATO ADMITIDO: usuario@dominio.com", false, false);
                txtEmail.Focus();

                return false;
            }

            return true;
        }

        private void EstadoCampos(string accion)
        {
            switch (accion)
            {
                case "SI":
                    dgvUsuarios.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    numDNI.Enabled = true;
                    cboPerfiles.Enabled = true;
                    txtNombreUsuario.Enabled = true;
                    txtClave.Enabled = true;
                    txtEmail.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;

                case "NO":
                    dgvUsuarios.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    numDNI.Enabled = false;
                    cboPerfiles.Enabled = false;
                    txtNombreUsuario.Enabled = false;
                    txtClave.Enabled = false;
                    txtEmail.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Text = "Guardar";

                    return;

                case "ELIMINAR":
                    dgvUsuarios.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    numDNI.Enabled = false;
                    cboPerfiles.Enabled = false;
                    txtNombreUsuario.Enabled = false;
                    txtClave.Enabled = false;
                    txtEmail.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnCancelar.Focus();

                    return;

                case "Modificar":
                    dgvUsuarios.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    numDNI.Enabled = false;
                    cboPerfiles.Enabled = true;
                    txtNombreUsuario.Enabled = true;
                    txtClave.Enabled = true;
                    txtEmail.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;
            }
        }

        private void LimpiarCampos()
        {
            numDNI.Value = 0;
            cboPerfiles.SelectedIndex = -1;
            txtNombreUsuario.Text = "";
            txtClave.Text = "";
            txtEmail.Text = "";
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarTablaUsuariosNoBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
            CargarComboBox(cboPerfiles, Perfil.ObtenerPerfilesComboBox());
            lblCantidad.Text = "Total de registros: " + dgvUsuarios.Rows.Count;
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                LimpiarCampos();
            }
            else
            {
                EstadoCampos("NO");
                CargarCampos();
                CargarInforme("INFORME", false, true);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            botonPresionado = "Agregar";
            EstadoCampos("SI");
            LimpiarCampos();
            CargarInforme("INFORME", false, true);
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN USUARIOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvUsuarios.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN USUARIO", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Modificar";
            EstadoCampos("Modificar");
            CargarInforme("INFORME", false, true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN USUARIOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvUsuarios.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN USUARIO", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Eliminar";
            btnGuardar.Text = "Eliminar";
            EstadoCampos("ELIMINAR");
            CargarInforme("¿DESEAS DAR DE BAJA AL USUARIO?", false, false);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string dni = numDNI.Text.ToString();
                Perfil perfil = new Perfil(Perfil.ObtenerPerfilPorNombre(cboPerfiles.Text).Id_perfil, cboPerfiles.Text, false);
                string nombreUsuario = txtNombreUsuario.Text.ToString();
                string clave = txtClave.Text.ToString();
                string email = txtEmail.Text.ToString();

                Usuario usuarioAuxiliar = new Usuario(dni, nombreUsuario, perfil, clave, email, false);
                string error = "";

                switch (botonPresionado)
                {
                    case "Agregar":
                        usuario = Usuario.ObtenerUsuario(numDNI.Text);
                        if (usuario != null)
                        {
                            if (usuario.Borrado)
                            {
                                error = Usuario.ModificarUsuario(usuarioAuxiliar);
                            }
                            else
                            {
                                CargarInforme("EL USUARIO YA SE ENCUENTRA REGISTRADO", false, false);

                                numDNI.Focus();

                                return;
                            }
                        }
                        else
                        {
                            error = Usuario.AgregarUsuario(usuarioAuxiliar);
                        }

                        break;

                    case "Modificar":
                        error = Usuario.ModificarUsuario(usuarioAuxiliar);

                        break;

                    case "Eliminar":
                        error = Usuario.EliminarUsuario(usuario);

                        break;
                }

                if (error == "")
                {
                    if (botonPresionado == "Agregar")
                    {
                        CargarInforme("USUARIO REGISTRADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Modificar")
                    {
                        CargarInforme("USUARIO MODIFICADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Eliminar")
                    {
                        CargarInforme("USUARIO ELIMINADO CON ÉXITO", true, false);
                    }
                }
                else
                {
                    CargarInforme(error, false, false);
                }

                CargarTablaUsuariosNoBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
                EstadoCampos("NO");
                lblCantidad.Text = "Total de registros: " + dgvUsuarios.Rows.Count;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            EstadoCampos("NO");
            LimpiarCampos();
            dgvUsuarios.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaUsuariosBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
                btnMostrarBorradosPresionado = true;
            }
            else
            {
                CargarTablaUsuariosNoBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
                btnMostrarBorradosPresionado = false;
            }
            lblCantidad.Text = "Total de registros: " + dgvUsuarios.Rows.Count;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorradosPresionado)
            {
                CargarTablaUsuariosBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuariosFiltro(txtBuscarDNI.Text));
            }
            else
            {
                CargarTablaUsuariosNoBorrados(dgvUsuarios, Usuario.ObtenerTablaUsuariosFiltro(txtBuscarDNI.Text));
            }
            
            lblCantidad.Text = "Total de registros: " + dgvUsuarios.Rows.Count;

            if (dgvUsuarios.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN USUARIOS\n DE ACUERDO AL FILTRO APLICADO", false, false);
            }
            else
            {
                CargarInforme("SE ENCONTRARON USUARIOS\n DE ACUERDO AL FILTRO APLICADO", true, false);
            }
        }
    }
}