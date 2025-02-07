using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Product
{
        public int  ProductId { get; set; }
        public required string ProductTitle { get; set; }
        public required string ProductPicURl { get; set; }
   
        public required Guid SellerId { get; set; }

        [ForeignKey("SellerId")]
        public User? Seller { get; set; }     

    
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }  

}
