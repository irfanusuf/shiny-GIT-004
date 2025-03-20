using System;
using Razorpay.Api;
using WebApplication1.Models.JunctionModels;

namespace WebApplication1.Models.ViewModel;

public class HybridViewModel
{
    public NavbarModel Navbar { get; set; } = new NavbarModel();
    public Address? Address {get; set;}
    public Product? Product {get; set;}
    public List<Product> Products { get; set; } = [];
    public Cart? Cart {get; set;}
    public  List<CartProduct> CartProducts {get; set;} =[];
    public Order? Order {get; set;}
    public  List<OrderProduct> OrderProducts {get; set;} =[];
    
}
