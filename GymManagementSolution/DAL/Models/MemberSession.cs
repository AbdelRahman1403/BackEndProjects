using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MemberSession : BaseEntity
    {

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
    }
}
