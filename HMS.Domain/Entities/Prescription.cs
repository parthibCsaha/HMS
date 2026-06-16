using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Prescription : BaseEntity
    {
        public string PrescriptionCode { get; set; } = string.Empty;   // RX-00001
        public Guid MedicalRecordId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public bool IsDispensed { get; set; } = false;
        public DateTime? DispensedAt { get; set; }
        public Guid? DispensedBy { get; set; }
    }
}
