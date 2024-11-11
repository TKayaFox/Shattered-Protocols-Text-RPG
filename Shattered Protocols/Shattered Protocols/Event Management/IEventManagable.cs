using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management
{
    public interface IEventManagable
    {
        // Methods to register or unregister this object with the EventManager
        void ManageMe();
        void UnManageMe();
    }

}
