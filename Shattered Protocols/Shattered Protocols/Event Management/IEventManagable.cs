using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management
{
    public interface IEventManagable
    {
        // Method to register this object with the EventManager
        void ManageMe(EventManager eventManager);
    }

}
