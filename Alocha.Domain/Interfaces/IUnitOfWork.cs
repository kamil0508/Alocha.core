using Alocha.Domain.Entities;
using Alocha.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alocha.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 
        IGenericRepository<User> User { get; }
        IGenericRepository<Sow> Sow { get; }
        IGenericRepository<SmallPig> Smallpig { get; }

        Task<bool> SaveChangesAsync();
    }
}
