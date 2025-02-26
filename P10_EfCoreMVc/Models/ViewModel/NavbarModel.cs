using System;
using WebApplication1.Types;

namespace WebApplication1.Models.ViewModel;

public class NavbarModel
{
    public Role UserRole { get; set; }
    public  bool IsLoggedin {get ; set;} 

}
