namespace HMS.Application.Features.AuditLogs.DTOs;

public record AuditLogDto(
    Guid Id,
    Guid? UserId,
    string Action,
    string EntityName,
    Guid? EntityId,
    string? OldValues,
    string? NewValues,
    string? IpAddress,
    DateTime LoggedAt
);
