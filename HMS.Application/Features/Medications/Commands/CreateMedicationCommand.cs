using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public record CreateMedicationCommand(
    string Name,
    string GenericName,
    string Category,
    string? Manufacturer,
    string? DosageForm,
    string? Strength,
    decimal UnitPrice,
    int StockQuantity,
    int ReorderLevel,
    DateTime? ExpiryDate
) : IRequest<ApiResponse<Guid>>;
