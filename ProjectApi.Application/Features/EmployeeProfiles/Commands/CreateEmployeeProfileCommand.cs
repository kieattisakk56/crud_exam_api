using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.EmployeeProfiles.Commands;

public class CreateEmployeeProfileCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ProfileBase64 { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
    public string Occupation { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
}

public class CreateEmployeeProfileCommandHandler : IRequestHandler<CreateEmployeeProfileCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateEmployeeProfileCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateEmployeeProfileCommand request, CancellationToken ct)
    {
        var profile = new EmployeeProfile
        {
            FirstName = request.FirstName, LastName = request.LastName,
            Email = request.Email, Phone = request.Phone,
            ProfileBase64 = request.ProfileBase64, BirthDay = request.BirthDay,
            Occupation = request.Occupation, Sex = request.Sex
        };
        await _uow.EmployeeProfiles.AddAsync(profile, ct);
        await _uow.SaveChangesAsync(ct);
        return profile.Id;
    }
}
