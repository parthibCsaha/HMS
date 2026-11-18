using HMS.Domain.Enums;

namespace HMS.Application.Common.Security;

/// <summary>
/// Single source of truth: which <see cref="UserRole"/> holds which permissions.
/// Add a new role? Add an entry here — zero controller changes required.
/// </summary>
public static class RolePermissionMapping
{
    private static readonly Dictionary<UserRole, HashSet<string>> Map = new()
    {
        // ── Admin — full access ─────────────────────────────
        [UserRole.Admin] = new(StringComparer.OrdinalIgnoreCase)
        {
            // Admissions
            Permissions.AdmissionsView,
            Permissions.AdmissionsAdmit,
            Permissions.AdmissionsDischarge,
            Permissions.AdmissionsTransfer,

            // Appointments
            Permissions.AppointmentsCreate,
            Permissions.AppointmentsCancel,
            Permissions.AppointmentsCheckIn,
            Permissions.AppointmentsComplete,

            // Audit Logs
            Permissions.AuditLogsView,

            // Beds
            Permissions.BedsCreate,
            Permissions.BedsUpdateStatus,
            Permissions.BedsDelete,

            // Dashboard
            Permissions.DashboardView,

            // Departments
            Permissions.DepartmentsCreate,
            Permissions.DepartmentsUpdate,
            Permissions.DepartmentsDelete,

            // Doctors
            Permissions.DoctorsCreate,
            Permissions.DoctorsUpdate,
            Permissions.DoctorsDelete,

            // Invoices
            Permissions.InvoicesView,
            Permissions.InvoicesCreate,
            Permissions.InvoicesAddPayment,
            Permissions.InvoicesCancel,

            // Lab Orders
            Permissions.LabOrdersView,
            Permissions.LabOrdersCreate,
            Permissions.LabOrdersAddResult,

            // Lab Tests
            Permissions.LabTestsCreate,
            Permissions.LabTestsUpdate,

            // Medical Records
            Permissions.MedicalRecordsView,
            Permissions.MedicalRecordsCreate,
            Permissions.MedicalRecordsUpdate,

            // Medications
            Permissions.MedicationsViewLowStock,
            Permissions.MedicationsCreate,
            Permissions.MedicationsUpdate,
            Permissions.MedicationsDelete,

            // Nursing Notes
            Permissions.NursingNotesView,
            Permissions.NursingNotesCreate,

            // Patients
            Permissions.PatientsView,
            Permissions.PatientsCreate,
            Permissions.PatientsUpdate,
            Permissions.PatientsDelete,

            // Prescriptions
            Permissions.PrescriptionsView,
            Permissions.PrescriptionsCreate,
            Permissions.PrescriptionsDispense,

            // Staff
            Permissions.StaffCreate,
            Permissions.StaffUpdate,
            Permissions.StaffDelete,

            // System Settings
            Permissions.SystemSettingsView,
            Permissions.SystemSettingsUpsert,

            // Vital Signs
            Permissions.VitalSignsView,
            Permissions.VitalSignsRecord,

            // Wards
            Permissions.WardsCreate,
            Permissions.WardsUpdate,
            Permissions.WardsDelete,
        },

        // ── Doctor ──────────────────────────────────────────
        [UserRole.Doctor] = new(StringComparer.OrdinalIgnoreCase)
        {
            Permissions.AdmissionsView,
            Permissions.AdmissionsAdmit,
            Permissions.AdmissionsDischarge,
            Permissions.AdmissionsTransfer,

            Permissions.AppointmentsCreate,
            Permissions.AppointmentsCancel,
            Permissions.AppointmentsComplete,

            Permissions.LabOrdersView,
            Permissions.LabOrdersCreate,

            Permissions.MedicalRecordsView,
            Permissions.MedicalRecordsCreate,
            Permissions.MedicalRecordsUpdate,

            Permissions.NursingNotesView,

            Permissions.PatientsView,
            Permissions.PatientsCreate,
            Permissions.PatientsUpdate,

            Permissions.PrescriptionsView,
            Permissions.PrescriptionsCreate,

            Permissions.VitalSignsView,
            Permissions.VitalSignsRecord,
        },

        // ── Nurse ───────────────────────────────────────────
        [UserRole.Nurse] = new(StringComparer.OrdinalIgnoreCase)
        {
            Permissions.AdmissionsView,
            Permissions.AdmissionsAdmit,
            Permissions.AdmissionsDischarge,
            Permissions.AdmissionsTransfer,

            Permissions.AppointmentsCheckIn,

            Permissions.BedsUpdateStatus,

            Permissions.MedicalRecordsView,

            Permissions.NursingNotesView,
            Permissions.NursingNotesCreate,

            Permissions.PatientsView,
            Permissions.PatientsCreate,
            Permissions.PatientsUpdate,

            Permissions.VitalSignsView,
            Permissions.VitalSignsRecord,
        },

        // ── Receptionist ────────────────────────────────────
        [UserRole.Receptionist] = new(StringComparer.OrdinalIgnoreCase)
        {
            Permissions.AppointmentsCreate,
            Permissions.AppointmentsCancel,
            Permissions.AppointmentsCheckIn,

            Permissions.InvoicesView,
            Permissions.InvoicesCreate,
            Permissions.InvoicesAddPayment,
            Permissions.InvoicesCancel,

            Permissions.PatientsView,
            Permissions.PatientsCreate,
            Permissions.PatientsUpdate,
        },

        // ── Pharmacist ──────────────────────────────────────
        [UserRole.Pharmacist] = new(StringComparer.OrdinalIgnoreCase)
        {
            Permissions.MedicationsViewLowStock,
            Permissions.MedicationsCreate,
            Permissions.MedicationsUpdate,

            Permissions.PrescriptionsView,
            Permissions.PrescriptionsDispense,
        },

        // ── Lab Technician ──────────────────────────────────
        [UserRole.LabTechnician] = new(StringComparer.OrdinalIgnoreCase)
        {
            Permissions.LabOrdersView,
            Permissions.LabOrdersAddResult,

            Permissions.LabTestsCreate,
            Permissions.LabTestsUpdate,
        },

        // ── Patient (self-service only — no privileged permissions) ──
        [UserRole.Patient] = new(StringComparer.OrdinalIgnoreCase)
        {
            // Patients with the Patient role have no admin/clinical permissions.
            // They rely on [Authorize]-only endpoints (view own data, etc.).
        },
    };

    /// <summary>Returns <c>true</c> when <paramref name="role"/> holds <paramref name="permission"/>.</summary>
    public static bool HasPermission(UserRole role, string permission)
        => Map.TryGetValue(role, out var perms) && perms.Contains(permission);

    /// <summary>Returns every permission granted to <paramref name="role"/>.</summary>
    public static IReadOnlySet<string> GetPermissions(UserRole role)
        => Map.TryGetValue(role, out var perms) ? perms : new HashSet<string>();
}

