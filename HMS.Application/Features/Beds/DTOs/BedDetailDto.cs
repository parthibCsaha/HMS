namespace HMS.Application.Features.Beds.DTOs;

public record BedDetailDto(Guid Id, string BedNumber, Guid WardId, string WardName, string Status,
    Guid? CurrentPatientId, string? CurrentPatientName, DateTime? OccupiedAt, string? Notes, DateTime CreatedAt);
