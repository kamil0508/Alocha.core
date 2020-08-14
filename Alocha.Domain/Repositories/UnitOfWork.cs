using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alocha.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            User = new GenericRepository<User>(_applicationDbContext);
            Sow = new GenericRepository<Sow>(_applicationDbContext);
            Smallpig = new GenericRepository<SmallPig>(_applicationDbContext);
        }

        public IGenericRepository<User> User { get; private set; }

        public IGenericRepository<Sow> Sow { get; private set; }

        public IGenericRepository<SmallPig> Smallpig { get; private set; }
       
        public async Task<bool> SaveChangesAsync()
        {
            return (await _applicationDbContext.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                _applicationDbContext.Dispose();
            }
            disposed = true;
        }
    }
}
