using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public record GetStaffByIdQuery(Guid Id) : IRequest<ApiResponse<StaffDetailDto>>;
