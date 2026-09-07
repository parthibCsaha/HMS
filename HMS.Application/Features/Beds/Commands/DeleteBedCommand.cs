using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public record DeleteBedCommand(Guid Id) : IRequest<ApiResponse>;
