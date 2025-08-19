using System.ComponentModel.DataAnnotations;
using Nucleus.Api;

namespace Auth.Dtos;

public record UserLoginRequestDto
{

    [Required(ErrorMessage = "Please provide an email address.")]
    [ValidateEmail(ErrorMessage = "The email address you entered is not valid.")]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Please provide your password.")]
    public string? Password { get; init; }
}