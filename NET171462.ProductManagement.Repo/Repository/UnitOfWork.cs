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
        private IGenericRepository<Product> productRepository;
        //private IGenericRepository<Category> categoryRepository;
        //private IGenericRepository<AccountMember> accountRepository;
        private AccountRepository accountRepository;
        private ProductRepository productCustomRepository;


        /*
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }
        */

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

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        /*
        public IGenericRepository<AccountMember> AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new GenericRepository<AccountMember>(context);
                }
                return accountRepository;
            }
        }
        */

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

        public ProductRepository ProductCustomRepository
        {
            get
            {
                if (productCustomRepository == null)
                {
                    productCustomRepository = new ProductRepository(context);
                }
                return productCustomRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
