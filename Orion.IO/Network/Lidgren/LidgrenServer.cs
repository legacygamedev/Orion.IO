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
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Orion.IO.Network.Lidgren
{
    public class LidgrenServer : AbstractLidgrenNetwork<NetServer>
    {
        public static NetServer CreateServer(NetworkOptions options)
        {
            var config = new NetPeerConfiguration(options.Identifier);
            config.AcceptIncomingConnections = true;
            config.UseMessageRecycling = true;
            config.SetMessageTypeEnabled(NetIncomingMessageType.DiscoveryRequest, true);
            config.SetMessageTypeEnabled(NetIncomingMessageType.ConnectionApproval, true);

            config.MaximumConnections = options.ClientLimit;
            config.ConnectionTimeout = 5;
            
            if (options.PingInterval > 0)
            {
                config.PingInterval = options.PingInterval / 1000f;
            }

            if (options.RetryInterval > 0)
            {
                config.ResendHandshakeInterval = config.ResendHandshakeInterval / 1000f;
            }

            if (options.Host != null && options.Host.Trim().Length > 0)
            {
                config.LocalAddress = IPAddress.Parse(options.Host);
            }

            config.Port = options.Port;
            
            return new NetServer(config);
        }

        private List<LidgrenConnection> mClients;
        private Dictionary<NetConnection, LidgrenConnection> mClientConnectionMap;
        private Dictionary<Guid, LidgrenConnection> mClientIdMap;

        public LidgrenServer(NetworkOptions options) : base(CreateServer(options), options)
        {
            mClients = new List<LidgrenConnection>();
            mClientConnectionMap = new Dictionary<NetConnection, LidgrenConnection>();
            mClientIdMap = new Dictionary<Guid, LidgrenConnection>();
        }

        protected override void OnStart()
        {
            Peer.Start();
        }

        protected override void OnStop()
        {
            Peer.Shutdown("Shutting down the server...");
        }

        protected override void Tick()
        {
            NetIncomingMessage incomingMessage;
            LidgrenConnection connection;

            while ((incomingMessage = Peer.WaitMessage(1000)) != null)
            {
                if (!mClientConnectionMap.ContainsKey(incomingMessage.SenderConnection))
                {
                    connection = new LidgrenServerConnection(this, incomingMessage.SenderConnection);
                    mClients.Add(connection);
                    mClientConnectionMap.Add(incomingMessage.SenderConnection, connection);
                    mClientIdMap.Add(connection.Guid, connection);
                } else
                {
                    connection = mClientConnectionMap[incomingMessage.SenderConnection];
                }

                connection.QueueMessage(incomingMessage);
            }
        }

        protected class LidgrenServerConnection : LidgrenConnection
        {
            public LidgrenServer Server { get; private set; }

            public LidgrenServerConnection(LidgrenServer server, NetConnection connection) : base(connection)
            {
                Server = server;
            }

            protected override void ProcessMessages()
            {
                if (MessageQueue.Count < 1)
                {
                    return;
                }

                var message = MessageQueue.Take();
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        var status = (NetConnectionStatus)message.ReadByte();
                        Console.WriteLine(message.ReadString());

                        //If a player disconnected, signal a disconnect packet to the server
                        if (status == NetConnectionStatus.Disconnected)
                        {
                        }
                        break;

                    case NetIncomingMessageType.Data:
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
