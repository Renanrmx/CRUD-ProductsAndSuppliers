using System;
using System.Linq;
using System.Data.Entity;


namespace ProductsCRUD.Model {
    public partial class HandleDB {

        protected void save(BaseContext entry) {
            if (entry.ChangeTracker.HasChanges()) {
                try {
                    entry.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException) {
                    if (entry.ChangeTracker.Entries().First().State is EntityState.Deleted) {
                        throw new Exception("O item a ser deletado possivelmente não existe mais");
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException) {
                    throw new Exception("Um ou mais valores fornecidos são inválidos");
                }
                catch (Exception ex) {
                    throw new Exception("Ocorreu um erro inesperado \n\n" + ex);
                }
            }
            else {
                throw new Exception("Não foram feitas mudanças");
            }
        }

        protected void create(object entiry) {
            using (var db = new BaseContext()) {
                db.autoContext(entiry).Add(entiry);
                save(db);
            }
        }

        protected void update(object supplier, int? id) {
            using (var db = new BaseContext()) {
                var entity = db.autoContext(supplier).Find(id);
                if (entity != null) {
                    db.Entry(entity).CurrentValues.SetValues(supplier);
                    save(db);
                }
            }
        }

        protected void delete(object supplier) {
            using (var db = new BaseContext()) {
                db.Entry(supplier).State = EntityState.Deleted;
                save(db);
            }
        }

        internal bool checkSupplier(int id) {
            using (var db = new BaseContext()) {
                return db.suppliers.Find(id) != null;
            }
        }

        internal bool checkProduct(int id) {
            using (var db = new BaseContext()) {
                return db.products.Find(id) != null;
            }
        }
    }
}
