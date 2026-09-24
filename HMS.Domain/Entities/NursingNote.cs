using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Common;

namespace HMS.Domain.Entities
{
    public class NursingNote : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid NurseId { get; set; }
        public Guid? AdmissionId { get; set; }
        public Guid? WardId { get; set; }
        public Guid? BedId { get; set; }
        public DateTime NoteDateTime { get; set; } = DateTime.UtcNow;
        public string NoteType { get; set; } = string.Empty; // Admission, Shift, Medication, Observation, Discharge
        public string Note { get; set; } = string.Empty;
        public string? ActionTaken { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; } = null!;
        public AdmissionRecord? Admission { get; set; }
        public Ward? Ward { get; set; }
        public Bed? Bed { get; set; }
    }
}
