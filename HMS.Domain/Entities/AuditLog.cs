using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;       // CREATE, UPDATE, DELETE, LOGIN, LOGOUT
        public string EntityName { get; set; } = string.Empty;   // Patient, Doctor, Invoice...
        public Guid? EntityId { get; set; }
        public string? OldValues { get; set; }                   // JSON snapshot before
        public string? NewValues { get; set; }                   // JSON snapshot after
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Endpoint { get; set; }
        public string? HttpMethod { get; set; }
        public int? StatusCode { get; set; }
        public long? DurationMs { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? ErrorMessage { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    }
}
