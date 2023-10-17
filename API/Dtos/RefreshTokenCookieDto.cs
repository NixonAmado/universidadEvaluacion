using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RefreshTokenCookieDto
{
    [Required]
    public string RefreshToken { get; set; }
}