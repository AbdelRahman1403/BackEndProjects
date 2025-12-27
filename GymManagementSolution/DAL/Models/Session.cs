using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Session : BaseEntity
    {
        public int Capacity { get; set; }
        public string? Descrption { get; set; } 

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int TrainerId { get; set; }
        public Trainer TrainerSession { get; set; } = null!;

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
    }
}
