using System.Collections.Generic;

namespace AccManager.Models.BusinessModels.Account
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RolePermission> Permissions { get; set; }
        public List<User> Users { get; set; }
    }
}
