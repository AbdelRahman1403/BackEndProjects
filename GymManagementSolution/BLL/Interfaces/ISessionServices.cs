using BLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISessionServices
    {
        bool CreateSession(CreateSessionViewModel createSession);
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int sessionId);

        SessionToUpdateViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(SessionToUpdateViewModel sessionVM,int sessionId);

        bool DeleteSession(int sessionId);

        IEnumerable<GetTrainerForDropDown> getTrainerForDropDown();
        IEnumerable<GetCategoryForDropDown> getCategoryForDropDown();
    }
}
