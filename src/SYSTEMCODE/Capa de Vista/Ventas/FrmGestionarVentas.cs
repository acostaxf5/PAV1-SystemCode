using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;
using SYSTEMCODE.Capa_de_Vista.Informes;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class FrmGestionarVentas : Form
    {
        readonly string nombreUsuario = "";
        bool cboDescripcionPresionado = false;
        string botonPresionado = "";

        Cliente clienteNuevo;
        Factura facturaNueva;

        public FrmGestionarVentas(string nombreUsuario)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario;
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

        private void CargarComboBox(ComboBox cbo, DataTable tabla, string modo)
        {
            cbo.DataSource = tabla;
            cbo.DisplayMember = (modo.Equals("Barrio")) ? tabla.Columns[1].ColumnName : (modo.Equals("Proyecto")) ? tabla.Columns[2].ColumnName : tabla.Columns[3].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private void ControlCampos(bool estado)
        {
            txtCUIT.Enabled = estado;
            btnBuscarCliente.Enabled = estado;
            cboDescripcion.Enabled = estado;
            txtCantidadLicencias.Enabled = estado;
            txtCostoLicencia.Enabled = estado;
            dgvVentas.Enabled = estado;
            btnAgregarProyecto.Enabled = estado;
            btnQuitarProyecto.Enabled = estado;
            btnGuardar.Enabled = estado;
            btnCancelar.Enabled = estado;
            btnGenerarFactura.Enabled = estado;

            btnGenerarVenta.Enabled = !estado;
            btnAnularVenta.Enabled = estado;
        }

        private void ActualizarTotal()
        {
            int total = 0;

            for (int i = 0; i < dgvVentas.Rows.Count; i++)
            {
                total += Convert.ToInt32(dgvVentas.Rows[i].Cells["costoTotal"].Value.ToString().Substring(1));
            }

            txtCostoTotal.Text = "$" + total.ToString();
        }

        private void LimpiarCampos()
        {
            txtNumeroVenta.Text = "";
            txtCUIT.Text = "";
            txtRazonSocial.Text = "";
            txtCalle.Text = "";
            txtNumero.Text = "";
            cboBarrios.SelectedIndex = -1;
            cboDescripcion.SelectedIndex = -1;
            txtVersion.Text = "";
            txtAlcance.Text = "";
            cboResponsable.SelectedIndex = -1;
            txtFechaRegistrada.Text = "";
            dgvVentas.Rows.Clear();
        }

        private void FrmGestionarVentas_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");

            CargarComboBox(cboBarrios, Barrio.ObtenerBarriosComboBox(), "Barrio");
            CargarComboBox(cboDescripcion, Proyecto.ObtenerTablaProyectosComboBox(), "Proyecto");
            CargarComboBox(cboResponsable, Usuario.ObtenerTablaUsuariosComboBox(), "Usuario");
        }

        private void BtnVerVentas_Click(object sender, EventArgs e)
        {
            FrmConsultarVentas consultarVentas = new FrmConsultarVentas
            {
                Text = "Listado de Ventas [Usuario: " + nombreUsuario + "]"
            };
            consultarVentas.ShowDialog();

            facturaNueva = consultarVentas.FacturaNueva;

            if (facturaNueva != null)
            {
                btnGenerarVenta.Enabled = false;
                btnAnularVenta.Enabled = true;
                btnCancelar.Enabled = true;

                CargarInforme("INFORME", false, true);

                txtNumeroVenta.Text = facturaNueva.NumeroFactura;
                txtFechaRegistrada.Text = facturaNueva.FechaCreacion.ToString("dd-MM-yyyy");

                clienteNuevo = Cliente.ObtenerCliente(facturaNueva.Cliente.Cuit);

                txtCUIT.Text = clienteNuevo.Cuit;
                txtRazonSocial.Text = clienteNuevo.Razon_social;
                txtCalle.Text = clienteNuevo.Calle;
                txtNumero.Text = clienteNuevo.Numero.ToString();
                cboBarrios.SelectedIndex = clienteNuevo.BarrioAsociado.Id_barrio - 1;

                dgvVentas.Rows.Clear();

                IList<FacturaDetalle> listaFacturaDetalles = FacturaDetalle.ObtenerListaFacturaDetalle(facturaNueva.NumeroFactura);

                for (int i = 0; i < listaFacturaDetalles.Count; i++)
                {
                    dgvVentas.Rows.Add
                    (
                        listaFacturaDetalles[i].ProyectoAsociado.Descripcion,
                        listaFacturaDetalles[i].CantidadLicencias.ToString(),
                        "$" + Convert.ToString(Convert.ToInt32(listaFacturaDetalles[i].Precio.ToString()) / Convert.ToInt32(listaFacturaDetalles[i].CantidadLicencias.ToString())),
                        "$" + listaFacturaDetalles[i].Precio.ToString()
                    );
                }

                dgvVentas.ClearSelection();

                ControlCampos(false);
                btnGenerarVenta.Enabled = false;
                btnAnularVenta.Enabled = true;
                btnGenerarFactura.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = true;
            }
        }

        private void BtnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (txtCUIT.TextLength < 11)
            {
                CargarInforme("DATO OBLIGATORIO: CUIT [11 DÍGITOS]", false, false);
                txtCUIT.Focus();
                return;
            }

            clienteNuevo = Cliente.ObtenerCliente(txtCUIT.Text);
            if (clienteNuevo != null && !clienteNuevo.Borrado)
            {
                CargarInforme("CLIENTE ENCONTRADO", true, false);

                txtRazonSocial.Text = clienteNuevo.Razon_social;
                txtCalle.Text = clienteNuevo.Calle;
                txtNumero.Text = clienteNuevo.Numero.ToString();
                cboBarrios.SelectedIndex = clienteNuevo.BarrioAsociado.Id_barrio - 1;
                ControlCampos(true);
                btnAnularVenta.Enabled = false;
                btnGenerarFactura.Enabled = false;
            }
            else
            {
                if (clienteNuevo == null)
                {
                    CargarInforme("CLIENTE NO REGISTRADO", false, false);
                }
                else if (clienteNuevo.Borrado)
                {
                    CargarInforme("CLIENTE DADO DE BAJA", false, false);
                }
                
                ControlCampos(false);
                txtCUIT.Enabled = true;
                btnBuscarCliente.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private void BtnAgregarProyecto_Click(object sender, EventArgs e)
        {
            if (cboDescripcion.SelectedIndex == -1)
            {
                CargarInforme("DEBE SELECCIONAR UN PROYECTO", false, false);
                cboDescripcion.Focus();
                return;
            }

            if (txtCantidadLicencias.TextLength == 0 || txtCantidadLicencias.Text == "0")
            {
                CargarInforme("DEBE REGISTRAR AL MENOS UNA LICENCIA", false, false);
                txtCantidadLicencias.Focus();
                return;
            }

            if (txtCostoLicencia.Text.Length == 0 || txtCostoLicencia.Text == "$0")
            {
                CargarInforme("EL VALOR DE LA LICENCIA, NO PUEDE SER NULO", false, false);
                txtCostoLicencia.Focus();
                return;
            }

            dgvVentas.Rows.Add
            (
                cboDescripcion.Text,
                txtCantidadLicencias.Text,
                txtCostoLicencia.Text,
                "$" + Convert.ToString(Convert.ToInt32(txtCantidadLicencias.Text) * Convert.ToInt32(txtCostoLicencia.Text.Substring(1)))
            );

            dgvVentas.ClearSelection();

            CargarInforme("INFORME", false, true);

            cboDescripcion.SelectedIndex = -1;
            txtCantidadLicencias.Text = "";
            txtCostoLicencia.Text = "";
            txtVersion.Text = "";
            txtAlcance.Text = "";
            cboResponsable.SelectedIndex = -1;
        }

        private void BtnQuitarProyecto_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count > 0)
            {
                if (!dgvVentas.CurrentRow.Selected)
                {
                    CargarInforme("DEBE SELECCIONAR UN PROYECTO DE LA LISTA", false, false);
                    return;
                }
                else
                {
                    CargarInforme("INFORME", false, true);
                }
            }
            else
            {
                CargarInforme("LA LISTA ESTA VACÍA", false, false);
                return;
            }

            dgvVentas.Rows.Remove(dgvVentas.CurrentRow);
            dgvVentas.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnGenerarVenta_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            ControlCampos(false);
            btnAnularVenta.Enabled = false;
            btnGenerarFactura.Enabled = false;
            txtCUIT.Enabled = true;
            btnBuscarCliente.Enabled = true;
            btnCancelar.Enabled = true;

            CargarInforme("INFORME", false, true);

            txtFechaRegistrada.Text = DateTime.Now.ToString("dd-MM-yyyy");
            botonPresionado = "Generar Venta";
        }

        private void BtnAnularVenta_Click(object sender, EventArgs e)
        {
            btnGuardar.Text = "Anular";
            botonPresionado = "Anular Venta";

            CargarInforme("¿DESEAS ANULAR LA VENTA?", false, false);

            btnAnularVenta.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (botonPresionado.Equals("Generar Venta"))
            {
                if (clienteNuevo == null)
                {
                    CargarInforme("DEBE SELECCIONAR UN CLIENTE REGISTRADO", false, false);
                    txtCUIT.Focus();
                    return;
                }

                if (clienteNuevo.Borrado)
                {
                    CargarInforme("DEBE SELECCIONAR UN CLIENTE ACTIVO", false, false);
                    txtCUIT.Focus();
                    return;
                }

                if (dgvVentas.Rows.Count == 0)
                {
                    CargarInforme("DEBE CARGAR PROYECTOS", false, false);
                    return;
                }

                string numeroFactura = Convert.ToString(Convert.ToInt32(Factura.ObtenerNumeroUltimaFactura()) + 1);

                facturaNueva = new Factura(numeroFactura, clienteNuevo, DateTime.Now, false);
                IList<FacturaDetalle> listaFacturasDetalles = new List<FacturaDetalle>();

                for (int i = 0; i < dgvVentas.Rows.Count; i++)
                {
                    Proyecto proyectoAsociado = Proyecto.ObtenerProyectoPorDescripcion(dgvVentas.Rows[i].Cells["descripcion"].Value.ToString());
                    int cantidadLicencias = Convert.ToInt32(dgvVentas.Rows[i].Cells["cantidadLicencias"].Value.ToString());
                    int costoTotal = Convert.ToInt32(dgvVentas.Rows[i].Cells["costoTotal"].Value.ToString().Substring(1));

                    listaFacturasDetalles.Add(new FacturaDetalle(numeroFactura, proyectoAsociado, cantidadLicencias, costoTotal, false));
                }

                string error = Factura.AgregarFactura(facturaNueva, listaFacturasDetalles);
                if (error == "")
                {
                    string numeroVenta = Factura.ObtenerNumeroUltimaFactura();

                    ControlCampos(false);
                    btnGenerarFactura.Enabled = true;
                    txtNumeroVenta.Text = numeroVenta;

                    CargarInforme("VENTA NÚMERO " + numeroVenta + " GENERADA CON ÉXITO", true, false);
                }
                else
                {
                    CargarInforme(error, false, false);
                }
            }

            if (botonPresionado.Equals("Anular Venta"))
            {
                string error = Factura.AnularFactura(facturaNueva);
                if (error == "")
                {
                    CargarInforme("VENTA NÚMERO " + txtNumeroVenta.Text + " ANULADA CON ÉXITO", true, false);

                    LimpiarCampos();
                    ControlCampos(false);
                    btnGuardar.Text = "Guardar";
                }
                else
                {
                    CargarInforme(error, false, false);
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtNumeroVenta.Text = "";
            ControlCampos(false);
            CargarInforme("INFORME", false, true);

            btnGuardar.Text = "Guardar";
        }

        private void DgvVentas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualizarTotal();
        }

        private void DgvVentas_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarTotal();
        }

        private void TxtCostoLicencia_Leave(object sender, EventArgs e)
        {
            if (!txtCostoLicencia.Text.StartsWith("$") && txtCostoLicencia.Text.Length > 0)
            {
                txtCostoLicencia.Text = "$" + txtCostoLicencia.Text;
            }
        }

        private void CboDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDescripcionPresionado)
            {
                Proyecto proyecto = Proyecto.ObtenerProyectoPorDescripcion(cboDescripcion.Text);

                txtVersion.Text = proyecto.Version;
                txtAlcance.Text = proyecto.Alcance;
                cboResponsable.SelectedIndex = proyecto.Responsable.Id_usuario - 1;

                cboDescripcionPresionado = false;
            }
        }

        private void CboDescripcion_Click(object sender, EventArgs e)
        {
            cboDescripcionPresionado = true;
        }

        private void BtnGenerarFactura_Click(object sender, EventArgs e)
        {
            IList<string> parametros = new List<string>
            {
                txtFecha.Text,
                txtCUIT.Text,
                txtRazonSocial.Text,
                txtCalle.Text,
                txtNumero.Text,
                cboBarrios.Text,
                txtCostoTotal.Text.Substring(1)
            };

            FrmFacturaVenta facturaVenta = new FrmFacturaVenta(txtNumeroVenta.Text, parametros)
            {
                Text = "Visualización de Factura [Usuario logueado: " + nombreUsuario + "]"
            };
            facturaVenta.ShowDialog();
        }
    }
}