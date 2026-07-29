using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
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
        public string? DischargeCondition { get; set; }  // Stable, Improved, Deceased, LAMA
        public bool IsActive { get; set; } = true;
        public Guid? DischargedBy { get; set; }
    }
}
