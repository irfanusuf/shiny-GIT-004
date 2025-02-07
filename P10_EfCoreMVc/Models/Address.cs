using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Address
{
    [Key]
    public Guid AddressId { get; set; }
    public required string Street1 { get; set; }
    public required string Street2 { get; set; }
    public required string District { get; set; }
    public required string State { get; set; }
    public required string Pincode { get; set; }
    public required string Phone { get; set; }
    public required string Landmark { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public required Guid BuyerId { get; set; }

    [ForeignKey("BuyerId")]
    public User? Buyer { get; set; }


}
