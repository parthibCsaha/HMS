using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class LabTest : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;          // LT-CBC, LT-LFT
        public string Category { get; set; } = string.Empty;      // Hematology, Biochemistry
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? SampleType { get; set; }                   // Blood, Urine, Stool
        public string? PreparationInstructions { get; set; }
        public string? ReferenceRange { get; set; }               // Stored as JSON
        public string? Unit { get; set; }
        public int? TurnaroundTimeHours { get; set; }
        public bool IsActive { get; set; } = true;
        public bool RequiresFasting { get; set; } = false;
    }
}
