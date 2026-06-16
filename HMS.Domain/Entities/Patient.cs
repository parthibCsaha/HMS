using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public Guid UserId { get; set; }
        public string PatientCode { get; set; } = string.Empty;   // PAT-00001
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string EmergencyContactRelation { get; set; } = string.Empty;
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public DateTime? InsuranceExpiry { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicConditions { get; set; }
        public string? Notes { get; set; }
        public bool IsAdmitted { get; set; } = false;
        public int Age => DateTime.UtcNow.Year - DateOfBirth.Year -
            (DateTime.UtcNow.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    }
}
