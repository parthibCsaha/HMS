using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, ApiResponse<PatientDetail>>
    {
        public async Task<ApiResponse<PaginatedResponse<PatientDetail>>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var query = new PaginationQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                IsDescending = request.IsDescending
            };


        }
    }
}
