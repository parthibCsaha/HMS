using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.Medications.DTOs;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

[RequirePermission(Permissions.MedicationsViewLowStock)]
public record GetLowStockMedicationsQuery
    : IRequest<ApiResponse<IEnumerable<MedicationListItemDto>>>;
