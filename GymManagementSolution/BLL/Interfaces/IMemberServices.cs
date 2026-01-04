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

        public bool CreateMember(CreateMemberViewModel memberViewModel);

        public MemberViewModel DetailsOfMember(int MemberId);

        public HealthRecordViewModel GetHealthRecordByMemberId(int memberId);

        public MemberToUpdateViewModel GetMemberForUpdate(int memberId);

        public bool UpdateMember(int memberId ,MemberToUpdateViewModel memberViewModel);

        public bool DeleteMember(int memberId);

    }
}
