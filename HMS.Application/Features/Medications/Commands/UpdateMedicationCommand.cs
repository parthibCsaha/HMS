using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

[RequirePermission(Permissions.MedicationsUpdate)]
public record UpdateMedicationCommand(
    Guid Id,
    string Name,
    string GenericName,
    string Category,
    decimal UnitPrice,
    int StockQuantity,
    int ReorderLevel,
    bool IsActive
) : IRequest<ApiResponse>;
