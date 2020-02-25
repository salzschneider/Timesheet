using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Timesheet.DAL.Locators
{
    public class DbContextLocator<TContext> : IDisposable where TContext : DbContext, new()
    {
        private TContext dbContext;

        public TContext Current { get => dbContext; }

        public DbContextLocator()
        {
            this.dbContext = Create();
        }

        public TContext Create()
        {
            return new TContext();
        }

        public virtual void Reset()
        {
            dbContext.Dispose();
            dbContext = Create();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
