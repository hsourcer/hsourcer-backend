using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Primary Key 
            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(true);
            #endregion

            #region Relationships/Keys
            builder.HasOne(w => w.Team)
                .WithMany(w => w.Users)
                .HasForeignKey(w => w.TeamId)
                .HasConstraintName("FK_Users_Teams")
                .OnDelete(DeleteBehavior.Restrict);

           builder.HasOne(w => w.CreatedBy)
                .WithMany(w => w.UsersCreatedBy)
                .HasForeignKey(w => w.CreatedByUserId)
                .HasConstraintName("FK_Users_UsersCreator")
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Indexes
            builder.ForSqlServerHasIndex(w => w.TeamId);
            #endregion

            #region Properties
            builder.Property(e => e.Id)
                .HasMaxLength(20)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Active)
                .HasDefaultValue(true);

            #region default
            //builder.Property(e => e.CreatedByUserId)
            //    .IsRequired(true);

            //builder.Property(e => e.CreationDate)
            //    .HasDefaultValueSql("getutcdate()");

            //builder.Property(e => e.TeamId)
            //    .IsRequired(true);

            //builder.Property(e => e.FirstName)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.LastName)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.Position)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.PhoneNumber)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.EmailAddress)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.PhoneNumber)
            //    .IsRequired(true)
            //    .HasMaxLength(255);

            //builder.Property(e => e.Role)
            //    .IsRequired(true);
            #endregion
            #region for creation
            builder.Property(e => e.CreatedByUserId)
               .IsRequired(false);

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            
            builder.Property(e => e.FirstName)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(e => e.LastName)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(e => e.Position)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(255);
            #endregion



            #endregion
        }
    }
}

