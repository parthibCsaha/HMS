using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Enums
{
    public enum LabTestStatus
    {
        Pending = 1, SampleCollected = 2, InProgress = 3,
        Completed = 4, Verified = 5, Cancelled = 6, Rejected = 7
    }
}
