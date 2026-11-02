using HMS.Domain.Common;
using HMS.Domain.Enums;

namespace HMS.Domain.Entities;

public class Appointment : BaseEntity
{
    public string AppointmentCode { get; set; } = string.Empty; // APT-00001
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid DepartmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public int DurationMinutes { get; set; } = 30;
    public AppointmentType Type { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? ChiefComplaint { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public bool IsPaid { get; set; }
    public decimal? ConsultationFee { get; set; }
    public DateTime? CheckedInAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid? ReferredByDoctorId { get; set; }

    // Navigation Properties
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public Doctor? ReferredByDoctor { get; set; }

    // ──── Domain Methods ────

    /// <summary>
    /// Marks the patient as checked in for this appointment.
    /// </summary>
    public void CheckIn()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException(
                $"Cannot check in an appointment with status '{Status}'.");

        Status = AppointmentStatus.InProgress;
        CheckedInAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks this appointment as completed.
    /// </summary>
    public void Complete()
    {
        if (Status != AppointmentStatus.InProgress)
            throw new InvalidOperationException(
                $"Cannot complete an appointment with status '{Status}'.");

        Status = AppointmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels this appointment with a required reason.
    /// </summary>
    public void Cancel(string reason)
    {
        if (Status is AppointmentStatus.Completed or AppointmentStatus.Cancelled)
            throw new InvalidOperationException(
                $"Cannot cancel an appointment with status '{Status}'.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Cancellation reason is required.", nameof(reason));

        Status = AppointmentStatus.Cancelled;
        CancellationReason = reason;
    }

    /// <summary>
    /// Reschedules this appointment to a new date/time.
    /// </summary>
    public void Reschedule(DateTime newDate, TimeSpan newTime)
    {
        if (Status is AppointmentStatus.Completed or AppointmentStatus.Cancelled)
            throw new InvalidOperationException(
                $"Cannot reschedule an appointment with status '{Status}'.");

        AppointmentDate = newDate;
        AppointmentTime = newTime;
        Status = AppointmentStatus.Scheduled;
        CheckedInAt = null;
    }
}
