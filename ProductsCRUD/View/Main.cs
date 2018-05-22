using System;
using System.Windows.Forms;
using ProductsCRUD.View;


namespace ProductsCRUD {
    internal partial class Main : Form {

        internal Main() {
            InitializeComponent();
            
            navOverview_Click(this, new EventArgs());
        }

        private void validate() {
            navOverview.Enabled = true;
            navSuppliers.Enabled = true;
            navProducts.Enabled = true;
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
        }

        private void navOverview_Click(object sender, EventArgs e) {
            validate();
            ActiveMdiChild?.Close();
            Overview overview = new Overview {
                MdiParent = this,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            overview.Show();
            navOverview.Enabled = false;
        }

        private void navSuppliers_Click(object sender, EventArgs e) {
            validate();
            ActiveMdiChild?.Close();
            Suppliers suppliers = new Suppliers {
                MdiParent = this,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            suppliers.Show();
            navSuppliers.Enabled = false;
        }

        private void navProducts_Click(object sender, EventArgs e) {
            validate();
            ActiveMdiChild?.Close();
            Products products = new Products {
                MdiParent = this,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            products.Show();
            navProducts.Enabled = false;
        }
    }
}