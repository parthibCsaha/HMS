using HMS.Domain.Common;
using HMS.Domain.Enums;

namespace HMS.Domain.Entities;

public class Bed : BaseEntity
{
    public string BedNumber { get; set; } = string.Empty;
    public Guid WardId { get; set; }
    public BedStatus Status { get; set; } = BedStatus.Available;
    public Guid? CurrentPatientId { get; set; }
    public DateTime? OccupiedAt { get; set; }
    public string? Notes { get; set; }

    // Navigation Properties
    public Ward Ward { get; set; } = null!;
    public Patient? CurrentPatient { get; set; }
    public ICollection<AdmissionRecord> AdmissionRecords { get; set; } = [];

    // ──── Domain Methods ────

    public void Occupy(Guid patientId)
    {
        if (Status != BedStatus.Available)
            throw new InvalidOperationException($"Bed '{BedNumber}' is not available.");

        CurrentPatientId = patientId;
        Status = BedStatus.Occupied;
        OccupiedAt = DateTime.UtcNow;
    }

    public void Release()
    {
        CurrentPatientId = null;
        Status = BedStatus.Available;
        OccupiedAt = null;
    }
}
