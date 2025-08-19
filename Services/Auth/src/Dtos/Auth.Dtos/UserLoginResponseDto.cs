namespace Auth.Dtos;

public record UserLoginResponseDto
{
    public string? AccessToken { get; set; }
    public UserLoginDetailsResponseDto? UserDetails { get; set; }
}

public record UserLoginDetailsResponseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RoleName { get; set; }
    public string? RoleId { get; set; }
    public string? StaffId { get; set; }
    public string? CompanyId { get; set; }
}
