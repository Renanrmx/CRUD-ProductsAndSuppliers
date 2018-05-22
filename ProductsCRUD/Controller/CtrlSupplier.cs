using ProductsCRUD.Model;


namespace ProductsCRUD.Controller {
    public class CtrlSupplier : HandleDB {
        Supplier supplier = new Supplier();
        CleanSupplier clean = new CleanSupplier();

        public void setId(int id) {
            supplier.supplierId = clean.supplierId(id);
        }

        public void setName(string name) {
            supplier.name = clean.name(name);
        }

        public void setEmail(string email) {
            supplier.email = clean.email(email);
        }

        public void setPhone(string phone) {
            supplier.phone = clean.phone(phone);
        }

        public void clear() {
            supplier.supplierId = null;
            supplier.name = null;
            supplier.email = null;
            supplier.phone = null;
        }

        public void create() {
            create(supplier);
        }

        public void update() {
            update(supplier, supplier.supplierId);
        }

        public void delete() {
            delete(supplier);
        }
    }
}
