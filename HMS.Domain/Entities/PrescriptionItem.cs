using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Common;

namespace HMS.Domain.Entities
{
    public class PrescriptionItem : BaseEntity
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicationId { get; set; }
        public string Dosage { get; set; } = string.Empty; // e.g. "500mg"
        public string Frequency { get; set; } = string.Empty; // e.g. "3 times a day"
        public string Route { get; set; } = string.Empty; // Oral, IV, IM, Topical
        public int DurationDays { get; set; }
        public int Quantity { get; set; }
        public string? Instructions { get; set; }
        public bool IsDispensed { get; set; } = false;

        // Navigation Properties
        public Prescription Prescription { get; set; } = null!;
        public Medication Medication { get; set; } = null!;
    }
}
