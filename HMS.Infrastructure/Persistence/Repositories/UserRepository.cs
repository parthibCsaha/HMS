using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, ct);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
    {
        return await Context.Users.AnyAsync(u => u.Email == email && !u.IsDeleted, ct);
    }

    public async Task UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry, CancellationToken ct = default)
    {
        var user = await GetByIdAsync(userId, ct);
        if (user is null) return;
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = expiry;
        Context.Users.Update(user);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        return await Context.Users.FirstOrDefaultAsync(
            u => u.RefreshToken == refreshToken && !u.IsDeleted, ct);
    }
}
