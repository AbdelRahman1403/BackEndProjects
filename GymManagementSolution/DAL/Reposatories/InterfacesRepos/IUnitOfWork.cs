using DAL.Models;
using DAL.Reposatories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.InterfacesRepos
{
    public interface IUnitOfWork
    {

        IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity , new();

        int SaveChanges();
    }
}
