using System;

namespace WebApplication1.Interfaces;

public interface ITokenService 
{

public void CreateToken(string userId , string username , string email);


}
