using NET171462.ProductManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET171462.ProductManagement.Repo.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(MyStoreContext context) : base(context)
        {
        }

        public bool IsExistProduct(int id)
           => context.Products.Find(id) == null ? false : true;
    }


   
}
