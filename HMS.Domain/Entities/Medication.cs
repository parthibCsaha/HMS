using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public class Medication : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string GenericName { get; set; } = string.Empty;
    public string? BrandName { get; set; }
    public string Category { get; set; } = string.Empty; // Antibiotic, Analgesic, etc.
    public string DosageForm { get; set; } = string.Empty; // Tablet, Syrup, Injection
    public string Strength { get; set; } = string.Empty; // 500mg, 10mg/ml
    public string? Description { get; set; }
    public string? SideEffects { get; set; }
    public string? Contraindications { get; set; }
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public string? Manufacturer { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? BatchNumber { get; set; }
    public string? StorageConditions { get; set; }
    public bool IsActive { get; set; } = true;
    public bool RequiresPrescription { get; set; } = true;
    public bool IsControlledSubstance { get; set; }

    // Navigation Properties
    public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = [];

    // ──── Domain Methods ────

    public bool IsLowStock => StockQuantity <= ReorderLevel;
    public bool IsExpired => ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow;

    public void Deduct(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        if (StockQuantity < quantity)
            throw new InvalidOperationException(
                $"Insufficient stock for '{Name}'. Available: {StockQuantity}, Requested: {quantity}.");

        StockQuantity -= quantity;
    }

    public void Restock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));

        StockQuantity += quantity;
    }
}
