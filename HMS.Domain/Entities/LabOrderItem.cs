using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class LabOrderItem : BaseEntity
    {
        public Guid LabOrderId { get; set; }
        public Guid LabTestId { get; set; }
        public LabTestStatus Status { get; set; } = LabTestStatus.Pending;
        public decimal Price { get; set; }
    }
}
