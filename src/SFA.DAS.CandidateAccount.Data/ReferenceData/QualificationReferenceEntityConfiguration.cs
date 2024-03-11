using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.ReferenceData;

public class QualificationReferenceEntityConfiguration : IEntityTypeConfiguration<QualificationReferenceEntity>
{
    public void Configure(EntityTypeBuilder<QualificationReferenceEntity> builder)
    {
        builder.ToTable("QualificationReference");
        builder.HasKey(x => x.Id);
        builder.HasKey(x => x.Name);
    }
}