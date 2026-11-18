using System.Reflection;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Security;
using HMS.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that enforces <see cref="RequirePermissionAttribute"/>.
/// Runs before validation — no point validating a request the user can't execute.
/// </summary>
public sealed class PermissionBehavior<TRequest, TResponse>(
    ICurrentUserService currentUser,
    ILogger<PermissionBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var attribute = typeof(TRequest)
            .GetCustomAttribute<RequirePermissionAttribute>();

        if (attribute is null)
            return await next();

        var permission = attribute.Permission;

        if (!currentUser.IsAuthenticated)
        {
            logger.LogWarning(
                "Unauthenticated request to {Request} requiring permission {Permission}",
                typeof(TRequest).Name, permission);
            throw new UnauthorizedException();
        }

        var roleString = currentUser.Role;

        if (string.IsNullOrEmpty(roleString) ||
            !Enum.TryParse<UserRole>(roleString, ignoreCase: true, out var role))
        {
            logger.LogWarning(
                "User {UserId} has invalid or missing role '{Role}' — denied {Permission}",
                currentUser.UserId, roleString, permission);
            throw new ForbiddenException(
                $"Your role is not recognized. Required permission: {permission}.");
        }

        if (!RolePermissionMapping.HasPermission(role, permission))
        {
            logger.LogWarning(
                "User {UserId} with role {Role} denied permission {Permission} on {Request}",
                currentUser.UserId, role, permission, typeof(TRequest).Name);
            throw new ForbiddenException(
                $"You do not have the required permission: {permission}.");
        }

        logger.LogDebug(
            "User {UserId} ({Role}) granted {Permission}",
            currentUser.UserId, role, permission);

        return await next();
    }
}

