
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication1.Models.DomainModel;
using WebApplication1.Types;


namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }  = Guid.NewGuid();
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; set; } = Role.Buyer;
        public string? ProfilePictureUrl { get; set; } = "dummy Url";

        // public bool IsSellerReqAccepted  {get; set;} = false;
        

        // One-to-one relationship with Cart
        public Cart? Cart { get; set; }

        // we took collection of Orders, Products and Addresses because user can have many orders , products for selling and 

        // one to many 
 
        public ICollection<Order> Orders { get; set; } = [];
        
        public ICollection<Product> Products { get; set; } = [];
          // many Addrese like for office or home for  proper business management
        public ICollection<Address> Addresses { get; set; } = [];

        public ICollection<Review> Reviews { get; set; } = [];

        // Instead of Icollections We can also use list which handles dynamic data well 




        

        public DateTime DateCreated { get; set; }  = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
    }
}
