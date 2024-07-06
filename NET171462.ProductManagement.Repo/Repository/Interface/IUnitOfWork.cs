using NET171462.ProductManagement.Repo.Models;
using SE171462.ProductManagement.Repo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET171462.ProductManagement.Repo.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }
        CategoryRepository CategoryRepository { get; }
        AccountRepository AccountRepository { get; }
        ProductRepository ProductCustomRepository { get; }
        void Save();
    }
}
