
using MongoDB.Bson;
using WebApplication1.Types;



namespace WebApplication1.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public required string Username { get; set; }

        // [EmailAddress (ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Role Role { get; set; } = Role.User;
        public string? ProfilePictureUrl { get; set; }
        public string? Phone { get; set; }

        public List<ObjectId> Posts {get;set;} =[];
        public List<ObjectId> Followers {get;set;} =[];
        public List<ObjectId> Following {get;set;} =[];

        public DateTime DateCreated { get; set; } =  DateTime.UtcNow;
        public DateTime? DateModified { get; set; } =  DateTime.UtcNow;


    }
}