using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public class User
  {
    // Assign to actual scheme from token
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
  }
}
