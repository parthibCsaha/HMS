using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public record DeleteWardCommand(Guid Id) : IRequest<ApiResponse>;
