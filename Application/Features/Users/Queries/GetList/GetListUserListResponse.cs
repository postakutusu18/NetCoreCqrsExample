using Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetList;
public class GetListUserListResponse : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }

    public GetListUserListResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public GetListUserListResponse(Guid id, string firstName, string lastName, string email, bool status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
    }
}
