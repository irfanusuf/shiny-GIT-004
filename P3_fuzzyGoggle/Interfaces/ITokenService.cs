using System;
using MongoDB.Bson;

namespace WebApplication1.Interfaces;

public interface ITokenService
{
 string CreateToken(string userId, string email, string username);

 ObjectId VerifyTokenAndGetId(string token);
}
