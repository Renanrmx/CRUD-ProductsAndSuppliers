using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using ProductsCRUD.Controller;


namespace ProductsCRUD {
    internal partial class Products : Form {
        CtrlSupplier ctrlSupplier = new CtrlSupplier();
        CtrlProduct ctrlProduct = new CtrlProduct();
        CleanProduct cleanProduct = new CleanProduct();
        CtrlSelect ctrlSelect = new CtrlSelect();

        Dictionary<string, Dictionary<string, bool>> stateEntries;

        internal Products() {
            InitializeComponent();
            
            stateEntries = new Dictionary<string, Dictionary<string, bool>> {
                { "valid", new Dictionary<string, bool> {
                    { "name", false },      //not null
                    { "details", true },
                    { "price", false },     //not null
                    { "stock", false },     //not null
                    { "supplier", false }}  //not null
                },
                { "empty", new Dictionary<string, bool> {
                    { "name", true },
                    { "details", true },
                    { "price", true },
                    { "stock", true },
                    { "supplier", true },
                    { "search", true }}
                }
            };

            gridUpdate();

            // A ordem é importante pro combobox
            cmbSupplier.ValueMember = "supplierId";
            cmbSupplier.DisplayMember = "name";
            cmbSupplier.DataSource = ctrlSelect.getSuppliers();
            cmbSupplier.SelectedIndex = -1;
            
            validate();
        }

        private void gridUpdate(object data = null) {
            if (data == null) data = ctrlSelect.getProducts();

            tblProducts.DataSource = data;
            // Oculta o ID e FK
            tblProducts.Columns[0].Visible = false;
            tblProducts.Columns[2].Visible = false;
            // Formato monetário
            tblProducts.Columns[4].DefaultCellStyle.Format = "c2";
        }

        private void clearFields(bool find = false) {
            ctrlProduct.clear();

            txtName.Text = string.Empty;
            txtDetails.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtStock.Text = string.Empty;

            cmbSupplier.SelectedIndex = -1;

            if (!find) txtSearch.Text = string.Empty;

            validate();
        }

        private void fillFields() {
            clearFields(true);
            txtName.Text = (string)tblProducts.CurrentRow.Cells[1].Value;
            txtPrice.Text = ((decimal)tblProducts.CurrentRow.Cells[4].Value).ToString(CultureInfo.CurrentCulture);
            txtStock.Text = ((int)tblProducts.CurrentRow.Cells[5].Value).ToString();
            txtDetails.Text = (string)tblProducts.CurrentRow.Cells[6].Value;
            cmbSupplier.SelectedValue = (int)tblProducts.CurrentRow.Cells[2].Value;
        }

        private void retrieveEntries() {
            if (tblProducts.CurrentRow != null) ctrlProduct.setId((int)tblProducts.CurrentRow.Cells[0].Value);
            ctrlProduct.setName(txtName.Text);
            ctrlProduct.setDetails(txtDetails.Text);
            ctrlProduct.setPrice(txtPrice.Text);
            ctrlProduct.setStock(txtStock.Text);
            ctrlProduct.setSupplier((int) cmbSupplier.SelectedValue);
        }

        private void validate(bool filled = false) {
            if (filled || stateEntries["valid"].ContainsValue(false)) {
                btnCreate.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else {
                btnCreate.Enabled = true;
                if (tblProducts.RowCount != 0) btnUpdate.Enabled = true;
            }

            if (stateEntries["empty"].ContainsValue(false)) btnClear.Enabled = true;

            if (filled) {
                btnDelete.Enabled = true;
            }
            else if (!stateEntries["empty"].ContainsValue(false)) {
                lblNameError.Text = string.Empty;
                lblSupplierError.Text = string.Empty;
                lblDetailsError.Text = string.Empty;
                lblPriceError.Text = string.Empty;
                lblStockError.Text = string.Empty;

                btnDelete.Enabled = false;
                btnClear.Enabled = false;
            }
        }

        private DialogResult alertMessage(string title, string message) {
            return MessageBox.Show(message + "\n\n" + (string)tblProducts.CurrentRow.Cells[1].Value,
                                   title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button2);
        }

        private void errorMessage(string title, string message) {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            retrieveEntries();
            try {
                cleanProduct.supplierId((int)cmbSupplier.SelectedValue, true);
                ctrlProduct.create();
                clearFields();
            }
            catch (Exception msg) {
                errorMessage("Erro", msg.Message);
            }
            gridUpdate();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (alertMessage("Alteração", "Tem certeza que deseja alterar este produto?") 
                == DialogResult.Yes) {

                retrieveEntries();
                try {
                    cleanProduct.productId((int)tblProducts.CurrentRow.Cells[0].Value, true);
                    ctrlProduct.update();
                    clearFields();
                }
                catch (Exception msg) {
                    errorMessage("Erro", msg.Message);
                }
                gridUpdate();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (alertMessage("Exclusão", "Tem certeza que deseja excluir este produto?") 
                == DialogResult.Yes) {

                retrieveEntries();
                try {
                    ctrlProduct.delete();
                }
                catch (Exception msg) {
                    errorMessage("Erro", msg.Message);
                }
                clearFields();
                gridUpdate();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            gridUpdate(ctrlSelect.getProducts(txtSearch.Text));
            stateEntries["empty"]["search"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void tblProducts_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (tblProducts.RowCount != 0) fillFields();
            validate(filled: true);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == ',') {
                e.Handled |= txtPrice.SelectionStart == 0;
                e.Handled |= ((TextBox) sender).Text.IndexOf(',') > -1;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
            }
            else if (((TextBox) sender).Text.IndexOf(',') > -1 &&
                     ((TextBox) sender).Text.Length > (((TextBox) sender).Text.IndexOf(',') + 2) &&
                     char.IsNumber(e.KeyChar)) {
                e.Handled |= txtPrice.SelectionStart > ((TextBox) sender).Text.IndexOf(',');
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cleanName_TextChanged(object sender, EventArgs e) {
            try {
                cleanProduct.name(txtName.Text, true);
                lblNameError.Text = string.Empty;
                stateEntries["valid"]["name"] = true;
            }
            catch (Exception msg) {
                lblNameError.Text = msg.Message;
                stateEntries["valid"]["name"] = false;
            }
            stateEntries["empty"]["name"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void cleanDetails_TextChanged(object sender, EventArgs e) {
            try {
                cleanProduct.details(txtDetails.Text, true);
                lblDetailsError.Text = string.Empty;
                stateEntries["valid"]["details"] = true;
            }
            catch (Exception msg) {
                lblDetailsError.Text = msg.Message;
                stateEntries["valid"]["details"] = false;
            }
            stateEntries["empty"]["details"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void cleanPrice_TextChanged(object sender, EventArgs e) {
            try {
                cleanProduct.price(txtPrice.Text, true);
                lblPriceError.Text = string.Empty;
                stateEntries["valid"]["price"] = true;
            }
            catch (Exception msg) {
                lblPriceError.Text = msg.Message;
                stateEntries["valid"]["price"] = false;
            }
            stateEntries["empty"]["price"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void cleanStock_TextChanged(object sender, EventArgs e) {
            try {
                cleanProduct.stock(txtStock.Text, true);
                lblStockError.Text = string.Empty;
                stateEntries["valid"]["stock"] = true;
            }
            catch (Exception msg) {
                lblStockError.Text = msg.Message;
                stateEntries["valid"]["stock"] = false;
            }
            stateEntries["empty"]["stock"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void cleanSupplier_SelectedChanged(object sender, EventArgs e) {
            try {
                cleanProduct.supplierId(cmbSupplier.SelectedIndex == -1 ?
                                            cmbSupplier.SelectedIndex :
                                            (int)cmbSupplier.SelectedValue, true);
                lblSupplierError.Text = string.Empty;
                stateEntries["valid"]["supplier"] = true;
            }
            catch (Exception msg) {
                lblSupplierError.Text = msg.Message;
                stateEntries["valid"]["supplier"] = false;
            }
            stateEntries["empty"]["supplier"] = (sender as ComboBox).SelectedIndex == -1;
            validate();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            clearFields();
        }
    }
}