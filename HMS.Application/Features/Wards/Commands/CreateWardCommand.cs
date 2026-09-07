using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public record CreateWardCommand(string Name, string WardNumber, string WardType, Guid DepartmentId,
    int TotalBeds, string? Description, decimal ChargePerDay, string? Location) : IRequest<ApiResponse<Guid>>;
