using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public record UpdateWardCommand(
    Guid Id,
    string Name,
    string? Description,
    string? Location,
    decimal ChargePerDay,
    bool IsActive
) : IRequest<ApiResponse>;
