using System;
using ProductsCRUD.Model;


namespace ProductsCRUD.Controller {
    class CleanProduct {

        public int? productId(int value, bool ex = false) {
            HandleDB db = new HandleDB();
            
            if (db.checkProduct(value)) {
                return value;
            }
            else if (ex) {
                throw new Exception("O produto escolhido não existe mais");
            }
            return null;
        }

        public string name(string value, bool ex = false) {
            if (value.Length > 0 && value.Length <= 38) {
                return value;
            }
            else if (ex) {
                if (value.Length > 38) throw new Exception("Nome muito longo");
                else throw new Exception("Digite o nome do produto");
            }
            return null;
        }

        public string details(string value, bool ex = false) {
            if (value.Length > 100) {
                if (ex) throw new Exception("Extenso demais");
            }
            else if (value.Length > 0) {
                return value;
            }
            return null;
        }

        public decimal? price(string value, bool ex = false) {
            if (value.Length == 0 && ex) {
                throw new Exception("Insira o valor");
            }
            else if (decimal.TryParse(value, out decimal num)) {
                if (num > 0) {
                    return num;
                }
                else if (ex) {
                    throw new Exception("Valor inválido");
                }
            }
            else if (ex) {
                throw new Exception("Deve ser numérico");
            }
            return null;
        }

        public int? stock(string value, bool ex = false) {
            if (value.Length == 0 && ex) {
                if (ex) throw new Exception("Insira a quantidade");
            }
            else if (int.TryParse(value, out int num)) {
                if (num >= 0) {
                    return num;
                }
                else if (ex) {
                    throw new Exception("Quantidade inválida");
                }
            }
            else if (ex) {
                if (long.TryParse(value, out long num64)) throw new Exception("Quantidade acima do limite");
                else throw new Exception("Deve ser numérico");
            }
            return null;
        }
        
        public int? supplierId(int value, bool ex = false) {
            HandleDB db = new HandleDB();
            
            if (value <= 0 && ex) {
                throw new Exception("Selecione o fornecedor");
            }
            else if (db.checkSupplier(value)) {
                return value;
            }
            else if (ex) {
                throw new Exception("O fornecedor escolhido não existe mais");
            }
            return null;
        }
    }
}
