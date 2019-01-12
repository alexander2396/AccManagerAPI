using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AccManager.Models.BusinessModels.Account;

namespace AccManager.DataAccess.EF.Context
{
    public class IdentityDbContext : IdentityDbContext<User>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }
    }
}
