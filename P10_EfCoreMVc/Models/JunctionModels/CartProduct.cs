using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.JunctionModels;

 public class CartProduct
    {
        [Key]
        public Guid CartProductId { get; set; } = Guid.NewGuid();

        public Guid CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }




        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }




        public int Quantity { get; set; }  // Quantity of the product in the cart    // i changed this specifically
    }
