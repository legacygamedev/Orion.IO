/*
MIT License

Copyright (c) 2017 Robert Lodico

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Lidgren.Network;
using System;
using Orion.IO.Network.Packets;
using System.Threading;
using System.Collections.Concurrent;

namespace Orion.IO.Network.Lidgren
{
    public abstract class LidgrenConnection : IConnection
    {
        public Guid Guid { get; private set; }

        public bool IsConnected => (State == ConnectionState.Connected);

        public ConnectionState State
        {
            get
            {
                switch (Connection.Status)
                {
                    case NetConnectionStatus.Connected:
                        return ConnectionState.Connected;

                    case NetConnectionStatus.Disconnected:
                        return ConnectionState.Disconnected;

                    case NetConnectionStatus.Disconnecting:
                        return ConnectionState.Disconnecting;

                    case NetConnectionStatus.InitiatedConnect:
                    case NetConnectionStatus.ReceivedInitiation:
                    case NetConnectionStatus.RespondedAwaitingApproval:
                    case NetConnectionStatus.RespondedConnect:
                        return ConnectionState.Connecting;

                    default:
                        return ConnectionState.Unknown;
                }
            }
        }

        public ConnectionStatistics Statistics => new ConnectionStatistics(State, (ushort)(1000 * Connection.AverageRoundtripTime), DateTime.Now, Connection.RemoteTimeOffset);

        public NetConnection Connection { get; private set; }

        private BlockingCollection<NetIncomingMessage> mMessageQueue;
        protected BlockingCollection<NetIncomingMessage> MessageQueue
        {
            get
            {
                if (mMessageQueue == null)
                {
                    mMessageQueue = new BlockingCollection<NetIncomingMessage>(1);
                }

                return mMessageQueue;
            }
        }

        public LidgrenConnection(NetConnection connection) : this(Guid.NewGuid(), connection) {
            /* Do nothing */
        }

        public LidgrenConnection(Guid guid, NetConnection connection)
        {
            Guid = guid;
            Connection = connection;
        }

        public virtual bool Kill(string message = null)
        {
            if (Connection.Status != NetConnectionStatus.Disconnected && Connection.Status != NetConnectionStatus.Disconnecting)
            {
                Connection.Disconnect(message);
                return true;
            }

            return false;
        }

        public virtual bool Send<T>(T? packet) where T : struct, IBarePacket<T>
        {
            if (packet.HasValue)
            {
                var message = Connection.Peer.CreateMessage();
                packet.Value.Write(new LidgrenPacketSerializer(message));
                Connection.SendMessage(message, NetDeliveryMethod.ReliableOrdered, 0);

                return true;
            }

            return false;
        }

        protected abstract void ProcessMessages();

        public void QueueMessage(NetIncomingMessage message)
        {
            MessageQueue.Add(message);
        }
    }
}
