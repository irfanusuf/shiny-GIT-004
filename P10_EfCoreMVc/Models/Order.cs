using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public required string OrderStatus { get; set; }
        public required decimal OrderPrice { get; set; }

      // we took BuyerId as fk because an order can have single user 
        public required Guid BuyerId { get; set; }

        [ForeignKey("BuyerId")]    // information passed to efCore


         // fetch data from user     .... and User Model Will be the blue print of that data
        public User? Buyer { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}