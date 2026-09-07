using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IUserRepository userRepository, ICurrentUserService currentUser)
    : IRequestHandler<GetCurrentUserQuery, ApiResponse<CurrentUserDto>>
{
    public async Task<ApiResponse<CurrentUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId
            ?? throw new UnauthorizedException();

        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User", userId);

        return ApiResponse<CurrentUserDto>.Success(
            new CurrentUserDto(
                    user.Id, 
                    user.FirstName, 
                    user.LastName, 
                    user.Email,
                    user.Phone, 
                    user.Role.ToString(), 
                    user.ProfileImageUrl, 
                    user.LastLoginAt
                )
            );
    }
}
