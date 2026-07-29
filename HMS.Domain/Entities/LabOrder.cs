using HMS.Domain.Common;
using HMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class LabOrder : BaseEntity
    {
        public string OrderCode { get; set; } = string.Empty;   // LO-00001
        public Guid PatientId { get; set; }
        public Guid OrderingDoctorId { get; set; }
        public Guid? MedicalRecordId { get; set; }
        public Guid? AdmissionId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public LabTestStatus Status { get; set; } = LabTestStatus.Pending;
        public string? ClinicalNotes { get; set; }
        public string Priority { get; set; } = "Routine";        // Routine, Urgent, STAT
        public DateTime? SampleCollectedAt { get; set; }
        public Guid? SampleCollectedBy { get; set; }
        public Guid? ProcessedBy { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public bool IsBilled { get; set; } = false;
    }
}
