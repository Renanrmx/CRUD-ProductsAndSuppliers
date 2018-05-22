using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProductsCRUD.Controller;


namespace ProductsCRUD {
    internal partial class Overview : Form {
        CtrlSelect ctrlSelect = new CtrlSelect();

        internal Overview() {
            InitializeComponent();

            var filters = new Dictionary<string, string> {
                { "all", "Todos"},
                { "product", "Produto"},
                { "price", "Preço"},
                { "stock", "Estoque"},
                { "details", "Detalhes"},
                { "supplier", "Fornecedor"},
                { "email", "Email"},
                { "phone", "Telefone"}
            };
            
            cmbFilters.ValueMember = "KEY";
            cmbFilters.DisplayMember = "VALUE";
            cmbFilters.DataSource = new BindingSource(filters, null);
            cmbFilters.SelectedIndex = 0;

            gridUpdate();
        }

        private void gridUpdate(object data = null) {
            if (data == null) data = ctrlSelect.getAll();
            tblOverview.DataSource = data;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            gridUpdate(ctrlSelect.getAll(txtSearch.Text, (string)cmbFilters.SelectedValue));
        }

        private void cmbFilters_SelectedValueChanged(object sender, EventArgs e) {
            gridUpdate(ctrlSelect.getAll(txtSearch.Text, (string)cmbFilters.SelectedValue));
        }
    }
}
