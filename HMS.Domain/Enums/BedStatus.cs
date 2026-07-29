using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Enums
{
    public enum BedStatus
    {
        Available = 1, Occupied = 2, Reserved = 3,
        UnderMaintenance = 4, Cleaning = 5
    }
}
