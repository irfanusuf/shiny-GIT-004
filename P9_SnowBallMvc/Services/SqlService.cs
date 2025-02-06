using System;
using Microsoft.Data.SqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class SqlService : ISqlService
{
    private readonly string _connectionString;
    public SqlService(IConfiguration configuration)
    {
        _connectionString = configuration["Sql:connectionString"] ?? throw new ArgumentNullException("conectionString  is not configured.");
    }

    public string CreateUser(User user)
    {
        string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";

        using SqlConnection connection = new(_connectionString);    // connecting with sql 

        try
        {
            connection.Open();
            using SqlCommand command = new(query, connection);

            // append data // pairing 
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("User saved successfully!");
                return "done";
            }
            else
            {
                Console.WriteLine("Failed to save the user.");
                return "failure";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred:" + ex.Message);
            return "failure";
        }
    }

    public User FindUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User?  FindUserById(int id)
  {
        string query = "SELECT Username, Email, Password FROM Users WHERE Id = @Id";

        using SqlConnection connection = new(_connectionString);
        try
        {
            connection.Open();

            using SqlCommand command = new(query, connection);


            command.Parameters.AddWithValue("@Id", id);


            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }
            else
            {
                Console.WriteLine("No user found with the provided ID.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    public bool DeleteUserById(int id)
    {
        string query = "SELECT Username, Email, Password FROM Users WHERE Id = @Id";
        string delQuery = "DELETE FROM Users WHERE Id = @Id";
        using SqlConnection connection = new(_connectionString);
        try
        {
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                SqlCommand command2 = new(delQuery, connection);
                int rowsAffected = command2.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("User Deleted Succesfully!");
                }
                return true;
            }
            else
            {
                Console.WriteLine("No user found with the provided ID.");
                return false;
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }



    }


    public User UpdateUserById(int id, User user)
    {
        throw new NotImplementedException();
    }

 
}




