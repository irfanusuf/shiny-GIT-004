using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Cart
{

    [Key]
    public Guid CartId { get; set; }

    public Guid BuyerId { get; set; } // Every Cart belongs to a specific User

    [ForeignKey("BuyerId")]
    public User? Buyer { get; set; }  


    public ICollection<Product> Products { get; set; } = [];

    public int Qty { get; set; }

    public int CartValue { get; set; }


}
