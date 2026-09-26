using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
