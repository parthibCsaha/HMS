using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.Commands;
using HMS.Application.Features.LabOrders.DTOs;
using HMS.Domain.Enums;

namespace HMS.Application.Features.LabOrders.Services;

public interface ILabOrderService
{
    Task<ApiResponse<PaginatedResponse<LabOrderListItemDto>>> GetLabOrdersAsync(PaginationQuery q, Guid? patientId, LabTestStatus? status, CancellationToken ct);
    Task<ApiResponse<LabOrderDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateLabOrderCommand cmd, CancellationToken ct);
    Task<ApiResponse> AddResultAsync(AddLabResultCommand cmd, CancellationToken ct);
}
