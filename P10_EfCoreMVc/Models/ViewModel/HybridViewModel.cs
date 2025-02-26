using System;

namespace WebApplication1.Models.ViewModel;

public class HybridViewModel
{
    public List<Product> Products { get; set; } = [];
    public NavbarModel Navbar { get; set; } = new NavbarModel();
    // public ErrorViewModel Error { get; set; } = new ErrorViewModel();
}
