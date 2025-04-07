using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.EmploymentLocation
{
    public class EmploymentLocationEntityConfiguration : IEntityTypeConfiguration<EmploymentLocationEntity>
    {
        public void Configure(EntityTypeBuilder<EmploymentLocationEntity> builder)
        {
            builder.ToTable("EmploymentLocation");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Addresses).HasColumnName("Addresses").HasColumnType("varchar(max)");
            builder.Property(x => x.EmployerLocationOption).HasColumnName("EmployerLocationOption").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.EmploymentLocationInformation).HasColumnName("EmploymentLocationInformation").HasColumnType("varchar(max)");
            builder.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasColumnType("uniqueidentifier").IsRequired();
            
            builder.HasOne(x => x.ApplicationEntity)
                .WithMany(x => x.EmploymentLocationEntities)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
