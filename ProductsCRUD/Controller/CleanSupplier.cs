using System;
using System.Text.RegularExpressions;
using ProductsCRUD.Model;


namespace ProductsCRUD.Controller {
    class CleanSupplier {

        public int? supplierId(int value, bool ex = false) {
            HandleDB db = new HandleDB();

            if (db.checkSupplier(value)) {
                return value;
            }
            else if (ex) {
                throw new Exception("O fornecedor escolhido não existe mais");
            }
            return null;
        }

        public string name(string value, bool ex = false) {
            if (value.Length > 0 && value.Length <= 38) {
                return value;
            }
            else if (ex) {
                if (value.Length > 38) throw new Exception("Nome muito longo");
                else throw new Exception("Digite o nome do fornecedor");
            }
            return null;
        }

        public string email(string value, bool ex = false) {
            if (value.Length > 0) {
                try {
                    var address = new System.Net.Mail.MailAddress(value);
                    return value;
                }
                catch {
                    if (ex) throw new Exception("Email em formato incorreto");
                }
            }
            return null;
        }

        public string phone(string value, bool ex = false) {
            string num = Regex.Replace(value, "[^0-9]", "");
            
            if (num.Length == 10 || num.Length == 11) {
                return num;
            }
            else if (ex && num.Length != 0) throw new Exception("Numero incompleto");
            else return null;
        }       
    }
}
