using System;
using MongoDB.Bson;

namespace WebApplication1.Interfaces;

public interface ITokenService
{
 string CreateToken(ObjectId userId, string email, string username);


 ObjectId VerifyTokenAndGetId(string token);
   
}