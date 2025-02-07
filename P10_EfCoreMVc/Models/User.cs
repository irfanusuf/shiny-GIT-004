
using System.ComponentModel.DataAnnotations;
using WebApplication1.Types;


namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Role Role { get; set; }
        public string? ProfilePictureUrl { get; set; } 
        

        public Cart? Cart { get; set; }      //Cart object belongs to user ///// one to one relation  

        // we took collection of Orders, Products and Addresses because user can have many orders , products for selling and 

        // many Addreseslike for office or home for  proper business management

        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Address> Addresses { get; set; } = [];

        // Instead of Icollections We can also use list which handles dynamic data well 




        public DateTime DateCreated { get; set; }  = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}