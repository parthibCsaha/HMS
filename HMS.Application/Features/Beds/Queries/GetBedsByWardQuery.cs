using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.DTOs;
using MediatR;

namespace HMS.Application.Features.Beds.Queries;

public record GetBedsByWardQuery(Guid WardId) : IRequest<ApiResponse<IEnumerable<BedListItemDto>>>;
