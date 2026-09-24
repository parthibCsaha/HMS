namespace HMS.Application.Common.Models;

public record DashboardStatsDto(
    int TotalPatients,
    int TotalDoctors,
    int TotalAppointmentsToday,
    int ActiveAdmissions,
    int PendingLabOrders,
    decimal RevenueToday,
    decimal RevenueThisMonth
);

public record OccupancyReportDto(
    Guid WardId,
    string WardName,
    string WardType,
    int TotalBeds,
    int OccupiedBeds,
    int AvailableBeds,
    decimal OccupancyPercent
);

public record AppointmentTrendDto(
    DateTime Date,
    int TotalAppointments,
    int Completed,
    int Cancelled,
    int NoShow
);

public record RevenueSummaryDto(
    DateTime Date,
    string? Department,
    string? PaymentMethod,
    decimal TotalAmount,
    decimal PaidAmount,
    int InvoiceCount
);

public record PatientDemographicsDto(
    IEnumerable<AgeGroupDto> AgeGroups,
    IEnumerable<GenderDistributionDto> GenderDistribution,
    IEnumerable<BloodGroupDistributionDto> BloodGroupDistribution
);

public record AgeGroupDto(string AgeRange, int Count);

public record GenderDistributionDto(string Gender, int Count);

public record BloodGroupDistributionDto(string BloodGroup, int Count);
