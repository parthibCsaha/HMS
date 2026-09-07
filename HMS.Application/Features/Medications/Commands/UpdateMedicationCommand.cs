using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public record UpdateMedicationCommand(Guid Id, string Name, string GenericName, string Category,
    decimal UnitPrice, int StockQuantity, int ReorderLevel, bool IsActive) : IRequest<ApiResponse>;
