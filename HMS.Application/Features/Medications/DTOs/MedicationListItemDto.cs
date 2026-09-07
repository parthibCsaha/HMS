namespace HMS.Application.Features.Medications.DTOs;

public record MedicationListItemDto(Guid Id, string Name, string GenericName, string Category,
    decimal UnitPrice, int StockQuantity, int ReorderLevel, bool IsActive);
