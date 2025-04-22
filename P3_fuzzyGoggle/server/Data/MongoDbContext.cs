using System;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;
using WebApplication1.Models.DomainModels;

namespace WebApplication1.Data;

public class MongoDbContext
{

    private readonly IMongoDatabase database;    // inheritance 
                                                  // function which takes connection string from config and establishes a connection with mongodb server 


    public MongoDbContext(IConfiguration configuration)
    {

        var connectionString = configuration["MongoDB:ConnectionString"];  // importing connection strting 
        var databaseName = configuration["MongoDB:DatabaseName"];     //importing data base name from appsettings

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        this.database = database;

    }


    public IMongoCollection<User> Users => database.GetCollection<User>("Users");
    public IMongoCollection<Post> Posts => database.GetCollection<Post>("Posts");
    public IMongoCollection<Comment> Comments => database.GetCollection<Comment>("Commnets");





}



