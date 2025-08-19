namespace Nucleus.Dtos;

public record ActiveUserDto
{
    public string? ActiveFirstName { get; set; }
    public string? ActiveLastName { get; set; }
    public string? ActiveUserId { get; set; }
    public string? ActiveEmail { get; set; }
    public string? ActiveRoleName { get; set; }
    public long ActiveRoleId { get; set; }
    public string? ActiveStaffId { get; set; }
    public long ActiveCompanyId { get; set; }
}