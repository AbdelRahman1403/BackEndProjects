using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.InterfacesRepos
{
    public interface IPlan
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int id);
        void Update(Plan entity);
    }
}
