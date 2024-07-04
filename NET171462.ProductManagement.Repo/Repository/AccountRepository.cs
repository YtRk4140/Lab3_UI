using Microsoft.EntityFrameworkCore;
using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository;

namespace SE171462.ProductManagement.Repo.Repository
{
    public class AccountRepository : GenericRepository<AccountMember>
    {
        private readonly MyStoreContext context;
        public AccountRepository(MyStoreContext context) : base(context)
        {
            this.context = context;
        }

        public bool IsExistAccount(string id) => context.AccountMembers.Find(id) == null ? false : true;

        public async Task<AccountMember> FindByEmailAsync(string email)
            => await context.AccountMembers.FirstOrDefaultAsync(x => x.EmailAddress == email);
    }
}
