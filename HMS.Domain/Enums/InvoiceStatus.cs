using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Enums
{
    public enum InvoiceStatus
    {
        Draft = 1, Pending = 2, Paid = 3, PartiallyPaid = 4,
        Overdue = 5, Cancelled = 6, Refunded = 7, WrittenOff = 8
    }
}
