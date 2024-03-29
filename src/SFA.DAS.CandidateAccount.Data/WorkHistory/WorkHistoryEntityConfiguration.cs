﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.WorkExperience;

public class WorkHistoryEntityConfiguration : IEntityTypeConfiguration<WorkHistoryEntity>
{
    public void Configure(EntityTypeBuilder<WorkHistoryEntity> builder)
    {
        builder.ToTable("WorkHistory");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Employer).HasColumnName("Employer").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.JobTitle).HasColumnName("JobTitle").HasColumnType("varchar").HasMaxLength(150).IsRequired();
        builder.Property(x => x.StartDate).HasColumnName("StartDate").HasColumnType("datetime").IsRequired();
        builder.Property(x => x.EndDate).HasColumnName("EndDate").HasColumnType("datetime");
        builder.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasColumnType("uniqueidentifier").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("varchar").IsRequired();
        builder.Property(x => x.WorkHistoryType).HasColumnName("WorkHistoryType").HasColumnType("tinyint").IsRequired();

        builder
            .HasOne(c => c.ApplicationEntity)
            .WithMany(c => c.WorkHistoryEntities)
            .HasForeignKey(c => c.ApplicationId)
            .HasPrincipalKey(c => c.Id);
    }
}

