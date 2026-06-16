using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public Guid UserId { get; set; }
        public string DoctorCode { get; set; } = string.Empty;   // DOC-00001
        public Guid DepartmentId { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Biography { get; set; }
        public string? AvailableDays { get; set; }    // JSON: ["Monday","Wednesday","Friday"]
        public TimeSpan? ConsultationStartTime { get; set; }
        public TimeSpan? ConsultationEndTime { get; set; }
        public int? SlotDurationMinutes { get; set; } = 30;
    }
}
