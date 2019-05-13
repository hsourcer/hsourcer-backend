using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence.Configurations
{
    public class AbsenceConfiguration : IEntityTypeConfiguration<Absence>
    {
        public void Configure(EntityTypeBuilder<Absence> builder)
        {
            #region Primary Key 
            builder.HasKey(e => e.AbsenceId)
                .ForSqlServerIsClustered(true); //it would be more usefull on userId
            #endregion

            #region Relationships/Keys
            builder.HasOne(w => w.User)
                .WithMany(w => w.Absences)
                .HasForeignKey(w => w.UserId)
                .HasConstraintName("FK_Absences_Users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.TeamLeader)
                .WithMany(w => w.AbsencesTeamLeader)
                .HasForeignKey(w => w.TeamLeaderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Absences_TeamLeaders");

            builder.HasOne(w => w.ContactPerson)
                .WithMany(w => w.AbsencesContactPerson)
                .HasForeignKey(w => w.ContactPersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Absences_ContactPerson");
            #endregion

            #region Indexes
            builder.ForSqlServerHasIndex(w => w.UserId);
            builder.ForSqlServerHasIndex(w => w.StartDate);
            builder.ForSqlServerHasIndex(w => w.EndDate);
            #endregion

            #region Properties
            builder.Property(e => e.AbsenceId)
                .HasColumnName("AbsenceID")
                .HasMaxLength(20)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(e => e.TeamLeaderId)
                .IsRequired(false);

            builder.Property(e => e.DecisionDate)
                .IsRequired(false);

            builder.Property(e => e.Comment)
                .IsRequired(false)
                .HasMaxLength(255);
            #endregion
        }
    }
}