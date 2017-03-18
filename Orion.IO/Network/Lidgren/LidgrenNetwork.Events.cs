using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.IO.Network.Lidgren
{
    public delegate void HandleIncomingMessage(LidgrenConnection connection, NetIncomingMessage message);
}
