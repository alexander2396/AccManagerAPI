using AccManager.Models.BusinessModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccManager.Common.Restrictions;

namespace AccManager.DataAccess.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            builder.Property(u => u.FirstName).HasMaxLength(GeneralModelRestrictions.NameMaxLength).IsRequired(true);
            builder.Property(u => u.LastName).HasMaxLength(GeneralModelRestrictions.NameMaxLength).IsRequired(true);
            builder.Property(u => u.Email).HasMaxLength(GeneralModelRestrictions.EmailMaxLength).IsRequired(true);
            builder.Property(u => u.PhoneNumber).HasMaxLength(GeneralModelRestrictions.PhoneNumberMaxLength);
            builder.Property(u => u.Notes).HasMaxLength(GeneralModelRestrictions.NotesMaxLength);
            builder.Ignore(u => u.IdentityUser);
        }
    }
}
