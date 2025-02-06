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

      // we took userId as fk because an order can have single user 
        public required Guid UserId { get; set; }

        // fetch data from user     .... and User Model Will be the blue print of that data

        [ForeignKey("UserId")]    // information passed to efCore

        public User? User { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}