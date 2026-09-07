namespace HMS.Application.Features.SystemSettings.DTOs;

public record SystemSettingDto(Guid Id, string Key, string Value, string? Category, string? Description);
