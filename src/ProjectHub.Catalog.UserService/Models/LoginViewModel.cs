using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Catalog.UserService.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}