﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.MonitoringInformation;

public class MonitoringInformationConfiguration : IEntityTypeConfiguration<MonitoringInformationEntity>
{
    public void Configure(EntityTypeBuilder<MonitoringInformationEntity> builder)
    {
        builder.ToTable("MonitoringInformation");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Gender).HasColumnName("Gender").HasColumnType("varchar").IsRequired();
        builder.Property(x => x.DisabilityStatus).HasColumnName("DisabilityStatus").HasColumnType("varchar").IsRequired(); 
        builder.Property(x => x.Ethnicity).HasColumnName("Ethnicity").HasColumnType("varchar").IsRequired();
        builder.Property(x => x.CandidateId).HasColumnName("CandidateId").HasColumnType("uniqueidentifier").IsRequired();
    }
}