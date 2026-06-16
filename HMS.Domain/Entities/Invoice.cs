using HMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } = string.Empty;   // INV-00001
        public Guid PatientId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? AdmissionId { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
        public decimal SubTotal { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? TransactionReference { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? Notes { get; set; }
        public string? InsuranceClaimNumber { get; set; }
        public decimal? InsuranceCoveredAmount { get; set; }
    }
}
