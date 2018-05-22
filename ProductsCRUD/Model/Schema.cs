using System;
using System.Data.Entity;


namespace ProductsCRUD.Model {
    public class BaseContext : DbContext {

        public DbSet<Supplier> suppliers { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet autoContext(object entity) {
            if (entity is Supplier) {
                return suppliers;
            }
            else if (entity is Product) {
                return products;
            }

            throw new Exception("Entidade inválida");
        }
    }
}
