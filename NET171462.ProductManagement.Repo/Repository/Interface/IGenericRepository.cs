using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NET171462.ProductManagement.Repo.Repository.Interface
{
    public interface IGenericRepository<TEntity>
    {
        public IEnumerable<TEntity> Get();
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            string includeProperties = "",
            bool? isAscending = true,
            int? pageIndex = null,
            int? pageSize = null);

        public TEntity GetByID(object id);
        public int Count();
        public void Insert(TEntity entity);
        public void Delete(object id);
        public void Delete(TEntity entityToDelete);
        public void Update(TEntity entityToUpdate);
        public void Update(object id, TEntity entityToUpdate);
    }
}
