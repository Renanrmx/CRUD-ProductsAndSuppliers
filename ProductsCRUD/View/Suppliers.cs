using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProductsCRUD.Controller;


namespace ProductsCRUD {
    internal partial class Suppliers : Form {
        CtrlSupplier ctrlSupplier = new CtrlSupplier();
        CleanSupplier cleanSupplier = new CleanSupplier();
        CtrlSelect ctrlSelect = new CtrlSelect();

        Dictionary<string, Dictionary<string, bool>> stateEntries;

        internal Suppliers() {
            InitializeComponent();
            
            stateEntries = new Dictionary<string, Dictionary<string, bool>> {
                { "valid", new Dictionary<string, bool> {
                    { "name", false },  //not null
                    { "email", true },
                    { "phone", true }}
                },
                { "empty", new Dictionary<string, bool> {
                    { "name", true },
                    { "email", true },
                    { "phone", true },
                    { "search", true }}
                }
            };

            gridUpdate();
            validate();
        }

        private void gridUpdate(object data = null) {
            if (data == null) data = ctrlSelect.getSuppliers();
            
            tblSuppliers.DataSource = data;
            // Oculta o ID
            tblSuppliers.Columns[0].Visible = false;
        }

        private void clearFields(bool find = false) {
            ctrlSupplier.clear();

            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;

            if (!find) txtSearch.Text = string.Empty;

            validate();
        }

        private void fillFields() {
            clearFields(true);
            txtName.Text = (string)tblSuppliers.CurrentRow.Cells[1].Value;
            txtEmail.Text = (string)tblSuppliers.CurrentRow.Cells[2].Value;
            txtPhone.Text = (string)tblSuppliers.CurrentRow.Cells[3].Value;
        }

        private void retrieveEntries() {
            if (tblSuppliers.CurrentRow != null) ctrlSupplier.setId((int)tblSuppliers.CurrentRow.Cells[0].Value);
            ctrlSupplier.setName(txtName.Text);
            ctrlSupplier.setEmail(txtEmail.Text);
            ctrlSupplier.setPhone(txtPhone.Text);
        }

        private void validate(bool filled = false) {
            if (filled || stateEntries["valid"].ContainsValue(false)) {
                btnCreate.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else {
                btnCreate.Enabled = true;
                if (tblSuppliers.RowCount != 0) btnUpdate.Enabled = true;
            }

            if (stateEntries["empty"].ContainsValue(false)) btnClear.Enabled = true;

            if (filled) {
                btnDelete.Enabled = true;
            }
            else if (!stateEntries["empty"].ContainsValue(false)) {
                lblNameError.Text = string.Empty;
                lblEmailError.Text = string.Empty;
                lblPhoneError.Text = string.Empty;

                btnDelete.Enabled = false;
                btnClear.Enabled = false;
            }
        }

        private DialogResult alertMessage(string title, string message) {
            return MessageBox.Show(message + "\n\n" + (string)tblSuppliers.CurrentRow.Cells[1].Value,
                                   title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button2);
        }

        private void errorMessage(string title, string message) {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            retrieveEntries();
            try {
                ctrlSupplier.create();
                clearFields();
            }
            catch (Exception msg) {
                errorMessage("Erro", msg.Message);
            }
            gridUpdate();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (alertMessage("Alteração", "Tem certeza que deseja alterar este fornecedor?") 
                == DialogResult.Yes) {

                retrieveEntries();
                try {
                    cleanSupplier.supplierId((int)tblSuppliers.CurrentRow.Cells[0].Value, true);
                    ctrlSupplier.update();
                    clearFields();
                }
                catch (Exception msg) {
                    errorMessage("Erro", msg.Message);
                }
                gridUpdate();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (alertMessage("Exclusão", "Tem certeza que deseja excluir este fornecedor?") 
                == DialogResult.Yes) {

                retrieveEntries();
                try {
                    ctrlSupplier.delete();
                }
                catch (Exception msg) {
                    errorMessage("Erro", msg.Message);
                }
                clearFields();
                gridUpdate();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            gridUpdate(ctrlSelect.getSuppliers(txtSearch.Text));
            stateEntries["empty"]["search"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void tblSuppliers_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (tblSuppliers.RowCount != 0) fillFields();
            validate(filled: true);
        }

        private void cleanName_TextChanged(object sender, EventArgs e) {
            try {
                cleanSupplier.name(txtName.Text, true);
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

        private void cleanEmail_TextChanged(object sender, EventArgs e) {
            try {
                cleanSupplier.email(txtEmail.Text, true);
                lblEmailError.Text = string.Empty;
                stateEntries["valid"]["email"] = true;
            }
            catch (Exception msg) {
                lblEmailError.Text = msg.Message;
                stateEntries["valid"]["email"] = false;
            }
            stateEntries["empty"]["email"] = (sender as TextBox).Text == String.Empty;
            validate();
        }

        private void cleanPhone_TextChanged(object sender, EventArgs e) {
            try {
                cleanSupplier.phone(txtPhone.Text, true);
                lblPhoneError.Text = string.Empty;
                stateEntries["valid"]["phone"] = true;
            }
            catch (Exception msg) {
                lblPhoneError.Text = msg.Message;
                stateEntries["valid"]["phone"] = false;
            }
            
            stateEntries["empty"]["phone"] = (sender as MaskedTextBox).Text == String.Empty;
            validate();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            clearFields();
        }   
    }
}