using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate
{
    public class ApplicationTemplateEntityConfiguration : IEntityTypeConfiguration<ApplicationTemplateEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationTemplateEntity> builder)
        {
            builder.ToTable("ApprenticeTemplate");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ApprenticeId).HasColumnName("ApprenticeId").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.DisabilityStatus).HasColumnName("DisabilityStatus").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        }
    }
}
