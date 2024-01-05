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
    public class AboutYouEntityConfiguration : IEntityTypeConfiguration<AboutYouEntity>
    {
        public void Configure(EntityTypeBuilder<AboutYouEntity> builder)
        {
            builder.ToTable("AboutYou");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Strengths).HasColumnName("Strengths").HasColumnType("varchar").IsRequired();
            builder.Property(x => x.Improvements).HasColumnName("Improvements").HasColumnType("varchar").IsRequired(); 
            builder.Property(x => x.HobbiesAndInterests).HasColumnName("HobbiesAndInterests").HasColumnType("varchar").IsRequired(); 
            builder.Property(x => x.Support).HasColumnName("Support").HasColumnType("varchar").IsRequired();
            builder.Property(x => x.ApplicationTemplateId).HasColumnName("ApplicationTemplateId").HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
