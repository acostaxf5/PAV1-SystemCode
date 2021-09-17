using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC
{
    public partial class FrmProyectos : Form
    {
        Proyecto proyecto;

        private string botonPresionado = "";
        private bool btnMostrarBorradosPresionado = false;

        public FrmProyectos()
        {
            InitializeComponent();
        }

        private void CargarTablaProyectosNoBorrados(DataGridView dgv, IList<Proyecto> listaProyectos)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaProyectos.Count; i++)
            {
                if (!listaProyectos[i].Borrado)
                {
                    dgv.Rows.Add(listaProyectos[i].Descripcion);
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaProyectosBorrados(DataGridView dgv, IList<Proyecto> listaProyectos)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < listaProyectos.Count; i++)
            {
                dgv.Rows.Add(listaProyectos[i].Descripcion);

                if (listaProyectos[i].Borrado)
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
            cbo.DisplayMember = tabla.Columns[3].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private void CargarCampos()
        {
            proyecto = Proyecto.ObtenerProyectoPorDescripcion(dgvProyectos.CurrentRow.Cells[0].Value.ToString());

            txtDescripcion.Text = proyecto.Descripcion.ToString();
            txtVersion.Text = proyecto.Version.ToString();
            txtAlcance.Text = proyecto.Alcance.ToString();
            cboResponsable.SelectedIndex = proyecto.Responsable.Id_usuario - 1;
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
            if (txtDescripcion.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: DESCRIPCIÓN", false, false);
                txtDescripcion.Focus();

                return false;
            }

            if (txtVersion.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: VERSIÓN", false, false);
                txtVersion.Focus();

                return false;
            }

            if (txtAlcance.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: ALCANCE", false, false);
                txtAlcance.Focus();

                return false;
            }

            if (cboResponsable.SelectedIndex == -1)
            {
                CargarInforme("DATO OBLIGATORIO: USUARIO RESPONSABLE", false, false);
                cboResponsable.Focus();

                return false;
            }

            return true;
        }

        private void EstadoCampos(string accion)
        {
            switch (accion)
            {
                case "SI":
                    dgvProyectos.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtDescripcion.Enabled = true;
                    txtVersion.Enabled = true;
                    txtAlcance.Enabled = true;
                    cboResponsable.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;

                case "NO":
                    dgvProyectos.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    txtDescripcion.Enabled = false;
                    txtVersion.Enabled = false;
                    txtAlcance.Enabled = false;
                    cboResponsable.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Text = "Guardar";

                    return;

                case "ELIMINAR":
                    dgvProyectos.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtDescripcion.Enabled = false;
                    txtVersion.Enabled = false;
                    txtAlcance.Enabled = false;
                    cboResponsable.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnCancelar.Focus();

                    return;
            }
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Text = "";
            txtVersion.Text = "";
            txtAlcance.Text = "";
            cboResponsable.SelectedIndex = -1;
        }

        private void FrmProyectos_Load(object sender, EventArgs e)
        {
            CargarTablaProyectosNoBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectos());
            CargarComboBox(cboResponsable, Usuario.ObtenerTablaUsuariosComboBox());
            lblCantidad.Text = "Total de registros: " + dgvProyectos.Rows.Count;
        }

        private void DgvProyectos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProyectos.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
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
            if (dgvProyectos.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN PROYECTOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvProyectos.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN PROYECTO", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Modificar";
            EstadoCampos("SI");
            CargarInforme("INFORME", false, true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProyectos.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN PROYECTOS REGISTRADOS", false, false);
                return;
            }
            else if (!dgvProyectos.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN PROYECTO", false, false);
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
                string descripcion = txtDescripcion.Text.ToString();
                string version = txtVersion.Text.ToString();
                string alcance = txtAlcance.Text.ToString();
                Usuario asignado = new Usuario(cboResponsable.SelectedIndex + 1, cboResponsable.Text);
                

                Proyecto proyectoAuxiliar = new Proyecto(descripcion, version, alcance, asignado, false);
                string error = "";

                switch (botonPresionado)
                {
                    case "Agregar":
                        proyecto = Proyecto.ObtenerProyectoPorDescripcion(txtDescripcion.Text);
                        if (proyecto != null)
                        {
                            if (proyecto.Borrado)
                            {
                                error = Proyecto.ModificarProyecto(proyectoAuxiliar);
                            }
                            else
                            {
                                CargarInforme("EL PROYECTO YA SE ENCUENTRA REGISTRADO", false, false);

                                txtDescripcion.Focus();

                                return;
                            }
                        }
                        else
                        {
                            error = Proyecto.AgregarProyecto(proyectoAuxiliar);
                        }

                        break;

                    case "Modificar":
                        error = Proyecto.ModificarProyecto(proyectoAuxiliar);

                        break;

                    case "Eliminar":
                        error = Proyecto.EliminarProyecto(proyecto);

                        break;
                }

                if (error == "")
                {
                    if (botonPresionado == "Agregar")
                    {
                        CargarInforme("PROYECTO REGISTRADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Modificar")
                    {
                        CargarInforme("PROYECTO MODIFICADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Eliminar")
                    {
                        CargarInforme("PROYECTO ELIMINADO CON ÉXITO", true, false);
                    }
                }
                else
                {
                    CargarInforme(error, false, false);
                }

                CargarTablaProyectosNoBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectos());
                EstadoCampos("NO");
                lblCantidad.Text = "Total de registros: " + dgvProyectos.Rows.Count;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            EstadoCampos("NO");
            LimpiarCampos();
            dgvProyectos.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaProyectosBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectos());
                btnMostrarBorradosPresionado = true;
            }
            else
            {
                CargarTablaProyectosNoBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectos());
                btnMostrarBorradosPresionado = false;
            }
            lblCantidad.Text = "Total de registros: " + dgvProyectos.Rows.Count;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorradosPresionado)
            {
                CargarTablaProyectosBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectosFiltro(txtBuscarDescripcion.Text));
            }
            else
            {
                CargarTablaProyectosNoBorrados(dgvProyectos, Proyecto.ObtenerTablaProyectosFiltro(txtBuscarDescripcion.Text));
            }
            
            lblCantidad.Text = "Total de registros: " + dgvProyectos.Rows.Count;

            if (dgvProyectos.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN PROYECTOS\n DE ACUERDO AL FILTRO APLICADO", false, false);
            }
            else
            {
                CargarInforme("SE ENCONTRARON PROYECTOS\n DE ACUERDO AL FILTRO APLICADO", true, false);
            }
        }
    }
}