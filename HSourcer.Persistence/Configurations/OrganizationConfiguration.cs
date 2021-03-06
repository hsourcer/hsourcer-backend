using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            #region Primary Key 
            builder.HasKey(e => e.OrganizationId)
                .ForSqlServerIsClustered(true);
            #endregion

            #region Properties
            builder.Property(e => e.OrganizationId)
                .HasColumnName("OrganizationID")
                .HasMaxLength(20)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(e => e.Description)
                .HasMaxLength(255);
            #endregion
        }
    }
}