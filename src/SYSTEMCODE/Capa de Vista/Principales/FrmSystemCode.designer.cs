namespace SYSTEMCODE
{
    partial class FrmSystemCode
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemCode));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBarrios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProyectos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInformes = new System.Windows.Forms.ToolStripMenuItem();
            this.listadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientesActivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosActivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proyectosActivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventasPorFechasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proyectosPorClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadísticasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barriosConMásVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proyectosConMásVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.elemento1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.menuVentas,
            this.menuInformes});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuClientes,
            this.menuBarrios,
            this.menuProyectos,
            this.toolStripSeparator1,
            this.menuSalir});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(126, 22);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.Click += new System.EventHandler(this.UsuariosToolStripMenuItem_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.Size = new System.Drawing.Size(126, 22);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.Click += new System.EventHandler(this.ClientesToolStripMenuItem_Click);
            // 
            // menuBarrios
            // 
            this.menuBarrios.Name = "menuBarrios";
            this.menuBarrios.Size = new System.Drawing.Size(126, 22);
            this.menuBarrios.Text = "Barrios";
            this.menuBarrios.Click += new System.EventHandler(this.BarriosToolStripMenuItem_Click);
            // 
            // menuProyectos
            // 
            this.menuProyectos.Name = "menuProyectos";
            this.menuProyectos.Size = new System.Drawing.Size(126, 22);
            this.menuProyectos.Text = "Proyectos";
            this.menuProyectos.Click += new System.EventHandler(this.ProyectosToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // menuSalir
            // 
            this.menuSalir.Name = "menuSalir";
            this.menuSalir.Size = new System.Drawing.Size(126, 22);
            this.menuSalir.Text = "Salir";
            this.menuSalir.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // menuVentas
            // 
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.Size = new System.Drawing.Size(53, 20);
            this.menuVentas.Text = "Ventas";
            this.menuVentas.Click += new System.EventHandler(this.MenuVentas_Click);
            // 
            // menuInformes
            // 
            this.menuInformes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listadosToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.estadísticasToolStripMenuItem});
            this.menuInformes.Name = "menuInformes";
            this.menuInformes.Size = new System.Drawing.Size(66, 20);
            this.menuInformes.Text = "Informes";
            // 
            // listadosToolStripMenuItem
            // 
            this.listadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientesActivosToolStripMenuItem,
            this.usuariosActivosToolStripMenuItem,
            this.proyectosActivosToolStripMenuItem,
            this.ventasPorFechasToolStripMenuItem});
            this.listadosToolStripMenuItem.Name = "listadosToolStripMenuItem";
            this.listadosToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.listadosToolStripMenuItem.Text = "Listados";
            // 
            // ClientesActivosToolStripMenuItem
            // 
            this.ClientesActivosToolStripMenuItem.Name = "ClientesActivosToolStripMenuItem";
            this.ClientesActivosToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ClientesActivosToolStripMenuItem.Text = "Clientes Activos";
            this.ClientesActivosToolStripMenuItem.Click += new System.EventHandler(this.ClientesActivosToolStripMenuItem_Click);
            // 
            // usuariosActivosToolStripMenuItem
            // 
            this.usuariosActivosToolStripMenuItem.Name = "usuariosActivosToolStripMenuItem";
            this.usuariosActivosToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.usuariosActivosToolStripMenuItem.Text = "Usuarios Activos";
            this.usuariosActivosToolStripMenuItem.Click += new System.EventHandler(this.UsuariosActivosToolStripMenuItem_Click);
            // 
            // proyectosActivosToolStripMenuItem
            // 
            this.proyectosActivosToolStripMenuItem.Name = "proyectosActivosToolStripMenuItem";
            this.proyectosActivosToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.proyectosActivosToolStripMenuItem.Text = "Proyectos Activos";
            this.proyectosActivosToolStripMenuItem.Click += new System.EventHandler(this.ProyectosActivosToolStripMenuItem_Click);
            // 
            // ventasPorFechasToolStripMenuItem
            // 
            this.ventasPorFechasToolStripMenuItem.Name = "ventasPorFechasToolStripMenuItem";
            this.ventasPorFechasToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ventasPorFechasToolStripMenuItem.Text = "Ventas por Fechas";
            this.ventasPorFechasToolStripMenuItem.Click += new System.EventHandler(this.VentasPorFechasToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proyectosPorClienteToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // proyectosPorClienteToolStripMenuItem
            // 
            this.proyectosPorClienteToolStripMenuItem.Name = "proyectosPorClienteToolStripMenuItem";
            this.proyectosPorClienteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.proyectosPorClienteToolStripMenuItem.Text = "Proyectos por Cliente";
            this.proyectosPorClienteToolStripMenuItem.Click += new System.EventHandler(this.ProyectosPorClienteToolStripMenuItem_Click);
            // 
            // estadísticasToolStripMenuItem
            // 
            this.estadísticasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barriosConMásVentasToolStripMenuItem,
            this.proyectosConMásVentasToolStripMenuItem});
            this.estadísticasToolStripMenuItem.Name = "estadísticasToolStripMenuItem";
            this.estadísticasToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.estadísticasToolStripMenuItem.Text = "Estadísticas";
            // 
            // barriosConMásVentasToolStripMenuItem
            // 
            this.barriosConMásVentasToolStripMenuItem.Name = "barriosConMásVentasToolStripMenuItem";
            this.barriosConMásVentasToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.barriosConMásVentasToolStripMenuItem.Text = "Barrios con más Ventas";
            this.barriosConMásVentasToolStripMenuItem.Click += new System.EventHandler(this.BarriosConMásVentasToolStripMenuItem_Click);
            // 
            // proyectosConMásVentasToolStripMenuItem
            // 
            this.proyectosConMásVentasToolStripMenuItem.Name = "proyectosConMásVentasToolStripMenuItem";
            this.proyectosConMásVentasToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.proyectosConMásVentasToolStripMenuItem.Text = "Proyectos con más Ventas";
            this.proyectosConMásVentasToolStripMenuItem.Click += new System.EventHandler(this.ProyectosConMásVentasToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Eurostile ExtendedTwo", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(326, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 44);
            this.label1.TabIndex = 3;
            this.label1.Text = "SYSTEMCODE";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(394, 135);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 212);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.lblBienvenida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBienvenida.Font = new System.Drawing.Font("Eurostile", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.ForeColor = System.Drawing.Color.White;
            this.lblBienvenida.Location = new System.Drawing.Point(-5, 492);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(1019, 38);
            this.lblBienvenida.TabIndex = 5;
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSystemCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1008, 528);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmSystemCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SYSTEMCODE";
            this.Load += new System.EventHandler(this.FrmSystemCode_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.ToolTip elemento1;
        private System.Windows.Forms.ToolStripMenuItem menuVentas;
        private System.Windows.Forms.ToolStripMenuItem menuInformes;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem menuClientes;
        private System.Windows.Forms.ToolStripMenuItem menuBarrios;
        private System.Windows.Forms.ToolStripMenuItem menuProyectos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuSalir;
        private System.Windows.Forms.ToolStripMenuItem listadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClientesActivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosActivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proyectosActivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventasPorFechasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proyectosPorClienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadísticasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barriosConMásVentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proyectosConMásVentasToolStripMenuItem;
    }
}
