using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public record CreateBedCommand(string BedNumber, Guid WardId, string? Notes)
    : IRequest<ApiResponse<Guid>>;
