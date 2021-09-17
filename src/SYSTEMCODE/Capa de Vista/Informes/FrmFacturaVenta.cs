using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.Informes
{
    public partial class FrmFacturaVenta : Form
    {
        public string idFactura;
        public IList<string> parametrosRecibidos = new List<string>();

        public FrmFacturaVenta(string idFactura, IList<string> parametrosRecibidos)
        {
            this.idFactura = idFactura;
            this.parametrosRecibidos = parametrosRecibidos;
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

        private void FrmFacturaVenta_Load(object sender, EventArgs e)
        {
            ReportDataSource reporte = new ReportDataSource("DetalleFactura", FacturaDetalle.ObtenerDetalleFactura(idFactura));

            IList<ReportParameter> parametros = new List<ReportParameter>
            {
                new ReportParameter("NumeroFactura", idFactura.ToString()),
                new ReportParameter("Fecha", parametrosRecibidos[0]),
                new ReportParameter("Cuit", parametrosRecibidos[1]),
                new ReportParameter("RazonSocial", parametrosRecibidos[2]),
                new ReportParameter("Calle", parametrosRecibidos[3]),
                new ReportParameter("NumeroCalle", parametrosRecibidos[4]),
                new ReportParameter("Barrio", parametrosRecibidos[5]),
                new ReportParameter("ImporteTotal", parametrosRecibidos[6])
            };

            this.rvListado.LocalReport.ReportEmbeddedResource = "SYSTEMCODE.Capa_de_Vista.Informes.Reportes.FacturaVenta.rdlc";
            this.rvListado.LocalReport.DataSources.Clear();
            this.rvListado.LocalReport.DataSources.Add(reporte);
            this.rvListado.LocalReport.SetParameters(parametros);
            this.rvListado.RefreshReport();
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
                FileName = "Factura [" + idFactura.ToString() + "]",
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
                correo.Subject = "Factura Número " + idFactura.ToString();
                correo.Attachments.Add(new Attachment(new MemoryStream(bytes), "Factura [" + idFactura.ToString() + "].pdf"));
                correo.Body = "Estimado, se le adjunta la factura correspondiente a su compra. Saludos!";

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
    }
}