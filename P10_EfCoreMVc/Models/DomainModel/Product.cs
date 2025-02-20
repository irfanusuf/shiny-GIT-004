using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.JunctionModels;

namespace WebApplication1.Models;

public class Product
{
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public required string ProductTitle { get; set; }
        public required string Description { get; set; }
        public required string ProductPicURl { get; set; }
        public required int Qty { get; set; }
        public required int Price { get; set; }
        public required string Category { get; set; }
        public required string SubCategory { get; set; }
        public required string HashTags { get; set; }
        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public required Guid SellerId { get; set; }

        [ForeignKey("SellerId")]
        public User? Seller { get; set; }

        public ICollection<CartProduct> Carts { get; set; } = [];


        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;

}
