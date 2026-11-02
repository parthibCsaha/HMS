using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public class SystemSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = "General"; // General, Billing, Lab, Pharmacy
    public string DataType { get; set; } = "string"; // string, int, bool, decimal, json
    public bool IsReadOnly { get; set; }
    public bool IsPublic { get; set; }
}
