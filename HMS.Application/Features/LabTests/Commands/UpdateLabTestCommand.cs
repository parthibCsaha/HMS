using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.LabTests.Commands;

[RequirePermission(Permissions.LabTestsUpdate)]
public record UpdateLabTestCommand(
    Guid Id,
    string Name,
    string Category,
    string? Description,
    decimal Price,
    string? ReferenceRange,
    string? Unit,
    bool IsActive
) : IRequest<ApiResponse>;
