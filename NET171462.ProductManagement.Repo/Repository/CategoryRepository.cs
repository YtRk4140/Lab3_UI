using NET171462.ProductManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NET171462.ProductManagement.Repo.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        private readonly MyStoreContext context;
        public CategoryRepository(MyStoreContext context) : base(context)
        {
            this.context = context;
        }

        public bool IsExistCategory(int id)
            => context.Categories.Find(id) == null ? false : true;

        public bool IsExistCategoryByName(string categoryName)
            => context.Categories.Any(c => c.CategoryName == categoryName);

    }
}
