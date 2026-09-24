namespace HMS.Application.Features.Beds.DTOs;

public record BedListItemDto(
    Guid Id,
    string BedNumber,
    string WardName,
    string Status,
    string? CurrentPatientName
);
