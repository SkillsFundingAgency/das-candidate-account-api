using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate;

public class CandidateEntityConfiguration : IEntityTypeConfiguration<CandidateEntity>
{
    public void Configure(EntityTypeBuilder<CandidateEntity> builder)
    {
        builder.ToTable("Candidate");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("FirstName").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.MiddleNames).HasColumnName("MiddleNames").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.LastName).HasColumnName("LastName").HasColumnType("varchar").HasMaxLength(150).IsRequired(false);
        builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(255).IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").HasColumnType("DateTime").IsRequired(false);
        builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("DateTime").IsRequired();
        builder.Property(x => x.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("DateTime").IsRequired(false);
        builder.Property(x => x.TermsOfUseAcceptedOn).HasColumnName("TermsOfUseAcceptedOn").HasColumnType("DateTime").IsRequired(false);
        builder.Property(x => x.GovUkIdentifier).HasColumnName("GovUkIdentifier").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        
        
        builder.HasIndex(x => x.GovUkIdentifier).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}