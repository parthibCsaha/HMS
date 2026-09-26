using System.Reflection;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // ──── Identity & Access ────
    public DbSet<User> Users => Set<User>();

    // ──── People ────
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Staff> Staff => Set<Staff>();

    // ──── Organisation ────
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Ward> Wards => Set<Ward>();
    public DbSet<Bed> Beds => Set<Bed>();

    // ──── Appointments & Clinical ────
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<VitalSigns> VitalSigns => Set<VitalSigns>();

    // ──── Prescriptions ────
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();

    // ──── Laboratory ────
    public DbSet<LabTest> LabTests => Set<LabTest>();
    public DbSet<LabOrder> LabOrders => Set<LabOrder>();
    public DbSet<LabOrderItem> LabOrderItems => Set<LabOrderItem>();
    public DbSet<LabResult> LabResults => Set<LabResult>();

    // ──── Pharmacy ────
    public DbSet<Medication> Medications => Set<Medication>();

    // ──── Nursing ────
    public DbSet<NursingNote> NursingNotes => Set<NursingNote>();

    // ──── Admissions ────
    public DbSet<AdmissionRecord> AdmissionRecords => Set<AdmissionRecord>();

    // ──── Billing ────
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

    // ──── System ────
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // NOTE: Audit stamping (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy) is handled
    // entirely by AuditableEntityInterceptor. No override of SaveChangesAsync needed here.
}
