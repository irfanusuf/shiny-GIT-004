using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.JunctionModels;
using WebApplication1.Types;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }  = Guid.NewGuid();
        public required Status OrderStatus { get; set; } = Status.Pending;
        public required int OrderPrice { get; set; }
        // we took BuyerId as fk because an order belongs to single user 
        public required Guid BuyerId { get; set; }
  
        [ForeignKey("BuyerId")]    // information passed to efCore
         // fetch data from user     .... and User Model Will be the blue print of that dat
         // one to many
        public User? Buyer { get; set; }
        public ICollection<OrderProduct> OrderProducts {get; set;} = [];
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }

        
    }
}