namespace Auth.Models;

public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RoleName { get; set; }
    public int RoleId { get; set; }
    public long StaffId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public long CompanyId { get; set; }
}
