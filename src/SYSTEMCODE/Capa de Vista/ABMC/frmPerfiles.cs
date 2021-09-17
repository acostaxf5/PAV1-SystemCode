using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Datos;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC
{
    public partial class FrmPerfiles : Form
    {
        Perfil perfil;

        private string botonPresionado = "";

        public FrmPerfiles()
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

        private void CargarTablaPerfilesNoBorrados(DataGridView dgv, DataTable tablaPerfiles)
        {
            btnMostrarBorrados.Text = "Mostrar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(tablaPerfiles.Rows[i]["Borrado"]))
                {
                    dgv.Rows.Add(tablaPerfiles.Rows[i]["Nombre"]);
                }
            }

            dgv.ClearSelection();
        }

        private void CargarTablaPerfilesBorrados(DataGridView dgv, DataTable tablaPerfiles)
        {
            btnMostrarBorrados.Text = "Ocultar Borrados";

            dgv.Rows.Clear();

            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                dgv.Rows.Add(tablaPerfiles.Rows[i]["Nombre"]);

                if (Convert.ToBoolean(tablaPerfiles.Rows[i]["Borrado"]))
                {
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                }
            }

            dgv.ClearSelection();
        }

        private void CargarCampos()
        {
            perfil = Perfil.ObtenerPerfilPorNombre(dgvPerfiles.CurrentRow.Cells[0].Value.ToString());
            txtNombrePerfil.Text = perfil.Nombre;
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
            if (txtNombrePerfil.Text == "")
            {
                CargarInforme("DATO OBLIGATORIO: NOMBRE DE PERFIL", false, false);
                txtNombrePerfil.Focus();

                return false;
            }

            return true;
        }

        private void EstadoCampos(string accion)
        {
            switch (accion)
            {
                case "SI":
                    dgvPerfiles.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtNombrePerfil.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    return;

                case "NO":
                    dgvPerfiles.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    txtNombrePerfil.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Text = "Guardar";

                    return;

                case "ELIMINAR":
                    dgvPerfiles.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                    txtNombrePerfil.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnCancelar.Focus();

                    return;
            }
        }

        private void LimpiarCampos()
        {
            txtNombrePerfil.Text = "";
        }

        private void FrmPerfiles_Load(object sender, EventArgs e)
        {
            CargarTablaPerfilesNoBorrados(dgvPerfiles, Perfil.ObtenerPerfiles());
            lblCantidad.Text = "Total de registros: " + dgvPerfiles.Rows.Count;
        }

        private void DgvPerfiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPerfiles.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
            {
                dgvPerfiles.ClearSelection();
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
            if (dgvPerfiles.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN PERFILES REGISTRADOS", false, false);
                return;
            }
            else if (!dgvPerfiles.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN PERFIL", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Modificar";
            EstadoCampos("SI");
            CargarInforme("INFORME", false, true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPerfiles.Rows.Count == 0)
            {
                CargarInforme("NO EXISTEN PERFILES REGISTRADOS", false, false);
                return;
            }
            else if (!dgvPerfiles.CurrentRow.Selected)
            {
                CargarInforme("DEBE SELECCIONAR UN PERFIL", false, false);
                LimpiarCampos();
                return;
            }

            botonPresionado = "Eliminar";
            btnGuardar.Text = "Eliminar";
            EstadoCampos("ELIMINAR");
            CargarInforme("¿DESEAS DAR DE BAJA AL PERFIL?", false, false);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string nombrePerfil = txtNombrePerfil.Text.ToString();

                Perfil perfilAuxiliar = new Perfil(nombrePerfil);
                string error = "";

                switch (botonPresionado)
                {
                    case "Agregar":
                        perfil = Perfil.ObtenerPerfilPorNombre(nombrePerfil);
                        if (perfil != null)
                        {
                            if (perfil.Borrado)
                            {
                                perfilAuxiliar.Id_perfil = perfil.Id_perfil;
                                error = Perfil.ModificarPerfil(perfilAuxiliar);
                            }
                            else
                            {
                                CargarInforme("EL PERFIL YA SE ENCUENTRA REGISTRADO", false, false);

                                txtNombrePerfil.Focus();

                                return;
                            }
                        }
                        else
                        {
                            error = Perfil.AgregarPerfil(perfilAuxiliar);
                        }

                        break;

                    case "Modificar":
                        perfilAuxiliar.Id_perfil = Perfil.ObtenerPerfilPorNombre(dgvPerfiles.CurrentRow.Cells[0].Value.ToString()).Id_perfil;
                        error = Perfil.ModificarPerfil(perfilAuxiliar);

                        break;

                    case "Eliminar":
                        IList<Usuario> listaUsuarios = UsuarioDatos.ConsultarTablaUsuarios();
                        for (int i = 0; i < listaUsuarios.Count; i++)
                        {
                            if (listaUsuarios[i].Perfil.Id_perfil.Equals(Perfil.ObtenerPerfilPorNombre(dgvPerfiles.CurrentRow.Cells[0].Value.ToString()).Id_perfil))
                            {
                                CargarInforme("EXISTEN USUARIOS ASIGNADOS A ESTE PERFIL", false, false);
                                return;
                            }
                        }

                        error = Perfil.EliminarPerfil(perfil);

                        break;
                }

                if (error == "")
                {
                    if (botonPresionado == "Agregar")
                    {
                        CargarInforme("PERFIL REGISTRADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Modificar")
                    {
                        CargarInforme("PERFIL MODIFICADO CON ÉXITO", true, false);
                    }

                    if (botonPresionado == "Eliminar")
                    {
                        CargarInforme("PERFIL ELIMINADO CON ÉXITO", true, false);
                    }
                }
                else
                {
                    CargarInforme(error, false, false);
                }

                CargarTablaPerfilesNoBorrados(dgvPerfiles, Perfil.ObtenerPerfiles());
                EstadoCampos("NO");
                lblCantidad.Text = "Total de registros: " + dgvPerfiles.Rows.Count;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            EstadoCampos("NO");
            LimpiarCampos();
            dgvPerfiles.ClearSelection();
            CargarInforme("INFORME", false, true);
        }

        private void BtnMostrarBorrados_Click(object sender, EventArgs e)
        {
            if (btnMostrarBorrados.Text == "Mostrar Borrados")
            {
                CargarTablaPerfilesBorrados(dgvPerfiles, Perfil.ObtenerPerfiles());
            }
            else
            {
                CargarTablaPerfilesNoBorrados(dgvPerfiles, Perfil.ObtenerPerfiles());
            }
            lblCantidad.Text = "Total de registros: " + dgvPerfiles.Rows.Count;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarTablaPerfilesNoBorrados(dgvPerfiles, Perfil.ObtenerTablaPerfilesFiltro(txtBuscarPerfil.Text));
            lblCantidad.Text = "Total de registros: " + dgvPerfiles.Rows.Count;

            if (dgvPerfiles.Rows.Count == 0)
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