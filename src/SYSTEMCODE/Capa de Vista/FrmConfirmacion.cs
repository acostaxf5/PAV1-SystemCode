using System;
using System.Windows.Forms;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class FrmConfirmacion : Form
    {
        bool btnSiPresionado = false;

        public FrmConfirmacion()
        {
            InitializeComponent();
        }

        public bool BtnSiPresionado { get => btnSiPresionado; set => btnSiPresionado = value; }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Close();
            BtnSiPresionado = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }
    }
}