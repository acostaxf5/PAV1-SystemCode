using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.Informes
{
    public partial class FrmListados : Form
    {
        private readonly string usuarioGenerador;
        private readonly string modoApertura;

        public FrmListados(string usuarioGenerador, string modoApertura)
        {
            this.usuarioGenerador = usuarioGenerador;
            this.modoApertura = modoApertura;

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

        private bool ValidarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return (Regex.Replace(email, expresion, String.Empty).Length == 0);
            }

            return false;
        }

        private void HabilitarControles(bool modo)
        {
            txtEmail.Enabled = modo;
            btnImprimir.Enabled = modo;
            btnPDF.Enabled = modo;
            btnEmail.Enabled = modo;
        }

        private void ModoListado(DataTable tablaDatos, string modoApertura, IList<ReportParameter> parametrosAdicionales = null) {
            ReportDataSource reporte = new ReportDataSource(modoApertura, tablaDatos);

            IList<ReportParameter> parametros = new List<ReportParameter>
            {
                new ReportParameter("FechaCreacion", DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")),
                new ReportParameter("Cantidad" + modoApertura, tablaDatos.Rows.Count.ToString()),
                new ReportParameter("UsuarioCreador", usuarioGenerador.ToString())
            };

            if (parametrosAdicionales != null)
            {
                for (int i = 0; i < parametrosAdicionales.Count; i++)
                {
                    parametros.Add(parametrosAdicionales[i]);
                }
            }

            this.rvListado.LocalReport.ReportEmbeddedResource = "SYSTEMCODE.Capa_de_Vista.Informes.Listados." + modoApertura + ".rdlc";
            this.rvListado.LocalReport.DataSources.Clear();
            this.rvListado.LocalReport.DataSources.Add(reporte);
            this.rvListado.LocalReport.SetParameters(parametros);
            this.rvListado.RefreshReport();
        }

        private void FrmListados_Load(object sender, EventArgs e)
        {
            dtpFechaDesde.Value = DateTime.Now;
            dtpFechaHasta.Value = DateTime.Now;

            switch (modoApertura)
            {
                case "Clientes Activos":
                    ModoListado(Cliente.ObtenerListadoClientesActivos(), "ClientesActivos");
                    break;

                case "Usuarios Activos":
                    ModoListado(Usuario.ObtenerListadoUsuariosActivos(), "UsuariosActivos");
                    break;

                case "Proyectos Activos":
                    ModoListado(Proyecto.ObtenerListadoProyectosActivos(), "ProyectosActivos");
                    break;

                case "Ventas Por Fecha":
                    HabilitarControles(false);
                    dtpFechaDesde.Enabled = true;
                    dtpFechaHasta.Enabled = true;
                    btnGenerar.Enabled = true;
                    break;

                default:
                    break;
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (this.rvListado.PrintDialog() == DialogResult.OK)
            {
                CargarInforme("DOCUMENTO ENVIADO A COLA DE IMPRESIÓN", true, false);
            }
            else
            {
                CargarInforme("OPERACIÓN CANCELADA", false, false);
            }
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            Byte[] bytes = this.rvListado.LocalReport.Render("PDF");

            SaveFileDialog guardar = new SaveFileDialog
            {
                FileName = "Listado",
                DefaultExt = ".pdf",
                Filter = "PDF (*.pdf)|*.pdf"
            };

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                FileStream file = File.Create(guardar.FileName);
                file.Write(bytes, 0, bytes.Length);
                file.Flush();
                file.Close();

                CargarInforme("DOCUMENTO PDF GENERADO CON ÉXITO", true, false);
            }
            else
            {
                CargarInforme("OPERACIÓN CANCELADA", false, false);
            }
        }

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Correo Electrónico")
            {
                CargarInforme("DATO OBLIGATORIO: CORREO ELECTRÓNICO", false, false);
                txtEmail.Text = "";
                txtEmail.Focus();

                return;
            }

            if (ValidarEmail(txtEmail.Text))
            {
                Byte[] bytes = this.rvListado.LocalReport.Render("PDF");

                string nombreRemitente = "SYSTEMCODE";
                string emailRemitente = "systemcodetest@gmail.com";
                string claveRemitente = "riopc13579";

                string emailDestinatario = txtEmail.Text;

                MailMessage correo = new MailMessage { From = new MailAddress(emailRemitente, nombreRemitente) };
                correo.To.Add(new MailAddress(emailDestinatario));
                correo.Subject = "Listado de " + modoApertura.ToString();
                correo.Attachments.Add(new Attachment(new MemoryStream(bytes), "Listado.pdf"));
                correo.Body = "Estimado, se le adjunta el listado solicitado. Saludos!";

                try
                {
                    SmtpClient smtpCliente = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential(emailRemitente, claveRemitente)
                    };

                    smtpCliente.Send(correo);

                    CargarInforme("DOCUMENTO PDF ENVIADO CON ÉXITO", true, false);
                }
                catch (Exception)
                {
                    CargarInforme("ERROR DE SERVIDOR AL ENVIAR DOCUMENTO PDF", false, false);
                }
            }
            else
            {
                CargarInforme("FORMATO DE EMAIL INCORRECTO\nFORMATO ADMITIDO: usuario@dominio.com", false, false);
                txtEmail.Focus();

                return;
            }
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Correo Electrónico";
            }
        }

        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Correo Electrónico")
            {
                txtEmail.Text = "";
            }
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            if (dtpFechaDesde.Value > dtpFechaHasta.Value)
            {
                CargarInforme("FECHA INICIAL NO PUEDE SER SUPERIOR A\n" + dtpFechaHasta.Value.ToString("dd/MM/yyyy"), false, false);
                return;
            }

            if (dtpFechaHasta.Value > DateTime.Now)
            {
                CargarInforme("FECHA FINAL NO PUEDE SER SUPERIOR A\n" + DateTime.Now.ToString("dd/MM/yyyy"), false, false);
                return;
            }

            if (dtpFechaHasta.Value < dtpFechaDesde.Value)
            {
                CargarInforme("FECHA FINAL NO PUEDE SER INFERIOR A\n" + dtpFechaDesde.Value.ToString("dd/MM/yyyy"), false, false);
                return;
            }

            string fechaDesde = dtpFechaDesde.Value.ToString("yyyy-MM-dd");
            string fechaHasta = dtpFechaHasta.Value.ToString("yyyy-MM-dd");

            IList<ReportParameter> parametros = new List<ReportParameter>
            {
                new ReportParameter("FechaInicial", dtpFechaDesde.Value.ToString("dd/MM/yyyy")),
                new ReportParameter("FechaFinal", dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            };

            CargarInforme("INFORME", false, true);
            
            DataTable tablaResultado = Factura.ObtenerListadoFacturasPorFecha(fechaDesde, fechaHasta);
            if (tablaResultado.Rows.Count > 0)
            {
                HabilitarControles(true);
                ModoListado(Factura.ObtenerListadoFacturasPorFecha(fechaDesde, fechaHasta), "VentasPorFecha", parametros);
            }
            else
            {
                HabilitarControles(false);
                rvListado.Clear();
                CargarInforme("LA BÚSQUEDA NO ARROJÓ RESULTADOS", false, false);
            }
        }
    }
}