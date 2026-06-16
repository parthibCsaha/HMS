using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class VitalSigns : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? AdmissionId { get; set; }
        public Guid RecordedBy { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
        public decimal? TemperatureCelsius { get; set; }
        public int? HeartRateBpm { get; set; }
        public int? RespiratoryRatePerMin { get; set; }
        public string? BloodPressure { get; set; }       // e.g. "120/80"
        public decimal? OxygenSaturationPercent { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? HeightCm { get; set; }
        public decimal? BmiValue { get; set; }
        public decimal? BloodGlucoseMgDl { get; set; }
        public string? PainLevel { get; set; }           // 0-10 scale
        public string? Notes { get; set; }
    }
}
