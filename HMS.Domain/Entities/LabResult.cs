using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class LabResult : BaseEntity
    {
        public Guid LabOrderItemId { get; set; }
        public Guid LabOrderId { get; set; }
        public Guid PatientId { get; set; }
        public Guid LabTestId { get; set; }
        public string Result { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public bool? IsAbnormal { get; set; }
        public string? Interpretation { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentUrl { get; set; }
        public Guid ResultedBy { get; set; }
        public DateTime ResultedAt { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
        public Guid? VerifiedBy { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }
}
