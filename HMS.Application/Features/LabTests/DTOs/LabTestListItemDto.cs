namespace HMS.Application.Features.LabTests.DTOs;

public record LabTestListItemDto(Guid Id, string Code, string Name, string Category, decimal Price, bool IsActive);
