using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Enums
{
    public enum AppointmentStatus
    {
        Scheduled = 1, Confirmed = 2, CheckedIn = 3,
        InProgress = 4, Completed = 5, Cancelled = 6,
        NoShow = 7, Rescheduled = 8
    }
}
