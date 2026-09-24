using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Common;

namespace HMS.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // DEPT-CARD, DEPT-ORTH
        public string? Description { get; set; }
        public Guid? HeadDoctorId { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Doctor? HeadDoctor { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = [];
        public ICollection<Ward> Wards { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<Staff> Staff { get; set; } = [];
    }
}
