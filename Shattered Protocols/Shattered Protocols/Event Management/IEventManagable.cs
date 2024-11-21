using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management
{
    /// <summary>
    /// This interface is intended to be used with Event Manager class.
    /// Ensures specific objects have a ManageMe() and UnManageMe() method to subscribe and unsubscribe to needed events.
    /// </summary>
    public interface IEventManagable
    {
        // Methods to register or unregister this object with the EventManager
        void ManageMe();
        void UnManageMe();
    }

}
