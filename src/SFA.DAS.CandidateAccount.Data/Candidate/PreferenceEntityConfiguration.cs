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
    public class PreferenceEntityConfiguration : IEntityTypeConfiguration<PreferenceEntity>
    {
        public void Configure(EntityTypeBuilder<PreferenceEntity> builder)
        {
            builder.ToTable("Preference");
            builder.HasKey(x => x.PreferenceId);

            builder.Property(x => x.PreferenceId).HasColumnName("PreferenceId").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.PreferenceMeaning).HasColumnName("PreferenceMeaning").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.PreferenceHint).HasColumnName("PreferenceHint").HasColumnType("varchar").HasMaxLength(500).IsRequired();
        }
    }
}
