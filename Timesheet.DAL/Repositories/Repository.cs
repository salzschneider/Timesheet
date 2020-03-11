using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Timesheet.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;

        public Repository(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsNoTracking().AsEnumerable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await DbContext.Set<TEntity>().FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        /*public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }*/

        public void Add(TEntity entity)
        { 
            DbContext.Set<TEntity>().Add(entity);
        }
    }
}
