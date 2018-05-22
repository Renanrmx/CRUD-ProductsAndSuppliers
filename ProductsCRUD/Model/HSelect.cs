using System.Data.Entity;
using System.Linq;


namespace ProductsCRUD.Model {
    public partial class HandleDB {

        protected object readSuppliers(string entry = null) {
            using (var db = new BaseContext()) {
                if (entry is null) {
                    return db.suppliers.AsNoTracking().ToList();
                }
                else {
                    return db.suppliers.Where(x => x.name.Contains(entry) ||
                                                   x.email.Contains(entry) ||
                                                   x.phone.Contains(entry)).AsNoTracking().ToList();
                }
            }
        }

        protected object readProducts(string entry = null) {
            using (var db = new BaseContext()) {
                var query = (from prod in db.products
                             join supp in db.suppliers
                                 on prod.supplierId equals supp.supplierId
                             select new {
                                 ID = prod.productId,
                                 Produto = prod.name,
                                 FK = prod.supplierId,
                                 Fornecedor = supp.name,
                                 Preço = prod.price,
                                 Estoque = prod.stock,
                                 Detalhes = prod.details
                             }).AsNoTracking();

                if (entry != null) {
                    query = query.Where(x => x.Produto.Contains(entry) ||
                                             x.Preço.ToString().Contains(entry) ||
                                             x.Estoque.ToString().Contains(entry) ||
                                             x.Detalhes.Contains(entry) ||
                                             x.Fornecedor.Contains(entry)).AsNoTracking();
                }

                return query.ToList();
            }
        }

        protected object readAll(string entry = null, string filter = null) {
            using (var db = new BaseContext()) {
                var query = (from prod in db.products
                             join supp in db.suppliers
                                 on prod.supplierId equals supp.supplierId
                             orderby prod.name
                             select new {
                                 Produto = prod.name,
                                 Preço = prod.price,
                                 Estoque = prod.stock,
                                 Detalhes = prod.details,
                                 Fornecedor = supp.name,
                                 Email = supp.email,
                                 Telefone = supp.phone,
                             }).AsNoTracking();

                if (entry != null) {
                    // O where não aceita collections então foram usadas variáveis simples
                    string all = filter == "all" ? entry : null;

                    string product = filter == "product" ? entry : all;
                    string price = filter == "price" ? entry : all;
                    string stock = filter == "stock" ? entry : all;
                    string details = filter == "details" ? entry : all;
                    string supplier = filter == "supplier" ? entry : all;
                    string email = filter == "email" ? entry : all;
                    string phone = filter == "phone" ? entry : all;

                    query = query.Where(x => x.Produto.Contains(product) ||
                                             x.Preço.ToString().Contains(price) ||
                                             x.Estoque.ToString().Contains(stock) ||
                                             x.Detalhes.Contains(details) ||
                                             x.Fornecedor.Contains(supplier) ||
                                             x.Email.Contains(email) ||
                                             x.Telefone.Contains(phone)).AsNoTracking();
                }

                return query.ToList();
            }
        }
    }
}
