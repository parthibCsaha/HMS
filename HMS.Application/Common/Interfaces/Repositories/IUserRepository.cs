using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    Task UpdateRefreshTokenAsync(
        Guid userId,
        string? refreshToken,
        DateTime? expiry,
        CancellationToken ct = default
    );
    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
}
