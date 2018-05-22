using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProductsCRUD.Model {
    public class Product {

        [DisplayName("ID")]
        public int? productId { get; set; }

        [DisplayName("Produto")]
        [Required]
        public string name { get; set; }

        [DisplayName("Detalhes")]
        public string details { get; set; }

        [DisplayName("Preço")]
        [Required]
        public decimal? price { get; set; }

        [DisplayName("Estoque")]
        [Required]
        public int? stock { get; set; }

        [DisplayName("FK Fornecedor")]
        [Required]
        public int? supplierId { get; set; }

        public virtual Supplier supplier { get; set; }
    }
}