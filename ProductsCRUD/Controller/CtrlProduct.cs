using ProductsCRUD.Model;


namespace ProductsCRUD.Controller {
    public class CtrlProduct : HandleDB {
        Product product = new Product();
        CleanProduct clean = new CleanProduct();

        public void setId(int id) {
            product.productId = clean.productId(id);
        }

        public void setName(string name) {
            product.name = clean.name(name);
        }

        public void setDetails(string details) {
            product.details = clean.details(details);
        }

        public void setPrice(string price) {
            product.price = clean.price(price);
        }

        public void setStock(string stock) {
            product.stock = clean.stock(stock);
        }

        public void setSupplier(int fk) {
            product.supplierId = clean.supplierId(fk);
        }

        public void clear() {
            product.productId = null;
            product.name = null;
            product.details = null;
            product.price = null;
            product.stock = null;
            product.supplierId = null;
        }
        
        public void create() {
            create(product);
        }

        public void update() {
            update(product, product.productId);
        }
        
        public void delete() {
            delete(product);
        }
    }
}
