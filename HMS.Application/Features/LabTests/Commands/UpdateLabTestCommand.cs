using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.LabTests.Commands;

public record UpdateLabTestCommand(Guid Id, string Name, string Category, string? Description,
    decimal Price, string? ReferenceRange, string? Unit, bool IsActive) : IRequest<ApiResponse>;
