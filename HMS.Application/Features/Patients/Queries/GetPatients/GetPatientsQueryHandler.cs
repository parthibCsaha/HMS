using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, ApiResponse<PaginatedResponse<PatientListItem>>>
    {
        public async Task<ApiResponse<PaginatedResponse<PatientListItem>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var query = new PaginationQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                IsDescending = request.IsDescending
            };

            var (items, totalCounts) = await patientRepository.GetPagedAsync(query, cancellationToken);
            var response = PaginatedResponse<PatientListItem>.Create(items, query.PageNumber, query.PageSize, totalCounts);

        }
    }
}
