using System.ComponentModel.DataAnnotations;

namespace Product.Data.Entity
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }

        [StringLength(200), Required]
        public string Title { get; set; }
        public string Review { get; set; }
    }
}