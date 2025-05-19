using System;
using MongoDB.Bson;

namespace WebApplication1.Models.DomainModels;

public class Comment
{

    public ObjectId Id { get; set; } // Unique identifier for the comment
    public ObjectId PostId { get; set; } // Reference to the post this comment belongs to
    public ObjectId UserId { get; set; } // Reference to the user who made the comment
    public string? Content { get; set; } // The text content of the comment
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the comment was created
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the comment was last updated
    public List<ObjectId> Likes { get; set; } = []; // List of user IDs who liked the comment



}
