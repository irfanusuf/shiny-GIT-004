
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Bio { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
        // we took collection of Orders because user can have many order
        public ICollection<Order> Orders { get; set; } = [];
    }
}