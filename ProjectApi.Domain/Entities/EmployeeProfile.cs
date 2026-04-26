using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 04: Employee Profile Form
/// Profile image stored as Base64 string
/// </summary>
public class EmployeeProfile : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ProfileBase64 { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
    public string Occupation { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty; // Male / Female
}
