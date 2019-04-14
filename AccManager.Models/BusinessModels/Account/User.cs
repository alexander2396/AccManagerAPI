using System;
using Microsoft.AspNetCore.Identity;

namespace AccManager.Models.BusinessModels.Account
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public string IdentityUserId { get; set; }

        public Role Role { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
