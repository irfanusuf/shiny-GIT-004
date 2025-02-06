using System;

namespace WebApplication1.Models;

public class Product
{
        public int Id { get; set; }
        public required string ProductTitle { get; set; }
        public required string ProductPicURl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public User? Seller {get ; set;}

}
