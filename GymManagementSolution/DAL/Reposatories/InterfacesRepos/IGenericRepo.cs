using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Interfaces
{
    public interface IGenericRepo<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(Func<TEntity,bool>? condition = null);
        TEntity? GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
