using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class MedicalRecord : BaseEntity
    {
        public string RecordCode { get; set; } = string.Empty;   // MR-00001
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? AdmissionId { get; set; }
        public DateTime VisitDate { get; set; }
        public string ChiefComplaint { get; set; } = string.Empty;
        public string? PresentIllnessHistory { get; set; }
        public string? PastMedicalHistory { get; set; }
        public string? FamilyHistory { get; set; }
        public string? SocialHistory { get; set; }
        public string? ReviewOfSystems { get; set; }
        public string? PhysicalExamination { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string? DifferentialDiagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Procedures { get; set; }
        public string? Notes { get; set; }
        public string? FollowUpInstructions { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool IsConfidential { get; set; } = false;
    }
}
