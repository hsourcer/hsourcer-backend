using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            #region Primary Key 
            builder.HasKey(e => e.TeamId)
                .ForSqlServerIsClustered(false); //I think that clustered index would be more usefull on teams
            #endregion

            #region Relationships/Keys
            builder.HasOne(w => w.Organization)
                .WithMany(w => w.Teams)
                .HasForeignKey(w => w.OrganizationId)
                .HasConstraintName("FK_Teams_Organizations")
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Indexes
            builder.ForSqlServerHasIndex(w => w.OrganizationId)
                .ForSqlServerIsClustered(true);

            builder.ForSqlServerHasIndex(w => w.TeamId)
                .ForSqlServerIsClustered(false);
            #endregion

            #region Properties
            builder.Property(e => e.TeamId)
                .HasColumnName("TeamID")
                .HasMaxLength(20)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.OrganizationId);

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired(true);


            builder.Property(e => e.Description)
                .HasMaxLength(255);
            //for init not require stuff, to change later

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("getutcdate()")
                .IsRequired(true);

            builder.Property(e => e.CreatedBy)
                .IsRequired(false);


            //builder.Property(e => e.CreationDate)
            //    .HasDefaultValueSql("getutcdate()");
            #endregion
        }
    }
}

