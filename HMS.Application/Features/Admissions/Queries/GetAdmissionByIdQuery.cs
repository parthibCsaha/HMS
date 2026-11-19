using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.Admissions.DTOs;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

[RequirePermission(Permissions.AdmissionsView)]
public record GetAdmissionByIdQuery(Guid Id) : IRequest<ApiResponse<AdmissionDetailDto>>;
