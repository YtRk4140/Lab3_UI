using Microsoft.EntityFrameworkCore;
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

        public Product Create(Product product)
        {
            try
            {
                context.Add(product);
                context.SaveChanges();
                Product p = context.Products.Where(p => product.ProductId == p.ProductId).Include(p => p.Category).FirstOrDefault();
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsExistProduct(int id)
            => context.Products.Find(id) == null ? false : true;
    }


   
}
