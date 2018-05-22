using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProductsCRUD.Model {
    public class Supplier {

        [DisplayName("ID")]
        public int? supplierId { get; set; }

        [DisplayName("Fornecedor")]
        [Required]
        public string name { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Telefone")]
        public string phone { get; set; }

        public virtual List<Product> products { get; set; }
    }
}