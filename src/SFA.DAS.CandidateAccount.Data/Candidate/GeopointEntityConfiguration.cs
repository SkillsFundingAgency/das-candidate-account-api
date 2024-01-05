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
    public class GeopointEntityConfiguration : IEntityTypeConfiguration<GeopointEntity>
    {
        public void Configure(EntityTypeBuilder<GeopointEntity> builder)
        {
            builder.ToTable("Geopoint");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Latitude).HasColumnName("Latitude").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Longitude).HasColumnName("Longitude").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Easting).HasColumnName("Easting").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Northing).HasColumnName("Northing").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.AddressId).HasColumnName("AddressId").HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
