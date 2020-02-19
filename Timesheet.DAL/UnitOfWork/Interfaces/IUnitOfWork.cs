using System;

namespace Timesheet.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void CommitAsync();
    }
}