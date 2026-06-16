using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Bed : BaseEntity
    {
        public string BedNumber { get; set; } = string.Empty;
        public Guid WardId { get; set; }
        public BedStatus Status { get; set; } = BedStatus.Available;
        public Guid? CurrentPatientId { get; set; }
        public DateTime? OccupiedAt { get; set; }
        public string? Notes { get; set; }
    }
}
