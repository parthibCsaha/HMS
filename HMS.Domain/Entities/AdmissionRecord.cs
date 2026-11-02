using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public class AdmissionRecord : BaseEntity
{
    public string AdmissionCode { get; set; } = string.Empty;
    public Guid PatientId { get; set; }
    public Guid AdmittingDoctorId { get; set; }
    public Guid WardId { get; set; }
    public Guid BedId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public string ReasonForAdmission { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? DischargeSummary { get; set; }
    public string? DischargeCondition { get; set; } // Stable, Improved, Deceased, LAMA
    public bool IsActive { get; set; } = true;
    public Guid? DischargedBy { get; set; }

    // Navigation Properties
    public Patient Patient { get; set; } = null!;
    public Doctor AdmittingDoctor { get; set; } = null!;
    public Ward Ward { get; set; } = null!;
    public Bed Bed { get; set; } = null!;
    public ICollection<NursingNote> NursingNotes { get; set; } = [];
    public ICollection<VitalSigns> VitalSigns { get; set; } = [];

    // ──── Domain Methods ────

    /// <summary>
    /// Discharges the patient with summary, condition, and who performed the discharge.
    /// </summary>
    public void Discharge(string summary, string condition, Guid dischargedByUserId)
    {
        if (!IsActive)
            throw new InvalidOperationException("This admission has already been discharged.");

        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Discharge summary is required.", nameof(summary));

        if (string.IsNullOrWhiteSpace(condition))
            throw new ArgumentException("Discharge condition is required.", nameof(condition));

        DischargeSummary = summary;
        DischargeCondition = condition;
        DischargedBy = dischargedByUserId;
        DischargeDate = DateTime.UtcNow;
        IsActive = false;
    }

    /// <summary>
    /// Transfers the patient to a different ward/bed.
    /// </summary>
    public void Transfer(Guid newWardId, Guid newBedId)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot transfer a discharged patient.");

        WardId = newWardId;
        BedId = newBedId;
    }
}
