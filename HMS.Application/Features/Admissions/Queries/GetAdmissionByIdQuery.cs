using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public record GetAdmissionByIdQuery(Guid Id) : IRequest<ApiResponse<AdmissionDetailDto>>;
