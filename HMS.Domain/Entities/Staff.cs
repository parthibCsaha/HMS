using HMS.Domain.Common;
using HMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Staff : BaseEntity
    {
        public Guid UserId { get; set; }
        public string StaffCode { get; set; } = string.Empty;
        public StaffType StaffType { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? WardId { get; set; }
        public string Qualification { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public string? Shift { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
