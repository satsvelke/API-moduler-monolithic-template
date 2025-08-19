namespace Nucleus.Models;

public class JwtClaims
{
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? RoleName { get; set; }
    public int RoleId { get; set; }
    public string? StaffId { get; set; }
    public long UserId { get; set; }
    public long CompanyId { get; set; }
    public JwtSettings? JwtSettings { get; set; }
}
