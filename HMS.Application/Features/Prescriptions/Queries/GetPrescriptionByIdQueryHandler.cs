using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetPrescriptionByIdQueryHandler(IPrescriptionRepository repo)
    : IRequestHandler<GetPrescriptionByIdQuery, ApiResponse<PrescriptionDetailDto>>
{
    public async Task<ApiResponse<PrescriptionDetailDto>> Handle(
        GetPrescriptionByIdQuery request,
        CancellationToken ct
    )
    {
        var prescription =
            await repo.GetWithItemsAsync(request.Id, ct) ?? throw new NotFoundException("Prescription", request.Id);

        var items = prescription
            .Items.Select(item => new PrescriptionItemDto(
                item.Id,
                item.Medication?.Name ?? "",
                item.Dosage,
                item.Frequency,
                item.Route,
                item.DurationDays,
                item.Quantity,
                item.Instructions,
                item.IsDispensed
            ))
            .ToList();

        var dto = new PrescriptionDetailDto(
            prescription.Id,
            prescription.PrescriptionCode,
            prescription.PatientId,
            prescription.DoctorId,
            prescription.Doctor?.User != null
                ? $"{prescription.Doctor.User.FirstName} {prescription.Doctor.User.LastName}"
                : "",
            prescription.IssuedDate,
            prescription.ExpiryDate,
            prescription.Instructions,
            prescription.Notes,
            prescription.IsDispensed,
            prescription.DispensedAt,
            items,
            prescription.CreatedAt
        );

        return ApiResponse<PrescriptionDetailDto>.Success(dto);
    }
}
