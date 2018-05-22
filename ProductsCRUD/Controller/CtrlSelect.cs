using ProductsCRUD.Model;


namespace ProductsCRUD.Controller {
    public class CtrlSelect : HandleDB {

        public object getSuppliers(string entry = null) {
            return readSuppliers(entry);
        }

        public object getProducts(string entry = null) {
            return readProducts(entry);
        }

        public object getAll(string entry = null, string filter = null) {
            return readAll(entry, filter);
        }
    }
}
