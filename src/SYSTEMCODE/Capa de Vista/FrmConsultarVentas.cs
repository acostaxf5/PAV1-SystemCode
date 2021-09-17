using System;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class FrmConsultarVentas : Form
    {
        Factura facturaNueva;

        public Factura FacturaNueva { get => facturaNueva; set => facturaNueva = value; }

        public FrmConsultarVentas()
        {
            InitializeComponent();
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

        private void CargarTablaVentasNoBorradas(DataGridView dgv, DataTable tablaFacturas)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaFacturas.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(tablaFacturas.Rows[i]["borrado"]))
                {
                    dgv.Rows.Add
                    (
                        tablaFacturas.Rows[i]["numero_factura"].ToString(),
                        Cliente.ObtenerCliente(Convert.ToInt32(tablaFacturas.Rows[i]["id_cliente"].ToString())).Razon_social,
                        Convert.ToDateTime(tablaFacturas.Rows[i]["fecha"]).ToString("dd-MM-yyyy")
                    );
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaVentasBorradas(DataGridView dgv, DataTable tablaFacturas)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaFacturas.Rows.Count; i++)
            {
                dgv.Rows.Add
                (
                    tablaFacturas.Rows[i]["numero_factura"].ToString(),
                    Cliente.ObtenerCliente(Convert.ToInt32(tablaFacturas.Rows[i]["id_cliente"].ToString())).Razon_social,
                    Convert.ToDateTime(tablaFacturas.Rows[i]["fecha"]).ToString("dd-MM-yyyy")
                );

                if (Convert.ToBoolean(tablaFacturas.Rows[i]["borrado"]))
                {
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                }
            }

            dgv.ClearSelection();
        }

        private void FrmConsultarVentas_Load(object sender, EventArgs e)
        {
            CargarTablaVentasNoBorradas(dgvVentas, Factura.ObtenerTablaFacturas());
            lblCantidad.Text = "Total de registros: " + dgvVentas.Rows.Count;

            dgvVentas.ClearSelection();
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count > 0)
            {
                if (!dgvVentas.CurrentRow.Selected)
                {
                    CargarInforme("DEBE SELECCIONAR UNA VENTA DE LA TABLA", false, false);
                    return;
                }
            }
            else
            {
                CargarInforme("NO EXISTEN VENTAS REGISTRADAS", false, false);
                return;
            }

            FacturaNueva = Factura.ObtenerFactura(dgvVentas.CurrentRow.Cells[0].Value.ToString());

            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            CargarInforme("INFORME", false, true);

            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaVentasBorradas(dgvVentas, Factura.ObtenerTablaFacturas());
            }
            else
            {
                CargarTablaVentasNoBorradas(dgvVentas, Factura.ObtenerTablaFacturas());
            }
            lblCantidad.Text = "Total de registros: " + dgvVentas.Rows.Count;
        }

        private void DgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
            {
                dgvVentas.ClearSelection();
            }
            else
            {
                CargarInforme("INFORME", false, true);
            }
        }
    }
}