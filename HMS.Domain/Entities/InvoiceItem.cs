using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public class InvoiceItem : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty; // Consultation, LabTest, Medication, Bed, Procedure
    public Guid? ReferenceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation Properties
    public Invoice Invoice { get; set; } = null!;

    // ──── Domain Methods ────

    /// <summary>
    /// Recalculates line-item totals from quantity, unit price, tax, and discount.
    /// </summary>
    public void Recalculate()
    {
        var lineTotal = Quantity * UnitPrice;
        TaxAmount = lineTotal * (TaxPercent / 100m);
        DiscountAmount = lineTotal * (DiscountPercent / 100m);
        TotalPrice = lineTotal + TaxAmount - DiscountAmount;
    }
}
