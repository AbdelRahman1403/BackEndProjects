using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; } // Not implement set; becasue to refuse to create an object
    }
}
