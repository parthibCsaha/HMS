using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Ward : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string WardNumber { get; set; } = string.Empty;
        public WardType WardType { get; set; }
        public Guid DepartmentId { get; set; }
        public int TotalBeds { get; set; }
        public int AvailableBeds { get; set; }
        public string? Description { get; set; }
        public decimal? ChargePerDay { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
