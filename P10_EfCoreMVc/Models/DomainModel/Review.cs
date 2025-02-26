using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models.DomainModel;

public class Review
{

    [Key]
    public Guid ReviewId { get; set; } = Guid.NewGuid();
    public required string ReviewText { get; set; }
    public required int Rating { get; set; }

    [ForeignKey("ProductId")]
    public required Guid ProductId { get; set; }
    public Product? Product {get ; set;}



    [ForeignKey("UserId")]
    public required Guid UserId { get; set; }

    public User? User {get; set;}
}