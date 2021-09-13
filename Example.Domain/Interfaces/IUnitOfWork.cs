using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
