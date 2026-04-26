using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.Persons.Queries;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public int Age => DateTime.Today.Year - DateOfBirth.Year; // Simple calculation as requested
}

public class GetPersonsQuery : IRequest<List<PersonDto>>
{
}

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, List<PersonDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPersonsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PersonDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await Task.FromResult(_unitOfWork.Persons.GetQueryable().ToList());
        
        return persons.Select(p => new PersonDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Address = p.Address
        }).ToList();
    }
}
