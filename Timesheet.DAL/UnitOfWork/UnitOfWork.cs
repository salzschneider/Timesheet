using System.Data.Entity;

namespace Timesheet.DAL.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext DbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void CommitAsync()
        {
            DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
