
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; } = string.Empty;
 
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required] 
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be between 4 and 8 characters, contain at least one number, one uppercase and one lowercase letter.")]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;
}