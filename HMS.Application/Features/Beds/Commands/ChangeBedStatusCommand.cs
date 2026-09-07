using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public record ChangeBedStatusCommand(Guid Id, string Status) : IRequest<ApiResponse>;
