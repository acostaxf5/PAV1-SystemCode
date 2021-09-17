using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC
{
    public partial class FrmBarrios : Form
    {
        Barrio barrio;

        private string botonPresionado = "";
        private bool btnMostrarBorradosPresionado = false;

        public FrmBarrios()
        {
            InitializeComponent();
        }

        private void Letra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void CargarTablaBarriosNoBorrados(DataGridView dgv, DataTable tablaBarrios)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaBarrios.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(tablaBarrios.Rows[i]["Borrado"]))
                {
                    dgv.Rows.Add(tablaBarrios.Rows[i]["Nombre"]);
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaBarriosBorrados(DataGridView dgv, DataTable tablaBarrios)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaBarrios.Rows.Count; i++)
            {
                dgv.Rows.Add(tablaBarrios.Rows[i]["Nombre"]);

                if (Convert.ToBoolean(tablaBarrios.Rows[i]["Borrado"]))
                {
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                }
            }

            dgv.ClearSelection();
        }

        private void CargarCampos()
        {
            barrio = Barrio.ObtenerBarrioPorNombre(dgvBarrios.CurrentRow.Cells[0].Value.ToString());
            txtNombreBarrio.Text = barrio.Nombre;
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
            if (txtNombreBarrio.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: NOMBRE DE BARRIO", false, false);
                txtNombreBarrio.Focus();

                return false;
            }

            return true;
        }

        private void EstadoCampos(string accion)
        {
            switch (accion)
            {
                case "SI":
                    dgvBarrios.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtNombreBarrio.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;

                case "NO":
                    dgvBarrios.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    txtNombreBarrio.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Text = "Guardar";

                    return;

                case "ELIMINAR":
                    dgvBarrios.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtNombreBarrio.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnCancelar.Focus();

                    return;
            }
        }

        private void LimpiarCampos()
        {
            txtNombreBarrio.Text = "";
        }

        private void FrmBarrios_Load(object sender, EventArgs e)
        {
            CargarTablaBarriosNoBorrados(dgvBarrios, Barrio.ObtenerBarrios());
            lblCantidad.Text = "Total de registros: " + dgvBarrios.Rows.Count;
        }

        private void DgvBarrios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBarrios.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
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
            if (dgvBarrios.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN BARRIOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvBarrios.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN BARRIO", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Modificar";
            EstadoCampos("SI");
            CargarInforme("INFORME", false, true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBarrios.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN BARRIOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvBarrios.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN BARRIO", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Eliminar";
            btnGuardar.Text = "Eliminar";
            EstadoCampos("ELIMINAR");
            CargarInforme("¿DESEAS DAR DE BAJA AL BARRIO?", false, false);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string nombreBarrio = txtNombreBarrio.Text.ToString();

                Barrio barrioAuxiliar = new Barrio(nombreBarrio);
                string error = "";

                switch (botonPresionado)
                {
                    case "Agregar":
                        barrio = Barrio.ObtenerBarrioPorNombre(nombreBarrio);
                        if (barrio != null)
                        {
                            if (barrio.Borrado)
                            {
                                barrioAuxiliar.Id_barrio = barrio.Id_barrio;
                                error = Barrio.ModificarBarrio(barrioAuxiliar);
                            }
                            else
                            {
                                CargarInforme("EL BARRIO YA SE ENCUENTRA REGISTRADO", false, false);

                                txtNombreBarrio.Focus();

                                return;
                            }
                        }
                        else
                        {
                            error = Barrio.AgregarBarrio(barrioAuxiliar);
                        }

                        break;

                    case "Modificar":
                        barrioAuxiliar.Id_barrio = Barrio.ObtenerBarrioPorNombre(dgvBarrios.CurrentRow.Cells[0].Value.ToString()).Id_barrio;
                        error = Barrio.ModificarBarrio(barrioAuxiliar);

                        break;

                    case "Eliminar":
                        IList<Cliente> listaClientes = Cliente.ObtenerTablaClientes();
                        for (int i = 0; i < listaClientes.Count; i++)
                        {
                            if (listaClientes[i].BarrioAsociado.Id_barrio.Equals(Barrio.ObtenerBarrioPorNombre(dgvBarrios.CurrentRow.Cells[0].Value.ToString()).Id_barrio) && !listaClientes[i].Borrado)
                            {
                                CargarInforme("EXISTEN CLIENTES ASIGNADOS A ESTE BARRIO", false, false);
                                return;
                            }
                        }

                        error = Barrio.EliminarBarrio(barrio);

                        break;
                }

                if (error == "")
                {
                    if (botonPresionado == "Agregar")
                    {
                        CargarInforme("BARRIO REGISTRADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Modificar")
                    {
                        CargarInforme("BARRIO MODIFICADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Eliminar")
                    {
                        CargarInforme("BARRIO ELIMINADO CON ÉXITO", true, false);
                    }
                }
                else
                {
                    CargarInforme(error, false, false);
                }

                CargarTablaBarriosNoBorrados(dgvBarrios, Barrio.ObtenerBarrios());
                EstadoCampos("NO");
                lblCantidad.Text = "Total de registros: " + dgvBarrios.Rows.Count;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            EstadoCampos("NO");
            LimpiarCampos();
            dgvBarrios.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaBarriosBorrados(dgvBarrios, Barrio.ObtenerBarrios());
                btnMostrarBorradosPresionado = true;
            }
            else
            {
                CargarTablaBarriosNoBorrados(dgvBarrios, Barrio.ObtenerBarrios());
                btnMostrarBorradosPresionado = false;
            }

            lblCantidad.Text = "Total de registros: " + dgvBarrios.Rows.Count;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorradosPresionado)
            {
                CargarTablaBarriosBorrados(dgvBarrios, Barrio.ObtenerTablaBarriosFiltro(txtBuscarBarrio.Text));
            }
            else
            {
                CargarTablaBarriosNoBorrados(dgvBarrios, Barrio.ObtenerTablaBarriosFiltro(txtBuscarBarrio.Text));
            }

            lblCantidad.Text = "Total de registros: " + dgvBarrios.Rows.Count;

            if (dgvBarrios.Rows.Count == 0)
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