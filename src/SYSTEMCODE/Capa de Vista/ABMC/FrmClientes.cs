using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC
{
    public partial class FrmClientes : Form
    {
        Cliente cliente;

        private string botonPresionado = "";
        private bool btnMostrarBorradosPresionado = false;

        public FrmClientes()
        {
            InitializeComponent();
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

        private bool ValidarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return (Regex.Replace(email, expresion, String.Empty).Length == 0);
            }

            return false;
        }

        private void CargarTablaClientesNoBorrados(DataGridView dgv, IList<Cliente> listaClientes)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaClientes.Count; i++)
            {
                if (!listaClientes[i].Borrado)
                {
                    dgv.Rows.Add
                    (
                        listaClientes[i].Cuit,
                        listaClientes[i].Razon_social,
                        listaClientes[i].Fecha_alta.ToString("dd-MM-yyyy")
                    );
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaClientesBorrados(DataGridView dgv, IList<Cliente> listaClientes)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaClientes.Count; i++)
            {
                dgv.Rows.Add
                (
                    listaClientes[i].Cuit,
                    listaClientes[i].Razon_social,
                    listaClientes[i].Fecha_alta.ToString("dd-MM-yyyy")
                );

                if (listaClientes[i].Borrado)
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
            cliente = Cliente.ObtenerCliente(dgvClientes.CurrentRow.Cells[0].Value.ToString());

            numCUIT.Text = cliente.Cuit.ToString();
            txtRazonSocial.Text = cliente.Razon_social.ToString();
            txtCalle.Text = cliente.Calle.ToString();
            numDomicilio.Text = cliente.Numero.ToString();
            cboBarrios.SelectedIndex = cliente.BarrioAsociado.Id_barrio - 1;

            Contacto contactoAuxiliar = Contacto.ObtenerContacto(numCUIT.Text);
            txtNombre.Text = contactoAuxiliar.Nombre.ToString();
            txtApellido.Text = contactoAuxiliar.Apellido.ToString();
            txtEmail.Text = contactoAuxiliar.Email.ToString();
            txtTelefono.Text = contactoAuxiliar.Telefono.ToString();
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

        private bool ValidarCampos()
        {
            if (numCUIT.Value.ToString().Length < 11)
            {
                CargarInforme("DATO OBLIGATORIO: CUIT [11 NÚMEROS]", false, false);
                numCUIT.Focus();
                
                return false;
            }

            if (txtRazonSocial.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: RAZÓN SOCIAL", false, false);
                txtRazonSocial.Focus();

                return false;
            }

            if (txtCalle.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: CALLE", false, false);
                txtCalle.Focus();

                return false;
            }

            if (numDomicilio.Value == -1)
            {
                CargarInforme("DATO OBLIGATORIO: NÚMERO", false, false);
                numDomicilio.Focus();

                return false;
            }

            if (cboBarrios.SelectedIndex == -1)
            {
                CargarInforme("DATO OBLIGATORIO: BARRIO", false, false);
                cboBarrios.Focus();

                return false;
            }

            if (txtNombre.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: NOMBRE", false, false);
                txtNombre.Focus();

                return false;
            }

            if (txtApellido.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: APELLIDO", false, false);
                txtApellido.Focus();

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

            if (txtTelefono.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: TELÉFONO", false, false);
                txtTelefono.Focus();

                return false;
            }

            return true;
        }

        private void EstadoCampos(string accion)
        {
            switch (accion)
            {
                case "SI":
                    dgvClientes.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    numCUIT.Enabled = true;
                    txtRazonSocial.Enabled = true;
                    txtCalle.Enabled = true;
                    numDomicilio.Enabled = true;
                    cboBarrios.Enabled = true;
                    txtNombre.Enabled = true;
                    txtApellido.Enabled = true;
                    txtEmail.Enabled = true;
                    txtTelefono.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;

                case "NO":
                    dgvClientes.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    numCUIT.Enabled = false;
                    txtRazonSocial.Enabled = false;
                    txtCalle.Enabled = false;
                    numDomicilio.Enabled = false;
                    cboBarrios.Enabled = false;
                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtEmail.Enabled = false;
                    txtTelefono.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Text = "Guardar";

                    return;

                case "ELIMINAR":
                    dgvClientes.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    numCUIT.Enabled = false;
                    txtRazonSocial.Enabled = false;
                    txtCalle.Enabled = false;
                    numDomicilio.Enabled = false;
                    cboBarrios.Enabled = false;
                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtEmail.Enabled = false;
                    txtTelefono.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnCancelar.Focus();

                    return;
            }
        }

        private void LimpiarCampos()
        {
            numCUIT.Value = 0;
            txtRazonSocial.Text = "";
            txtCalle.Text = "";
            numDomicilio.Value = -1;
            cboBarrios.SelectedIndex = -1;
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarTablaClientesNoBorrados(dgvClientes, Cliente.ObtenerTablaClientes());
            CargarComboBox(cboBarrios, Barrio.ObtenerBarriosComboBox());
            lblCantidad.Text = "Total de registros: " + dgvClientes.Rows.Count;
        }

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
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
            if (dgvClientes.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN CLIENTES REGISTRADOS", false, false);
                return;
            }
            else if (!dgvClientes.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN CLIENTE", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Modificar";
            EstadoCampos("SI");
            numCUIT.Enabled = false;
            CargarInforme("INFORME", false, true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN CLIENTES REGISTRADOS", false, false);
                return;
            }
            else if (!dgvClientes.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN CLIENTE", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Eliminar";
            btnGuardar.Text = "Eliminar";
            EstadoCampos("ELIMINAR");
            CargarInforme("¿DESEAS DAR DE BAJA AL CLIENTE?", false, false);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string cuit = numCUIT.Value.ToString();
                string razonSocial = txtRazonSocial.Text.ToString();
                string calle = txtCalle.Text.ToString();
                int numero = Convert.ToInt32(numDomicilio.Value);
                Barrio barrio = new Barrio(Barrio.ObtenerBarrioPorNombre(cboBarrios.Text).Id_barrio, cboBarrios.Text, false);
                string nombre = txtNombre.Text.ToString();
                string apellido = txtApellido.Text.ToString();
                string email = txtEmail.Text.ToString();
                string telefono = txtTelefono.Text.ToString();
                Contacto contacto = new Contacto(cuit, nombre, apellido, email, telefono);

                Cliente clienteAuxiliar = new Cliente(cuit, razonSocial, false, calle, numero, DateTime.Today, barrio, contacto);
                string error = "";

                switch (botonPresionado)
                {
                    case "Agregar":
                        cliente = Cliente.ObtenerCliente(numCUIT.Text.ToString());
                        if (cliente != null)
                        {
                            if (cliente.Borrado)
                            {
                                error = Cliente.ModificarCliente(clienteAuxiliar);
                            }
                            else
                            {
                                CargarInforme("EL CLIENTE YA SE ENCUENTRA REGISTRADO", false, false);

                                numCUIT.Focus();

                                return;
                            }
                        }
                        else
                        {
                            error = Cliente.AgregarCliente(clienteAuxiliar);
                        }

                        break;

                    case "Modificar":
                        error = Cliente.ModificarCliente(clienteAuxiliar);

                        break;

                    case "Eliminar":
                        error = Cliente.EliminarCliente(clienteAuxiliar);

                        break;
                }

                if (error == "")
                {
                    if (botonPresionado == "Agregar")
                    {
                        CargarInforme("CLIENTE REGISTRADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Modificar")
                    {
                        CargarInforme("CLIENTE MODIFICADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Eliminar")
                    {
                        CargarInforme("CLIENTE ELIMINADO CON ÉXITO", true, false);
                    }
                }
                else
                {
                    CargarInforme(error, false, false);
                }

                CargarTablaClientesNoBorrados(dgvClientes, Cliente.ObtenerTablaClientes());
                EstadoCampos("NO");
                lblCantidad.Text = "Total de registros: " + dgvClientes.Rows.Count;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            EstadoCampos("NO");
            LimpiarCampos();
            dgvClientes.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaClientesBorrados(dgvClientes, Cliente.ObtenerTablaClientes());
                btnMostrarBorradosPresionado = true;
            }
            else
            {
                CargarTablaClientesNoBorrados(dgvClientes, Cliente.ObtenerTablaClientes());
                btnMostrarBorradosPresionado = false;
            }

            lblCantidad.Text = "Total de registros: " + dgvClientes.Rows.Count;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorradosPresionado)
            {
                CargarTablaClientesBorrados(dgvClientes, Cliente.ObtenerTablaClientesFiltro(txtBuscarCUIT.Text));
            }
            else
            {
                CargarTablaClientesNoBorrados(dgvClientes, Cliente.ObtenerTablaClientesFiltro(txtBuscarCUIT.Text));
            }
            
            lblCantidad.Text = "Total de registros: " + dgvClientes.Rows.Count;

            if (dgvClientes.Rows.Count == 0)
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