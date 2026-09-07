using FluentValidation;
using HMS.Application.Common.Behaviors;
using HMS.Application.Features.Admissions.Services;
using HMS.Application.Features.Appointments.Services;
using HMS.Application.Features.Auth.Services;
using HMS.Application.Features.Beds.Services;
using HMS.Application.Features.Departments.Services;
using HMS.Application.Features.Doctors.Services;
using HMS.Application.Features.Invoices.Services;
using HMS.Application.Features.LabOrders.Services;
using HMS.Application.Features.MedicalRecords.Services;
using HMS.Application.Features.Medications.Services;
using HMS.Application.Features.Patients.Services;
using HMS.Application.Features.Prescriptions.Services;
using HMS.Application.Features.Staff.Services;
using HMS.Application.Features.Wards.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // ──── Feature Services ────
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAdmissionService, AdmissionService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IWardService, WardService>();
        services.AddScoped<IBedService, BedService>();
        services.AddScoped<IMedicationService, MedicationService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<ILabOrderService, LabOrderService>();

        return services;
    }
}
