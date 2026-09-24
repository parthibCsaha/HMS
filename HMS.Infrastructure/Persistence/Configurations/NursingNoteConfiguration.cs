using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class NursingNoteConfiguration : IEntityTypeConfiguration<NursingNote>
{
    public void Configure(EntityTypeBuilder<NursingNote> builder)
    {
        builder.ToTable("nursing_notes");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.NoteType).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Note).IsRequired().HasMaxLength(4000);
        builder.Property(e => e.ActionTaken).HasMaxLength(2000);

        builder.HasIndex(e => new { e.PatientId, e.NoteDateTime });

        builder
            .HasOne(e => e.Patient)
            .WithMany(p => p.NursingNotes)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Admission)
            .WithMany(a => a.NursingNotes)
            .HasForeignKey(e => e.AdmissionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.Ward)
            .WithMany()
            .HasForeignKey(e => e.WardId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.Bed)
            .WithMany()
            .HasForeignKey(e => e.BedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
