using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Features.Patients.Queries.GetPatients
{
    public record GetPatientsQuery
    (
        int PageNumber = 1, int PageSize = 10,
        string? SearchTerm = null, string? SortBy = null, bool IsDescending = false

    ) : IRequest<ApiResponse<PaginatedResponse<PatientListItem>>>;
}
