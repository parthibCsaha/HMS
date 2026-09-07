using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public record DeleteDepartmentCommand(Guid Id) : IRequest<ApiResponse>;
