using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnalyticsService
    {
        public AnalyitcsData GetAnalyticsData();
    }
}
