using BLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMemberServices
    {
        public IEnumerable<MemberViewModel> GetAllMembers();

    }
}
