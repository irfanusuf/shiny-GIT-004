using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Login
{
  // [Required(ErrorMessage = "Email is required")]
  // [EmailAddress(ErrorMessage = "Invalid email address")]
  public required string Email { get; set; }


  // [Required(ErrorMessage = "Password is required")]
  public required string Password { get; set; }

}
