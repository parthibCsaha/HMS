using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace HMS.Domain.Entities
{
    public class Medication : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string? BrandName { get; set; }
        public string Category { get; set; } = string.Empty;     // Antibiotic, Analgesic, etc.
        public string DosageForm { get; set; } = string.Empty;   // Tablet, Syrup, Injection
        public string Strength { get; set; } = string.Empty;     // 500mg, 10mg/ml
        public string? Description { get; set; }
        public string? SideEffects { get; set; }
        public string? Contraindications { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }
        public string? StorageConditions { get; set; }
        public bool IsActive { get; set; } = true;
        public bool RequiresPrescription { get; set; } = true;
        public bool IsControlledSubstance { get; set; } = false;
    }
}
