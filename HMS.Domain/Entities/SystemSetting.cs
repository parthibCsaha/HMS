using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = "General";     // General, Billing, Lab, Pharmacy
        public string DataType { get; set; } = "string";      // string, int, bool, decimal, json
        public bool IsReadOnly { get; set; } = false;
        public bool IsPublic { get; set; } = false;
    }
}
