using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Seeding
{
    public interface ISeedingData
    {
        Task SeedDataAsync();
    }
}
