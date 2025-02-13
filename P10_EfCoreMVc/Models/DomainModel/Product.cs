using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.JunctionModels;

namespace WebApplication1.Models;

public class Product
{       
        [Key]
        public Guid  ProductId { get; set; }  = Guid.NewGuid();
        public required string ProductTitle { get; set; }
        public required string ProductPicURl { get; set; }


        public required Guid SellerId { get; set; }

        [ForeignKey("SellerId")]
        public User? Seller { get; set; }     
        // public required Guid CartID { get; set; }

        // [ForeignKey("CartID")]
        // public Cart? Cart { get; set; }     



        public ICollection<CartProduct> Carts { get; set; } = [];



        
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }  

}
