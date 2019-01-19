using AccManager.Common.Enums;

namespace AccManager.Models.BusinessModels.Account
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
