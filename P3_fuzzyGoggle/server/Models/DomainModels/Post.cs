using System;
using System.Collections.Generic;
using MongoDB.Bson;
using WebApplication1.Types;

namespace WebApplication1.Models.DomainModels;

public class Post
{
    public ObjectId Id { get; set; }
    public string? PostPicUrl { get; set; }
    public string? PostVidUrl { get; set; }
    public string? Caption { get; set; }
    public ObjectId UserId {get; set; }  // relationship to user
    public List<Comment> Comments { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public Visibilty Visibility { get; set; } = Visibilty.Public; // Default visibility
    public string? Location { get; set; } // Geotagging
    public Dictionary<string, int> Reactions { get; set; } = [];
    public bool IsEdited { get; set; } = false; // Track if the post is edited
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}