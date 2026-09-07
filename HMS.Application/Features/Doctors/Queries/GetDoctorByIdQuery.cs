using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public record GetDoctorByIdQuery(Guid Id) : IRequest<ApiResponse<DoctorDetailDto>>;
