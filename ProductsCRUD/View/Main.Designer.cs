namespace ProductsCRUD
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.navMenuStrip = new System.Windows.Forms.MenuStrip();
            this.navOverview = new System.Windows.Forms.ToolStripMenuItem();
            this.navEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.navSuppliers = new System.Windows.Forms.ToolStripMenuItem();
            this.navProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.navAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.navMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // navMenuStrip
            // 
            this.navMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navOverview,
            this.navEdit,
            this.navAbout});
            this.navMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.navMenuStrip.Name = "navMenuStrip";
            this.navMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.navMenuStrip.TabIndex = 0;
            this.navMenuStrip.Text = "menuStrip";
            // 
            // navOverview
            // 
            this.navOverview.Name = "navOverview";
            this.navOverview.Size = new System.Drawing.Size(46, 20);
            this.navOverview.Text = "Geral";
            this.navOverview.Click += new System.EventHandler(this.navOverview_Click);
            // 
            // navEdit
            // 
            this.navEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navSuppliers,
            this.navProducts});
            this.navEdit.Name = "navEdit";
            this.navEdit.Size = new System.Drawing.Size(49, 20);
            this.navEdit.Text = "Editar";
            // 
            // navSuppliers
            // 
            this.navSuppliers.Name = "navSuppliers";
            this.navSuppliers.Size = new System.Drawing.Size(145, 22);
            this.navSuppliers.Text = "Fornecedores";
            this.navSuppliers.Click += new System.EventHandler(this.navSuppliers_Click);
            // 
            // navProducts
            // 
            this.navProducts.Name = "navProducts";
            this.navProducts.Size = new System.Drawing.Size(145, 22);
            this.navProducts.Text = "Produtos";
            this.navProducts.Click += new System.EventHandler(this.navProducts_Click);
            // 
            // navAbout
            // 
            this.navAbout.Name = "navAbout";
            this.navAbout.Size = new System.Drawing.Size(49, 20);
            this.navAbout.Text = "Sobre";
            this.navAbout.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.navMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.navMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Main";
            this.Text = "Products CRUD";
            this.navMenuStrip.ResumeLayout(false);
            this.navMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip navMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem navAbout;
        private System.Windows.Forms.ToolStripMenuItem navEdit;
        private System.Windows.Forms.ToolStripMenuItem navSuppliers;
        private System.Windows.Forms.ToolStripMenuItem navProducts;
        private System.Windows.Forms.ToolStripMenuItem navOverview;
    }
}

