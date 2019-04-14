using System;
using System.ComponentModel.DataAnnotations;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.Restrictions;

namespace AccManager.Models.ViewModels.Account
{
    public class UserViewModel : ViewModelBase<User>
    {
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(GeneralModelRestrictions.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GeneralModelRestrictions.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(GeneralModelRestrictions.EmailMaxLength)]
        public string Email { get; set; }

        [MaxLength(GeneralModelRestrictions.PhoneNumberMaxLength)]
        public int? PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }

        [MaxLength(GeneralModelRestrictions.NotesMaxLength)]
        public string Notes { get; set; }

        public bool IsActive { get; set; }

        public RoleViewModel Role { get; set; }

        public UserViewModel SetFrom(User entity)
        {
            base.SetFromEntity(entity);

            if (entity.Role != null)
            {
                Role = new RoleViewModel().SetFrom(entity.Role);
            }

            return this;
        }

        public override User UpdateEntity(User entity)
        {
            entity = base.UpdateEntity(entity);
            entity.RoleId = RoleId;

            return entity;
        }
    }
}
