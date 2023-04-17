using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Catalog.UserService.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}