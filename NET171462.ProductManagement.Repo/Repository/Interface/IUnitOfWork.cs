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
        CategoryRepository CategoryRepository { get; }
        ProductRepository ProductRepository { get; }
        AccountRepository AccountRepository { get; }
        void Save();
    }
}
