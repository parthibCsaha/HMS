using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<ApiResponse<CurrentUserDto>>;

public record CurrentUserDto
    (
        Guid Id, 
        string FirstName, 
        string LastName, 
        string Email,
        string Phone, 
        string Role, 
        string? ProfileImageUrl, 
        DateTime? LastLoginAt
    );
