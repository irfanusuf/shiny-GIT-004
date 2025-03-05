using System;
using WebApplication1.Models.JunctionModels;

namespace WebApplication1.Models.ViewModel;

public class HybridViewModel
{
    public List<Product> Products { get; set; } = [];
    public NavbarModel Navbar { get; set; } = new NavbarModel();
    public Product? Product {get; set;}
    public  List<CartProduct> CartProducts {get; set;} =[];
    public Cart? Cart {get; set;}

    public Address? Address {get; set;}
   
}
