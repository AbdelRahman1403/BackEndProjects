using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.UOW
{
    public interface IUnitOfWork
    {
        IGenericRepo<TEntity , TKey> GetRepo<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
    }
}
