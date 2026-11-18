namespace HMS.Application.Common.Security;

/// <summary>
/// Compile-safe permission constants used by <see cref="RequirePermissionAttribute"/>.
/// One permission per domain action — roles are mapped to permissions in <see cref="RolePermissionMapping"/>.
/// </summary>
public static class Permissions
{
    // ── Admissions ──────────────────────────────────────────
    public const string AdmissionsView = "admissions.view";
    public const string AdmissionsAdmit = "admissions.admit";
    public const string AdmissionsDischarge = "admissions.discharge";
    public const string AdmissionsTransfer = "admissions.transfer";

    // ── Appointments ────────────────────────────────────────
    public const string AppointmentsCreate = "appointments.create";
    public const string AppointmentsCancel = "appointments.cancel";
    public const string AppointmentsCheckIn = "appointments.checkin";
    public const string AppointmentsComplete = "appointments.complete";

    // ── Audit Logs ──────────────────────────────────────────
    public const string AuditLogsView = "audit-logs.view";

    // ── Beds ────────────────────────────────────────────────
    public const string BedsCreate = "beds.create";
    public const string BedsUpdateStatus = "beds.update-status";
    public const string BedsDelete = "beds.delete";

    // ── Dashboard ───────────────────────────────────────────
    public const string DashboardView = "dashboard.view";

    // ── Departments ─────────────────────────────────────────
    public const string DepartmentsCreate = "departments.create";
    public const string DepartmentsUpdate = "departments.update";
    public const string DepartmentsDelete = "departments.delete";

    // ── Doctors ─────────────────────────────────────────────
    public const string DoctorsCreate = "doctors.create";
    public const string DoctorsUpdate = "doctors.update";
    public const string DoctorsDelete = "doctors.delete";

    // ── Invoices ────────────────────────────────────────────
    public const string InvoicesView = "invoices.view";
    public const string InvoicesCreate = "invoices.create";
    public const string InvoicesAddPayment = "invoices.add-payment";
    public const string InvoicesCancel = "invoices.cancel";

    // ── Lab Orders ──────────────────────────────────────────
    public const string LabOrdersView = "lab-orders.view";
    public const string LabOrdersCreate = "lab-orders.create";
    public const string LabOrdersAddResult = "lab-orders.add-result";

    // ── Lab Tests ───────────────────────────────────────────
    public const string LabTestsCreate = "lab-tests.create";
    public const string LabTestsUpdate = "lab-tests.update";

    // ── Medical Records ─────────────────────────────────────
    public const string MedicalRecordsView = "medical-records.view";
    public const string MedicalRecordsCreate = "medical-records.create";
    public const string MedicalRecordsUpdate = "medical-records.update";

    // ── Medications ─────────────────────────────────────────
    public const string MedicationsViewLowStock = "medications.view-low-stock";
    public const string MedicationsCreate = "medications.create";
    public const string MedicationsUpdate = "medications.update";
    public const string MedicationsDelete = "medications.delete";

    // ── Nursing Notes ───────────────────────────────────────
    public const string NursingNotesView = "nursing-notes.view";
    public const string NursingNotesCreate = "nursing-notes.create";

    // ── Patients ────────────────────────────────────────────
    public const string PatientsView = "patients.view";
    public const string PatientsCreate = "patients.create";
    public const string PatientsUpdate = "patients.update";
    public const string PatientsDelete = "patients.delete";

    // ── Prescriptions ───────────────────────────────────────
    public const string PrescriptionsView = "prescriptions.view";
    public const string PrescriptionsCreate = "prescriptions.create";
    public const string PrescriptionsDispense = "prescriptions.dispense";

    // ── Staff ───────────────────────────────────────────────
    public const string StaffCreate = "staff.create";
    public const string StaffUpdate = "staff.update";
    public const string StaffDelete = "staff.delete";

    // ── System Settings ─────────────────────────────────────
    public const string SystemSettingsView = "system-settings.view";
    public const string SystemSettingsUpsert = "system-settings.upsert";

    // ── Vital Signs ─────────────────────────────────────────
    public const string VitalSignsView = "vital-signs.view";
    public const string VitalSignsRecord = "vital-signs.record";

    // ── Wards ───────────────────────────────────────────────
    public const string WardsCreate = "wards.create";
    public const string WardsUpdate = "wards.update";
    public const string WardsDelete = "wards.delete";
}

