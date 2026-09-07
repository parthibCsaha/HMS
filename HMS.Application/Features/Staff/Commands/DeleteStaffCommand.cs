using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public record DeleteStaffCommand(Guid Id) : IRequest<ApiResponse>;
