using NET171462.ProductManagement.Repo.Models;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171462.ProductManagement.Repo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET171462.ProductManagement.Repo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyStoreContext context = new MyStoreContext();
        public UnitOfWork(MyStoreContext context)
        {
            this.context = context;
        }

        private CategoryRepository categoryRepository;
        private ProductRepository productRepository;
        private AccountRepository accountRepository;

        public CategoryRepository CategoryRepository
        {
            get
            {

                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {

                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public AccountRepository AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(context);
                }
                return accountRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
