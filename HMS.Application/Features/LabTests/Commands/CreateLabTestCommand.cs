using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.LabTests.Commands;

public record CreateLabTestCommand(
    string Code,
    string Name,
    string Category,
    string? Description,
    decimal Price,
    string? SampleType,
    string? ReferenceRange,
    string? Unit,
    int? TurnaroundTimeHours
) : IRequest<ApiResponse<Guid>>;
