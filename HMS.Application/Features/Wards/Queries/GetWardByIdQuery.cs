using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public record GetWardByIdQuery(Guid Id) : IRequest<ApiResponse<WardDetailDto>>;
