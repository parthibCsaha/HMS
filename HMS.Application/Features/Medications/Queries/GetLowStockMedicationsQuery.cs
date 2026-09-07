using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public record GetLowStockMedicationsQuery : IRequest<ApiResponse<IEnumerable<MedicationListItemDto>>>;
