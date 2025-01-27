namespace FurnitureEmporium.Models.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; } // User who owns the cart
        public int ProductId { get; set; } // Product added to the cart
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Additional properties for display
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
    }
}
