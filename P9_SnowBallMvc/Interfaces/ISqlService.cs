using System;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface ISqlService
{
    string CreateUser(User user);
    public User FindUserByEmail(string email);   // implemntation of this will be in the child class 
    public User? FindUserById(int id);   // implemntation of this will be in the child class 
    public bool DeleteUserById(int id);
    public User UpdateUserById(int id, User user);

}
