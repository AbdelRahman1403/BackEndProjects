using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ContractsRepoInterfaces
{
    public interface IGenericRepo<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdSpecificationsAsync(ISpecification<TEntity, TKey> specification);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity,TKey> specification);
        Task<int> GetCountWithSpecificationsAsync(ISpecification<TEntity,TKey> specification);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
