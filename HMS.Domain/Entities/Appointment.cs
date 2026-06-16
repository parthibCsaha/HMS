using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public string AppointmentCode { get; set; } = string.Empty;   // APT-00001
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
        public bool IsPaid { get; set; } = false;
        public decimal? ConsultationFee { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Guid? ReferredByDoctorId { get; set; }
    }
}
