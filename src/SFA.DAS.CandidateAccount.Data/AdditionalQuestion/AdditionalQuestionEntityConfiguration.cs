using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.AdditionalQuestion;

public class AdditionalQuestionEntityConfiguration : IEntityTypeConfiguration<AdditionalQuestionEntity>
{
    public void Configure(EntityTypeBuilder<AdditionalQuestionEntity> builder)
    {
        builder.ToTable("AdditionalQuestion");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Answer).HasColumnName("Answer").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.QuestionId).HasColumnName("QuestionId").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasColumnType("uniqueidentifier").IsRequired();
    }
}