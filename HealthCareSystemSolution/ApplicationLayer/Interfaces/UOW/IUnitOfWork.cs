using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.UOW
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepo<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
