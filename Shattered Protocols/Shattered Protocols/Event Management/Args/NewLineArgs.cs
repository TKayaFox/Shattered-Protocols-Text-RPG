using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management.Args
{
    internal class NewLineArgs : EventArgs
    {
        String line;

        public string Line { get => line; set => line = value; }
    }
}
